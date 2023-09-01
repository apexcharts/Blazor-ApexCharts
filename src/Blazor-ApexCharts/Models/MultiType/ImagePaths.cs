using System.Collections.Generic;

namespace ApexCharts
{
    /// <remarks>
    /// 
    /// </remarks>
    public class ImagePaths : ValueOrList<string>
    {
        /// <summary>
        /// 
        /// </summary>
        public static implicit operator List<string>(ImagePaths source) => source.values;

        /// <summary>
        /// 
        /// </summary>
        public static implicit operator ImagePaths(List<string> source) => new(source);

        /// <summary>
        /// 
        /// </summary>
        public static implicit operator ImagePaths(string source) => new(source);

        /// <summary>
        /// 
        /// </summary>
        public ImagePaths(params string[] values) : base(values) { }

        /// <summary>
        /// 
        /// </summary>
        public ImagePaths(IEnumerable<string> values) : base(values) { }
    }
}
