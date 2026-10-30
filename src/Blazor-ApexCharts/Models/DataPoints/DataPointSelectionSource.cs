namespace ApexCharts
{
    /// <summary>
    /// What caused a data point selection reported through <see cref="SelectedData{TItem}.Source"/>
    /// </summary>
    /// <remarks>
    /// On pie, donut and polar area charts, clicking a legend item also selects the matching slice, so
    /// <see cref="ApexChart{TItem}.OnDataPointSelection"/> runs for both a slice click and a legend click.
    /// Use this value to tell them apart.
    /// </remarks>
    public enum DataPointSelectionSource
    {
        /// <summary>
        /// The user clicked or tapped the data point itself, for example a slice, a bar or a marker
        /// </summary>
        DataPoint,

        /// <summary>
        /// The user clicked a legend item, or pressed Enter or Space on it, and the chart selected the matching slice.
        /// Only pie, donut and polar area charts select a data point from the legend.
        /// </summary>
        Legend,

        /// <summary>
        /// The selection was made in code, for example with <see cref="ApexChart{TItem}.ToggleDataPointSelectionAsync(int, int?)"/>
        /// </summary>
        Method
    }
}
