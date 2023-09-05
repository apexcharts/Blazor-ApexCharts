using System.Collections.Generic;

namespace ApexCharts
{
    /// <remarks>
    /// Accepts either a single value or collection of values. Providing a collection of values in a multi-series chart will assign image paths where each index corresponds to the series index.<br /><br />
    /// 
    /// Example:<br /><br />
    /// 
    /// <code>
    /// // Create options
    /// var options = new ApexChartOptions&lt;object&gt;() 
    /// {
    ///     Fill = new Fill
    ///     {
    ///         Image = new FillImage()
    ///     }
    /// };
    /// 
    /// // Single image
    /// var src = "https://raw.githubusercontent.com/apexcharts/Blazor-ApexCharts/master/BasicPieChart.png";
    /// options.Fill.Image.Src = src;
    /// 
    /// // Multiple images
    /// var srcs = new List&lt;string&gt; { "../assets/gradient-1.png", "../assets/happy-face.png" };
    /// options.Fill.Image.Src = srcs;
    /// </code>
    /// </remarks>
    public class ImagePaths : ValueOrList<string>
    {
        /// <summary>
        /// Converts an image path collection into a list of strings
        /// </summary>
        public static implicit operator List<string>(ImagePaths source) => source.values;

        /// <summary>
        /// Converts a list of strings into an image path collection
        /// </summary>
        public static implicit operator ImagePaths(List<string> source) => new(source);

        /// <summary>
        /// Converts a string into an image path collection
        /// </summary>
        public static implicit operator ImagePaths(string source) => new(source);

        /// <summary>
        /// Creates a new collection of image paths with the provided values
        /// </summary>
        public ImagePaths(params string[] values) : base(values) { }

        /// <summary>
        /// Creates a new collection of image paths with the provided values
        /// </summary>
        public ImagePaths(IEnumerable<string> values) : base(values) { }
    }
}
