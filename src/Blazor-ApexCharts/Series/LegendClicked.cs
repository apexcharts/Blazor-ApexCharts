namespace ApexCharts
{
    /// <summary>
    /// Return data sent when <see cref="ApexChart{TItem}.OnLegendClicked"/> is invoked
    /// </summary>
    /// <typeparam name="TItem">The data type of the items to display in the chart</typeparam>
    public class LegendClicked<TItem> where TItem : class
    {
        /// <summary>
        /// The data series associated with the legend item that was clicked
        /// </summary>
        public Series<TItem> Series { get; set; }

        /// <summary>
        /// Specifies whether the series associated with the legend item is collapsed
        /// </summary>
        public bool Collapsed { get; set; }

        /// <summary>
        /// The clicked datapoint, Only valid for no axis charts
        /// </summary>
        public IDataPoint<TItem> DataPoint { get; set; }

    }
}
