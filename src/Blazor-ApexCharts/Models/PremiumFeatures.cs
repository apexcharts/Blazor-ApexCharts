using System.Collections.Generic;

namespace ApexCharts;

/// <summary>
/// Payload for the ink-layer annotation events (ApexCharts v6.0): created, dragged, edited, styled, deleted.
/// Fields are populated per event type; unused fields are null.
/// </summary>
public class InkAnnotationEventData
{
    /// <summary>Annotation kind (e.g. point / xaxis / yaxis).</summary>
    public string Type { get; set; }

    /// <summary>Annotation id.</summary>
    public string Id { get; set; }

    /// <summary>Index within its annotation collection.</summary>
    public int? Index { get; set; }

    /// <summary>Primary x value (number or category). Present for created/dragged.</summary>
    public object X { get; set; }

    /// <summary>Primary y value. Present for created/dragged.</summary>
    public double? Y { get; set; }

    /// <summary>Secondary x value for a range annotation. Present when applicable.</summary>
    public object X2 { get; set; }

    /// <summary>Secondary y value for a range annotation. Present when applicable.</summary>
    public double? Y2 { get; set; }

    /// <summary>Edited label text. Present for the edited event.</summary>
    public string Text { get; set; }

    /// <summary>Label config. Present for the styled event.</summary>
    public object Label { get; set; }

    /// <summary>Marker config. Present for the styled event.</summary>
    public object Marker { get; set; }
}

/// <summary>
/// Payload for the measure ruler <c>measured</c> event (ApexCharts v6.0).
/// </summary>
public class MeasuredData
{
    /// <summary>Start point of the measurement.</summary>
    public MeasurePoint From { get; set; }

    /// <summary>End point of the measurement.</summary>
    public MeasurePoint To { get; set; }

    /// <summary>Change in x.</summary>
    public double? Dx { get; set; }

    /// <summary>Change in y.</summary>
    public double? Dy { get; set; }

    /// <summary>Percent change between the two points.</summary>
    public double? PercentChange { get; set; }

    /// <summary>Slope between the two points.</summary>
    public double? Slope { get; set; }
}

/// <summary>
/// A point (x, y) of a <see cref="MeasuredData"/> measurement.
/// </summary>
public class MeasurePoint
{
    /// <summary>X value.</summary>
    public double? X { get; set; }

    /// <summary>Y value.</summary>
    public double? Y { get; set; }
}

/// <summary>
/// Payload for the storyboard <c>beatChange</c> event (ApexCharts v6.0).
/// </summary>
public class BeatChangeData
{
    /// <summary>Index of the active beat.</summary>
    public int? Index { get; set; }

    /// <summary>Key of the active beat (or null).</summary>
    public string Key { get; set; }

    /// <summary>Scroll direction that triggered the change.</summary>
    public string Direction { get; set; }
}

/// <summary>
/// Options passed to <see cref="ApexChart{TItem}.StoryboardBindAsync"/> to bind prose sections to saved chart views
/// (ApexCharts v6.0 scroll-driven "Storyboard").
/// </summary>
public class StoryboardOptions
{
    /// <inheritdoc cref="ApexCharts.StoryboardBeat" />
    public List<StoryboardBeat> Beats { get; set; }

    /// <summary>
    /// CSS selector (or element) of the scroll container whose scroll position drives the beats. When set, the
    /// storyboard observes this panel's own scroll instead of the page/viewport, so the story can live in a bounded
    /// column. Leave unset to observe the page viewport.
    /// </summary>
    public string Scroller { get; set; }

    /// <summary>Animate transitions between beats (ignored under prefers-reduced-motion). Defaults to true.</summary>
    public bool? Animate { get; set; }

    /// <summary>Fraction (0..1) down the scroller at which a step activates its beat. Defaults to 0.5 (middle).</summary>
    public double? Offset { get; set; }
}

/// <summary>
/// A single storyboard beat: when the element matched by <see cref="Selector"/> scrolls past the trigger, the chart
/// applies <see cref="View"/> (and optionally merges an <see cref="Options"/> update).
/// </summary>
public class StoryboardBeat
{
    /// <summary>CSS selector of the prose element that triggers this beat.</summary>
    public string Selector { get; set; }

    /// <summary>Optional key identifying this beat.</summary>
    public string Key { get; set; }

    /// <summary>The view state to apply (e.g. zoom window, theme, static annotations).</summary>
    public object View { get; set; }

    /// <summary>Optional <c>updateOptions</c> payload merged when this beat activates (e.g. recolor, or morph chart type).</summary>
    public object Options { get; set; }

    /// <summary>Optional text announced to assistive tech (aria-live) when this beat activates.</summary>
    public string Announce { get; set; }
}

/// <summary>
/// State emitted by the crossfilter engine's <c>change</c> event (ApexCharts v6 premium <c>link</c> feature), delivered
/// to a handler subscribed via <see cref="IApexChartService.SubscribeCrossfilterAsync{T}"/>.
/// </summary>
public class CrossfilterState
{
    /// <summary>Total number of records in the shared set.</summary>
    public int Total { get; set; }

    /// <summary>Number of records passing the currently active filters.</summary>
    public int FilteredCount { get; set; }

    /// <summary>Active filters keyed by the originating chart id; each value is the list of selected dimension keys.</summary>
    public Dictionary<string, List<string>> Filters { get; set; }
}
