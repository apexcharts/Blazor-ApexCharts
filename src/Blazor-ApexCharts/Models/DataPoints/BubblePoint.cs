using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ApexCharts
{
    public class BubblePoint<TItem> : IDataPoint<TItem>
    {
        public string FillColor { get; set; }

        public object X { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public decimal? Y{ get; set; }
       
        public decimal Z { get; set; }
      
        public object Extra { get; set; }

        [JsonIgnore]
        public IEnumerable<TItem> Items { get; set; }
    
    }
}
