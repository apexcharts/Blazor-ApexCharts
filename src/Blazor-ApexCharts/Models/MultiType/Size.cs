using System.Collections.Generic;
using System.Linq;

namespace ApexCharts
{
    /// <remarks>
    /// 
    /// </remarks>
    public class Size : ValueOrList<double>
    {
        /// <summary>
        /// 
        /// </summary>
        public static implicit operator List<double>(Size source) => source.values;

        /// <summary>
        /// 
        /// </summary>
        public static implicit operator Size(List<double> source) => new(source);

        /// <summary>
        /// 
        /// </summary>
        public static implicit operator Size(List<int> source) 
        {
            if (source == null)
                return new Size();
            else 
                return new Size(source.Cast<double>());
        }

        /// <summary>
        /// 
        /// </summary>
        public static implicit operator Size(double source) => new(source);

        /// <summary>
        /// 
        /// </summary>
        public Size(params double[] values) : base(values) { }

        /// <summary>
        /// 
        /// </summary>
        public Size(IEnumerable<double> values) : base(values) { }
    }
}
