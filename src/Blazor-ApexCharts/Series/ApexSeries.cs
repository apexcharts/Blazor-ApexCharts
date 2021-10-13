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
        [Parameter] public MixedType? MixedType { get; set; }
        [Parameter] public Expression<Func<TItem, object>> XValue { get; set; }
        [Parameter] public Expression<Func<TItem, decimal>> YValue { get; set; }
        [Parameter] public Expression<Func<IEnumerable<TItem>, decimal>> YAggregate { get; set; }
        [Parameter] public Expression<Func<IEnumerable<TItem>, decimal>> ZAggregate { get; set; }
        [Parameter] public Expression<Func<DataPoint<TItem>, object>> OrderBy { get; set; }
        [Parameter] public Expression<Func<DataPoint<TItem>, object>> OrderByDescending { get; set; }
        [Parameter] public bool ShowDataLabels { get; set; }
        [Parameter] public IEnumerable<TItem> Items { get; set; }
        [Parameter] public SeriesStroke Stroke { get; set; }

        private readonly Series<TItem> series = new();

        protected override void OnParametersSet()
        {
            series.Name = Name;
            series.ShowDataLabels = ShowDataLabels;
            series.Stroke = Stroke;
            series.Type = MixedType;
            series.Data = GetData();

        }

        protected override void OnInitialized()
        {
            if (Chart.Options.Series == null) { Chart.Options.Series = new List<Series<TItem>>(); }
            Chart.Options.Series.Add(series);
        }

        private IEnumerable<IDataPoint<TItem>> GetData()
        {

            if (Chart.DataCategory == DataCategory.Point ||
                Chart.DataCategory == DataCategory.NoAxis ||
                MixedType != null)
            {
                return GetPointData();
            }

            if (Chart.DataCategory == DataCategory.BoxPlot)
            {
                return GetBoxPlotData();
            }

            if (Chart.DataCategory == DataCategory.Candle)
            {
                return GetCandleData();
            }

            if (Chart.DataCategory == DataCategory.Range)
            {
                return GetRangeData();
            }

            if (Chart.DataCategory == DataCategory.XYZ)
            {
                return GetXYZData();
            }

            return null;
        }

        private IEnumerable<IDataPoint<TItem>> GetRangeData()
        {
            var xCompiled = XValue.Compile();
            return Items.GroupBy(e => xCompiled.Invoke(e))
                .Select(d => new ListPoint<TItem>
                {
                    X = d.Key,
                    Y = new List<decimal> { d.AsQueryable().Min(YValue), d.AsQueryable().Max(YValue) },
                    Items = d
                });
        }

        private IEnumerable<IDataPoint<TItem>> GetBoxPlotData()
        {
            var xCompiled = XValue.Compile();
            return Items.GroupBy(e => xCompiled.Invoke(e)).Select(d => new ListPoint<TItem> { X = d.Key, Y = d.AsQueryable().Select(YValue).OrderBy(o => o), Items = d });
        }
        private IEnumerable<IDataPoint<TItem>> GetCandleData()
        {
            var xCompiled = XValue.Compile();
            return Items.GroupBy(e => xCompiled.Invoke(e)).Select(d => new ListPoint<TItem> { X = d.Key, Y = d.AsQueryable().Select(YValue), Items = d });
        }

        private IEnumerable<IDataPoint<TItem>> GetXYZData()
        {
            var xCompiled = XValue.Compile();
            IEnumerable<BubblePoint<TItem>> datalist;
           
            var yAggCompiled = YAggregate.Compile();
            var zAggCompiled = ZAggregate.Compile();
            datalist = Items.GroupBy(e => xCompiled.Invoke(e)).Select(d => new BubblePoint<TItem>
            {
                X = d.Key,
                Y =  yAggCompiled.Invoke(d),
                Z = yAggCompiled.Invoke(d),
                Items =  d.ToList()
            });
           

            return datalist;
        }

        private IEnumerable<IDataPoint<TItem>> GetPointData()
        {
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

            return datalist;
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