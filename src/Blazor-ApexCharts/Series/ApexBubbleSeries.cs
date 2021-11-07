using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ApexCharts
{
    public class ApexBubbleSeries<TItem> : ApexBaseSeries<TItem>, IApexSeries<TItem> where TItem : class
    {
        [Parameter] public Expression<Func<IEnumerable<TItem>, decimal>> YAggregate { get; set; }
        [Parameter] public Expression<Func<IEnumerable<TItem>, decimal>> ZAggregate { get; set; }
        [Parameter] public Expression<Func< BubblePoint<TItem>, object>> OrderBy { get; set; }
        [Parameter] public Expression<Func<BubblePoint<TItem>, object>> OrderByDescending { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            SetData();
            SetChartType(ChartType.Bubble);
        }

        private void SetData()
        {

            var data = Items.GroupBy(e => XValue.Compile().Invoke(e)).Select(d => new BubblePoint<TItem>
            {
                X = d.Key,
                Y = YAggregate.Compile().Invoke(d),
                Z =  ZAggregate.Compile().Invoke(d),
                Items = d.ToList()
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