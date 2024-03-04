using System.Collections.Generic;

namespace ApexCharts
{
    /// <remarks>
    /// Accepts either a single value or collection of values. 
    /// By providing a collection in the case of chart annotations, the supplied strings will be printed as separate lines, thus allowing for multiline annotation handling.<br /><br />
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
    ///                         Text = new LabelText("Annotation Text")
    ///                     }
    ///                 }
    ///             }
    /// </code>
    /// </remarks>
    public class LabelText : ValueOrList<string>
    {
        /// <summary>
        /// Converts a label text collection into a list of strings
        /// </summary>
        public static implicit operator List<string>(LabelText source) => source.values;

        /// <summary>
        /// Converts a list of strings into an label text collection
        /// </summary>
        public static implicit operator LabelText(List<string> source) => new(source);

        /// <summary>
        /// Converts a string into an label text collection
        /// </summary>
        public static implicit operator LabelText(string source) => new(source);

        /// <summary>
        /// Creates a new collection of label texts with the provided values
        /// </summary>
        public LabelText(params string[] values) : base(values) { }

        /// <summary>
        /// Creates a new collection of label texts with the provided values
        /// </summary>
        public LabelText(IEnumerable<string> values) : base(values) { }
    }
}
