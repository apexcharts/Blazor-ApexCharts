using System.Collections.Generic;

namespace ApexCharts
{
    /// <remarks>
    /// 
    /// </remarks>
    public class CustomFunction : ValueOrList<string>
    {
        /// <summary>
        /// 
        /// </summary>
        public static implicit operator List<string>(CustomFunction source) => source.values;

        /// <summary>
        /// 
        /// </summary>
        public static implicit operator CustomFunction(List<string> source) => new(source);

        /// <summary>
        /// 
        /// </summary>
        public static implicit operator CustomFunction(string source) => new(source);

        /// <summary>
        /// 
        /// </summary>
        public CustomFunction(params string[] values) : base(values) { }

        /// <summary>
        /// 
        /// </summary>
        public CustomFunction(IEnumerable<string> values) : base(values) { }
    }
}
