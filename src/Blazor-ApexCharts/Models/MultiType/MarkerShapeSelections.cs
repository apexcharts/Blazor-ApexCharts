using System.Collections.Generic;

namespace ApexCharts
{
    /// <remarks>
    /// Accepts either a single value or collection of values. Providing a collection of values in a multi-series chart will assign fill types where each index corresponds to the series index.<br /><br />
    /// </remarks>
    public class MarkerShapeSelections : ValueOrList<MarkerShape>
    {
        /// <summary>
        /// Converts a fill type collection into a list of fill types
        /// </summary>
        public static implicit operator List<MarkerShape>(MarkerShapeSelections source) => source.values;

        /// <summary>
        /// Converts a list of fill types into a fill type collection
        /// </summary>
        public static implicit operator MarkerShapeSelections(List<MarkerShape> source) => new(source);

        /// <summary>
        /// Converts a fill type into a fill type collection
        /// </summary>
        public static implicit operator MarkerShapeSelections(MarkerShape source) => new(source);

        /// <summary>
        /// Creates a new collection of fill types with the provided values
        /// </summary>
        public MarkerShapeSelections(params MarkerShape[] values) : base(values) { }

        /// <summary>
        /// Creates a new collection of fill types with the provided values
        /// </summary>
        public MarkerShapeSelections(IEnumerable<MarkerShape> values) : base(values) { }
    }
}
