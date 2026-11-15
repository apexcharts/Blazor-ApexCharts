using ApexCharts.Tests.TestHelpers;

namespace ApexCharts.Tests.Components;

/// <summary>
/// Checks the "Updating a chart with new data" patterns in README.md. The load waits like a real
/// request, and the button is clicked through the renderer, so the event handler goes through the
/// same render steps as in a browser.
/// </summary>
public class ReadmeUpdateExampleTests : ApexChartsTestContext
{
    private string[] ClickAndCaptureUpdatedMonths(string button)
    {
        var cut = Render<ReadmeUpdateExampleHost>();
        cut.WaitForAssertion(() => Assert.NotEmpty(InvocationsOf(JsIdentifiers.RenderChart)));

        cut.Find(button).Click();
        cut.WaitForAssertion(() => Assert.NotEmpty(InvocationsOf(JsIdentifiers.UpdateSeries)));

        var series = LastJsonArgument(JsIdentifiers.UpdateSeries, 1);
        return series.Get("0.data")!.Value.EnumerateArray()
            .Select(p => p.Get("x")!.Value.GetString()!)
            .ToArray();
    }

    [Theory]
    [InlineData("#reassign-render")]
    [InlineData("#in-place")]
    public void The_readme_patterns_send_the_reloaded_data(string button)
    {
        Assert.Equal(ReadmeUpdateExampleHost.FreshMonths, ClickAndCaptureUpdatedMonths(button));
    }

    /// <summary>
    /// Canary for the trap the README warns about. The handler's first await renders the page with
    /// the old list, so a list assigned afterwards has not reached the series when
    /// <c>UpdateSeriesAsync</c> reads it. If this starts sending the new data, the README can drop
    /// the <c>StateHasChanged()</c> call.
    /// </summary>
    [Fact]
    public void Assigning_a_new_list_without_a_render_sends_the_previous_data()
    {
        var initialMonths = SampleData.Sales().Select(s => s.Month).ToArray();

        Assert.Equal(initialMonths, ClickAndCaptureUpdatedMonths("#reassign"));
    }
}
