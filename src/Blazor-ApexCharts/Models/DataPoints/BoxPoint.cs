using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ApexCharts
{
    public class BoxPoint<TItem> : IDataPoint<TItem>
    {
        public object X { get; set; }
        public List<decimal> Y { get; set; }
      
        [JsonIgnore]
        public object YObject => Y;

        [JsonIgnore]
        public IList<TItem> Items { get; set; }
    }
}
