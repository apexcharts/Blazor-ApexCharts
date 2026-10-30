using System.Text.Json;
using ApexCharts.Tests.TestHelpers;

namespace ApexCharts.Tests.Serialization;

/// <summary>
/// Issue #517: <see cref="AnnotationsPoint.Y"/> only took a number, so a point annotation could not
/// name a category. On a horizontal rangeBar the categories run along the y-axis, and apexcharts.js
/// places a point annotation there by looking up <c>y</c> among the category labels.
/// A number must still be sent as a number.
/// </summary>
public class Issue517PointAnnotationYTests : ApexChartsTestContext
{
    public class Shift
    {
        public string Machine { get; set; } = "";
        public long Start { get; set; }
        public long End { get; set; }
    }

    private static readonly DateTime Day = new(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    [Fact]
    public void Category_y_is_sent_as_a_string()
    {
        var y = ChartJson.ToElement<Sale>(new AnnotationsPoint { X = 1, Y = "Beta" }).Get("y");

        Assert.Equal(JsonValueKind.String, y?.ValueKind);
        Assert.Equal("Beta", y!.Value.GetString());
    }

    [Fact]
    public void Number_y_is_still_sent_as_a_number()
    {
        double? fromNullable = 7;

        var y = ChartJson.ToElement<Sale>(new AnnotationsPoint { X = "Feb", Y = 12.5 }).Get("y");
        var y2 = ChartJson.ToElement<Sale>(new AnnotationsPoint { X = "Feb", Y = fromNullable }).Get("y");

        Assert.Equal(JsonValueKind.Number, y?.ValueKind);
        Assert.Equal(12.5, y!.Value.GetDouble());
        Assert.Equal(7, y2?.GetDouble());
        Assert.Null(ChartJson.ToElement<Sale>(new AnnotationsPoint { X = "Feb" }).Get("y"));
    }

    [Fact]
    public void Horizontal_rangeBar_point_annotation_reaches_renderChart_with_timestamp_x_and_category_y()
    {
        var at = Day.AddHours(4).ToUnixTimeMilliseconds();
        var options = new ApexChartOptions<Shift>
        {
            PlotOptions = new PlotOptions { Bar = new PlotOptionsBar { Horizontal = true } },
            Annotations = new Annotations
            {
                Points = new List<AnnotationsPoint>
                {
                    new AnnotationsPoint { X = at, Y = "Beta", Marker = new AnnotationMarker { Size = 6 } }
                }
            }
        };

        var shifts = new List<Shift>
        {
            new() { Machine = "Alpha", Start = Day.AddHours(1).ToUnixTimeMilliseconds(), End = Day.AddHours(5).ToUnixTimeMilliseconds() },
            new() { Machine = "Beta", Start = Day.AddHours(3).ToUnixTimeMilliseconds(), End = Day.AddHours(9).ToUnixTimeMilliseconds() },
        };

        RenderChart<Shift>(p => p
            .Add(c => c.Options, options)
            .Add(c => c.XAxisType, XAxisType.Datetime)
            .AddChildContent<ApexRangeSeries<Shift>>(s => s
                .Add(x => x.Items, shifts)
                .Add(x => x.XValue, e => e.Machine)
                .Add(x => x.YMinValue, e => e.Start)
                .Add(x => x.YMaxValue, e => e.End)));

        var json = RenderedOptions();

        Assert.Equal(at, json.Get("annotations.points.0.x")?.GetInt64());
        Assert.Equal("Beta", json.Get("annotations.points.0.y")?.GetString());
        // The category matches a series x value, which is what apexcharts.js looks the label up by.
        Assert.Equal("Beta", json.Get("series.0.data.1.x")?.GetString());
    }

    [Fact]
    public async Task Category_y_passed_to_AddPointAnnotationAsync_is_sent_as_a_string()
    {
        var cut = RenderChart<Sale>(p => p
            .AddChildContent<ApexPointSeries<Sale>>(s => s
                .Add(x => x.Items, SampleData.Sales())
                .Add(x => x.SeriesType, SeriesType.Bar)
                .Add(x => x.XValue, e => e.Month)
                .Add(x => x.YValue, e => e.Amount)));

        await cut.InvokeAsync(() => cut.Instance.AddPointAnnotationAsync(new AnnotationsPoint { X = 15, Y = "Feb" }, true));

        var json = LastJsonArgument("blazor_apexchart.addPointAnnotation", 1);
        Assert.Equal("Feb", json.Get("y")?.GetString());
    }
}
