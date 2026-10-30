using System.Text.Json;
using ApexCharts.Tests.TestHelpers;

namespace ApexCharts.Tests.Components;

/// <summary>
/// Issue #618: a line chart with numeric X values and an X axis TickAmount exported wrong
/// categories to CSV. The cause is in apexcharts.js (the CSV export reads the axis tick labels
/// when the category axis has been turned numeric). These tests pin what the wrapper sends, so
/// that the plain-JS reproduction stays representative, and pin the documented workaround.
/// </summary>
public class Issue618Tests : ApexChartsTestContext
{
    private sealed class Reading
    {
        public int X { get; set; }
        public double Y { get; set; }
    }

    private static List<Reading> Readings(int count) =>
        Enumerable.Range(1, count).Select(i => new Reading { X = i, Y = i * 10 }).ToList();

    private IRenderedComponent<ApexChart<Reading>> RenderLineChart(ApexChartOptions<Reading> options, XAxisType? xAxisType = null) =>
        RenderChart<Reading>(p => p
            .Add(c => c.Options, options)
            .Add(c => c.XAxisType, xAxisType)
            .AddChildContent<ApexPointSeries<Reading>>(s => s
                .Add(x => x.Name, "Readings")
                .Add(x => x.Items, Readings(1600))
                .Add(x => x.SeriesType, SeriesType.Line)
                .Add(x => x.XValue, e => e.X)
                .Add(x => x.YValue, e => (decimal)e.Y)));

    [Fact]
    public void Numeric_x_values_are_sent_as_numbers_and_the_axis_type_is_left_to_apexcharts()
    {
        RenderLineChart(new ApexChartOptions<Reading> { Xaxis = new XAxis { TickAmount = 40 } });

        var options = RenderedOptions();

        Assert.Equal(1600, options.Get("series.0.data")?.GetArrayLength());
        Assert.Equal(JsonValueKind.Number, options.Get("series.0.data.0.x")?.ValueKind);
        Assert.Equal(1, options.Get("series.0.data.0.x")?.GetInt32());
        Assert.Equal(1600, options.Get("series.0.data.1599.x")?.GetInt32());
        Assert.Equal(40, options.Get("xaxis.tickAmount")?.GetInt32());
        Assert.Null(options.Get("xaxis.type"));
    }

    [Fact]
    public void XAxisType_numeric_workaround_reaches_the_chart_options()
    {
        RenderLineChart(new ApexChartOptions<Reading> { Xaxis = new XAxis { TickAmount = 40 } }, XAxisType.Numeric);

        Assert.Equal("numeric", RenderedOptions().Get("xaxis.type")?.GetString());
    }

    [Fact]
    public void Xaxis_type_numeric_set_in_options_reaches_the_chart_options()
    {
        RenderLineChart(new ApexChartOptions<Reading> { Xaxis = new XAxis { Type = XAxisType.Numeric, TickAmount = 40 } });

        var options = RenderedOptions();

        Assert.Equal("numeric", options.Get("xaxis.type")?.GetString());
        Assert.Equal(40, options.Get("xaxis.tickAmount")?.GetInt32());
    }
}
