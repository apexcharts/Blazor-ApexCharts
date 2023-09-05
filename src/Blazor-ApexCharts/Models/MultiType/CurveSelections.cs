using System.Collections.Generic;

namespace ApexCharts
{
    /// <remarks>
    /// Accepts either a single value or collection of values. Providing a collection of values in a multi-series chart will assign curves where each index corresponds to the series index.<br /><br />
    /// 
    /// Example:<br /><br />
    /// 
    /// <code>
    /// // Create options
    /// var options = new ApexChartOptions&lt;object&gt;() 
    /// {
    ///     Stroke = new Stroke()
    /// };
    /// 
    /// // Single curve
    /// var curve = Curve.Smooth;
    /// options.Stroke.Curve = curve;
    /// 
    /// // Multiple curves
    /// var curves = new List&lt;Curve&gt; { Curve.Stepline, Curve.Straight };
    /// options.Stroke.Curve = curves;
    /// </code>
    /// </remarks>
    public class CurveSelections : ValueOrList<Curve>
    {
        /// <summary>
        /// Converts a curve collection into a list of curves
        /// </summary>
        public static implicit operator List<Curve>(CurveSelections source) => source.values;

        /// <summary>
        /// Converts a list of curves into a curve collection
        /// </summary>
        public static implicit operator CurveSelections(List<Curve> source) => new(source);

        /// <summary>
        /// Converts a curve into a curve collection
        /// </summary>
        public static implicit operator CurveSelections(Curve source) => new(source);

        /// <summary>
        /// Creates a new collection of curves with the provided values
        /// </summary>
        public CurveSelections(params Curve[] values) : base(values) { }

        /// <summary>
        /// Creates a new collection of curves with the provided values
        /// </summary>
        public CurveSelections(IEnumerable<Curve> values) : base(values) { }
    }
}
