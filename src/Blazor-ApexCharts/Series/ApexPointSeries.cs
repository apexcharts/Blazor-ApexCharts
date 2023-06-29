using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ApexCharts
{
    public class ApexPointSeries<TItem> : ApexBaseSeries<TItem>, IApexSeries<TItem> where TItem : class
    {
        [Parameter] public Func<TItem, decimal?> YValue { get; set; }
        [Parameter] public Func<IEnumerable<TItem>, decimal?> YAggregate { get; set; }
        [Parameter] public Func<DataPoint<TItem>, object> OrderBy { get; set; }
        [Parameter] public Func<DataPoint<TItem>, object> OrderByDescending { get; set; }
        [Parameter] public SeriesType SeriesType { get; set; }
        [Parameter] public Action<DataPoint<TItem>> DataPointMutator { get; set; }

       

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
                case SeriesType.RangeArea:
                    return ChartType.RangeArea;
                default:
                    throw new SystemException($"SeriesType {SeriesType} can not be converted to ChartType");
            }
        }

        public IEnumerable<IDataPoint<TItem>> GenerateDataPoints(IEnumerable<TItem> items)
        {

            if (items == null)
            {
                return Enumerable.Empty<IDataPoint<TItem>>();
            }


            IEnumerable<DataPoint<TItem>> data;


            if (YValue != null)
            {
                data = items.Select(e => new DataPoint<TItem>
                {
                    X = XValue.Invoke(e),
                    Y = YValue.Invoke(e),
                    Items = new List<TItem> { e },
                    FillColor = GetPointColor(e)
                });

            }
            else if (YAggregate != null)
            {
                data = items.GroupBy(XValue)
               .Select(d => new DataPoint<TItem>
               {
                   X = d.Key,
                   Y = YAggregate.Invoke(d),
                   Items = d.ToList(),
                   FillColor = GetPointColor(d)
               });
            }
            else
            {
                return new List<IDataPoint<TItem>>();
            }

            data = GroupData(data.ToList());


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

        private List<DataPoint<TItem>> GroupData(List<DataPoint<TItem>> dataPoints)
        {
            if (Chart.GroupPoints == null || !dataPoints.Any())
            {
                return dataPoints;
            }

            if (Chart.XAxisType != XAxisType.Category)
            {
                throw new Exception("If chart GroupValues is specified then XAxisType must be set to Category.");
            }

            if (Chart.GroupPoints.PercentageThreshold == null && Chart.GroupPoints.MaxCount == null)
            {
                return dataPoints;
            }


            var newData = new List<DataPoint<TItem>>();
            decimal? thresholdValue = null;
            var maxCount = Chart.GroupPoints.MaxCount;
            int currentCount = 0;
            var groupedPoint = new DataPoint<TItem>
            {
                GroupedPoints = new List<DataPoint<TItem>>()
            };

            if (Chart.GroupPoints.PercentageThreshold != null)
            {
                thresholdValue = (decimal)(dataPoints.Sum(e => e.Y) * ((decimal)Chart.GroupPoints.PercentageThreshold / 100));
            }

            foreach (var dataPoint in dataPoints.OrderByDescending(e => e.Y))
            {

                if(ShouldGroup(maxCount, currentCount, dataPoint.Y, thresholdValue))
                {
                    groupedPoint.GroupedPoints.Add(dataPoint);
                }
                else
                {
                    newData.Add(dataPoint);
                    currentCount++;
                }
            }

            if (groupedPoint.GroupedPoints.Any())
            {
                groupedPoint.X = Chart.GroupPoints.Name;
                groupedPoint.Y = groupedPoint.GroupedPoints.Sum(e => e.Y);
                newData.Add(groupedPoint);
            }

            return newData;
        }

        private bool ShouldGroup(int? maxCount, int currentCount, decimal? currentValue, decimal? thresholdValue)
        {
            if(maxCount == null && thresholdValue == null)
            {
                return false;
            }

            if(maxCount != null && currentCount >= maxCount)
            {
                return true;
            }

            if (thresholdValue != null && currentValue < thresholdValue)
            {
                return true;
            }
            return false;

       }


        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Chart.RemoveSeries(this);
        }


    }
}
