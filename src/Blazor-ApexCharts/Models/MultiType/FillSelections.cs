using System.Collections.Generic;

namespace ApexCharts
{
    /// <remarks>
    /// 
    /// </remarks>
    public class FillSelections : ValueOrList<FillType>
    {
        /// <summary>
        /// 
        /// </summary>
        public static implicit operator List<FillType>(FillSelections source) => source.values;

        /// <summary>
        /// 
        /// </summary>
        public static implicit operator FillSelections(List<FillType> source) => new(source);

        /// <summary>
        /// 
        /// </summary>
        public static implicit operator FillSelections(FillType source) => new(source);

        /// <summary>
        /// 
        /// </summary>
        public FillSelections(params FillType[] values) : base(values) { }

        /// <summary>
        /// 
        /// </summary>
        public FillSelections(IEnumerable<FillType> values) : base(values) { }
    }
}
