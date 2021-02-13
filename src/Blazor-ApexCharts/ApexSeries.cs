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
    public class ApexSeries<TItem> : ComponentBase, IDisposable where TItem : class
    {
        [CascadingParameter(Name = "Chart")] public ApexChart<TItem> Chart { get; set; }
        [Parameter] public string Name { get; set; }
        [Parameter] public Expression<Func<TItem, object>> XValue { get; set; }
        [Parameter] public Expression<Func<TItem, decimal>> YValue { get; set; }
        [Parameter] public Expression<Func<IEnumerable<TItem>, decimal>> YAggregate { get; set; }
        [Parameter] public Expression<Func<DataPoint<TItem>, object>> OrderBy { get; set; }
        [Parameter] public Expression<Func<DataPoint<TItem>, object>> OrderByDescending { get; set; }
        [Parameter] public bool ShowDataLabels { get; set; }
        [Parameter] public IEnumerable<TItem> Items { get; set; }
        [Parameter] public SeriesStroke Stroke { get; set; }

        private readonly Series<TItem> series = new Series<TItem>();
        private IEnumerable<DataPoint<TItem>> currentDatalist;

        protected override async Task OnParametersSetAsync()
        {
            series.Name = Name;
            series.ShowDataLabels = ShowDataLabels;
            series.Stroke = Stroke;
            var xCompiled = XValue.Compile();
            IEnumerable<DataPoint<TItem>> datalist;
            if (YAggregate == null)
            {
                var yCompiled = YValue.Compile();
                datalist = Items.Select(e => new DataPoint<TItem> { X = xCompiled.Invoke(e), Y = yCompiled.Invoke(e), Items = new List<TItem> { e } });
            }
            else
            {
                var yAggCompiled = YAggregate.Compile();
                datalist = Items.GroupBy(e => xCompiled.Invoke(e)).Select(d => new DataPoint<TItem> { X = d.Key, Y = yAggCompiled.Invoke(d), Items = d.ToList() });
            }

            if (OrderBy != null)
            {
                datalist = datalist.OrderBy(o => OrderBy.Compile().Invoke(o));
            }
            else if (OrderByDescending != null)
            {
                datalist = datalist.OrderByDescending(o => OrderByDescending.Compile().Invoke(o));
            }

            datalist = datalist.ToList();
            series.Data = datalist;

            if (!Chart.ManualRender && Chart.ForceRender == false && currentDatalist != null && !currentDatalist.SequenceEqual(datalist, new DataPointComparer<TItem>()))
            {
                Chart.ForceRender = true;
            }

            currentDatalist = datalist;
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