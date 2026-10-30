using ApexCharts.Internal;
using ApexCharts.Tests.TestHelpers;
using Microsoft.JSInterop;

namespace ApexCharts.Tests.Components;

/// <summary>
/// Issue 420: the script holds a brush chart back until its target charts have rendered. While it
/// waits the chart does not exist in the browser, so <c>OnRendered</c> must wait for it too.
/// </summary>
public class Issue420Tests : ApexChartsTestContext
{
    private int renderedCount;

    private IRenderedComponent<ApexChart<Sale>> RenderBrushChart() =>
        RenderChart<Sale>(p => p
            .Add(c => c.Options, new ApexChartOptions<Sale>
            {
                Chart = new Chart { Brush = new Brush { Enabled = true, Target = "issue420-target" } }
            })
            .Add(c => c.OnRendered, () => renderedCount++)
            .AddChildContent<ApexPointSeries<Sale>>(s => s
                .Add(x => x.Items, SampleData.Sales())
                .Add(x => x.SeriesType, SeriesType.Area)
                .Add(x => x.XValue, e => e.Month)
                .Add(x => x.YValue, e => e.Amount)));

    [Fact]
    public void On_rendered_fires_after_a_chart_that_rendered()
    {
        // renderChart returns "rendered" when it drew the chart
        JSInterop.Setup<string>(JsIdentifiers.RenderChart, _ => true).SetResult("rendered");

        var cut = RenderBrushChart();

        cut.WaitForAssertion(() => Assert.Equal(1, renderedCount));
    }

    [Fact]
    public void On_rendered_fires_when_render_chart_returns_nothing()
    {
        // A script that returns nothing (an older or a custom copy) keeps the previous behaviour
        var cut = RenderBrushChart();

        cut.WaitForAssertion(() => Assert.Equal(1, renderedCount));
    }

    [Fact]
    public async Task On_rendered_waits_for_a_held_back_brush_chart()
    {
        // renderChart returns "waiting" when it holds the brush chart back for its targets
        JSInterop.Setup<string>(JsIdentifiers.RenderChart, _ => true).SetResult("waiting");

        var cut = RenderBrushChart();

        // The component has finished its render call; the chart is not in the browser yet
        await cut.InvokeAsync(() => Task.CompletedTask);
        Assert.Equal(0, renderedCount);

        // The script calls back once the brush chart has rendered
        var call = InvocationsOf(JsIdentifiers.RenderChart).Single();
        var handler = Assert.IsType<DotNetObjectReference<JSHandler<Sale>>>(call.Arguments[0]);
        await cut.InvokeAsync(() => handler.Value.JSRendered());

        Assert.Equal(1, renderedCount);
    }
}
