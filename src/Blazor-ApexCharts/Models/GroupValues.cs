namespace ApexCharts
{
    /// <summary>
    /// Class to define properties on how to group data points on a chart
    /// </summary>
    public class GroupPoints
    {
        /// <summary>
        /// Indicates the max number of points to show, excluding the grouped values.
        /// </summary>
        public int? MaxCount { get; set; }

        /// <summary>
        /// Indicates the min number of points to show, excluding the grouped values.
        /// </summary>
        public int? MinCount { get; set; }

        /// <summary>
        /// The percentage value that indicate if a value should be grouped.
        /// </summary>
        public decimal? PercentageThreshold { get; set; }

        /// <summary>
        /// The X-axis label to assign to the group
        /// </summary>
        public string Name { get; set; } = "Other";
    }
}
