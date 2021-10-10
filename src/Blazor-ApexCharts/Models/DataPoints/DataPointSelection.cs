using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApexCharts
{
    public class DataPointSelection<TItem>
    {
        public List<object> SelectedDataPoints { get; set; }
        public int DataPointIndex { get; set; }
        public int SeriesIndex { get; set; }

        public IDataPoint<TItem> DataPoint { get; set; }
        public object Label { get; set; }
    }


    public class SelectedData<TItem>
    {
        public Series<TItem> Series { get; set; }
        public IDataPoint<TItem> DataPoint { get; set; }

    }

}
