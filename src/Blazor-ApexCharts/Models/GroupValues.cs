
namespace ApexCharts
{
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
        /// The precentage value that indicate if a value should be grouped.
        /// </summary>
        public decimal? PercentageThreshold { get; set; }

        public string Name { get; set; } = "Other";
       
    }

  

}
