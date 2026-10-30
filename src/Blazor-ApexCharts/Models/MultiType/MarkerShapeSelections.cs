using System.Collections.Generic;

namespace ApexCharts
{
    /// <summary>
    /// One marker shape for every series, or one shape per series.
    /// </summary>
    /// <remarks>
    /// Accepts either a single value or collection of values. A single value applies to every series, and so does a collection with one entry: like every other option that takes one value or a list, it is sent to the chart as that single value. A collection with two or more entries assigns shapes by series index: the first shape goes to the first series, the second to the second series, and so on. The collection does not repeat, so a series past its last entry gets the default circle. To give the first series a shape and leave the others as circles, list two entries, such as <c>[MarkerShape.Square, MarkerShape.Circle]</c>.<br /><br />
    ///
    /// Example:<br /><br />
    ///
    /// <code>
    /// // Every series uses squares
    /// options.Markers.Shape = MarkerShape.Square;
    ///
    /// // Series 0 uses diamonds, series 1 crosses, series 2 and later circles
    /// options.Markers.Shape = new MarkerShapeSelections(MarkerShape.Diamond, MarkerShape.Cross);
    ///
    /// // Only series 0 uses squares: a one-entry list would apply to every series
    /// options.Markers.Shape = new MarkerShapeSelections(MarkerShape.Square, MarkerShape.Circle);
    /// </code>
    /// </remarks>
    public class MarkerShapeSelections : ValueOrList<MarkerShape>
    {
        /// <summary>
        /// Converts a marker shape collection into a list of marker shapes
        /// </summary>
        public static implicit operator List<MarkerShape>(MarkerShapeSelections source) => source.values;

        /// <summary>
        /// Converts a list of marker shapes into a marker shape collection. Two or more shapes are assigned by series index, a single shape applies to every series
        /// </summary>
        public static implicit operator MarkerShapeSelections(List<MarkerShape> source) => new(source);

        /// <summary>
        /// Converts a marker shape into a marker shape collection that applies to every series
        /// </summary>
        public static implicit operator MarkerShapeSelections(MarkerShape source) => new(source);

        /// <summary>
        /// Creates a new collection of marker shapes with the provided values. Two or more shapes are assigned by series index, a single shape applies to every series
        /// </summary>
        public MarkerShapeSelections(params MarkerShape[] values) : base(values) { }

        /// <summary>
        /// Creates a new collection of marker shapes with the provided values. Two or more shapes are assigned by series index, a single shape applies to every series
        /// </summary>
        public MarkerShapeSelections(IEnumerable<MarkerShape> values) : base(values) { }
    }
}
