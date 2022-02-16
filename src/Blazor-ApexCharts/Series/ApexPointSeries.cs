using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;

namespace ApexCharts
{
    public class ApexPointSeries<TItem> : ApexBaseSeries<TItem>, IApexSeries<TItem> where TItem : class
    {
        [Parameter] public IEnumerable<IDataPoint<TItem>> Data { get; set; }
        [Parameter] public Func<TItem, decimal?> YValue { get; set; }
        [Parameter] public Expression<Func<IEnumerable<TItem>, decimal?>> YAggregate { get; set; }
        [Parameter] public Expression<Func<DataPoint<TItem>, object>> OrderBy { get; set; }
        [Parameter] public Expression<Func<DataPoint<TItem>, object>> OrderByDescending { get; set; }
        [Parameter] public SeriesType SeriesType { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            Chart.AddSeries(this);
        }

        public ChartType GetChartType()
        {
            switch (SeriesType)
            {
                case SeriesType.Area:
                    return ChartType.Area;
                case SeriesType.Bar:
                    return ChartType.Bar;
                case SeriesType.Donut:
                    return ChartType.Donut;
                case SeriesType.Heatmap:
                    return ChartType.Heatmap;
                case SeriesType.Histogram:
                    return ChartType.Histogram;
                case SeriesType.Line:
                    return ChartType.Line;
                case SeriesType.Pie:
                    return ChartType.Pie;
                case SeriesType.PolarArea:
                    return ChartType.PolarArea;
                case SeriesType.Radar:
                    return ChartType.Radar;
                case SeriesType.RadialBar:
                    return ChartType.RadialBar;
                case SeriesType.Scatter:
                    return ChartType.Scatter;
                case SeriesType.Treemap:
                    return ChartType.Treemap;
                default:
                    throw new SystemException($"SeriesType {SeriesType} can not be converted to CartType");
            }

        }


        public IEnumerable<IDataPoint<TItem>> GetData()
        {
            if (Data != null) { return Data; }

            IEnumerable<DataPoint<TItem>> data;

            if (YValue != null)
            {
                data = Items.Select(e => new DataPoint<TItem>
                {
                    X = XValue.Invoke(e),
                    Y = YValue.Invoke(e),
                    Items = new List<TItem> { e }
                });

            }
            else if (YAggregate != null)
            {
                var yAggCompiled = YAggregate.Compile();
                data = Items.GroupBy(XValue)
               .Select(d => new DataPoint<TItem>
               {
                   X = d.Key,
                   Y = yAggCompiled.Invoke(d),
                   Items = d.ToList()
               });
            }
            else
            {
                return new List<IDataPoint<TItem>>();
            }


            if (OrderBy != null)
            {
                data = data.OrderBy(o => OrderBy.Compile().Invoke(o));
            }
            else if (OrderByDescending != null)
            {
                data = data.OrderByDescending(o => OrderByDescending.Compile().Invoke(o));
            }

            var sw = new Stopwatch();
            sw.Start();
            var result = data.ToList();
            sw.Stop();
            Console.WriteLine($"ToList {sw.ElapsedMilliseconds.ToString("N")}ms");
            return result;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Chart.RemoveSeries(this);
        }
    }
}
