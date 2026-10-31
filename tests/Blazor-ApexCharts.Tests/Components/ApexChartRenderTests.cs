using System.Text.Json;
using ApexCharts.Tests.TestHelpers;

namespace ApexCharts.Tests.Components;

/// <summary>
/// Baseline bUnit tests of <see cref="ApexChart{TItem}"/>. The browser is not involved: the tests
/// assert on the interop calls the component makes and on the JSON it passes to them.
/// </summary>
public class ApexChartRenderTests : ApexChartsTestContext
{
    private IRenderedComponent<ApexChart<Sale>> RenderBarChart(ApexChartOptions<Sale>? options = null) =>
        RenderChart<Sale>(p => p
            .Add(c => c.Options, options ?? new ApexChartOptions<Sale>())
            .Add(c => c.Title, "Sales")
            .AddChildContent<ApexPointSeries<Sale>>(s => s
                .Add(x => x.Name, "Amount")
                .Add(x => x.Items, SampleData.Sales())
                .Add(x => x.SeriesType, SeriesType.Bar)
                .Add(x => x.XValue, e => e.Month)
                .Add(x => x.YValue, e => e.Amount)));

    [Fact]
    public void Imports_the_module_then_renders_the_chart()
    {
        RenderBarChart();

        var import = Assert.Single(InvocationsOf(JsIdentifiers.Import));
        Assert.StartsWith("./_content/Blazor-ApexCharts/js/blazor-apexcharts.js", (string)import.Arguments[0]!);
        Assert.Single(InvocationsOf(JsIdentifiers.GetApexCharts));
        Assert.Single(InvocationsOf(JsIdentifiers.RenderChart));
    }

    [Fact]
    public void Render_chart_receives_the_series_and_the_parameters()
    {
        var cut = RenderBarChart();

        var options = RenderedOptions();

        Assert.Equal(cut.Instance.ChartId, options.Get("chart.id")?.GetString());
        Assert.Equal("bar", options.Get("chart.type")?.GetString());
        Assert.Equal("Sales", options.Get("title.text")?.GetString());

        var series = options.Get("series");
        Assert.Equal(JsonValueKind.Array, series?.ValueKind);
        Assert.Equal("Amount", options.Get("series.0.name")?.GetString());
        Assert.Equal(3, options.Get("series.0.data")?.GetArrayLength());
        Assert.Equal("Jan", options.Get("series.0.data.0.x")?.GetString());
        Assert.Equal(10m, options.Get("series.0.data.0.y")?.GetDecimal());
        Assert.Equal(30m, options.Get("series.0.data.2.y")?.GetDecimal());
    }

    [Fact]
    public void Render_chart_targets_the_container_element()
    {
        var cut = RenderBarChart();

        var call = InvocationsOf(JsIdentifiers.RenderChart).Single();

        // Argument 1 is the ElementReference of the chart div, whose id is the chart id.
        Assert.IsType<Microsoft.AspNetCore.Components.ElementReference>(call.Arguments[1]);
        Assert.Equal(cut.Instance.ChartId, cut.Find("div").Id);
    }

    [Fact]
    public async Task Disposing_the_component_destroys_the_chart()
    {
        var cut = RenderBarChart();
        var chartId = cut.Instance.ChartId;

        await DisposeComponentsAsync();

        cut.WaitForAssertion(() =>
        {
            var destroy = Assert.Single(InvocationsOf(JsIdentifiers.DestroyChart));
            Assert.Equal(chartId, destroy.Arguments[0]);
        });
    }
}
