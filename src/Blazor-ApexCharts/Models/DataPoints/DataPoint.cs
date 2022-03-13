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
        
}
