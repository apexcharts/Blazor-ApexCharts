using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ApexCharts
{
  
    public class DataPoint<TItem> : IDataPoint<TItem>
    {
        public object X { get; set; }
        public decimal? Y { get; set; }


        [JsonIgnore]
        public IList<TItem> Items { get; set; }

    }

    public class DataPointComparer<TItem> : IEqualityComparer<DataPoint<TItem>>
    {
        public bool Equals(DataPoint<TItem> dataPoint1, DataPoint<TItem> dataPoint2)
        {
            if (dataPoint1 == null && dataPoint2 == null) { return true; }
            if (dataPoint1 == null || dataPoint2 == null) { return false; }

            DataPoint<TItem> point1 = dataPoint1;
            DataPoint<TItem> point2 = dataPoint2;

            if (point1.X.ToString() != point2.X.ToString() || point1.Y != point2.Y) return false;

            return true;
        }

        public int GetHashCode(DataPoint<TItem> obj)
        {
            return obj.X.GetHashCode();
        }
    }
}
