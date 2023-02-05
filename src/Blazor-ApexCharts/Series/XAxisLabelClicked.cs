using System.Collections.Generic;

namespace ApexCharts
{
    public class XAxisLabelClicked<TItem> where TItem : class
    {
        public int LabelIndex { get; set; }
        public string Caption { get; set; }
        public List<SeriesDataPoint<TItem>> SeriesPoints { get; set; }

    }

    public class SeriesDataPoint<TItem> where TItem : class
    {
        public Series<TItem> Series { get; set; }
        public IDataPoint<TItem> DataPoint { get; set; }
  
    }

}