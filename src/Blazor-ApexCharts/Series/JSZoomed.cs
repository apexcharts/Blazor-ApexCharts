using System.Collections.Generic;

namespace ApexCharts.Series
{
    /// <summary>
    /// Return data from JavaScript when a zoom action is performed
    /// </summary>
    public class JSZoomed
    {
        /// <inheritdoc cref="SelectionXAxis"/>
        public SelectionXAxis XAxis { get; set; }

        public List<object> YAxis { get; set; }
    }
}
