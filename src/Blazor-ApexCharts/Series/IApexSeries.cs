using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApexCharts
{
    /// <summary>
    /// Interface to define shared requirements for all chart types
    /// </summary>
    /// <typeparam name="TItem">The data type to be used in the chart to create data points.</typeparam>
    public interface IApexSeries<TItem> : IDisposable where TItem : class
    {
        /// <summary>
        /// Provides a reference to the parent chart object
        /// </summary>
        ApexChart<TItem> Chart { get; set; }

        /// <summary>
        /// The text to identify the series with
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Expression to return the X-value for each data point in the series
        /// </summary>
        Func<TItem, object> XValue { get; set; }

        /// <summary>
        /// The collection of data points to display on the series
        /// </summary>
        IEnumerable<TItem> Items { get; set; }

        /// <summary>
        /// The color to assign to values in the data series
        /// </summary>
        string Color { get; set; }

        /// <summary>
        /// The name of the group
        /// </summary>
        string Group { get; set; }

        /// <inheritdoc cref="DataLabels.Enabled"/>
        bool ShowDataLabels { get; set; }

        /// <summary>
        /// Used to generate the <see cref="ApexChartOptions{TItem}.Stroke"/> values for the series
        /// </summary>
        SeriesStroke Stroke { get; set; }

        /// <summary>
        /// Prepares the provided items to be displayed on the chart
        /// </summary>
        /// <param name="items">The items to prepare</param>
        public IEnumerable<IDataPoint<TItem>> GenerateDataPoints(IEnumerable<TItem> items);

        /// <summary>
        /// Returns the type of chart for the current data series
        /// </summary>
        public ChartType GetChartType();

        /// <summary>
        /// This method allows you to toggle the visibility of series programmatically. Useful when you have a custom legend.
        /// </summary>
        /// <remarks>
        /// Links:
        /// 
        /// <see href="https://apexcharts.github.io/Blazor-ApexCharts/methods/show-hide-series">Blazor Example</see>,
        /// <see href="https://apexcharts.com/docs/methods/#toggleSeries">JavaScript Documentation</see>
        /// </remarks>
        public Task Toggle();

        /// <summary>
        /// This method allows you to show the hidden series. If the series is already visible, this doesn't affect it.
        /// </summary>
        /// <remarks>
        /// Links:
        /// 
        /// <see href="https://apexcharts.github.io/Blazor-ApexCharts/methods/show-hide-series">Blazor Example</see>,
        /// <see href="https://apexcharts.com/docs/methods/#showSeries">JavaScript Documentation</see>
        /// </remarks>
        public Task Show();

        /// <summary>
        /// This method allows you to hide the visible series. If the series is already hidden, this method doesn't affect it.
        /// </summary>
        /// <remarks>
        /// Links:
        /// 
        /// <see href="https://apexcharts.github.io/Blazor-ApexCharts/methods/show-hide-series">Blazor Example</see>,
        /// <see href="https://apexcharts.com/docs/methods/#hideSeries">JavaScript Documentation</see>
        /// </remarks>
        public Task Hide();
    }
}
