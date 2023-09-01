using System.Collections.Generic;

namespace ApexCharts
{
    /// <remarks>
    /// 
    /// </remarks>
    public class FillPatternStyleSelections : ValueOrList<FillPatternStyle>
    {
        /// <summary>
        /// 
        /// </summary>
        public static implicit operator List<FillPatternStyle>(FillPatternStyleSelections source) => source.values;

        /// <summary>
        /// 
        /// </summary>
        public static implicit operator FillPatternStyleSelections(List<FillPatternStyle> source) => new(source);

        /// <summary>
        /// 
        /// </summary>
        public static implicit operator FillPatternStyleSelections(FillPatternStyle source) => new(source);

        /// <summary>
        /// 
        /// </summary>
        public FillPatternStyleSelections(params FillPatternStyle[] values) : base(values) { }

        /// <summary>
        /// 
        /// </summary>
        public FillPatternStyleSelections(IEnumerable<FillPatternStyle> values) : base(values) { }
    }
}
