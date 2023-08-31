using System.Collections.Generic;

namespace ApexCharts
{
    /// <remarks>
    /// 
    /// </remarks>
    public class Opacity : ValueOrList<double>
    {
        /// <summary>
        /// 
        /// </summary>
        public static implicit operator List<double>(Opacity source) => source.values;

        /// <summary>
        /// 
        /// </summary>
        public static implicit operator Opacity(List<double> source) => new(source);

        /// <summary>
        /// 
        /// </summary>
        public static implicit operator Opacity(double source) => new(source);

        /// <summary>
        /// 
        /// </summary>
        public Opacity(params double[] values) : base(values) { }

        /// <summary>
        /// 
        /// </summary>
        public Opacity(IEnumerable<double> values) : base(values) { }
    }
}
