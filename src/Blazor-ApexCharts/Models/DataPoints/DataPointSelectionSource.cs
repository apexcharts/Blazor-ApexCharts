namespace ApexCharts
{
    /// <summary>
    /// What caused a data point selection reported through <see cref="SelectedData{TItem}.Source"/>
    /// </summary>
    /// <remarks>
    /// Tells a selection the user made on the chart from one made in code. A legend click does not select:
    /// on pie, donut and polar area charts it hides or shows the slice, as it hides a series on other charts.
    /// </remarks>
    public enum DataPointSelectionSource
    {
        /// <summary>
        /// The user clicked or tapped the data point itself, for example a slice, a bar or a marker
        /// </summary>
        DataPoint,

        /// <summary>
        /// The chart selected a slice from a legend click. apexcharts.js 6.6 and earlier did this on pie, donut and
        /// polar area charts; the bundled 8.x core hides or shows the slice instead, so this value is not reported
        /// unless an older core is loaded through <see cref="ApexChartsBlazorOptions.JavascriptPath"/>.
        /// </summary>
        Legend,

        /// <summary>
        /// The selection was made in code, for example with <see cref="ApexChart{TItem}.ToggleDataPointSelectionAsync(int, int?)"/>
        /// </summary>
        Method
    }
}
