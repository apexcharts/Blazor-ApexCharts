using System.Collections.Generic;

namespace ApexCharts
{
    /// <remarks>
    /// 
    /// </remarks>
    public class Color : ValueOrList<string>
    {
        /// <summary>
        /// 
        /// </summary>
        public static implicit operator List<string>(Color source) => source.values;

        /// <summary>
        /// 
        /// </summary>
        public static implicit operator Color(List<string> source) => new(source);

        /// <summary>
        /// 
        /// </summary>
        public static implicit operator Color(string source) => new(source);

        /// <summary>
        /// 
        /// </summary>
        public Color(params string[] values) : base(values) { }

        /// <summary>
        /// 
        /// </summary>
        public Color(IEnumerable<string> values) : base(values) { }
    }
}
