using System.Collections.Generic;

namespace ApexCharts
{
    public interface IApexChartOptions
    {
        Annotations Annotations { get; set; }
        ApexChartsBlazorOptions Blazor { get; set; }
        Chart Chart { get; set; }
        List<string> Colors { get; set; }
        DataLabels DataLabels { get; set; }
        bool? Debug { get; set; }
        Fill Fill { get; set; }
        ForecastDataPoints ForecastDataPoints { get; set; }
        Grid Grid { get; set; }
        List<string> Labels { get; set; }
        Legend Legend { get; set; }
        Markers Markers { get; set; }
        NoData NoData { get; set; }
        PlotOptions PlotOptions { get; set; }
        States States { get; set; }
        Stroke Stroke { get; set; }
        Subtitle Subtitle { get; set; }
        Theme Theme { get; set; }
        Title Title { get; set; }
        Tooltip Tooltip { get; set; }
        XAxis Xaxis { get; set; }
        List<YAxis> Yaxis { get; set; }
    }
}