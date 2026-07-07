using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApexCharts
{
    /// <summary>
    /// Component to create a <see cref="ChartType.Violin"/> data series in Blazor.
    /// </summary>
    /// <typeparam name="TItem">The data type to be used in the chart to create data points.</typeparam>
    /// <remarks>
    /// A violin chart renders a kernel-density curve per category from its raw observations. Each item
    /// supplies a single observation via <see cref="YValue"/>; items sharing the same <see cref="ApexBaseSeries{TItem}.XValue"/>
    /// (category) are grouped and their values become that category's distribution. Configure density scaling
    /// and the optional dot overlay via <see cref="PlotOptionsViolin"/>.
    ///
    /// Links:
    ///
    /// <see href="https://apexcharts.com/docs/chart-types/violin">JavaScript Documentation</see>
    /// </remarks>
    public class ApexViolinSeries<TItem> : ApexBaseSeries<TItem>, IApexSeries<TItem> where TItem : class
    {
        /// <summary>
        /// Expression to get a single raw observation from each item. All observations sharing the same
        /// <see cref="ApexBaseSeries{TItem}.XValue"/> are grouped into one violin's distribution.
        /// </summary>
        [Parameter] public Func<TItem, decimal> YValue { get; set; }

        /// <summary>
        /// Expression to determine the ordering of X-Values (categories) in the series
        /// </summary>
        [Parameter] public Func<ListPoint<TItem>, object> OrderBy { get; set; }

        /// <summary>
        /// Expression to determine the inverse ordering of X-Values (categories) in the series
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
            return ChartType.Violin;
        }

        /// <inheritdoc/>
        public IEnumerable<IDataPoint<TItem>> GenerateDataPoints(IEnumerable<TItem> items)
        {
            if (items == null)
            {
                return Enumerable.Empty<IDataPoint<TItem>>();
            }

            var data = items
               .GroupBy(d => XValue.Invoke(d))
               .Select(g => new ListPoint<TItem>
               {
                   X = g.Key,
                   Y = g.Select(d => (decimal?)YValue.Invoke(d)).ToList(),
                   Items = g.ToList()
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
