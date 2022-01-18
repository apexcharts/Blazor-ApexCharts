using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ApexCharts
{
    public class ApexRangeSeries<TItem> : ApexBaseSeries<TItem>, IApexSeries<TItem> where TItem : class
    {
        [Parameter] public Expression<Func<TItem, decimal>> YValue { get; set; }
        [Parameter] public Expression<Func<ListPoint<TItem>, object>> OrderBy { get; set; }
        [Parameter] public Expression<Func<ListPoint<TItem>, object>> OrderByDescending { get; set; }

        [Parameter] public Expression<Func<TItem, decimal>> YMinValue { get; set; }
        [Parameter] public Expression<Func<TItem, decimal>> YMaxValue { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            CheckInput();
            Chart.AddSeries(this);
        }

        private void CheckInput()
        {

            if (YValue == null && (YMinValue == null || YMinValue == null)) {
                throw new ArgumentNullException($"You have to set YValue or YMinValue and YMaxValue");
            }

        }

        public ChartType GetChartType()
        {
            return ChartType.RangeBar;
        }

        public IEnumerable<IDataPoint<TItem>> GetData()
        {
            IEnumerable<ListPoint<TItem>> data;

            if (YMinValue != null && YMaxValue != null)
            {
                data = Items
                       .Select(e => new ListPoint<TItem>
                       {
                           X = XValue.Compile().Invoke(e),
                           Y = new List<decimal> { YMinValue.Compile().Invoke(e), YMaxValue.Compile().Invoke(e) },
                           Items = new List<TItem> { e}
                       });
            }
            else
            {
                data = Items
                 .GroupBy(e => XValue.Compile().Invoke(e))
                 .Select(d => new ListPoint<TItem>
                 {
                     X = d.Key,
                     Y = new List<decimal> { d.AsQueryable().Min(YValue), d.AsQueryable().Max(YValue) },
                     Items = d
                 });
            }


            if (OrderBy != null)
            {
                data = data.OrderBy(o => OrderBy.Compile().Invoke(o));
            }
            else if (OrderByDescending != null)
            {
                data = data.OrderByDescending(o => OrderByDescending.Compile().Invoke(o));
            }

            return data;

        }

        public void Dispose()
        {
            Chart.RemoveSeries(this);
        }
    }
}
