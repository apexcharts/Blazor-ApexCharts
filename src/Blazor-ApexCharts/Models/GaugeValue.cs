using System;

namespace ApexCharts
{
    /// <summary>
    /// Class to define properties required for creating a gauge chart
    /// </summary>
    public class GaugeValue
    {
        /// <summary>
        /// The text name for the data series in the chart
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// The value to assign to the radial bar chart
        /// </summary>
        public decimal Percentage { get; set; }

#pragma warning disable CS1591 // Documentation not available for obsolete properties
        [Obsolete("This property is obsolete. Use Percentage instead.", false)]
        public decimal Precentage { get => Percentage; set => Percentage = value; }
#pragma warning restore CS1591
    }
}
