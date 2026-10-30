using System.Collections.Generic;
using System.Linq;

namespace ApexCharts
{
    /// <summary>
    /// Colors for the dots of a dumbbell chart, one entry per series.
    /// </summary>
    /// <remarks>
    /// An entry with a single color paints both dots of that series, an entry with two colors paints the start and end dot.<br /><br />
    ///
    /// Example:<br /><br />
    ///
    /// <code>
    /// // One color per series
    /// options.PlotOptions.Bar.DumbbellColors = ["#008FFB", "#00E396"];
    ///
    /// // Start and end color for each series
    /// options.PlotOptions.Bar.DumbbellColors = [["#008FFB", "#00E396"], ["#FEB019", "#FF4560"]];
    /// </code>
    /// </remarks>
    public class DumbbellColorList : List<Color>
    {
        /// <summary>
        /// Converts a list with one color per series
        /// </summary>
        public static implicit operator DumbbellColorList(List<string> source) => source == null ? null : new(source.Select(e => new Color(e)));

        /// <summary>
        /// Converts a list with a start and end color pair per series
        /// </summary>
        public static implicit operator DumbbellColorList(List<List<string>> source) => source == null ? null : new(source.Select(e => new Color(e)));

        /// <summary>
        /// Creates an empty list of dumbbell colors
        /// </summary>
        public DumbbellColorList() { }

        /// <summary>
        /// Creates a list of dumbbell colors with the provided values
        /// </summary>
        public DumbbellColorList(IEnumerable<Color> colors) : base(colors) { }
    }
}
