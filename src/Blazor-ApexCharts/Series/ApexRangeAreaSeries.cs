using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ApexCharts
{
    public class ApexRangeAreaSeries<TItem> : ApexBaseSeries<TItem>, IApexSeries<TItem> where TItem : class
    {
        [Parameter] public Func<TItem, decimal> Top { get; set; }
        [Parameter] public Func<TItem, decimal> Bottom { get; set; }
        [Parameter] public Func<ListPoint<TItem>, object> OrderBy { get; set; }
        [Parameter] public Func<ListPoint<TItem>, object> OrderByDescending { get; set; }
        [Parameter] public Action<ListPoint<TItem>> DataPointMutator { get; set; }


        protected override void OnInitialized()
        {
            base.OnInitialized();
            Chart.AddSeries(this);
        }

        public ChartType GetChartType()
        {
            return ChartType.RangeArea;
        }

        public IEnumerable<IDataPoint<TItem>> GenerateDataPoints(IEnumerable<TItem> items)
        {
            if(items == null)
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

        public void Dispose()
        {
            Chart.RemoveSeries(this);
        }

    }
}
