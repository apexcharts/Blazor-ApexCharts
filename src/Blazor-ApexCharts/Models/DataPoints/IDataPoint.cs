using System.Collections.Generic;

namespace ApexCharts
{
    /// <summary>
    /// Interface to define shared requirements for all data point types
    /// </summary>
    /// <typeparam name="TItem">The data type to be used in the chart to create data points.</typeparam>
    public interface IDataPoint<TItem>
    {
        /// <summary>
        /// Collection that contains the Y-values to create the data point on the chart
        /// </summary>
        IEnumerable<TItem> Items { get; set; }

        /// <summary>
        /// The X-value for the data point to create on the chart
        /// </summary>
        object X { get; set; }

        /// <summary>
        /// The color to assign to the data point
        /// </summary>
        string FillColor { get; set; }

        /// <summary>
        /// Additional information to serialize with the data point
        /// </summary>
        object Extra { get; set; }
    }
}
