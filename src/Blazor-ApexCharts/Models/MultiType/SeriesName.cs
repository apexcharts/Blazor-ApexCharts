using System.Collections.Generic;

namespace ApexCharts
{
    /// <remarks>
    /// Accepts either a single value or collection of values. 
    /// </remarks>
      public class SeriesName : ValueOrList<string>
    {
        /// <summary>
        /// Converts a text collection into a list of strings
        /// </summary>
        public static implicit operator List<string>(SeriesName source) => source.values;

        /// <summary>
        /// Converts a list of strings into a text collection
        /// </summary>
        public static implicit operator SeriesName(List<string> source) => new(source);

        /// <summary>
        /// Converts a string into a text collection
        /// </summary>
        public static implicit operator SeriesName(string source) => new(source);

        /// <summary>
        /// Creates a new collection of texts with the provided values
        /// </summary>
        public SeriesName(params string[] values) : base(values) { }

        /// <summary>
        /// Creates a new collection of texts with the provided values
        /// </summary>
        public SeriesName(IEnumerable<string> values) : base(values) { }
    }
}
