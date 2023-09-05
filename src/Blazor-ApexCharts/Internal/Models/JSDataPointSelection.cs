using System.Collections.Generic;

namespace ApexCharts.Internal
{
    /// <summary>
    /// Return data from JavaScript when a data point event occurs
    /// </summary>
    internal class JSDataPointSelection
    {
        /// <summary>
        /// List of selected DataPoints
        /// </summary>
        public List<List<int?>> SelectedDataPoints { get; set; }

        /// <summary>
        /// The index of the data point being selected
        /// </summary>
        public int DataPointIndex { get; set; }

        /// <summary>
        /// The index of the data series being selected
        /// </summary>
        public int SeriesIndex { get; set; }
    }
}
