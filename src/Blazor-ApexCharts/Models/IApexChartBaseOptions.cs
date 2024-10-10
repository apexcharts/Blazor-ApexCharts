using System.Collections.Generic;

namespace ApexCharts
{
    /// <summary>
    /// Contains all none generic chart options
    /// </summary>
    public interface IApexChartBaseOptions
    {
        /// <inheritdoc cref="ApexCharts.Annotations" />
        Annotations Annotations { get; set; }

        /// <inheritdoc cref="ApexCharts.ApexChartsBlazorOptions" />
        ApexChartsBlazorOptions Blazor { get; set; }

        /// <inheritdoc cref="ApexCharts.Chart" />
        Chart Chart { get; set; }

        /// <inheritdoc cref="ApexCharts.Color" />
        List<string> Colors { get; set; }

        /// <inheritdoc cref="ApexCharts.DataLabels" />
        DataLabels DataLabels { get; set; }

        /// <summary>
        /// Logs function calls and options to the browser console when true
        /// </summary>
        bool? Debug { get; set; }

        /// <inheritdoc cref="ApexCharts.Fill" />
        Fill Fill { get; set; }

        /// <inheritdoc cref="ApexCharts.ForecastDataPoints" />
        ForecastDataPoints ForecastDataPoints { get; set; }

        /// <inheritdoc cref="ApexCharts.Grid" />
        Grid Grid { get; set; }

        /// <inheritdoc cref="ApexCharts.Label" />
        List<string> Labels { get; set; }

        /// <inheritdoc cref="ApexCharts.Legend" />
        Legend Legend { get; set; }

        /// <inheritdoc cref="ApexCharts.Markers" />
        Markers Markers { get; set; }

        /// <inheritdoc cref="ApexCharts.NoData" />
        NoData NoData { get; set; }

        /// <inheritdoc cref="ApexCharts.PlotOptions" />
        PlotOptions PlotOptions { get; set; }

        /// <inheritdoc cref="ApexCharts.States" />
        States States { get; set; }

        /// <inheritdoc cref="ApexCharts.Stroke" />
        Stroke Stroke { get; set; }

        /// <inheritdoc cref="ApexCharts.Subtitle" />
        Subtitle Subtitle { get; set; }

        /// <inheritdoc cref="ApexCharts.Theme" />
        Theme Theme { get; set; }

        /// <inheritdoc cref="ApexCharts.Title" />
        Title Title { get; set; }

        /// <inheritdoc cref="ApexCharts.Tooltip" />
        Tooltip Tooltip { get; set; }

        /// <inheritdoc cref="ApexCharts.XAxis" />
        XAxis Xaxis { get; set; }

        /// <inheritdoc cref="ApexCharts.YAxis" />
        List<YAxis> Yaxis { get; set; }
    }
}