using System.Collections.Generic;

namespace ApexCharts
{
    public class JSDataPointSelection
    {
        public List<List<int?>> SelectedDataPoints { get; set; }
        public int DataPointIndex { get; set; }
        public int SeriesIndex { get; set; }
    }

    public class JSXAxisLabelClick
    {
        public int LabelIndex { get; set; }
        public string Caption { get; set; }
    }

    public class SelectedData<TItem> where TItem : class
    {
        public ApexChart<TItem> Chart { get; set; }
        public Series<TItem> Series { get; set; }
        public IDataPoint<TItem> DataPoint { get; set; }
        public bool IsSelected { get; internal set; }
        public int DataPointIndex { get; set; }
        public int SeriesIndex { get; set; }
    }

    public class HoverData<TItem> where TItem : class
    {
        public ApexChart<TItem> Chart { get; set; }
        public Series<TItem> Series { get; set; }
        public IDataPoint<TItem> DataPoint { get; set; }
        public int DataPointIndex { get; set; }
        public int SeriesIndex { get; set; }

    }

    public class ZoomedData<TItem> where TItem : class
    {
        public ApexChart<TItem> Chart { get; set; }

        public SelectionXAxis XAxis { get; set; }
        public List<object> YAxis { get; set; }

        public bool IsZoomed => XAxis?.Min != null && XAxis?.Max != null;
    }

    public class SelectionData<TItem> where TItem : class
    {
        public ApexChart<TItem> Chart { get; set; }

        public SelectionXAxis XAxis { get; set; }
        public SelectionYAxis YAxis { get; set; }
    }

    public class SelectionXAxis
    {
        public decimal? Min { get; set; }
        public decimal? Max { get; set; }
    }

    public class SelectionYAxis
    {
        public decimal? Min { get; set; }
        public decimal? Max { get; set; }
    }

}
