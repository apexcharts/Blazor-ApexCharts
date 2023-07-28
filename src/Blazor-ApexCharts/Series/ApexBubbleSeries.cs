using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApexCharts
{
    /// <summary>
    /// Component to create a <see cref="ChartType.Bubble"/> data series in Blazor
    /// </summary>
    /// <typeparam name="TItem">The data type to be used in the chart to create data points.</typeparam>
    /// <remarks>
    /// Links:
    /// 
    /// <see href="https://apexcharts.github.io/Blazor-ApexCharts/bubble-charts">Blazor Example</see>
    /// </remarks>
    public class ApexBubbleSeries<TItem> : ApexBaseSeries<TItem>, IApexSeries<TItem> where TItem : class
    {
        /// <summary>
        /// Expression to group Y-Values with. This will determine where each bubble is drawn on the Y-axis.
        /// </summary>
        [Parameter] public Func<IEnumerable<TItem>, decimal> YAggregate { get; set; }

        /// <summary>
        /// Expression to group Z-Values with. This will determine the size of each bubble.
        /// </summary>
        [Parameter] public Func<IEnumerable<TItem>, decimal> ZAggregate { get; set; }

        /// <summary>
        /// Expression to determine the ordering of X-Values in the series
        /// </summary>
        [Parameter] public Func<BubblePoint<TItem>, object> OrderBy { get; set; }

        /// <summary>
        /// Expression to determine the inverse ordering of X-Values in the series
        /// </summary>
        [Parameter] public Func<BubblePoint<TItem>, object> OrderByDescending { get; set; }

        /// <summary>
        /// Function to conditionally modify individual data points in the series
        /// </summary>
        [Parameter] public Action<BubblePoint<TItem>> DataPointMutator { get; set; }

        /// <inheritdoc/>
        protected override void OnInitialized()
        {
            base.OnInitialized();
            Chart.AddSeries(this);
        }

        /// <inheritdoc/>
        public ChartType GetChartType()
        {
            return ChartType.Bubble;
        }

        /// <inheritdoc/>
        public IEnumerable<IDataPoint<TItem>> GenerateDataPoints(IEnumerable<TItem> items)
        {
            if (items == null)
            {
                return Enumerable.Empty<IDataPoint<TItem>>();
            }

            var data = items.GroupBy(XValue).Select(d => new BubblePoint<TItem>
            {
                X = d.Key,
                Y = YAggregate.Invoke(d),
                Z = ZAggregate.Invoke(d),
                Items = d.ToList(),
                FillColor = GetPointColor(d)
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
