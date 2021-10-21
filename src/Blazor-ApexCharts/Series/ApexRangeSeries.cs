using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ApexCharts
{
    public class ApexRangeSeries<TItem> : ApexBaseSeries<TItem> where TItem : class
    {
        [Parameter] public Expression<Func<TItem, decimal>> YValue { get; set; }
        [Parameter] public Expression<Func<ListPoint<TItem>, object>> OrderBy { get; set; }
        [Parameter] public Expression<Func<ListPoint<TItem>, object>> OrderByDescending { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            SetData();
            SetChartType(ChartType.RangeBar);
        }

        private void SetData()
        {
            var data = Items
                    .GroupBy(e => XValue.Compile().Invoke(e))
                    .Select(d => new ListPoint<TItem>
                    {
                        X = d.Key,
                        Y = new List<decimal> { d.AsQueryable().Min(YValue), d.AsQueryable().Max(YValue) },
                        Items = d
                    });

            if (OrderBy != null)
            {
                data = data.OrderBy(o => OrderBy.Compile().Invoke(o));
            }
            else if (OrderByDescending != null)
            {
                data = data.OrderByDescending(o => OrderByDescending.Compile().Invoke(o));
            }

            series.Data = data;
        }
    }
}
