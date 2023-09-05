using System.Collections.Generic;

namespace ApexCharts
{
    /// <remarks>
    /// Accepts either a single value or collection of values. Providing a collection of values in a multi-series chart will assign colors where each index corresponds to the series index.<br /><br />
    /// 
    /// Values can be provided in either <see href="https://developer.mozilla.org/en-US/docs/Web/CSS/named-color">named color</see> or hexadecimal RGB formats. Example:<br /><br />
    /// 
    /// <code>
    /// // Create options
    /// var options = new ApexChartOptions&lt;object&gt;() 
    /// {
    ///     Xaxis = new XAxis 
    ///     {
    ///         Labels = new XAxisLabels 
    ///         {
    ///             Style = new AxisLabelStyle()
    ///         }
    ///     }
    /// };
    /// 
    /// // Single color
    /// var color = "red";
    /// options.Xaxis.Labels.Style.Colors = color;
    /// 
    /// // Multiple colors
    /// var colors = new List&lt;string&gt; { "#FF0000", "#00FF00", "#0000FF", "#0F0F0F" };
    /// options.Xaxis.Labels.Style.Colors = colors;
    /// </code>
    /// </remarks>
    public class Color : ValueOrList<string>
    {
        /// <summary>
        /// Converts a color collection into a list of strings
        /// </summary>
        public static implicit operator List<string>(Color source) => source.values;

        /// <summary>
        /// Converts a list of strings into a color collection
        /// </summary>
        public static implicit operator Color(List<string> source) => new(source);

        /// <summary>
        /// Converts a string into a color collection
        /// </summary>
        public static implicit operator Color(string source) => new(source);

        /// <summary>
        /// Creates a new collection of colors with the provided values
        /// </summary>
        public Color(params string[] values) : base(values) { }

        /// <summary>
        /// Creates a new collection of colors with the provided values
        /// </summary>
        public Color(IEnumerable<string> values) : base(values) { }
    }
}
