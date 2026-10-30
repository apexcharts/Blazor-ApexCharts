using ApexCharts.Tests.TestHelpers;

namespace ApexCharts.Tests.Components;

/// <summary>
/// Issue #660: hiding one of three line series left the shared tooltip with a single series.
/// The tooltip is drawn by apexcharts.js; these tests pin down that the wrapper's Hide and Show
/// hand the series name straight to apexcharts.js and do not rebuild or resend the series, so
/// the tooltip sees exactly the state the JS hideSeries and showSeries produce.
/// </summary>
public class Issue660Tests : ApexChartsTestContext
{
    private const string HideSeries = "blazor_apexchart.hideSeries";
    private const string ShowSeries = "blazor_apexchart.showSeries";

    private record Point(double X, double Y);

    private static readonly List<Point> Points = [new(1, 2), new(2, 2), new(3, 1), new(4, 1)];

    private IRenderedComponent<ApexChart<Point>> RenderThreeLines() =>
        RenderChart<Point>(p =>
        {
            p.Add(c => c.Options, new ApexChartOptions<Point>());
            p.Add(c => c.XAxisType, XAxisType.Numeric);
            p.AddChildContent(builder =>
            {
                foreach (var name in new[] { "Customer", "Customer_2", "Customer_3" })
                {
                    builder.OpenComponent<ApexPointSeries<Point>>(0);
                    builder.AddAttribute(1, nameof(ApexPointSeries<Point>.Name), name);
                    builder.AddAttribute(2, nameof(ApexPointSeries<Point>.Items), Points);
                    builder.AddAttribute(3, nameof(ApexPointSeries<Point>.SeriesType), SeriesType.Line);
                    builder.AddAttribute(4, nameof(ApexPointSeries<Point>.XValue), (Func<Point, object>)(e => e.X));
                    builder.AddAttribute(5, nameof(ApexPointSeries<Point>.YValue), (Func<Point, decimal?>)(e => (decimal)e.Y));
                    builder.AddAttribute(6, nameof(ApexPointSeries<Point>.OrderByDescending), (Func<DataPoint<Point>, object>)(e => e.X));
                    builder.CloseComponent();
                }
            });
        });

    [Fact]
    public async Task Hide_calls_hideSeries_with_the_series_name_and_resends_nothing()
    {
        var cut = RenderThreeLines();
        var chartId = cut.Instance.ChartId;
        var callsBefore = JSInterop.Invocations.Count;

        Assert.Equal(3, cut.Instance.Series.Count);

        await cut.InvokeAsync(() => cut.Instance.Series[0].Hide());

        var hide = Assert.Single(InvocationsOf(HideSeries));
        Assert.Equal(chartId, hide.Arguments[0]);
        Assert.Equal("Customer", hide.Arguments[1]);

        // The only interop call Hide makes is hideSeries: the series are not resent or re-rendered.
        var newCalls = JSInterop.Invocations.Skip(callsBefore).Select(i => i.Identifier).ToList();
        Assert.Equal([HideSeries], newCalls);
    }

    [Fact]
    public async Task Show_calls_showSeries_for_each_series_name()
    {
        var cut = RenderThreeLines();
        var chartId = cut.Instance.ChartId;

        await cut.InvokeAsync(async () =>
        {
            await cut.Instance.Series[0].Hide();
            foreach (var series in cut.Instance.Series)
            {
                await series.Show();
            }
        });

        var shows = InvocationsOf(ShowSeries);
        Assert.Equal(["Customer", "Customer_2", "Customer_3"], shows.Select(s => (string)s.Arguments[1]!));
        Assert.All(shows, s => Assert.Equal(chartId, s.Arguments[0]));
        Assert.Empty(InvocationsOf(JsIdentifiers.UpdateSeries));
        Assert.Empty(InvocationsOf(JsIdentifiers.UpdateOptions));
        Assert.Single(InvocationsOf(JsIdentifiers.RenderChart));
    }
}
