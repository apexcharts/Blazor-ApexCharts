using ApexCharts.Internal;
using ApexCharts.Tests.TestHelpers;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace ApexCharts.Tests.Components;

/// <summary>
/// #644: event handlers inside an ApexPointTooltip. blazor-apexcharts.js now moves the live
/// <c>tooltip_source_{id}</c> element into the chart tooltip instead of copying its innerHTML, and finds it
/// again through its hidden <c>tooltip_home_{id}</c> parent. These tests pin that markup contract; the
/// browser behaviour is covered by tests/e2e/specs/issue-644-tooltip-click.mjs.
/// </summary>
public class Issue644Tests : ApexChartsTestContext
{
    private int clicks;

    private IRenderedComponent<ApexChart<Sale>> RenderWithTooltip() =>
        RenderChart<Sale>(p => p
            .Add(c => c.Options, new ApexChartOptions<Sale>())
            .Add(c => c.ApexPointTooltip, (RenderFragment<HoverData<Sale>>)(data => builder =>
            {
                builder.OpenElement(0, "button");
                builder.AddAttribute(1, "id", "tooltip-button");
                builder.AddAttribute(2, "onclick", EventCallback.Factory.Create(this, () => clicks++));
                builder.AddContent(3, data.DataPoint.X);
                builder.CloseElement();
            }))
            .AddChildContent<ApexPointSeries<Sale>>(s => s
                .Add(x => x.Name, "Amount")
                .Add(x => x.Items, SampleData.Sales())
                .Add(x => x.SeriesType, SeriesType.Bar)
                .Add(x => x.XValue, e => e.Month)
                .Add(x => x.YValue, e => e.Amount)));

    private void HoverPoint(IRenderedComponent<ApexChart<Sale>> cut, int dataPointIndex)
    {
        var handler = (DotNetObjectReference<JSHandler<Sale>>)InvocationsOf(JsIdentifiers.RenderChart).Single().Arguments[0]!;
        cut.InvokeAsync(() => handler.Value.RazorTooltip(new JSDataPointSelection { SeriesIndex = 0, DataPointIndex = dataPointIndex }));
    }

    [Fact]
    public void Tooltip_source_is_the_only_child_of_its_hidden_home()
    {
        var cut = RenderWithTooltip();
        var id = cut.Instance.ChartId;

        var home = cut.Find($"[id='tooltip_home_{id}']");
        Assert.Single(home.Children);
        Assert.Equal($"tooltip_source_{id}", home.Children[0].Id);
    }

    [Fact]
    public void Tooltip_home_is_hidden_without_an_inline_style_attribute()
    {
        // #658: a strict style-src blocks style="display:none". The home stays hidden through the hidden
        // attribute (pinned with display:none !important from JS), and the source moved out of it into the
        // tooltip carries no style attribute from the markup either.
        var cut = RenderWithTooltip();
        var id = cut.Instance.ChartId;

        var home = cut.Find($"[id='tooltip_home_{id}']");
        Assert.True(home.HasAttribute("hidden"));
        Assert.False(home.HasAttribute("style"));
        Assert.False(cut.Find($"[id='tooltip_source_{id}']").HasAttribute("style"));
    }

    [Fact]
    public void Hovered_point_renders_into_the_source_and_the_tooltip_is_moved()
    {
        var cut = RenderWithTooltip();
        var id = cut.Instance.ChartId;

        HoverPoint(cut, 1);

        cut.WaitForAssertion(() =>
            Assert.Equal("Feb", cut.Find($"[id='tooltip_source_{id}'] #tooltip-button").TextContent));
        cut.WaitForAssertion(() =>
            Assert.Contains(InvocationsOf("blazor_apexchart.copyTooltipContent"), i => (string?)i.Arguments[0] == id));
    }

    [Fact]
    public void Click_handler_in_the_tooltip_fragment_is_wired()
    {
        var cut = RenderWithTooltip();
        var id = cut.Instance.ChartId;

        HoverPoint(cut, 0);
        cut.WaitForAssertion(() => cut.Find($"[id='tooltip_source_{id}'] #tooltip-button"));

        cut.Find("#tooltip-button").Click();

        Assert.Equal(1, clicks);
    }
}
