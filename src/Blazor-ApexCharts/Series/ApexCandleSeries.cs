using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ApexCharts
{
    public class ApexCandleSeries<TItem> : ApexBaseSeries<TItem> where TItem : class
    {
        [Parameter] public Expression<Func<TItem, decimal>> Open { get; set; }
        [Parameter] public Expression<Func<TItem, decimal>> High { get; set; }
        [Parameter] public Expression<Func<TItem, decimal>> Low { get; set; }
        [Parameter] public Expression<Func<TItem, decimal>> Close { get; set; }
    
        protected override void OnInitialized()
        {
            base.OnInitialized();
            SetData();
         
        }

        private void SetData()
        {
            var xCompile = XValue.Compile();
            var openCompile = Open.Compile();
            var highCompile = High.Compile();
            var lowCompile = Low.Compile();
            var closeCompile = Close.Compile();

            series.Data = Items
               .Select(d => new ListPoint<TItem>
               {
                   X = xCompile.Invoke(d),
                   Y = new List<decimal>
                   {
                        openCompile.Invoke(d),
                        highCompile.Invoke(d),
                        lowCompile.Invoke(d),
                       closeCompile.Invoke(d)
                   },
                   Items = Items
               });
        }
    }
}
