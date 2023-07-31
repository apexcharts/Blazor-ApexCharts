using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApexCharts
{
    /// <summary>
    /// Base class to create a data series for a chart
    /// </summary>
    /// <typeparam name="TItem">The data type to be used in the chart to create data points.</typeparam>
    public abstract class ApexBaseSeries<TItem> : ComponentBase where TItem : class
    {
        /// <inheritdoc cref="IApexSeries{TItem}.Chart"/>
        [CascadingParameter(Name = "Chart")] public ApexChart<TItem> Chart { get; set; }

        /// <inheritdoc cref="IApexSeries{TItem}.Name"/>
        [Parameter] public string Name { get; set; }

        /// <inheritdoc cref="IApexSeries{TItem}.XValue"/>
        [Parameter] public Func<TItem, object> XValue { get; set; }

        /// <inheritdoc cref="IApexSeries{TItem}.ShowDataLabels"/>
        [Parameter] public bool ShowDataLabels { get; set; }

        /// <inheritdoc cref="IApexSeries{TItem}.Items"/>
        [Parameter] public IEnumerable<TItem> Items { get; set; }

        /// <inheritdoc cref="IApexSeries{TItem}.Stroke"/>
        [Parameter] public SeriesStroke Stroke { get; set; }

        /// <inheritdoc cref="IApexSeries{TItem}.Color"/>
        [Parameter] public string Color { get; set; }

        /// <summary>
        /// A function to provide a custom color for data points in the series
        /// </summary>
        /// <remarks>
        /// Links:
        /// 
        /// <see href="https://apexcharts.com/docs/options/colors">JavaScript Reference</see>
        /// </remarks>
        [Parameter] public Func<TItem, string> PointColor { get; set; }

		/// <inheritdoc cref="IApexSeries{TItem}.Group"/>
		[Parameter] public string Group { get; set; }

		/// <inheritdoc cref="IApexSeries{TItem}.Toggle"/>
		public async Task Toggle()
        {
            await Chart?.ToggleSeriesAsync(Name);
        }

        /// <inheritdoc cref="IApexSeries{TItem}.Show"/>
        public async Task Show()
        {
            await Chart?.ShowSeriesAsync(Name);
        }

        /// <inheritdoc cref="IApexSeries{TItem}.Hide"/>
        public async Task Hide()
        {
            await Chart?.HideSeriesAsync(Name);
        }

        /// <summary>
        /// Executes the <see cref="PointColor"/> function on the provided data point and returns the color value
        /// </summary>
        /// <param name="item">The data point to return the color for</param>
        public string GetPointColor(TItem item)
        {
            if (PointColor == null || item == null) { return null; }
            return PointColor.Invoke(item);
        }

        /// <summary>
        /// Executes the <see cref="PointColor"/> function on the provided data points and returns the color value
        /// </summary>
        /// <param name="items">The data points to return the color for</param>
        /// <remarks>
        /// Only returns the color for the first item in the collection
        /// </remarks>
        public string GetPointColor(IEnumerable<TItem> items)
        {
            if (PointColor == null || items == null || !items.Any()) { return null; }
            return PointColor.Invoke(items.First());
        }

        internal List<T> UpdateDataPoints<T>(IEnumerable<T> items, Action<T> updateMethod)
        {
            var data = items.ToList();

            if (updateMethod == null)
            {
                return data;
            }

            foreach (T item in data)
            {
                updateMethod(item);
            }

            return data;
        }
    }
}
