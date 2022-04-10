using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ApexCharts
{
    public class ApexBubbleSeries<TItem> : ApexBaseSeries<TItem>, IApexSeries<TItem> where TItem : class
    {
        [Parameter] public Func<IEnumerable<TItem>, decimal> YAggregate { get; set; }
        [Parameter] public Func<IEnumerable<TItem>, decimal> ZAggregate { get; set; }
        [Parameter] public Func<BubblePoint<TItem>, object> OrderBy { get; set; }
        [Parameter] public Func<BubblePoint<TItem>, object> OrderByDescending { get; set; }
        [Parameter] public Action<BubblePoint<TItem>> DataPointMutator { get; set; }


        protected override void OnInitialized()
        {
            base.OnInitialized();
            Chart.AddSeries(this);
        }

        public ChartType GetChartType()
        {
            return ChartType.Bubble;
        }

        public IEnumerable<IDataPoint<TItem>> GenerateDataPoints(IEnumerable<TItem> items)
        {
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

        public void Dispose()
        {
            Chart.RemoveSeries(this);
        }
    }
}