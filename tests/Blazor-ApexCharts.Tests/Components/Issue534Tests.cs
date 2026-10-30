using ApexCharts.Tests.TestHelpers;

namespace ApexCharts.Tests.Components;

/// <summary>
/// Issue #534: when several line series share a point, the tooltip and the click events report one
/// series only. That limit is in apexcharts.js (intersect mode hit-tests the topmost marker), but the
/// wrapper is what puts a line chart into intersect mode when <c>OnDataPointSelection</c> is set.
/// These tests pin that switch, which the Data Point Selection docs page describes, and the
/// workaround it recommends (OnMarkerClick or OnClick leave the shared tooltip alone).
/// </summary>
public class Issue534Tests : ApexChartsTestContext
{
    private IRenderedComponent<ApexChart<Sale>> RenderLineChart(Action<ComponentParameterCollectionBuilder<ApexChart<Sale>>> events) =>
        RenderChart<Sale>(p =>
        {
            p.Add(c => c.Options, new ApexChartOptions<Sale>());
            events(p);
            p.AddChildContent<ApexPointSeries<Sale>>(s => s
                .Add(x => x.Name, "Amount")
                .Add(x => x.Items, SampleData.Sales())
                .Add(x => x.SeriesType, SeriesType.Line)
                .Add(x => x.XValue, e => e.Month)
                .Add(x => x.YValue, e => e.Amount));
        });

    [Fact]
    public void Data_point_selection_on_a_line_chart_switches_the_tooltip_to_intersect_mode()
    {
        RenderLineChart(p => p.Add(c => c.OnDataPointSelection, (SelectedData<Sale> _) => { }));

        var options = RenderedOptions();

        Assert.True(options.Get("tooltip.intersect")?.GetBoolean());
        Assert.False(options.Get("tooltip.shared")?.GetBoolean());
        Assert.Equal(5, options.Get("markers.size")?.GetInt32());
    }

    [Fact]
    public void Marker_click_and_click_leave_the_shared_tooltip_alone()
    {
        RenderLineChart(p => p
            .Add(c => c.OnMarkerClick, (SelectedData<Sale> _) => { })
            .Add(c => c.OnClick, (SelectedData<Sale> _) => { }));

        var options = RenderedOptions();

        Assert.Null(options.Get("tooltip.intersect"));
        Assert.Null(options.Get("tooltip.shared"));
    }
}
