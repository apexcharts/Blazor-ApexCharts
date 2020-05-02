using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ApexCharts
{

    public class BubblePoint : IDataPoint
    {
     

        public object X { get; set; }
        public object Y{ get; set; }
        public object Z { get; set; } 

        [JsonIgnore]
        public IList<object> Items { get; set; }

    }
}
