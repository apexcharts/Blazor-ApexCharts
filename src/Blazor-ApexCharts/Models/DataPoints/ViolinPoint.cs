using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ApexCharts
{
    /// <summary>
    /// A single violin (one category) for a <see cref="ChartType.Violin"/> series.
    /// </summary>
    /// <remarks>
    /// apexcharts.js does not compute the density curve itself: each violin must supply a
    /// precomputed density profile plus the raw observations for the jitter overlay (see the
    /// data contract in the core's <c>Data.handleViolinData()</c>). <see cref="ApexViolinSeries{TItem}"/>
    /// builds the <see cref="ViolinDistribution"/> from raw observations via a Gaussian kernel-density
    /// estimate, so callers only supply the observations. Construct this type directly if you would
    /// rather pass a density you computed elsewhere.
    /// </remarks>
    /// <typeparam name="TItem">The data type to be used in the chart to create data points.</typeparam>
    public class ViolinPoint<TItem> : IDataPoint<TItem>
    {
        /// <inheritdoc cref="IDataPoint{TItem}.X"/>
        public object X { get; set; }

        /// <summary>
        /// The precomputed density profile and raw observations for this violin.
        /// </summary>
        public ViolinDistribution Y { get; set; }

        /// <inheritdoc cref="IDataPoint{TItem}.FillColor"/>
        public string FillColor { get; set; }

        /// <inheritdoc cref="IDataPoint{TItem}.Extra"/>
        public object Extra { get; set; }

        /// <inheritdoc cref="IDataPoint{TItem}.Items"/>
        [JsonIgnore]
        public IEnumerable<TItem> Items { get; set; }
    }

    /// <summary>
    /// The violin data contract expected by apexcharts.js: a density profile that defines the
    /// curve shape, plus the raw observations drawn as jittered dots.
    /// </summary>
    public class ViolinDistribution
    {
        /// <summary>
        /// The density profile as <c>[value, weight]</c> pairs. <c>value</c> is a position on the
        /// value axis and <c>weight</c> is the estimated density there. apexcharts scales the widths
        /// via <see cref="PlotOptionsViolin.BandwidthScale"/> and <see cref="PlotOptionsViolin.Normalize"/>.
        /// </summary>
        [JsonPropertyName("density")]
        public List<decimal[]> Density { get; set; }

        /// <summary>
        /// The raw observations that produced the curve, overlaid as jittered dots when
        /// <see cref="ViolinPoints.Show"/> is enabled. Omitted when null.
        /// </summary>
        [JsonPropertyName("points")]
        public List<decimal> Points { get; set; }
    }
}
