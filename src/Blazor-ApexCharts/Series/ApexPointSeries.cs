using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ApexCharts
{
    public class ApexPointSeries<TItem> : ApexBaseSeries<TItem>, IApexSeries<TItem> where TItem : class
    {
        [Parameter] public Expression<Func<TItem, decimal>> YValue { get; set; }
        [Parameter] public Expression<Func<IEnumerable<TItem>, decimal>> YAggregate { get; set; }
        [Parameter] public Expression<Func<DataPoint<TItem>, object>> OrderBy { get; set; }
        [Parameter] public Expression<Func<DataPoint<TItem>, object>> OrderByDescending { get; set; }
        [Parameter] public PointType SeriesType { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            Chart.AddSeries(this);
        }

        public ChartType GetChartType()
        {
            switch (SeriesType)
            {
                case PointType.Area:
                    return ChartType.Area;
                case PointType.Bar:
                    return ChartType.Bar;
                case PointType.Donut:
                    return ChartType.Donut;
                case PointType.Heatmap:
                    return ChartType.Heatmap;
                case PointType.Histogram:
                    return ChartType.Histogram;
                case PointType.Line:
                    return ChartType.Line;
                case PointType.Pie:
                    return ChartType.Pie;
                case PointType.PolarArea:
                    return ChartType.PolarArea;
                case PointType.Radar:
                    return ChartType.Radar;
                case PointType.RadialBar:
                    return ChartType.RadialBar;
                case PointType.Scatter:
                    return ChartType.Scatter;
                case PointType.Treemap:
                    return ChartType.Treemap;
                default:
                    throw new SystemException($"SeriesType {SeriesType} can not be converted to CartType");
            }

        }


        public IEnumerable<IDataPoint<TItem>> GetData()
        {
            IEnumerable<DataPoint<TItem>> data;

            if (YValue != null)
            {
                data = Items.Select(e => new DataPoint<TItem>
                {
                    X = XValue.Compile().Invoke(e),
                    Y = YValue.Compile().Invoke(e),
                    Items = new List<TItem> { e }
                });

            }
            else if (YAggregate != null)
            {
                data = Items.GroupBy(e => XValue.Compile().Invoke(e))
               .Select(d => new DataPoint<TItem>
               {
                   X = d.Key,
                   Y = YAggregate.Compile().Invoke(d),
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

            return data;
        }

        public void Dispose()
        {
            Chart.RemoveSeries(this);
        }
    }
}
