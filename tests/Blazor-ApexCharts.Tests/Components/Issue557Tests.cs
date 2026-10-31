using System.Text.Json;
using ApexCharts.Tests.TestHelpers;

namespace ApexCharts.Tests.Components;

/// <summary>
/// Issue #557: an X range annotation lost its fill after zooming inside it. The cause is in
/// apexcharts.js and only affects a category axis, where an annotation is placed by looking up a
/// rendered axis label. These tests pin down what the wrapper sends for the two setups: Unix
/// milliseconds on a datetime axis (supported, keeps the fill when zoomed) and raw DateTime values
/// (sent as strings, so apexcharts.js treats the axis as categories).
/// </summary>
public class Issue557Tests : ApexChartsTestContext
{
    private static readonly DateTime Day = new(2024, 12, 19, 0, 0, 0, DateTimeKind.Utc);

    public class Reading
    {
        public DateTime Time { get; set; }
        public decimal Value { get; set; }
    }

    private static List<Reading> Readings() =>
        Enumerable.Range(0, 24 * 6).Select(i => new Reading { Time = Day.AddMinutes(10 * i), Value = i }).ToList();

    [Fact]
    public void Unix_millisecond_range_is_sent_as_numbers_on_the_same_scale_as_the_series()
    {
        var options = new ApexChartOptions<Reading>
        {
            Xaxis = new XAxis { Type = XAxisType.Datetime },
            Annotations = new Annotations
            {
                Xaxis = new List<AnnotationsXAxis>
                {
                    new AnnotationsXAxis { X = Day.ToUnixTimeMilliseconds(), X2 = Day.AddHours(12).ToUnixTimeMilliseconds(), FillColor = "#fcba03" }
                }
            }
        };

        RenderChart<Reading>(p => p
            .Add(c => c.Options, options)
            .AddChildContent<ApexPointSeries<Reading>>(s => s
                .Add(x => x.Items, Readings())
                .Add(x => x.SeriesType, SeriesType.Line)
                .Add(x => x.XValue, e => e.Time.ToUnixTimeMilliseconds())
                .Add(x => x.YValue, e => e.Value)));

        var json = RenderedOptions();

        Assert.Equal("datetime", json.Get("xaxis.type")?.GetString());

        var x = json.Get("annotations.xaxis.0.x");
        var x2 = json.Get("annotations.xaxis.0.x2");
        Assert.Equal(JsonValueKind.Number, x?.ValueKind);
        Assert.Equal(JsonValueKind.Number, x2?.ValueKind);
        Assert.Equal(1734566400000L, x!.Value.GetInt64());
        Assert.Equal(1734566400000L + 12 * 3600 * 1000, x2!.Value.GetInt64());

        // The first point and the start of the range carry the same number, so apexcharts.js
        // projects both through the numeric x scale, which clamps a range that spans the view.
        Assert.Equal(x.Value.GetInt64(), json.Get("series.0.data.0.x")?.GetInt64());
        Assert.Equal(x2.Value.GetInt64(), json.Get("series.0.data.72.x")?.GetInt64());
    }

    [Fact]
    public void DateTime_range_is_sent_as_strings()
    {
        // This is the setup from the issue. The strings make apexcharts.js treat the x axis as
        // categories. Changing this serialization would move existing charts onto a different axis
        // type, so it is pinned here on purpose.
        var json = ChartJson.ToElement<Reading>(new ApexChartOptions<Reading>
        {
            Annotations = new Annotations
            {
                Xaxis = new List<AnnotationsXAxis> { new AnnotationsXAxis { X = Day, X2 = Day.AddHours(12) } }
            }
        });

        Assert.Equal(JsonValueKind.String, json.Get("annotations.xaxis.0.x")?.ValueKind);
        Assert.StartsWith("2024-12-19T00:00:00", json.Get("annotations.xaxis.0.x")?.GetString());
        Assert.StartsWith("2024-12-19T12:00:00", json.Get("annotations.xaxis.0.x2")?.GetString());
    }
}
