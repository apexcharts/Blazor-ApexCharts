using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ApexCharts
{

    public class DataPoint<TItem> : IDataPoint<TItem>
    {
        public string FillColor { get; set; }
        public object X { get; set; }
        
        public List<DataPointGoal> Goals { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public decimal? Y { get; set; }

        [JsonIgnore]
        public IEnumerable<TItem> Items { get; set; }

        public object Extra { get; set; }
    }

    public class DataPointGoal
    {
        public string Name { get; set; }
        public decimal  Value { get; set; }
        public int? StrokeWidth { get; set; }
        public int? StrokeHeight { get; set; }
        public int? StrokeDashArray { get; set; }
        public string StrokeColor { get; set; }
        public StrokeLineCap? StrokeLineCap { get; set; }
    }

    public enum StrokeLineCap
    {
        Butt,
        Round,
        Square
    }
}
