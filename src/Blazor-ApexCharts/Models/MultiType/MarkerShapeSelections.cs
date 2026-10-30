using System.Collections.Generic;

namespace ApexCharts
{
    /// <summary>
    /// One marker shape for every series, or one shape per series.
    /// </summary>
    /// <remarks>
    /// Accepts either a single value or collection of values. A single value applies to every series. A collection assigns shapes by series index: the first shape goes to the first series, the second to the second series, and so on. The collection does not repeat, so a series without an entry gets the default circle.<br /><br />
    ///
    /// Example:<br /><br />
    ///
    /// <code>
    /// // Every series uses squares
    /// options.Markers.Shape = MarkerShape.Square;
    ///
    /// // Series 0 uses diamonds, series 1 crosses, series 2 and later circles
    /// options.Markers.Shape = new MarkerShapeSelections(MarkerShape.Diamond, MarkerShape.Cross);
    /// </code>
    /// </remarks>
    public class MarkerShapeSelections : ValueOrList<MarkerShape>
    {
        /// <summary>
        /// Converts a marker shape collection into a list of marker shapes
        /// </summary>
        public static implicit operator List<MarkerShape>(MarkerShapeSelections source) => source.values;

        /// <summary>
        /// Converts a list of marker shapes, one per series index, into a marker shape collection
        /// </summary>
        public static implicit operator MarkerShapeSelections(List<MarkerShape> source) => new(source);

        /// <summary>
        /// Converts a marker shape into a marker shape collection that applies to every series
        /// </summary>
        public static implicit operator MarkerShapeSelections(MarkerShape source) => new(source);

        /// <summary>
        /// Creates a new collection of marker shapes with the provided values, one per series index
        /// </summary>
        public MarkerShapeSelections(params MarkerShape[] values) : base(values) { }

        /// <summary>
        /// Creates a new collection of marker shapes with the provided values, one per series index
        /// </summary>
        public MarkerShapeSelections(IEnumerable<MarkerShape> values) : base(values) { }
    }
}
