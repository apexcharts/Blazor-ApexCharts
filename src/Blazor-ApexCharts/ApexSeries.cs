using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ApexCharts
{
    /// <summary>
    /// Adds a data series to the enclosing <see cref="Chart"/> component.
    /// </summary>
    public class ApexSeries<TItem> : ComponentBase, IDisposable where TItem : class
    {
        [CascadingParameter(Name = "Chart")] public ApexChart<TItem> ParentChart { get; set; }
        [Parameter] public string Name { get; set; }
        [Parameter] public Expression<Func<TItem, object>> XValue { get; set; }
        [Parameter] public Expression<Func<TItem, decimal>> YValue { get; set; }
        [Parameter] public Expression<Func<IEnumerable<TItem>, decimal>> YAggregate{ get; set; }
        [Parameter] public Expression<Func<TItem, object>> OrderBy { get; set; }
        [Parameter] public bool ShowDataLabels { get; set; }


        [Parameter] public IEnumerable<TItem> Items { get; set; }



        private readonly Series series = new Series();
        protected override void OnParametersSet()
        {
            series.Name = Name;
            series.ShowDataLabels = ShowDataLabels;
                       
            var xCompiled = XValue.Compile();
            if (YAggregate == null)
              {
                var yCompiled = YValue.Compile();
                series.Data = Items.Select(e => new DataPoint { X = xCompiled.Invoke(e), Y = yCompiled.Invoke(e), Items = new List<object> { e }}).ToList();
            }
            else
            {
                var yAggCompiled = YAggregate.Compile();
                series.Data  = Items.OrderBy(o=> OrderBy.Compile().Invoke(o)).GroupBy(e => xCompiled.Invoke(e)).Select(d => new DataPoint { X = d.Key, Y = yAggCompiled.Invoke(d), Items = d.ToList<object>() }).ToList();
            }
                       
         
        }

        protected override void OnInitialized()
        {
            if (ParentChart.Options.Series == null) { ParentChart.Options.Series = new List<Series>(); }
            ParentChart.Options.Series.Add(series);
        }
           

        void IDisposable.Dispose()
        {
            if (ParentChart.Options.Series != null && ParentChart.Options.Series.Contains(series)){
                ParentChart.Options.Series.Remove(series);
            }
            
        }
           
    }
}