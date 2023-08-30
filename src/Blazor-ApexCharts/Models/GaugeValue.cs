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
    }
}
