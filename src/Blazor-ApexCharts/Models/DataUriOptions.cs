namespace ApexCharts
{
    /// <summary>
    /// Class to provide options for <see cref="ApexChart{TItem}.GetDataUriAsync(DataUriOptions)"/>
    /// </summary>
    public class DataUriOptions
    {
        /// <summary>
        /// The width to assign to the resulting PDF
        /// </summary>
        public double? Width { get; set; }

        /// <summary>
        /// The amount to scale the resulting PDF
        /// </summary>
        public double? Scale { get; set; }
    }
}
