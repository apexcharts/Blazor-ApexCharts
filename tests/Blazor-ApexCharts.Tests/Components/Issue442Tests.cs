using System.Text.Json;
using ApexCharts.Internal;
using ApexCharts.Tests.TestHelpers;
using Microsoft.JSInterop;

namespace ApexCharts.Tests.Components;

/// <summary>
/// Issue 442: on pie and donut charts a legend click also selects the slice, so OnDataPointSelection
/// runs for both. <see cref="SelectedData{TItem}.Source"/> says which one it was. blazor-apexcharts.js
/// sends the origin as a string; these tests feed the component that payload the way JS interop does.
/// </summary>
public class Issue442Tests : ApexChartsTestContext
{
    // JS interop deserializes [JSInvokable] arguments with the web defaults (camelCase, case-insensitive).
    private static readonly JsonSerializerOptions InteropJson = new(JsonSerializerDefaults.Web);

    private SelectedData<Sale>? selected;
    private SelectedData<Sale>? markerClicked;

    private JSHandler<Sale> RenderPieChart()
    {
        RenderChart<Sale>(p => p
            .Add(c => c.Options, new ApexChartOptions<Sale>())
            .Add(c => c.OnDataPointSelection, (SelectedData<Sale> data) => selected = data)
            .Add(c => c.OnMarkerClick, (SelectedData<Sale> data) => markerClicked = data)
            .AddChildContent<ApexPointSeries<Sale>>(s => s
                .Add(x => x.Name, "Amount")
                .Add(x => x.Items, SampleData.Sales())
                .Add(x => x.SeriesType, SeriesType.Pie)
                .Add(x => x.XValue, e => e.Month)
                .Add(x => x.YValue, e => e.Amount)));

        var call = InvocationsOf(JsIdentifiers.RenderChart).Single();
        return Assert.IsType<DotNetObjectReference<JSHandler<Sale>>>(call.Arguments[0]).Value;
    }

    private static JSDataPointSelection Payload(string? source)
    {
        var sourceJson = source is null ? "" : $",\"source\":\"{source}\"";
        return JsonSerializer.Deserialize<JSDataPointSelection>(
            $"{{\"dataPointIndex\":1,\"seriesIndex\":0,\"selectedDataPoints\":[[1]]{sourceJson}}}", InteropJson)!;
    }

    [Theory]
    [InlineData("legend", DataPointSelectionSource.Legend)]
    [InlineData("dataPoint", DataPointSelectionSource.DataPoint)]
    [InlineData("method", DataPointSelectionSource.Method)]
    [InlineData(null, DataPointSelectionSource.DataPoint)]
    public void Data_point_selection_reports_its_source(string? source, DataPointSelectionSource expected)
    {
        var handler = RenderPieChart();

        handler.JSDataPointSelected(Payload(source));

        Assert.NotNull(selected);
        Assert.Equal(expected, selected.Source);
        Assert.Equal("Feb", selected.DataPoint.X);
        Assert.Equal(1, selected.DataPointIndex);
        Assert.True(selected.IsSelected);
    }

    [Fact]
    public void Marker_click_is_always_a_data_point_source()
    {
        var handler = RenderPieChart();

        handler.JSMarkerClick(Payload(null));

        Assert.NotNull(markerClicked);
        Assert.Equal(DataPointSelectionSource.DataPoint, markerClicked.Source);
    }
}
