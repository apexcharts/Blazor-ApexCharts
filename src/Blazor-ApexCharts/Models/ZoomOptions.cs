namespace ApexCharts
{
    /// <summary>
    /// Class to provide options for <see cref="ApexChart{TItem}.ZoomXAsync(ZoomOptions)"/>
    /// </summary>
    public class ZoomOptions
    {
        /// <summary>
        /// The starting x-axis value. Accepts timestamp or a number
        /// </summary>
        public decimal Start { get; set; }

        /// <summary>
        /// The ending x-axis value. Accepts timestamp or a number
        /// </summary>
        public decimal End { get; set; }
    }
}
