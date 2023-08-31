using System.Collections.Generic;

namespace ApexCharts
{
    /// <remarks>
    /// 
    /// </remarks>
    public class CurveSelections : ValueOrList<Curve>
    {
        /// <summary>
        /// 
        /// </summary>
        public static implicit operator List<Curve>(CurveSelections source) => source.values;

        /// <summary>
        /// 
        /// </summary>
        public static implicit operator CurveSelections(List<Curve> source) => new(source);

        /// <summary>
        /// 
        /// </summary>
        public static implicit operator CurveSelections(Curve source) => new(source);

        /// <summary>
        /// 
        /// </summary>
        public CurveSelections(params Curve[] values) : base(values) { }

        /// <summary>
        /// 
        /// </summary>
        public CurveSelections(IEnumerable<Curve> values) : base(values) { }
    }
}
