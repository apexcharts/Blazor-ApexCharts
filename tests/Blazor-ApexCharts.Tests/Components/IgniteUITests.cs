using ApexCharts.Tests.TestHelpers;

namespace ApexCharts.Tests.Components;

/// <summary>
/// <see cref="ApexChartsServiceOptions.IgniteUI"/> loads the Ignite UI add-on once, before the first
/// chart renders, so the first paint already reads the page's design system.
/// </summary>
public class IgniteUIEnabledTests : ApexChartsTestContext
{
    private const string LoadIgniteUI = "blazor_apexchart.loadIgniteUI";

    public IgniteUIEnabledTests() : base(o => o.IgniteUI = true) { }

    private void RenderBarChart() =>
        RenderChart<Sale>(p => p
            .Add(c => c.Options, new ApexChartOptions<Sale>())
            .AddChildContent<ApexPointSeries<Sale>>(s => s
                .Add(x => x.Items, SampleData.Sales())
                .Add(x => x.SeriesType, SeriesType.Bar)
                .Add(x => x.XValue, e => e.Month)
                .Add(x => x.YValue, e => e.Amount)));

    [Fact]
    public void Loads_the_add_on_before_the_first_render()
    {
        RenderBarChart();

        var calls = JSInterop.Invocations.Select(i => i.Identifier).ToList();
        var load = calls.IndexOf(LoadIgniteUI);
        Assert.True(load >= 0, "the add-on was not loaded");
        Assert.True(load < calls.IndexOf(JsIdentifiers.RenderChart), "the add-on loaded after the first render");
    }

    [Fact]
    public void Loads_the_add_on_once_for_every_chart()
    {
        RenderBarChart();
        RenderBarChart();

        Assert.Single(InvocationsOf(LoadIgniteUI));
    }
}

/// <summary>Without <see cref="ApexChartsServiceOptions.IgniteUI"/> the add-on is never requested.</summary>
public class IgniteUIDisabledTests : ApexChartsTestContext
{
    [Fact]
    public void Does_not_load_the_add_on()
    {
        RenderChart<Sale>(p => p
            .Add(c => c.Options, new ApexChartOptions<Sale>())
            .AddChildContent<ApexPointSeries<Sale>>(s => s
                .Add(x => x.Items, SampleData.Sales())
                .Add(x => x.SeriesType, SeriesType.Bar)
                .Add(x => x.XValue, e => e.Month)
                .Add(x => x.YValue, e => e.Amount)));

        Assert.Empty(InvocationsOf("blazor_apexchart.loadIgniteUI"));
    }
}
