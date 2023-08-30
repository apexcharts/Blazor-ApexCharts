using System.Collections.Generic;

namespace ApexCharts.Internal
{
    /// <summary>
    /// Return data from JavaScript when a zoom action is performed
    /// </summary>
    internal class JSZoomed
    {
        /// <inheritdoc cref="SelectionXAxis"/>
        public SelectionXAxis XAxis { get; set; }

        /// <summary>
        /// Y axis object
        /// </summary>
        public List<object> YAxis { get; set; }
    }
}
