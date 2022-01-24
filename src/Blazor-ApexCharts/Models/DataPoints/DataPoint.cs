using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace ApexCharts
{
  
    public class DataPoint<TItem> : IDataPoint<TItem>
    {
        public object X { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public decimal? Y { get; set; }


        [JsonIgnore]
        public IEnumerable<TItem> Items { get; set; }

    }

    public class DataPointComparer<TItem> : IEqualityComparer<IDataPoint<TItem>>
    {
        public bool Equals(IDataPoint<TItem> dataPoint1, IDataPoint<TItem> dataPoint2)
        {
            if (dataPoint1 == null && dataPoint2 == null) { return true; }
            if (dataPoint1 == null || dataPoint2 == null) { return false; }

            IDataPoint<TItem> point1 = dataPoint1;
            IDataPoint<TItem> point2 = dataPoint2;

            //if (point1.X.ToString() != point2.X.ToString() || point1.YObject != point2.YObject) return false;
            if (point1.X.ToString() != point2.X.ToString()) return false;


            return true;
        }

      

        public int GetHashCode([DisallowNull] IDataPoint<TItem> obj)
        {
            return obj.X.GetHashCode();
        }
    }
}
