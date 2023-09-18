using System.Collections.Generic;

namespace ApexCharts
{
    /// <summary>
    /// Return data sent when <see cref="ApexChart{TItem}.OnXAxisLabelClick"/> is invoked
    /// </summary>
    /// <typeparam name="TItem">The data type of the items to display in the chart</typeparam>
    public class XAxisLabelClicked<TItem> where TItem : class
    {
        /// <summary>
        /// The index of the X-axis label that was clicked
        /// </summary>
        public int LabelIndex { get; set; }

        /// <summary>
        /// The display text of the X-axis label that was clicked
        /// </summary>
        public string Caption { get; set; }

        /// <summary>
        /// The collection of data points associated with the X-axis label that was clicked
        /// </summary>
        public List<SeriesDataPoint<TItem>> SeriesPoints { get; set; }
    }

    /// <typeparam name="TItem">The data type of the items to display in the chart</typeparam>
    public class SeriesDataPoint<TItem> where TItem : class
    {
        /// <summary>
        /// The data series the <see cref="DataPoint"/> belongs to
        /// </summary>
        public Series<TItem> Series { get; set; }

        /// <summary>
        /// A data point within belonging to the X-axis label that was clicked
        /// </summary>
        public IDataPoint<TItem> DataPoint { get; set; }
    }
}
