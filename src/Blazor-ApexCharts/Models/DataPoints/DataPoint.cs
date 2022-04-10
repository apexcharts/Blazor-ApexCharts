using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace ApexCharts
{
  
    public class DataPoint<TItem> : IDataPoint<TItem>
    {
        public string FillColor { get; set; }
        public object X { get; set; }
        
        public List<Goal> Goals { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public decimal? Y { get; set; }

        [JsonIgnore]
        public IEnumerable<TItem> Items { get; set; }

    }

    public class Goal
    {
        public string Name { get; set; }
        public decimal  Value { get; set; }
        public int? StrokeWidth { get; set; }
        public int? StrokeHeight { get; set; }
        public int? StrokeDashArray { get; set; }
        public string StrokeColor { get; set; }
        public string StrokeLineCap { get; set; }


        
    }

    //goals: [
    //                {

    //                 name: 'Expected',
    //                 value: 14,
    //                 strokeWidth: 2,
    //                 strokeDashArray: 2,
    //                 strokeColor: '#775DD0'
    //                }
    //              ]

}
