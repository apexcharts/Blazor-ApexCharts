using System.Reflection;
using System.Text.Json;
using ApexCharts.Internal;
using ApexCharts.Tests.TestHelpers;

namespace ApexCharts.Tests.Serialization;

/// <summary>
/// Issue 619: tooltip markers lost their color for every series after the first when the chart set
/// <c>Markers.Shape = MarkerShape.Circle</c>. <see cref="MarkerShapeSelections"/> had no
/// <see cref="ValueOrListConverter{T}"/>, so System.Text.Json wrote it as a plain collection and a
/// single shape reached apexcharts.js as <c>["circle"]</c>. The chart reads an array as one shape per
/// series index, so series 1 and up got no shape.
/// <para>
/// Every <see cref="ValueOrList{TProperty}"/> type has the same contract: one value is written as a
/// scalar and several as an array. <see cref="Every_value_or_list_type_writes_one_value_as_a_scalar"/>
/// finds them all by reflection, so a new one added without a converter fails here.
/// </para>
/// </summary>
public class Issue619Tests
{
    [Fact]
    public void Single_marker_shape_is_sent_as_a_string()
    {
        var options = new ApexChartOptions<Sale> { Markers = new Markers { Shape = MarkerShape.Circle } };

        var json = ChartJson.ToElement<Sale>(options);

        Assert.Equal(JsonValueKind.String, json.Get("markers.shape")?.ValueKind);
        Assert.Equal("circle", json.Get("markers.shape")?.GetString());
    }

    [Fact]
    public void Marker_shape_list_is_sent_as_an_array_in_series_order()
    {
        var options = new ApexChartOptions<Sale>
        {
            Markers = new Markers { Shape = new MarkerShapeSelections(MarkerShape.Diamond, MarkerShape.Cross) }
        };

        var json = ChartJson.ToElement<Sale>(options);

        Assert.Equal(JsonValueKind.Array, json.Get("markers.shape")?.ValueKind);
        Assert.Equal("diamond", json.Get("markers.shape.0")?.GetString());
        Assert.Equal("cross", json.Get("markers.shape.1")?.GetString());
    }

    [Fact]
    public void Single_marker_shape_is_a_string_in_global_options()
    {
        var options = new ApexChartBaseOptions { Markers = new Markers { Shape = MarkerShape.Square } };

        var json = ChartJson.Parse(ChartJson.SerializeGlobal(options));

        Assert.Equal("square", json.Get("markers.shape")?.GetString());
    }

    public static TheoryData<Type> ValueOrListTypes()
    {
        var data = new TheoryData<Type>();
        foreach (var type in FindValueOrListTypes()) { data.Add(type); }
        return data;
    }

    [Fact]
    public void Reflection_finds_the_known_value_or_list_types()
    {
        // Guards the theory below against passing vacuously if the type search stops matching.
        var types = FindValueOrListTypes();

        Assert.Contains(typeof(MarkerShapeSelections), types);
        Assert.Contains(typeof(Color), types);
        Assert.Contains(typeof(Opacity), types);
        Assert.Contains(typeof(CurveSelections), types);
    }

    [Theory]
    [MemberData(nameof(ValueOrListTypes))]
    public void Every_value_or_list_type_writes_one_value_as_a_scalar(Type type)
    {
        var (first, _) = SampleValues(type);
        var value = Create(type, first);

        foreach (var options in SerializerOptions())
        {
            var json = ChartJson.Parse(JsonSerializer.Serialize(value, type, options));

            Assert.True(json.ValueKind != JsonValueKind.Array,
                $"{type.Name} with one value serialized as {json.GetRawText()}. Register a ValueOrListConverter for its item type in ChartSerializer.GenerateBaseOptions.");
            Assert.Equal(JsonSerializer.Serialize(first, first.GetType(), options), json.GetRawText());
        }
    }

    [Theory]
    [MemberData(nameof(ValueOrListTypes))]
    public void Every_value_or_list_type_writes_several_values_as_an_array(Type type)
    {
        var (first, second) = SampleValues(type);
        var value = Create(type, first, second);

        foreach (var options in SerializerOptions())
        {
            var json = ChartJson.Parse(JsonSerializer.Serialize(value, type, options));

            Assert.Equal(JsonValueKind.Array, json.ValueKind);
            Assert.Equal(2, json.GetArrayLength());
            Assert.Equal(JsonSerializer.Serialize(first, first.GetType(), options), json[0].GetRawText());
            Assert.Equal(JsonSerializer.Serialize(second, second.GetType(), options), json[1].GetRawText());
        }
    }

    private static List<Type> FindValueOrListTypes()
        => typeof(ValueOrList<>).Assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && ItemTypeOf(t) != null)
            .OrderBy(t => t.FullName)
            .ToList();

    private static Type? ItemTypeOf(Type type)
    {
        for (var current = type; current != null; current = current.BaseType)
        {
            if (current.IsGenericType && current.GetGenericTypeDefinition() == typeof(ValueOrList<>))
            {
                return current.GetGenericArguments()[0];
            }
        }

        return null;
    }

    private static IEnumerable<JsonSerializerOptions> SerializerOptions()
    {
        // Chart options and global options are written with different JsonSerializerOptions instances.
        yield return ChartSerializer.GetOptions<Sale>();
        yield return ChartSerializer.GetOptions();
    }

    private static (object First, object Second) SampleValues(Type type)
    {
        var item = ItemTypeOf(type)!;

        if (item == typeof(string)) { return ("#008FFB", "#00E396"); }
        if (item == typeof(double)) { return (0.5d, 2d); }
        if (item.IsEnum)
        {
            var values = Enum.GetValues(item);
            Assert.True(values.Length >= 2, $"{item.Name} needs two values for this test.");
            return (values.GetValue(0)!, values.GetValue(1)!);
        }

        throw new InvalidOperationException(
            $"{type.Name} holds {item.Name}. Add sample values for it here and register a ValueOrListConverter<{item.Name}> in ChartSerializer.GenerateBaseOptions.");
    }

    private static object Create(Type type, params object[] items)
    {
        var item = ItemTypeOf(type)!;
        var array = Array.CreateInstance(item, items.Length);
        for (var i = 0; i < items.Length; i++) { array.SetValue(items[i], i); }

        var ctor = type.GetConstructor(BindingFlags.Public | BindingFlags.Instance, [array.GetType()]);
        Assert.True(ctor != null, $"{type.Name} needs a public constructor taking {item.Name}[] like the other ValueOrList types.");
        return ctor!.Invoke([array]);
    }
}
