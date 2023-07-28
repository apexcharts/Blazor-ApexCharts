namespace ApexCharts
{
    /// <summary>
    /// Class to define the <see cref="ApexChartOptions{TItem}.Stroke"/> values for a single series
    /// </summary>
    public class SeriesStroke
    {
        /// <summary>
        /// The color value to add to <see cref="Stroke.Colors"/> for the series
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// The dash value to add to <see cref="Stroke.DashArray"/> for the series
        /// </summary>
        public int DashSpace { get; set; }

        /// <summary>
        /// The width value to add to <see cref="Stroke.Width"/> for the series
        /// </summary>
        public int Width { get; set; }
    }
}
