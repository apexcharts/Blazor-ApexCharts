using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApexCharts
{
    public class ApexBoxSeries<TItem> : ApexBaseSeries<TItem> where TItem : class
    {
        [Parameter] public Expression<Func<TItem, decimal>> YValue { get; set; }


        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            var xCompiled = XValue.Compile();

            IEnumerable<BoxPoint<TItem>> datalist;
            datalist = Items.GroupBy(e => xCompiled.Invoke(e)).Select(d => new BoxPoint<TItem> { X = d.Key, Y = d.AsQueryable().Select(YValue).ToList(), Items = d.ToList() });

            series.Data = datalist;

        }
    }
}
