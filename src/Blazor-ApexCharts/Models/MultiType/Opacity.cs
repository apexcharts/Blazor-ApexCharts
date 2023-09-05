using System.Collections.Generic;

namespace ApexCharts
{
    /// <remarks>
    /// Accepts either a single value or collection of values. Providing a collection of values in a multi-series chart will assign opacities where each index corresponds to the series index.<br /><br />
    /// 
    /// Values can range from 0 to 1. Example:<br /><br />
    /// 
    /// <code>
    /// // Create options
    /// var options = new ApexChartOptions&lt;object&gt;() 
    /// {
    ///     Fill = new Fill()
    /// };
    /// 
    /// // Single opacity
    /// var opacity = 0.8;
    /// options.Fill.Opacity = opacity;
    /// 
    /// // Multiple opacities
    /// var opacities = new List&lt;double&gt; { ( 1d / 3d ), 0.75, 0.25, 1.0 };
    /// options.Fill.Opacity = opacities;
    /// </code>
    /// </remarks>
    public class Opacity : ValueOrList<double>
    {
        /// <summary>
        /// Converts an opacity collection into a list of doubles
        /// </summary>
        public static implicit operator List<double>(Opacity source) => source.values;

        /// <summary>
        /// Converts a list of doubles into an opacity collection
        /// </summary>
        public static implicit operator Opacity(List<double> source) => new(source);

        /// <summary>
        /// Converts a double into an opacity collection
        /// </summary>
        public static implicit operator Opacity(double source) => new(source);

        /// <summary>
        /// Creates a new collection of opacities with the provided values
        /// </summary>
        public Opacity(params double[] values) : base(values) { }

        /// <summary>
        /// Creates a new collection of opacities with the provided values
        /// </summary>
        public Opacity(IEnumerable<double> values) : base(values) { }
    }
}
