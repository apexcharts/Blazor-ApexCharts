using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ApexCharts
{
    public class BubblePoint<TItem> : IDataPoint<TItem>
    {
        public object X { get; set; }
        public decimal Y{ get; set; }
        public decimal Z { get; set; }

        [JsonIgnore]
        public object YObject => Y;

        [JsonIgnore]
        public IEnumerable<TItem> Items { get; set; }

        public bool Equals(IDataPoint<TItem> x, IDataPoint<TItem> y)
        {
            throw new NotImplementedException();
        }

        public int GetHashCode(IDataPoint<TItem> obj)
        {
            throw new NotImplementedException();
        }
    }
}
