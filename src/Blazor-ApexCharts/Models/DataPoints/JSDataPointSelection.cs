using System.Collections.Generic;

namespace ApexCharts
{
    public class JSDataPointSelection
    {
        public List<List<int?>> SelectedDataPoints { get; set; }
        public int DataPointIndex { get; set; }
        public int SeriesIndex { get; set; }

    }


    public class SelectedData<TItem> where TItem:class
    {
        public Series<TItem> Series { get; set; }
        public IDataPoint<TItem> DataPoint { get; set; }
        public bool IsSelected { get; internal set; }

    }

}
