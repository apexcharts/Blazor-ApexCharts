using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApexCharts
{
    public class DataPointSelection
    {
        public List<object> SelectedDataPoints { get; set; }
        public int DataPointIndex { get; set; }
        public int SeriesIndex { get; set; }

        public DataPoint DataPoint { get; set; }
        public object Label { get; set; }
    }


    public class SelectedData<TItem>
    {
        public Series Series { get; set; }

        public IDataPoint DataPoint { get; set; }
        public List<TItem> Items { get; set; }
    }

}
