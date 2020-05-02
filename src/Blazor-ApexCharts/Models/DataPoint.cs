using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ApexCharts
{

    public interface IDataPoint {
        IList<object> Items { get; set; }
    }

    public class DataPoint: IDataPoint
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
        public object Y{ get; set; }
        

        [JsonIgnore]
        public IList<object> Items { get; set; }

    }
}
