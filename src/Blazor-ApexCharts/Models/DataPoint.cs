using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ApexCharts
{
    public interface IDataPoint<TItem>: IEqualityComparer<IDataPoint<TItem>> {
        IList<TItem> Items { get; set; }
        object X { get; set; }
    }

    public class DataPoint<TItem>: IDataPoint<TItem>
    {
        public DataPoint() { }

        public DataPoint(object x)
        {
            X = x;
        }

        public DataPoint(object x, decimal? y)
        {
            X = x;
            Y = y;
        }

        public object X { get; set; }
        public decimal? Y{ get; set; }
        

        [JsonIgnore]
        public IList<TItem> Items { get; set; }

        public bool Equals(IDataPoint<TItem> x, IDataPoint<TItem> y)
        {
            throw new System.NotImplementedException();
        }

        public int GetHashCode(IDataPoint<TItem> obj)
        {
            throw new System.NotImplementedException();
        }
    }
}
