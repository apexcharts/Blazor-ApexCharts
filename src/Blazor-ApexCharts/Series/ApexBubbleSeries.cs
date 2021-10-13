using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ApexCharts
{

    public class ApexBubbleSeries<TItem> : ApexBaseSeries<TItem> where TItem : class
    {
        [Parameter] public Expression<Func<IEnumerable<TItem>, decimal>> YAggregate { get; set; }
        [Parameter] public Expression<Func<IEnumerable<TItem>, decimal>> ZAggregate { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            SetData();

        }

        private void SetData()
        {
            var xCompile = XValue.Compile();
            var yComplie = YAggregate.Compile();
            var ZComplie = ZAggregate.Compile();

            var yAggCompiled = YAggregate.Compile();
            var zAggCompiled = ZAggregate.Compile();
            series.Data = Items.GroupBy(e => xCompile.Invoke(e)).Select(d => new BubblePoint<TItem>
            {
                X = d.Key,
                Y = yAggCompiled.Invoke(d),
                Z = yAggCompiled.Invoke(d),
                Items = d.ToList()
            });
        }
    }
}