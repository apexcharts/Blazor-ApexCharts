using System.Collections.Generic;

namespace ApexCharts
{
    /// <remarks>
    /// Accepts either a single value or collection of values. Providing a collection of values in a multi-series chart will assign fill types where each index corresponds to the series index.<br /><br />
    /// 
    /// Example:<br /><br />
    /// 
    /// <code>
    /// // Create options
    /// var options = new ApexChartOptions&lt;object&gt;() 
    /// {
    ///     Fill = new Fill()
    /// };
    /// 
    /// // Single fill type
    /// var fillType = FillType.Solid;
    /// options.Fill.Type = fillType;
    /// 
    /// // Multiple fill types
    /// var fillTypes = new List&lt;FillType&gt; { FillType.Gradient, FillType.Pattern };
    /// options.Fill.Type = fillTypes;
    /// </code>
    /// </remarks>
    public class FillTypeSelections : ValueOrList<FillType>
    {
        /// <summary>
        /// Converts a fill type collection into a list of fill types
        /// </summary>
        public static implicit operator List<FillType>(FillTypeSelections source) => source.values;

        /// <summary>
        /// Converts a list of fill types into a fill type collection
        /// </summary>
        public static implicit operator FillTypeSelections(List<FillType> source) => new(source);

        /// <summary>
        /// Converts a fill type into a fill type collection
        /// </summary>
        public static implicit operator FillTypeSelections(FillType source) => new(source);

        /// <summary>
        /// Creates a new collection of fill types with the provided values
        /// </summary>
        public FillTypeSelections(params FillType[] values) : base(values) { }

        /// <summary>
        /// Creates a new collection of fill types with the provided values
        /// </summary>
        public FillTypeSelections(IEnumerable<FillType> values) : base(values) { }
    }
}
