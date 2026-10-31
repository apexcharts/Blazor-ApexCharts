using ApexCharts.Tests.TestHelpers;

namespace ApexCharts.Tests.Components;

/// <summary>
/// Issue 658: a nonce-based Content Security Policy. <see cref="Chart.Nonce"/> has to reach
/// apexcharts.js as <c>chart.nonce</c>, which copies it onto the &lt;style&gt; elements it injects,
/// and the component itself must not rely on inline style attributes.
/// </summary>
public class Issue658Tests : ApexChartsTestContext
{
    private const string Nonce = "r4nd0m-n0nce";

    [Fact]
    public void Nonce_serializes_to_chart_nonce()
    {
        var options = new ApexChartOptions<Sale> { Chart = new Chart { Nonce = Nonce } };

        var json = ChartJson.ToElement<Sale>(options);

        Assert.Equal(Nonce, json.Get("chart.nonce")?.GetString());
    }

    [Fact]
    public void Nonce_is_omitted_when_not_set()
    {
        var options = new ApexChartOptions<Sale> { Chart = new Chart { Id = "sales" } };

        var json = ChartJson.ToElement<Sale>(options);

        Assert.Equal("sales", json.Get("chart.id")?.GetString());
        Assert.Null(json.Get("chart.nonce"));
    }

    [Fact]
    public void Nonce_in_global_options_serializes_to_chart_nonce()
    {
        var json = ChartJson.Parse(ChartJson.SerializeGlobal(new ApexChartBaseOptions { Chart = new Chart { Nonce = Nonce } }));

        Assert.Equal(Nonce, json.Get("chart.nonce")?.GetString());
    }

    [Fact]
    public void Render_chart_passes_the_nonce()
    {
        RenderChart<Sale>(p => p
            .Add(c => c.Options, new ApexChartOptions<Sale> { Chart = new Chart { Nonce = Nonce } })
            .AddChildContent<ApexPointSeries<Sale>>(s => s
                .Add(x => x.Items, SampleData.Sales())
                .Add(x => x.SeriesType, SeriesType.Bar)
                .Add(x => x.XValue, e => e.Month)
                .Add(x => x.YValue, e => e.Amount)));

        Assert.Equal(Nonce, RenderedOptions().Get("chart.nonce")?.GetString());
    }

    [Fact]
    public void Razor_tooltip_source_is_hidden_without_an_inline_style_attribute()
    {
        var cut = RenderChart<Sale>(p => p
            .Add(c => c.Options, new ApexChartOptions<Sale>())
            .Add(c => c.ApexPointTooltip, hover => "<b>tooltip</b>")
            .AddChildContent<ApexPointSeries<Sale>>(s => s
                .Add(x => x.Items, SampleData.Sales())
                .Add(x => x.SeriesType, SeriesType.Bar)
                .Add(x => x.XValue, e => e.Month)
                .Add(x => x.YValue, e => e.Amount)));

        var source = cut.Find($"#tooltip_source_{cut.Instance.ChartId}");
        var wrapper = source.ParentElement!;

        // A strict style-src blocks style="display:none", which would leave the source visible.
        Assert.True(wrapper.HasAttribute("hidden"));
        Assert.False(wrapper.HasAttribute("style"));
        Assert.Empty(cut.FindAll("[style]"));
    }
}
