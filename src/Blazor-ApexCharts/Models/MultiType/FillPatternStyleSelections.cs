using System.Collections.Generic;

namespace ApexCharts
{
    /// <remarks>
    /// Accepts either a single value or collection of values. Providing a collection of values in a multi-series chart will assign fill pattern styles where each index corresponds to the series index.<br /><br />
    /// 
    /// Example:<br /><br />
    /// 
    /// <code>
    /// // Create options
    /// var options = new ApexChartOptions&lt;object&gt;() 
    /// {
    ///     Fill = new Fill
    ///     {
    ///         Pattern = new FillPattern()
    ///     }
    /// };
    /// 
    /// // Single fill pattern style
    /// var style = FillPatternStyle.Circles;
    /// options.Fill.Pattern.Style = style;
    /// 
    /// // Multiple fill pattern styles
    /// var styles = new List&lt;FillPatternStyle&gt; { FillPatternStyle.Circles, FillPatternStyle.Squares };
    /// options.Fill.Pattern.Style = styles;
    /// </code>
    /// </remarks>
    public class FillPatternStyleSelections : ValueOrList<FillPatternStyle>
    {
        /// <summary>
        /// Converts a fill pattern style collection into a list of fill pattern styles
        /// </summary>
        public static implicit operator List<FillPatternStyle>(FillPatternStyleSelections source) => source.values;

        /// <summary>
        /// Converts a list of fill pattern styles into a fill pattern style collection
        /// </summary>
        public static implicit operator FillPatternStyleSelections(List<FillPatternStyle> source) => new(source);

        /// <summary>
        /// Converts a fill pattern style into a fill pattern style collection
        /// </summary>
        public static implicit operator FillPatternStyleSelections(FillPatternStyle source) => new(source);

        /// <summary>
        /// Creates a new collection of fill pattern styles with the provided values
        /// </summary>
        public FillPatternStyleSelections(params FillPatternStyle[] values) : base(values) { }

        /// <summary>
        /// Creates a new collection of fill pattern styles with the provided values
        /// </summary>
        public FillPatternStyleSelections(IEnumerable<FillPatternStyle> values) : base(values) { }
    }
}
