using System.Text.Json;
using ApexCharts.Internal;

namespace ApexCharts.Tests.TestHelpers;

/// <summary>
/// Serializes chart objects exactly the way <see cref="ApexChart{TItem}"/> does before it hands
/// them to JavaScript, so a test asserts on the same JSON the browser receives.
/// </summary>
public static class ChartJson
{
    /// <summary>
    /// Serializes <paramref name="value"/> with the per-item serializer options the component uses
    /// for options, series, annotations and every other interop payload
    /// (<c>JsonSerializer.Serialize(value, ChartSerializer.GetOptions&lt;TItem&gt;())</c>).
    /// </summary>
    public static string Serialize<TItem>(object value) where TItem : class
        => JsonSerializer.Serialize(value, ChartSerializer.GetOptions<TItem>());

    /// <summary>
    /// Serializes a full options object the way <c>renderChart</c> and <c>updateOptions</c> receive it.
    /// </summary>
    public static string Serialize<TItem>(ApexChartOptions<TItem> options) where TItem : class
        => Serialize<TItem>((object)options);

    /// <summary>
    /// Serializes <paramref name="value"/> and parses the result, for assertions on individual properties.
    /// </summary>
    public static JsonElement ToElement<TItem>(object value) where TItem : class
        => Parse(Serialize<TItem>(value));

    /// <summary>
    /// Serializes with the item-independent options used for global options
    /// (<see cref="ChartSerializer.SerializeOptions"/>).
    /// </summary>
    public static string SerializeGlobal(IApexChartBaseOptions options)
        => ChartSerializer.SerializeOptions(options);

    /// <summary>
    /// Parses a JSON string into a detached <see cref="JsonElement"/> that stays valid after the document is gone.
    /// </summary>
    public static JsonElement Parse(string json)
    {
        using var doc = JsonDocument.Parse(json);
        return doc.RootElement.Clone();
    }

    /// <summary>
    /// Walks a dotted path of camelCase property names (for example <c>"chart.type"</c> or
    /// <c>"series.0.data.1.y"</c>, where a number indexes into an array). Returns <c>null</c>
    /// when any segment is missing, which is how the serializer represents an option left unset.
    /// </summary>
    public static JsonElement? Get(this JsonElement element, string path)
    {
        var current = element;
        foreach (var segment in path.Split('.'))
        {
            if (current.ValueKind == JsonValueKind.Array && int.TryParse(segment, out var index))
            {
                if (index < 0 || index >= current.GetArrayLength()) { return null; }
                current = current[index];
            }
            else if (current.ValueKind == JsonValueKind.Object && current.TryGetProperty(segment, out var next))
            {
                current = next;
            }
            else
            {
                return null;
            }
        }

        return current;
    }
}
