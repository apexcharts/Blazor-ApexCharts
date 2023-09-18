namespace ApexCharts.Internal
{
    /// <summary>
    /// Return data from JavaScript when an X-axis label is clicked
    /// </summary>
    internal class JSXAxisLabelClick
    {
        /// <inheritdoc cref="XAxisLabelClicked{TItem}.LabelIndex"/>
        public int LabelIndex { get; set; }

        /// <inheritdoc cref="XAxisLabelClicked{TItem}.Caption"/>
        public string Caption { get; set; }
    }
}
