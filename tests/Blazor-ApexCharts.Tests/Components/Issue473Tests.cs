using System.Text.Json;
using ApexCharts.Tests.TestHelpers;

namespace ApexCharts.Tests.Components;

/// <summary>
/// Issue #473: bars drawn under the wrong hour on a category axis.
/// The wrapper sends every aggregated point with its own X value; apexcharts.js then places
/// category points by their position in the series, not by X (see
/// tests/e2e/repros/category-mapping). These tests pin the wrapper's half of that contract so
/// a regression here cannot be mistaken for the upstream behaviour, or the other way round.
/// </summary>
public class Issue473Tests : ApexChartsTestContext
{
    private sealed class Reading
    {
        public DateTime CreatedAt { get; set; }
        public decimal Weight { get; set; }
    }

    private sealed class HourTotal
    {
        public string Hour { get; set; } = "";
        public decimal? Weight { get; set; }
    }

    private static readonly DateTime Day = new(2024, 1, 1);

    private static readonly List<string> Hours =
        Enumerable.Range(0, 24).Select(h => Day.AddHours(h).ToString("HH:00")).ToList();

    private static Reading At(int hour, int minute, decimal weight) =>
        new() { CreatedAt = Day.AddHours(hour).AddMinutes(minute), Weight = weight };

    private IRenderedComponent<ApexChart<Reading>> RenderReporterChart(
        IEnumerable<Reading> first, IEnumerable<Reading> second) =>
        RenderChart<Reading>(p => p
            .Add(c => c.Options, new ApexChartOptions<Reading> { Xaxis = new XAxis { Categories = Hours } })
            .AddChildContent<ApexPointSeries<Reading>>(s => s
                .Add(x => x.Name, "test")
                .Add(x => x.Items, first)
                .Add(x => x.SeriesType, SeriesType.Bar)
                .Add(x => x.XValue, e => e.CreatedAt.ToString("HH:00"))
                .Add(x => x.YAggregate, e => e.Sum(r => r.Weight)))
            .AddChildContent<ApexPointSeries<Reading>>(s => s
                .Add(x => x.Name, "secondseries")
                .Add(x => x.Items, second)
                .Add(x => x.SeriesType, SeriesType.Bar)
                .Add(x => x.XValue, e => e.CreatedAt.ToString("HH:00"))
                .Add(x => x.YAggregate, e => e.Sum(r => r.Weight))));

    [Fact]
    public void Aggregated_points_keep_the_hour_they_were_grouped_by()
    {
        RenderReporterChart(
            first: [At(8, 5, 1500), At(8, 40, 500), At(9, 10, 6000), At(10, 0, 4000)],
            second: [At(8, 15, 11000), At(10, 30, 5000)]);

        var options = RenderedOptions();

        Assert.Equal(24, options.Get("xaxis.categories")?.GetArrayLength());
        Assert.Equal("00:00", options.Get("xaxis.categories.0")?.GetString());

        Assert.Equal(3, options.Get("series.0.data")?.GetArrayLength());
        Assert.Equal("08:00", options.Get("series.0.data.0.x")?.GetString());
        Assert.Equal(2000m, options.Get("series.0.data.0.y")?.GetDecimal());
        Assert.Equal("09:00", options.Get("series.0.data.1.x")?.GetString());
        Assert.Equal(6000m, options.Get("series.0.data.1.y")?.GetDecimal());
        Assert.Equal("10:00", options.Get("series.0.data.2.x")?.GetString());
        Assert.Equal(4000m, options.Get("series.0.data.2.y")?.GetDecimal());

        Assert.Equal(2, options.Get("series.1.data")?.GetArrayLength());
        Assert.Equal("08:00", options.Get("series.1.data.0.x")?.GetString());
        Assert.Equal(11000m, options.Get("series.1.data.0.y")?.GetDecimal());
        Assert.Equal("10:00", options.Get("series.1.data.1.x")?.GetString());
        Assert.Equal(5000m, options.Get("series.1.data.1.y")?.GetDecimal());
    }

    [Fact]
    public void A_series_with_a_point_for_every_category_lines_up_by_position()
    {
        // The workaround documented on XAxis.Categories: aggregate per category up front, give
        // every category a point (a null Y for an empty hour) and keep them in category order.
        // Point n then belongs to category n.
        var readings = new[] { At(8, 0, 1500), At(8, 30, 500), At(10, 0, 4000) };
        var byHour = readings.ToLookup(r => r.CreatedAt.ToString("HH:00"));
        var totals = Hours
            .Select(h => new HourTotal
            {
                Hour = h,
                Weight = byHour.Contains(h) ? byHour[h].Sum(r => r.Weight) : null
            })
            .ToList();

        RenderChart<HourTotal>(p => p
            .Add(c => c.Options, new ApexChartOptions<HourTotal> { Xaxis = new XAxis { Categories = Hours } })
            .AddChildContent<ApexPointSeries<HourTotal>>(s => s
                .Add(x => x.Name, "test")
                .Add(x => x.Items, totals)
                .Add(x => x.SeriesType, SeriesType.Bar)
                .Add(x => x.XValue, e => e.Hour)
                .Add(x => x.YValue, e => e.Weight)));

        var options = RenderedOptions();

        Assert.Equal(24, options.Get("series.0.data")?.GetArrayLength());
        for (var i = 0; i < Hours.Count; i++)
        {
            Assert.Equal(Hours[i], options.Get($"series.0.data.{i}.x")?.GetString());
        }
        Assert.Equal(JsonValueKind.Null, options.Get("series.0.data.0.y")?.ValueKind);
        Assert.Equal(2000m, options.Get("series.0.data.8.y")?.GetDecimal());
        Assert.Equal(JsonValueKind.Null, options.Get("series.0.data.9.y")?.ValueKind);
        Assert.Equal(4000m, options.Get("series.0.data.10.y")?.GetDecimal());
    }
}
