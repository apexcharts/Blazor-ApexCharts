namespace ApexCharts.Internal
{
    /// <summary>
    /// Return data from JavaScript when a legend item is clicked
    /// </summary>
    internal class JSLegendClicked
    {
        /// <summary>
        /// The index of the data series associated with the legend item that was clicked
        /// </summary>
        public int SeriesIndex { get; set; }

        /// <summary>
        /// Specifies whether the series associated with the legend item is collapsed
        /// </summary>
        public bool Collapsed { get; set; }
    }
}
