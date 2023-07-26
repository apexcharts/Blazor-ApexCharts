using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApexCharts
{
    /// <summary>
    /// Component to create a <see cref="ChartType.RangeBar"/> data series in Blazor
    /// </summary>
    /// <typeparam name="TItem">The data type to be used in the chart to create data points.</typeparam>
    /// <remarks>
    /// Links:
    /// 
    /// <see href="https://apexcharts.github.io/Blazor-ApexCharts/rangebar-charts">Blazor Example</see>,
    /// <see href="https://apexcharts.com/docs/chart-types/range-bar-chart">JavaScript Documentation</see>
    /// </remarks>
    public class ApexRangeSeries<TItem> : ApexBaseSeries<TItem>, IApexSeries<TItem> where TItem : class
    {
        [Parameter] public Func<TItem, decimal> YValue { get; set; }
        [Parameter] public Func<ListPoint<TItem>, object> OrderBy { get; set; }
        [Parameter] public Func<ListPoint<TItem>, object> OrderByDescending { get; set; }

        [Parameter] public Func<TItem, decimal> YMinValue { get; set; }
        [Parameter] public Func<TItem, decimal> YMaxValue { get; set; }
        [Parameter] public Action<ListPoint<TItem>> DataPointMutator { get; set; }

        /// <inheritdoc/>
        protected override void OnInitialized()
        {
            base.OnInitialized();
            CheckInput();
            Chart.AddSeries(this);
        }

        private void CheckInput()
        {
            if (YValue == null && (YMinValue == null || YMinValue == null))
            {
                throw new ArgumentNullException($"You have to set YValue or YMinValue and YMaxValue");
            }
        }

        /// <inheritdoc/>
        public ChartType GetChartType()
        {
            return ChartType.RangeBar;
        }

        /// <inheritdoc/>
        public IEnumerable<IDataPoint<TItem>> GenerateDataPoints(IEnumerable<TItem> items)
        {
            if (items == null)
            {
                return Enumerable.Empty<IDataPoint<TItem>>();
            }

            IEnumerable<ListPoint<TItem>> data;

            if (YMinValue != null && YMaxValue != null)
            {
                data = items
                       .Select(e => new ListPoint<TItem>
                       {
                           X = XValue.Invoke(e),
                           Y = new List<decimal?> { YMinValue.Invoke(e), YMaxValue.Invoke(e) },
                           Items = new List<TItem> { e },
                           FillColor = GetPointColor(e)
                       });
            }
            else
            {
                data = items
                 .GroupBy(XValue)
                 .Select(d => new ListPoint<TItem>
                 {
                     X = d.Key,
                     Y = new List<decimal?> { d.AsQueryable().Min(YValue), d.AsQueryable().Max(YValue) },
                     Items = d,
                     FillColor = GetPointColor(d)
                 });
            }

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

        public IEnumerable<IDataPoint<TItem>> GetData()
        {
            return GenerateDataPoints(Items);
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            Chart.RemoveSeries(this);
        }
    }
}
