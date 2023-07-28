using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ApexCharts
{
    /// <summary>
    /// Used to create data points for chart types with multiple Y-values
    /// </summary>
    /// <typeparam name="TItem">The data type to be used in the chart to create data points.</typeparam>
    public class ListPoint<TItem> : IDataPoint<TItem>
    {
        /// <inheritdoc cref="IDataPoint{TItem}.FillColor"/>
        public string FillColor { get; set; }

        /// <inheritdoc cref="IDataPoint{TItem}.X"/>
        public object X { get; set; }

        /// <summary>
        /// The Y-values for the data point to create on the chart
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        
        public IEnumerable<decimal?> Y { get; set; }

        /// <inheritdoc cref="IDataPoint{TItem}.Extra"/>
        public object Extra { get; set; }

        /// <inheritdoc cref="IDataPoint{TItem}.Items"/>
        [JsonIgnore]
        public IEnumerable<TItem> Items { get; set; }
    }
}
