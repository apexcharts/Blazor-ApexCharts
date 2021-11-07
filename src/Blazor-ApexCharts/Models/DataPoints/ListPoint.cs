using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ApexCharts
{
    public class ListPoint<TItem> : IDataPoint<TItem>
    {
        public object X { get; set; }
        public IEnumerable<decimal> Y { get; set; }
      
       
        [JsonIgnore]
        public IEnumerable<TItem> Items { get; set; }
    }
}
