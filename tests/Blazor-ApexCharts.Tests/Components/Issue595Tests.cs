using System.Text.Json;
using System.Text.RegularExpressions;
using ApexCharts.Internal;
using ApexCharts.Tests.TestHelpers;
using Microsoft.JSInterop;

namespace ApexCharts.Tests.Components;

/// <summary>
/// Issue #595: a .NET y-axis label formatter per axis. <see cref="ApexChart{TItem}.FormatYAxisLabelForAxis"/>
/// gives every y-axis its own formatter carrying the axis index, and the callback receives that index.
/// </summary>
public class Issue595Tests : ApexChartsTestContext
{
    private static readonly Regex AxisIndexInFormatter =
        new(@"getYAxisLabelForAxis\(value, index, w, (\d+)\)", RegexOptions.Compiled);

    private static ApexChartOptions<Sale> TwoAxisOptions() => new()
    {
        Yaxis = new List<YAxis>
        {
            new() { SeriesName = new SeriesName("Amount") },
            new() { SeriesName = new SeriesName("Double"), Opposite = true },
        }
    };

    private IRenderedComponent<ApexChart<Sale>> RenderTwoSeriesChart(
        ApexChartOptions<Sale> options,
        Func<decimal, int, string>? perAxis = null,
        Func<decimal, string>? single = null) =>
        RenderChart<Sale>(p =>
        {
            p.Add(c => c.Options, options);
            if (perAxis != null) { p.Add(c => c.FormatYAxisLabelForAxis, perAxis); }
            if (single != null) { p.Add(c => c.FormatYAxisLabel, single); }
            p.AddChildContent<ApexPointSeries<Sale>>(s => s
                .Add(x => x.Name, "Amount")
                .Add(x => x.Items, SampleData.Sales())
                .Add(x => x.SeriesType, SeriesType.Bar)
                .Add(x => x.XValue, e => e.Month)
                .Add(x => x.YValue, e => e.Amount));
            p.AddChildContent<ApexPointSeries<Sale>>(s => s
                .Add(x => x.Name, "Double")
                .Add(x => x.Items, SampleData.Sales())
                .Add(x => x.SeriesType, SeriesType.Line)
                .Add(x => x.XValue, e => e.Month)
                .Add(x => x.YValue, e => e.Amount * 2));
        });

    private static int? AxisIndexOf(JsonElement options, int axis)
    {
        var formatter = options.Get($"yaxis.{axis}.labels.formatter.@eval")?.GetString();
        if (formatter == null) { return null; }
        var match = AxisIndexInFormatter.Match(formatter);
        return match.Success ? int.Parse(match.Groups[1].Value) : null;
    }

    private JSHandler<Sale> Handler()
    {
        var call = InvocationsOf(JsIdentifiers.RenderChart).Last();
        var reference = Assert.IsType<DotNetObjectReference<JSHandler<Sale>>>(call.Arguments[0]);
        return reference.Value;
    }

    [Fact]
    public void Each_y_axis_gets_a_formatter_carrying_its_index()
    {
        RenderTwoSeriesChart(TwoAxisOptions(), perAxis: (v, axis) => $"{axis}:{v}");

        var options = RenderedOptions();

        Assert.Equal(2, options.Get("yaxis")?.GetArrayLength());
        Assert.Equal(0, AxisIndexOf(options, 0));
        Assert.Equal(1, AxisIndexOf(options, 1));
    }

    [Fact]
    public void A_single_axis_is_added_when_none_is_configured()
    {
        RenderTwoSeriesChart(new ApexChartOptions<Sale>(), perAxis: (v, axis) => $"{axis}:{v}");

        var options = RenderedOptions();

        Assert.Equal(1, options.Get("yaxis")?.GetArrayLength());
        Assert.Equal(0, AxisIndexOf(options, 0));
    }

    [Fact]
    public void The_callback_receives_the_axis_index()
    {
        var calls = new List<(decimal Value, int Axis)>();
        RenderTwoSeriesChart(TwoAxisOptions(), perAxis: (v, axis) =>
        {
            calls.Add((v, axis));
            return axis == 0 ? $"${v:0}" : $"{v:0}%";
        });

        var handler = Handler();

        Assert.Equal("$12", handler.JSGetFormattedYAxisValueForAxis(ChartJson.Parse("12"), 0));
        Assert.Equal("40%", handler.JSGetFormattedYAxisValueForAxis(ChartJson.Parse("40"), 1));
        Assert.Equal("", handler.JSGetFormattedYAxisValueForAxis(ChartJson.Parse("null"), 1));
        Assert.Equal(new[] { (12m, 0), (40m, 1) }, calls);
    }

    [Fact]
    public async Task Formatters_keep_their_index_after_update_options()
    {
        var cut = RenderTwoSeriesChart(TwoAxisOptions(), perAxis: (v, axis) => $"{axis}:{v}");

        await cut.InvokeAsync(() => cut.Instance.UpdateOptionsAsync(true, true, false));

        var options = LastJsonArgument(JsIdentifiers.UpdateOptions, 1);
        Assert.Equal(0, AxisIndexOf(options, 0));
        Assert.Equal(1, AxisIndexOf(options, 1));
    }

    [Fact]
    public void Per_axis_formatter_takes_precedence_over_FormatYAxisLabel()
    {
        RenderTwoSeriesChart(TwoAxisOptions(), perAxis: (v, axis) => $"{axis}:{v}", single: v => $"single:{v}");

        var options = RenderedOptions();

        Assert.Equal(0, AxisIndexOf(options, 0));
        Assert.Equal(1, AxisIndexOf(options, 1));
    }

    [Fact]
    public void FormatYAxisLabel_alone_still_formats_only_the_first_axis()
    {
        RenderTwoSeriesChart(TwoAxisOptions(), single: v => $"single:{v}");

        var options = RenderedOptions();

        Assert.Contains("getYAxisLabel(value, index, w)", options.Get("yaxis.0.labels.formatter.@eval")?.GetString());
        Assert.Null(options.Get("yaxis.1.labels.formatter"));
        Assert.Equal("single:5", Handler().JSGetFormattedYAxisValue(ChartJson.Parse("5")));
    }
}
