using System.Collections.Generic;
using System.Text.Json.Serialization;
using ApexCharts.Internal;

namespace ApexCharts
{
    /// <remarks>
    /// Accepts either a single value or collection of values. Providing a collection of values in a multi-series chart will assign custom functions where each index corresponds to the series index.<br /><br />
    /// 
    /// Example:<br /><br />
    /// 
    /// <code>
    /// // Create options
    /// var options = new ApexChartOptions&lt;object&gt;() 
    /// {
    ///     Tooltip = new Tooltip()
    /// };
    /// 
    /// // Single function
    /// var customFunction = "function({series, seriesIndex, dataPointIndex, w}) { return '&lt;div&gt;' + series[seriesIndex][dataPointIndex] + '&lt;/div&gt;' }";
    /// 
    /// options.Tooltip.Custom = customFunction;
    /// 
    /// // Multiple functions
    /// var customFunctions = new List&lt;string&gt; 
    /// {
    ///     "function({series, seriesIndex, dataPointIndex, w}) { return series[seriesIndex][dataPointIndex] }", 
    ///     "function({series, seriesIndex, dataPointIndex, w}) { return 'Invalid data' }" 
    /// };
    /// 
    /// options.Tooltip.Custom = customFunctions;
    /// </code>
    /// </remarks>
    public class CustomFunction : ValueOrList<string>
    {
        /// <summary>
        /// Converts a custom function collection into a list of strings
        /// </summary>
        public static implicit operator List<string>(CustomFunction source) => source.values;

        /// <summary>
        /// Converts a list of strings into a custom function collection
        /// </summary>
        public static implicit operator CustomFunction(List<string> source) => new(source);

        /// <summary>
        /// Converts a string into a custom function collection
        /// </summary>
        public static implicit operator CustomFunction(string source) => new(source);

        /// <summary>
        /// Creates a new collection of custom functions with the provided values
        /// </summary>
        public CustomFunction(params string[] values) : base(values) { }

        /// <summary>
        /// Creates a new collection of custom functions with the provided values
        /// </summary>
        public CustomFunction(IEnumerable<string> values) : base(values) { }
    }
}
