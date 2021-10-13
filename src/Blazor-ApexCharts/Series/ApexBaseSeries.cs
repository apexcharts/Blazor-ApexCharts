using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ApexCharts
{
    /// <summary>
    /// Adds a data series to the enclosing <see cref="Chart"/> component.
    /// </summary>
    public abstract class ApexBaseSeries<TItem> : ComponentBase, IDisposable where TItem : class
    {
        [CascadingParameter(Name = "Chart")] public ApexChart<TItem> Chart { get; set; }
        [Parameter] public string Name { get; set; }
        [Parameter] public Expression<Func<TItem, object>> XValue { get; set; }
      
        [Parameter] public bool ShowDataLabels { get; set; }
        [Parameter] public IEnumerable<TItem> Items { get; set; }
        [Parameter] public SeriesStroke Stroke { get; set; }

        internal readonly Series<TItem> series = new Series<TItem>();

        protected override void OnParametersSet()
        {
            series.Name = Name;
            series.ShowDataLabels = ShowDataLabels;
            series.Stroke = Stroke;
            //series.Type = MixedType;

                     
          
        }

        protected override void OnInitialized()
        {
            if (Chart.Options.Series == null) { Chart.Options.Series = new List<Series<TItem>>(); }
            Chart.Options.Series.Add(series);
        }

        void IDisposable.Dispose()
        {
            if (Chart.Options.Series != null && Chart.Options.Series.Contains(series))
            {
                Chart.Options.Series.Remove(series);
            }
        }
    }
}