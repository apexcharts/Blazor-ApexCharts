using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ApexCharts
{
    public class ApexBubbleSeries<TItem> : ApexBaseSeries<TItem>, IApexSeries<TItem> where TItem : class
    {
        [Parameter] public Func<IEnumerable<TItem>, decimal> YAggregate { get; set; }
        [Parameter] public Func<IEnumerable<TItem>, decimal> ZAggregate { get; set; }
        [Parameter] public Func< BubblePoint<TItem>, object> OrderBy { get; set; }
        [Parameter] public Func<BubblePoint<TItem>, object> OrderByDescending { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            Chart.AddSeries(this);
        }

        public ChartType GetChartType()
        {
            return ChartType.Bubble;
        }

        public IEnumerable<IDataPoint<TItem>> GetData()
        {
            var data = Items.GroupBy(XValue).Select(d => new BubblePoint<TItem>
            {
                X = d.Key,
                Y = YAggregate.Invoke(d),
                Z = ZAggregate.Invoke(d),
                Items = d.ToList()
            });

            if (OrderBy != null)
            {
                data = data.OrderBy(OrderBy);
            }
            else if (OrderByDescending != null)
            {
                data = data.OrderByDescending(OrderByDescending);
            }

            return data;
        }

        public void Dispose()
        {
            Chart.RemoveSeries(this);
        }
    }
}