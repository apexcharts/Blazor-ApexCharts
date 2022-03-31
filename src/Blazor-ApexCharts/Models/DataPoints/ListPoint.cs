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
        public string FillColor { get; set; }
        public object X { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public IEnumerable<decimal?> Y { get; set; }
      
       
        [JsonIgnore]
        public IEnumerable<TItem> Items { get; set; }
    }
}
