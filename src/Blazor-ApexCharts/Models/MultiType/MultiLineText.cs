using System.Collections.Generic;

namespace ApexCharts
{
    /// <remarks>
    /// Accepts either a single value or collection of values. 
    /// By providing a collection in the case of chart annotations or titles, the supplied strings will be printed as separate lines, thus allowing for multiline annotation or titles handling.<br /><br />
    /// 
    /// Example:<br /><br />
    /// 
    /// <code>
    /// // Single text
    /// options.Annotations[0].Xaxis.Label.Text = "TextAnnotations";
    /// 
    /// // Multiple texts
    /// Annotations = new Annotations {
    ///             Xaxis = new List&lt;AnnotationsXAxis&gt; { 
    ///                 new AnnotationsXAxis {
    ///                     Label = new Label {
    ///                         Text = new MultiLineText("Annotation Text")
    ///                     }
    ///                 }
    ///             }
    /// </code>
    /// </remarks>
    public class MultiLineText : ValueOrList<string>
    {
        /// <summary>
        /// Converts a text collection into a list of strings
        /// </summary>
        public static implicit operator List<string>(MultiLineText source) => source.values;

        /// <summary>
        /// Converts a list of strings into a text collection
        /// </summary>
        public static implicit operator MultiLineText(List<string> source) => new(source);

        /// <summary>
        /// Converts a string into a text collection
        /// </summary>
        public static implicit operator MultiLineText(string source) => new(source);

        /// <summary>
        /// Creates a new collection of texts with the provided values
        /// </summary>
        public MultiLineText(params string[] values) : base(values) { }

        /// <summary>
        /// Creates a new collection of texts with the provided values
        /// </summary>
        public MultiLineText(IEnumerable<string> values) : base(values) { }
    }
}
