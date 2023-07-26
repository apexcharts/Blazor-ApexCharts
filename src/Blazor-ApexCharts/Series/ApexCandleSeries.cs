using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApexCharts
{
    /// <summary>
    /// Component to create a <see cref="ChartType.Candlestick"/> data series in Blazor
    /// </summary>
    /// <typeparam name="TItem">The data type to be used in the chart to create data points.</typeparam>
    /// <remarks>
    /// Links:
    /// 
    /// <see href="https://apexcharts.github.io/Blazor-ApexCharts/candlestick-charts">Blazor Example</see>,
    /// <see href="https://apexcharts.com/docs/chart-types/candlestick">JavaScript Documentation</see>
    /// </remarks>
    public class ApexCandleSeries<TItem> : ApexBaseSeries<TItem>, IApexSeries<TItem> where TItem : class
    {
        /// <summary>
        /// Expression to get the starting Y-Value for each X-Value. This will determine where the top and bottom of the box are drawn.
        /// </summary>
        /// <remarks>
        /// If the open value is greater than the close value the candlestick will default to a red color.
        /// </remarks>
        [Parameter] public Func<TItem, decimal> Open { get; set; }

        /// <summary>
        /// Expression to get the highest Y-Value for each X-Value. This will determine where the top of the wick is drawn.
        /// </summary>
        [Parameter] public Func<TItem, decimal> High { get; set; }

        /// <summary>
        /// Expression to get the lowest Y-Value for each X-Value. This will determine where the bottom of the wick is drawn.
        /// </summary>
        [Parameter] public Func<TItem, decimal> Low { get; set; }

        /// <summary>
        /// Expression to get the starting Y-Value for each X-Value. This will determine where the top and bottom of the box are drawn.
        /// </summary>
        /// <remarks>
        /// If the close value is greater than the open value the candlestick will default to a green color.
        /// </remarks>
        [Parameter] public Func<TItem, decimal> Close { get; set; }

        /// <summary>
        /// Expression to determine the ordering of X-Values in the series
        /// </summary>
        [Parameter] public Func<ListPoint<TItem>, object> OrderBy { get; set; }

        /// <summary>
        /// Expression to determine the inverse ordering of X-Values in the series
        /// </summary>
        [Parameter] public Func<ListPoint<TItem>, object> OrderByDescending { get; set; }

        /// <summary>
        /// Function to conditionally modify individual data points in the series
        /// </summary>
        [Parameter] public Action<ListPoint<TItem>> DataPointMutator { get; set; }

        /// <inheritdoc/>
        protected override void OnInitialized()
        {
            base.OnInitialized();
            Chart.AddSeries(this);
        }

        /// <inheritdoc/>
        public ChartType GetChartType()
        {
            return ChartType.Candlestick;
        }

        /// <inheritdoc/>
        public IEnumerable<IDataPoint<TItem>> GenerateDataPoints(IEnumerable<TItem> items)
        {
            if (items == null)
            {
                return Enumerable.Empty<IDataPoint<TItem>>();
            }

            var data = items
             .Select(d => new ListPoint<TItem>
             {
                 X = XValue.Invoke(d),
                 Y = new List<decimal?>
                 {
                           Open.Invoke(d),
                           High.Invoke(d),
                           Low.Invoke(d),
                           Close.Invoke(d)
                 },
                 Items = new List<TItem> { d }
             });

            if (OrderBy != null)
            {
                data = data.OrderBy(OrderBy);
            }
            else if (OrderByDescending != null)
            {
                data = data.OrderByDescending(OrderByDescending);
            }

            return UpdateDataPoints(data, DataPointMutator);
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            Chart.RemoveSeries(this);
        }
    }
}
