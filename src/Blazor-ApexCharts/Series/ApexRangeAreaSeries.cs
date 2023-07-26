using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApexCharts
{
    /// <summary>
    /// Component to create a <see cref="ChartType.RangeArea"/> data series in Blazor
    /// </summary>
    /// <typeparam name="TItem">The data type to be used in the chart to create data points.</typeparam>
    /// <remarks>
    /// Links:
    /// 
    /// <see href="https://apexcharts.github.io/Blazor-ApexCharts/rangearea-charts">Blazor Example</see>,
    /// <see href="https://apexcharts.com/docs/chart-types/range-area-chart">JavaScript Documentation</see>
    /// </remarks>
    public class ApexRangeAreaSeries<TItem> : ApexBaseSeries<TItem>, IApexSeries<TItem> where TItem : class
    {
        [Parameter] public Func<TItem, decimal> Top { get; set; }
        [Parameter] public Func<TItem, decimal> Bottom { get; set; }
        [Parameter] public Func<ListPoint<TItem>, object> OrderBy { get; set; }
        [Parameter] public Func<ListPoint<TItem>, object> OrderByDescending { get; set; }
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
            return ChartType.RangeArea;
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
                     Bottom.Invoke(d),
                     Top.Invoke(d)
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
