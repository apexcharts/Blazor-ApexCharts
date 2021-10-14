using Microsoft.AspNetCore.Components;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace ApexCharts
{
    public class ApexBoxPlotSeries<TItem> : ApexBaseSeries<TItem> where TItem : class
    {
        [Parameter] public Expression<Func<TItem, decimal>> YValue { get; set; }
        [Parameter] public Expression<Func<ListPoint<TItem>, object>> OrderBy { get; set; }
        [Parameter] public Expression<Func<ListPoint<TItem>, object>> OrderByDescending { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            SetData();

        }

        private void SetData()
        {
            var data = Items.GroupBy(e => XValue.Compile().Invoke(e)).Select(d => new ListPoint<TItem>
            {
                X = d.Key,
                Y = d.AsQueryable().Select(YValue).OrderBy(o => o),
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
