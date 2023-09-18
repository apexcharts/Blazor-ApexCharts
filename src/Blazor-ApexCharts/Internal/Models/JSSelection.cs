namespace ApexCharts.Internal
{
    /// <summary>
    /// Return data from JavaScript when a selection is made
    /// </summary>
    internal class JSSelection
    {
        /// <inheritdoc cref="SelectionXAxis"/>
        public SelectionXAxis XAxis { get; set; }

        /// <inheritdoc cref="SelectionYAxis"/>
        public SelectionYAxis YAxis { get; set; }
    }
}
