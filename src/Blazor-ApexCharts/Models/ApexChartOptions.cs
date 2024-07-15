using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using ApexCharts.Internal;

namespace ApexCharts
{
    /// <summary>
    /// Main class to configure options that are to be serialized and passed to the JavaScript.
    /// </summary>
    /// <typeparam name="TItem">The data type to be used in the chart to create data points.</typeparam>
    public class ApexChartOptions<TItem> where TItem : class
    {
        /// <summary>
        /// Logs function calls and options to the browser console when true
        /// </summary>
        public bool Debug { get; set; }

        /// <inheritdoc cref="ApexCharts.ApexChartsBlazorOptions" />
        [JsonIgnore]
        public ApexChartsBlazorOptions Blazor { get; set; }

        /// <inheritdoc cref="ApexCharts.Annotations" />
        public Annotations Annotations { get; set; }

        /// <inheritdoc cref="ApexCharts.Chart" />
        public Chart Chart { get; set; } = new();

        /// <summary>
        /// Colors for the chart's series. When all colors of the array are used, it starts from the beginning. Predefined colors are available in <see href="https://apexcharts.com/docs/options/theme">theme palettes</see>
        /// </summary>
        /// <remarks>
        /// You should only provide either hex or rgb/rgba format. Color names are not accepted at the moment.
        /// 
        /// Links:
        /// 
        /// <see href="https://apexcharts.com/docs/colors">JavaScript Documentation</see>,
        /// <see href="https://apexcharts.com/docs/options/colors">JavaScript Reference</see>
        /// </remarks>
        public List<string> Colors { get; set; }

        /// <inheritdoc cref="ApexCharts.DataLabels" />
        public DataLabels DataLabels { get; set; }

        /// <inheritdoc cref="ApexCharts.Fill" />
        public Fill Fill { get; set; }

        /// <inheritdoc cref="ApexCharts.ForecastDataPoints" />
        public ForecastDataPoints ForecastDataPoints { get; set; }

        /// <inheritdoc cref="ApexCharts.Grid" />
        public Grid Grid { get; set; }

        /// <summary>
        /// In Axis Charts (line / column), labels can be set instead of setting xaxis categories option. While, in pie/donut charts, each label corresponds to value in series array.
        /// </summary>
        /// <remarks>
        /// Links:
        /// 
        /// <see href="https://apexcharts.com/docs/options/labels">JavaScript Reference</see>
        /// </remarks>
        public List<string> Labels { get; set; }

        /// <inheritdoc cref="ApexCharts.Legend" />
        public Legend Legend { get; set; }

        /// <inheritdoc cref="ApexCharts.Markers" />
        public Markers Markers { get; set; }

        /// <inheritdoc cref="ApexCharts.NoData" />
        public NoData NoData { get; set; }

        /// <inheritdoc cref="ApexCharts.PlotOptions" />
        public PlotOptions PlotOptions { get; set; }

        /// <inheritdoc cref="ApexCharts.Responsive" />
        public List<Responsive> Responsive { get; set; }

        /// <inheritdoc cref="ApexCharts.Series{TItem}" />
        public List<Series<TItem>> Series { get; set; }

        /// <inheritdoc cref="ApexCharts.States" />
        public States States { get; set; }

        /// <inheritdoc cref="ApexCharts.Stroke" />
        public Stroke Stroke { get; set; }

        /// <inheritdoc cref="ApexCharts.Subtitle" />
        public Subtitle Subtitle { get; set; }

        /// <inheritdoc cref="ApexCharts.Theme" />
        public Theme Theme { get; set; }

        /// <inheritdoc cref="ApexCharts.Title" />
        public Title Title { get; set; }

        /// <inheritdoc cref="ApexCharts.Tooltip" />
        public Tooltip Tooltip { get; set; }

        /// <inheritdoc cref="ApexCharts.XAxis" />
        public XAxis Xaxis { get; set; }

        /// <inheritdoc cref="ApexCharts.YAxis" />
        public List<YAxis> Yaxis { get; set; }
    }

    /// <summary>
    /// Class to define how additional data points that extrapolate the provided data should be generated
    /// </summary>
    /// <remarks>
    /// Links:
    /// 
    /// <see href="https://apexcharts.com/docs/options/forecastdatapoints">JavaScript Reference</see>
    /// </remarks>
    public class ForecastDataPoints
    {
        /// <summary>
        /// Number of ending data-points you want to indicate as a forecast or prediction values. The ending line/bar will result into a dashed border with a distinct look to differentiate from the rest of the data-points.
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Opacity of the fill attribute.
        /// </summary>
        public double? FillOpacity { get; set; }

        /// <summary>
        /// Sets the width of the border (applies to line/area/bar/boxplots).
        /// </summary>
        public double? StrokeWidth { get; set; }

        /// <summary>
        /// Creates dashes in borders of SVG path. Higher number creates more space between dashes in the border.
        /// </summary>
        public double? DashArray { get; set; }
    }

    /// <summary>
    /// Defines options specific to <see cref="ChartType.Treemap"/>
    /// </summary>
    /// <remarks>
    /// Links:
    /// 
    /// <see href="https://apexcharts.com/docs/chart-types/treemap-chart">JavaScript Documentation</see>,
    /// <see href="https://apexcharts.com/docs/options/plotoptions/treemap">JavaScript Reference</see>
    /// </remarks>
    public class PlotOptionsTreemap
    {
        /// <summary>
        /// Enable different shades of color depending on the value
        /// </summary>
        public bool? EnableShades { get; set; }

        /// <summary>
        /// Enable different shades of color depending on the value. Accepts from 0 to 1
        /// </summary>
        public double? ShadeIntensity { get; set; }

        /// <summary>
        /// Border radius for the treemap symbol
        /// </summary>
        public double? BorderRadius { get; set; }


        /// <summary>
        /// When turned on, each series in a treemap will have it's own lowest and highest range and colors will be shaded for each series. Default value is false.
        /// </summary>
        public bool? Distributed { get; set; }

        /// <summary>
        /// When enabled, it will reverse the shades for negatives but keep the positive shades as it is now. An example of such use-case would be in a profit/loss chart where darker reds mean larger losses, darker greens mean larger gains.
        /// </summary>
        public bool? ReverseNegativeShade { get; set; }

        /// <summary>
        /// When turned on, the stroke/border around the treemap cell has the same color as the cell color.
        /// </summary>
        public bool? UseFillColorAsStroke { get; set; }

        /// <inheritdoc cref="ApexCharts.TreemapColorScale" />
        public TreemapColorScale ColorScale { get; set; }

        /// <inheritdoc cref="ApexCharts.TreemapDataLabels" />
        public TreemapDataLabels DataLabels { get; set; }
    }

    /// <summary>
    /// Defines how to style data labels within a treemap chart.
    /// </summary>
    public class TreemapDataLabels
    {
        /// <summary>
        /// You can either set the labels to scale based on the box size, or you can keep the same font-size and let the labels truncate if they exceed the area.
        /// </summary>
        public Format Format { get; set; }
    }

    /// <summary>
    /// Defines how to color a given section of the treemap chart.
    /// </summary>
    public class TreemapColorScale
    {
        /// <summary>
        /// In a multiple series treemap, flip the color-scale to fill the treemaps vertically instead of horizontally.
        /// </summary>
        public bool? Inverse { get; set; }

        /// <inheritdoc cref="ApexCharts.TreemapRanges" />
        public List<TreemapRanges> Ranges { get; set; }

        /// <summary>
        /// Specify the lower range for treemap color calculation.
        /// </summary>
        public double? Min { get; set; }

        /// <summary>
        /// Specify the higher range for treemap color calculation.
        /// </summary>
        public double? Max { get; set; }
    }

    /// <summary>
    /// Defines zones and colors to apply to the treemap chart
    /// </summary>
    public class TreemapRanges
    {
        /// <summary>
        /// Value indicating range's upper limit
        /// </summary>
        public double? From { get; set; }

        /// <summary>
        /// Value indicating range's lower limit
        /// </summary>
        public double? To { get; set; }

        /// <summary>
        /// Background color to fill the range with.
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Fore Color of the text if data-labels is enabled.
        /// </summary>
        public string ForeColor { get; set; }

        /// <summary>
        /// Undocumented, this property exists in the TypeScript definition
        /// </summary>
        public string Name { get; set; }
    }

    /// <summary>
    /// Annotations in ApexCharts allows you to write custom text on specific data-points or on axes values.
    /// </summary>
    /// <remarks>
    /// Links:
    /// 
    /// <see href="https://apexcharts.github.io/Blazor-ApexCharts/features/annotations">Blazor Example</see>,
    /// <see href="https://apexcharts.com/docs/annotations">JavaScript Documentation</see>,
    /// <see href="https://apexcharts.com/docs/options/annotations">JavaScript Reference</see>
    /// </remarks>
    public class Annotations
    {
        /// <inheritdoc cref="ApexCharts.AnnotationsImage" />
        public List<AnnotationsImage> Images { get; set; }

        /// <inheritdoc cref="ApexCharts.AnnotationsPoint" />
        public List<AnnotationsPoint> Points { get; set; }

        /// <inheritdoc cref="ApexCharts.AnnotationsText" />
        public List<AnnotationsText> Texts { get; set; }

        /// <inheritdoc cref="ApexCharts.AnnotationsXAxis" />
        public List<AnnotationsXAxis> Xaxis { get; set; }

        /// <inheritdoc cref="ApexCharts.AnnotationsYAxis" />
        public List<AnnotationsYAxis> Yaxis { get; set; }


        internal List<IAnnotation> GetAllAnnotations()
        {
            var result = new List<IAnnotation>();

            if (Points != null)
            {
                result.AddRange(Points);
            }

            if (Xaxis != null)
            {
                result.AddRange(Xaxis);
            }

            if (Yaxis != null)
            {
                result.AddRange(Yaxis);
            }

            return result;

        }

    }

    /// <summary>
    /// Defines details for images to use on the data annotations
    /// </summary>
    public class AnnotationsImage
    {
        /// <summary>
        /// A query selector to which the image element will be appended.
        /// </summary>
        public string AppendTo { get; set; }

        /// <summary>
        /// The height of the image
        /// </summary>
        public double? Height { get; set; }

        /// <summary>
        /// The height of the image
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// The width of the image
        /// </summary>
        public double? Width { get; set; }

        /// <summary>
        /// Left position for the image relative to the element specified in <see cref="AppendTo"/> property
        /// </summary>
        public double? X { get; set; }

        /// <summary>
        /// Top position for the image relative to the element specified in <see cref="AppendTo"/> property
        /// </summary>
        public double? Y { get; set; }
    }

    /// <summary>
    /// Defines details for individual data annotation points
    /// </summary>
    public class AnnotationsPoint : IAnnotation
    {
        /// <summary>
        /// Undocumented, this property exists in the TypeScript definition
        /// </summary>
        public string Id { get; set; }

        /// <inheritdoc cref="ApexCharts.AnnotationsPointImage" />
        public AnnotationsPointImage Image { get; set; }

        /// <inheritdoc cref="ApexCharts.Label" />
        public Label Label { get; set; }

        /// <inheritdoc cref="ApexCharts.AnnotationMarker" />
        public AnnotationMarker Marker { get; set; }

        /// <summary>
        /// In a multiple series, you will have to specify which series the annotation's y value belongs to. Not required for single series
        /// </summary>
        public double? SeriesIndex { get; set; }

        /// <summary>
        /// X Value on which the annotation will be drawn (can be either timestamp for datetime x-axis or string category for category x-axis)
        /// </summary>
        public object X { get; set; }

        /// <summary>
        /// Y Value on which the annotation will be drawn
        /// </summary>
        public double? Y { get; set; }

        /// <summary>
        /// When there are multiple y-axis, setting this options will put the point annotation for that particular y-axis' y value
        /// </summary>
        public double? YAxisIndex { get; set; }

        /// <summary>
        /// Click function
        /// </summary>
        [JsonConverter(typeof(FunctionStringConverter))]
        public string Click { get; internal set; }

        /// <summary>
        /// Mouse Enter function
        /// </summary>
        [JsonConverter(typeof(FunctionStringConverter))]
        public string MouseEnter { get; internal set; }

        /// <summary>
        /// Mouse Leave function
        /// </summary>
        [JsonConverter(typeof(FunctionStringConverter))]
        public string MouseLeave { get; internal set; }


        internal void SetEventFunction(AnnotationEventType eventType)
        {
            var eventFunction = "function(annotation, e) {this.w.config.dotNetObject.invokeMethodAsync('JSAnnotationPointEvent',{id: annotation.id, eventType: '" + eventType.ToString() + "'});}";

            switch (eventType)
            {
                case AnnotationEventType.Click:
                    Click = eventFunction;
                    return;

                case AnnotationEventType.MouseLeave:
                    MouseLeave = eventFunction;
                    return;
                case AnnotationEventType.MouseEnter:
                    MouseEnter = eventFunction;
                    return;
            }
        }
    }

    /// <summary>
    /// Defines details for an image to use with an individual data annotation point
    /// </summary>
    public class AnnotationsPointImage
    {
        /// <summary>
        /// Height of image annotation.
        /// </summary>
        public double? Height { get; set; }

        /// <summary>
        /// Left offset of the image.
        /// </summary>
        public double? OffsetX { get; set; }

        /// <summary>
        /// Top offset of the image.
        /// </summary>
        public double? OffsetY { get; set; }

        /// <summary>
        /// Provide a full path of the image to display in place of annotation.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Width of image annotation.
        /// </summary>
        public double? Width { get; set; }
    }

    /// <summary>
    /// Defines the options for an annotation label
    /// </summary>
    public class Label
    {
        /// <summary>
        /// Border Color of the label
        /// </summary>
        public string BorderColor { get; set; }

        /// <summary>
        /// Border width of the label
        /// </summary>
        public double? BorderWidth { get; set; }

        /// <summary>
        /// Border-radius of the label
        /// </summary>
        public double? BorderRadius { get; set; }

        /// <summary>
        /// Sets the left offset for annotation label
        /// </summary>
        public double? OffsetX { get; set; }

        /// <summary>
        /// Sets the top offset for annotation label
        /// </summary>
        public double? OffsetY { get; set; }

        /// <summary>
        /// The direction to display the label
        /// </summary>
		public Orientation? Orientation { get; set; }

        /// <summary>
        /// Where to place the label
        /// </summary>
        public LabelPosition? Position { get; set; }

        /// <inheritdoc cref="ApexCharts.Style" />
        public Style Style { get; set; }

        /// <summary>
        /// Text for tha annotation label (single string or List of string) to put a multine label
        /// </summary>
        public MultiLineText Text { get; set; }

        /// <summary>
        /// The alignment of text relative to label's drawing position
        /// </summary>
        public TextAnchor? TextAnchor { get; set; }

        /// <summary>
        /// Click function
        /// </summary>
        [JsonConverter(typeof(FunctionStringConverter))]
        public string Click { get; internal set; }

        /// <summary>
        /// Mouse Enter function
        /// </summary>
        [JsonConverter(typeof(FunctionStringConverter))]
        public string MouseEnter { get; internal set; }

        /// <summary>
        /// Mouse Leave function
        /// </summary>
        [JsonConverter(typeof(FunctionStringConverter))]
        public string MouseLeave { get; internal set; }

        internal void SetEventFunction(AnnotationEventType eventType)
        {
            var eventFunction = "function(annotation, e) {this.w.config.dotNetObject.invokeMethodAsync('JSAnnotationLabelEvent',{id: annotation.id, eventType: '" + eventType.ToString() + "'});}";

            switch (eventType)
            {
                case AnnotationEventType.Click:
                    Click = eventFunction;
                    return;

                case AnnotationEventType.MouseLeave:
                    MouseLeave = eventFunction;
                    return;
                case AnnotationEventType.MouseEnter:
                    MouseEnter = eventFunction;
                    return;
            }
        }
    }




    /// <summary>
    /// Defines the styling options for the annotation label
    /// </summary>
    public class Style
    {
        /// <summary>
        /// Background Color for the annotation label
        /// </summary>
        public string Background { get; set; }

        /// <summary>
        /// Fore color for the annotation label
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// A custom Css Class to give to the annotation label elements
        /// </summary>
        public string CssClass { get; set; }

        /// <summary>
        /// Font-family for the annotation label
        /// </summary>
        public string FontFamily { get; set; }

        /// <summary>
        /// Font size for the annotation label
        /// </summary>
        public string FontSize { get; set; }

        /// <summary>
        /// Font-weight for the annotation label
        /// </summary>
        public object FontWeight { get; set; }

        /// <inheritdoc cref="ApexCharts.Padding"/>
        public Padding Padding { get; set; }
    }

    /// <summary>
    /// Defines the padding values for an object
    /// </summary>
    public class Padding
    {
        /// <summary>
        /// Bottom padding for the item
        /// </summary>
        public double? Bottom { get; set; }

        /// <summary>
        /// Left padding for the item
        /// </summary>
        public double? Left { get; set; }

        /// <summary>
        /// Right padding for the item
        /// </summary>
        public double? Right { get; set; }

        /// <summary>
        /// Top padding for the item
        /// </summary>
        public double? Top { get; set; }
    }

    /// <summary>
    /// Defines how to style the marker for a data annotation point
    /// </summary>
    public class AnnotationMarker
    {
        /// <summary>
        /// Additional CSS classes to append to the marker
        /// </summary>
        public string CssClass { get; set; }

        /// <summary>
        /// Fill Color of the marker point.
        /// </summary>
        public string FillColor { get; set; }

        /// <summary>
        /// Sets the left offset of the marker
        /// </summary>
        public double? OffsetX { get; set; }

        /// <summary>
        /// Sets the top offset of the marker
        /// </summary>
        public double? OffsetY { get; set; }

        /// <summary>
        /// Radius of the marker (applies to square shape)
        /// </summary>
        public double? Radius { get; set; }

        /// <summary>
        /// Shape of the marker.
        /// </summary>
        public ShapeEnum Shape { get; set; }

        /// <summary>
        /// Size of the marker.
        /// </summary>
        public double? Size { get; set; }

        /// <summary>
        /// Stroke Color of the marker point.
        /// </summary>
        public string StrokeColor { get; set; }

        /// <summary>
        /// Stroke Size of the marker point.
        /// </summary>
        public double? StrokeWidth { get; set; }
    }

    /// <summary>
    /// Defines how to style the individual data point annotations and their text
    /// </summary>
    public class AnnotationsText
    {
        /// <summary>
        /// A query selector to which the text element will be appended.
        /// </summary>
        public string AppendTo { get; set; }

        /// <summary>
        /// Undocumented, this property exists in the TypeScript definition
        /// </summary>
        public string BackgroundColor { get; set; }

        /// <summary>
        /// Border Color for the label
        /// </summary>
        public string BorderColor { get; set; }

        /// <summary>
        /// Border Radius for the label
        /// </summary>
        public double? BorderRadius { get; set; }

        /// <summary>
        /// Border width for the label
        /// </summary>
        public double? BorderWidth { get; set; }

        /// <summary>
        /// Font-family for the annotation label
        /// </summary>
        public string FontFamily { get; set; }

        /// <summary>
        /// Font size for the annotation label
        /// </summary>
        public object FontSize { get; set; }

        /// <summary>
        /// Font-weight for the annotation label
        /// </summary>
        public object FontWeight { get; set; }

        /// <summary>
        /// Undocumented, this property exists in the TypeScript definition
        /// </summary>
        public string ForeColor { get; set; }

        /// <summary>
        /// Bottom padding for the label
        /// </summary>
        public double? PaddingBottom { get; set; }

        /// <summary>
        /// Left padding for the label
        /// </summary>
        public double? PaddingLeft { get; set; }

        /// <summary>
        /// Right padding for the label
        /// </summary>
        public double? PaddingRight { get; set; }

        /// <summary>
        /// Top padding for the label
        /// </summary>
        public double? PaddingTop { get; set; }

        /// <summary>
        /// The main text to be displayed
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// The alignment of text relative to label's drawing position. 
        /// </summary>
        public TextAnchor TextAnchor { get; set; } = TextAnchor.Start;

        /// <summary>
        /// X (left) position for the text relative to the element specified in <see cref="AppendTo"/> property
        /// </summary>
        public double? X { get; set; }

        /// <summary>
        /// Y (top) position for the text relative to the element specified in <see cref="AppendTo"/> property
        /// </summary>
        public double? Y { get; set; }
    }

    /// <summary>
    /// Defines how to style the X-Axis for a data annotation point
    /// </summary>
    public class AnnotationsXAxis : IAnnotation
    {
        /// <summary>
        /// Undocumented, this property exists in the TypeScript definition
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Color of the annotation line
        /// </summary>
        public string BorderColor { get; set; }

        /// <summary>
        /// Sets the witdh of the line. Please note this property is missing in apexcharts.js docs.
        /// </summary>
        public double? BorderWidth { get; set; }

        /// <summary>
        /// Fill Color of the region. Only applicable if <see cref="X2"/> is provided.
        /// </summary>
        public string FillColor { get; set; }

        /// <inheritdoc cref="ApexCharts.Label" />
        public Label Label { get; set; }

        /// <summary>
        /// Sets the left offset for annotation line
        /// </summary>
        public double? OffsetX { get; set; }

        /// <summary>
        /// Sets the top offset for annotation line
        /// </summary>
        public double? OffsetY { get; set; }

        /// <summary>
        /// Opacity of the filled region.
        /// </summary>
        public double? Opacity { get; set; }

        /// <summary>
        /// Creates dashes in borders of SVG path. A higher number creates more space between dashes in the border.
        /// </summary>
        public double? StrokeDashArray { get; set; }

        /// <summary>
        /// Value on which the annotation will be drawn
        /// </summary>
        public object X { get; set; }

        /// <summary>
        /// If you provide this additional x2 value, a region will be drawn from x to x2.
        /// </summary>
        public object X2 { get; set; }
    }

    /// <summary>
    /// Defines how to style the Y-Axis for a data annotation point
    /// </summary>
    public class AnnotationsYAxis : IAnnotation
    {
        /// <summary>
        /// Undocumented, this property exists in the TypeScript definition
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Color of the annotation line
        /// </summary>
        public string BorderColor { get; set; }

        /// <summary>
        /// Undocumented, this property exists in the TypeScript definition
        /// </summary>
        public double? BorderWidth { get; set; }

        /// <summary>
        /// Fill Color of the region. Only applicable if <see cref="Y2"/> is provided.
        /// </summary>
        public string FillColor { get; set; }

        /// <inheritdoc cref="ApexCharts.Label" />
        public Label Label { get; set; }

        /// <summary>
        /// Sets the left offset for annotation line
        /// </summary>
        public double? OffsetX { get; set; }

        /// <summary>
        /// Sets the top offset for annotation line
        /// </summary>
        public double? OffsetY { get; set; }

        /// <summary>
        /// Opacity of the filled region.
        /// </summary>
        public double? Opacity { get; set; }

        /// <summary>
        /// Creates dashes in borders of the SVG path. A higher number creates more space between dashes in the border.
        /// </summary>
        public double? StrokeDashArray { get; set; }

        /// <summary>
        /// Sets the width for annotation line
        /// </summary>
        public double? Width { get; set; }

        /// <summary>
        /// Value on which the annotation will be drawn
        /// </summary>
        public object Y { get; set; }

        /// <summary>
        /// If you provide this additional y2 value, a region will be drawn from y to y2.
        /// </summary>
        public object Y2 { get; set; }

        /// <summary>
        /// When there are multiple y-axis, setting this options will put the annotation for that particular y-axis
        /// </summary>
        public double? YAxisIndex { get; set; }
    }

    /// <summary>
    /// Main class to configure options that will be applied to the generated chart.
    /// </summary>
    /// <remarks>
    /// Links:
    /// 
    /// <see href="https://apexcharts.com/docs/options/chart">JavaScript Reference</see>
    /// </remarks>
    public class Chart
    {
        /// <summary>
        /// Indicates if only bar series should be stacked. Valid in mixed charts
        /// </summary>
        public bool? StackOnlyBar { get; set; }

        /// <inheritdoc cref="ApexCharts.Animations" />
        public Animations Animations { get; set; }

        /// <summary>
        /// Background color for the chart area. If you want to set background with css, use .apexcharts-canvas to set it.
        /// </summary>
        public string Background { get; set; }

        /// <inheritdoc cref="ApexCharts.Brush" />
        public Brush Brush { get; set; }

        /// <summary>
        /// defaultLocale is the preselected language that the chart should render initially and the selected locale has to be present in the locales array. Read more in the <see href="https://apexcharts.com/docs/localization">localization</see> guide.
        /// </summary>
        public string DefaultLocale { get; set; }

        /// <inheritdoc cref="ApexCharts.DropShadow" />
        public DropShadow DropShadow { get; set; }

        /// <summary>
        /// A collection of JavaScript functions to execute on specific interations with the chart. Recommendation is to use events within <see cref="ApexChart{TItem}"/>. Available keys are:
        /// 
        /// animationEnd,
        /// beforeMount,
        /// mounted,
        /// updated,
        /// click,
        /// mouseMove,
        /// mouseLeave,
        /// legendClick,
        /// markerClick,
        /// xAxisLabelClick,
        /// selection,
        /// dataPointSelection,
        /// dataPointMouseEnter,
        /// dataPointMouseLeave,
        /// beforeZoom,
        /// beforeResetZoom,
        /// zoomed,
        /// scrolled,
        /// brushScrolled
        /// </summary>
        /// <remarks>
        /// <see href="https://apexcharts.com/docs/options/chart/events">JavaScript Reference</see>
        /// </remarks>
        public Dictionary<string, object> Events { get; set; }

        /// <summary>
        /// Sets the font family for all the text elements of the chart. Defaults to 'Helvetica, Arial, sans-serif'
        /// </summary>
        public string FontFamily { get; set; }

        /// <summary>
        /// Sets the text color for the chart. Defaults to #373d3f
        /// </summary>
        public string ForeColor { get; set; }

        /// <summary>
        /// A chart group is created to perform interactive operations at the same time in all the charts. In case you want to create <see href="https://apexcharts.com/docs/chart-types/synchronized-charts">synchronized charts</see>, you will need to provide this property.
        /// </summary>
        public string Group { get; set; }

        /// <summary>
        /// A chart ID is a unique identifier that will be used in calling certain ApexCharts methods. You will also need chart.id to be set in case you want to use any of the following functionalities.
        /// 
        /// <list type="bullet">
        /// <item><see href="https://apexcharts.com/docs/options/chart/brush">brush chart</see></item>
        /// <item><see href="https://apexcharts.com/docs/chart-types/synchronized-charts">synchronized chart</see></item>
        /// <item>Calling <see href="https://apexcharts.com/docs/methods/#exec">exec</see> method of ApexCharts</item>
        /// </list>
        /// </summary>
        public string Id { get; set; }

        /// <inheritdoc cref="ApexCharts.ChartLocale" />
        public List<ChartLocale> Locales { get; set; }

        /// <summary>
        /// Sets the left offset for the whole chart.
        /// </summary>
        public double? OffsetX { get; set; }

        /// <summary>
        /// Sets the top offset for the entire chart.
        /// </summary>
        public double? OffsetY { get; set; }

        /// <summary>
        /// A small increment in height added to the parent of chart element.
        /// </summary>
        public double? ParentHeightOffset { get; set; }

        /// <summary>
        /// Re-render the chart when the element size gets changed or the size of the parent element gets changed. Useful in conditions where the chart container resizes after page reload.
        /// </summary>
        public bool? RedrawOnParentResize { get; set; }

        /// <summary>
        /// Re-render the chart when the window in which chart is rendered gets resized. Useful when rendering chart in iframes.
        /// </summary>
        public bool? RedrawOnWindowResize { get; set; }

        /// <inheritdoc cref="ApexCharts.Selection" />
        public Selection Selection { get; set; }

        /// <inheritdoc cref="ApexCharts.ChartSparkline" />
        public ChartSparkline Sparkline { get; set; }

        /// <summary>
        /// Enables stacked option for axis charts. <see href="https://apexcharts.com/javascript-chart-demos/column-charts/stacked">Example</see>
        /// </summary>
        /// <remarks>
        /// A stacked chart works only for same chart types and won't work in combo/mixed charts combinations. So, an area series combined with a column series will not work.
        /// </remarks>
        public bool? Stacked { get; set; }

        /// <summary>
        /// When stacked, should the stacking be percentage based or normal stacking.
        /// </summary>
        public StackType? StackType { get; set; }

        /// <inheritdoc cref="ApexCharts.Toolbar" />
        public Toolbar Toolbar { get; set; }

        /// <summary>
        /// Specify the chart type
        /// </summary>
        public ChartType? Type { get; set; }

        /// <summary>
        /// Width of the chart.
        /// </summary>
        /// <remarks>
        /// Accepts Number (400) OR String ('400px')
        /// </remarks>
        public object Width { get; set; }

        /// <summary>
        /// Height of the chart. The default value 'auto' is calculated based on the golden ratio 1.618 which roughly translates to a 16:10 aspect ratio. Examples:
        /// 
        /// <code>
        /// height: 400 
        /// height: '400px' 
        /// height: '100%' 
        /// </code>
        /// </summary>
        /// <remarks>
        /// Note: If you provide a percentage value '100%', make sure to have a fixed height parent.
        /// </remarks>
        public object Height { get; set; }

        /// <inheritdoc cref="ApexCharts.Zoom" />
        public Zoom Zoom { get; set; }
    }

    /// <summary>
    /// Defines the options to use when exporting charts
    /// </summary>
    public class ExportOptions
    {
        /// <inheritdoc cref="ApexCharts.ExportCSV" />
        public ExportCSV Csv { get; set; }

        /// <inheritdoc cref="ApexCharts.ExportSvg" />
        public ExportSvg Svg { get; set; }

        /// <inheritdoc cref="ApexCharts.ExportPng" />
        public ExportPng Png { get; set; }
    }

    /// <summary>
    /// Defines the options to use when exporting a chart to SVG
    /// </summary>
    public class ExportSvg
    {
        /// <summary>
        /// Name of the SVG file. Defaults to auto generated chart ID
        /// </summary>
        public string Filename { get; set; }
    }

    /// <summary>
    /// Defines the options to use when exporting a chart to PNG
    /// </summary>
    public class ExportPng
    {
        /// <summary>
        /// Name of the PNG file. Defaults to auto generated chart ID
        /// </summary>
        public string Filename { get; set; }
    }

    /// <summary>
    /// Defines the options to use when exporting a chart to CSV
    /// </summary>
    public class ExportCSV
    {
       
        /// <summary>
        /// Name of the csv file. Defaults to auto generated chart ID
        /// </summary>
        public string Filename { get; set; }

        /// <summary>
        /// Delimeter to separate data-items. Defaults to comma.
        /// </summary>
        public string ColumnDelimiter { get; set; }

        /// <summary>
        /// Column Title of X values
        /// </summary>
        public string HeaderCategory { get; set; }

        /// <summary>
        /// Column Title of Y values
        /// </summary>
        public string HeaderValue { get; set; }

        /// <summary>
        /// Obsolete! Please use Categoryformatter
        /// </summary>
        [Obsolete("Please use CategoryFormatter")]
        public string DateFormatter { get => CategoryFormatter; set => CategoryFormatter = value; }

        /// <summary>
        /// If timestamps are provided as X values, those timestamps can be formatted to convert them to date strings.
        /// </summary>
        [JsonConverter(typeof(FunctionStringConverter))]
        public string CategoryFormatter { get; set; }

        /// <summary>
        /// Formats the value for the export
        /// </summary>
        [JsonConverter(typeof(FunctionStringConverter))]
        public string ValueFormatter { get; set; }

    }

    /// <summary>
    /// ApexCharts provides a smooth experience with the help of svgjs's built in animations.
    /// </summary>
    /// <remarks>
    /// Links:
    /// 
    /// <see href="https://apexcharts.com/docs/animations">JavaScript Documentation</see>,
    /// <see href="https://apexcharts.com/docs/options/chart/animations">JavaScript Reference</see>
    /// </remarks>
    public class Animations
    {
        /// <inheritdoc cref="ApexCharts.AnimateGradually" />
        public AnimateGradually AnimateGradually { get; set; }

        /// <inheritdoc cref="ApexCharts.DynamicAnimation" />
        public DynamicAnimation DynamicAnimation { get; set; }

        /// <summary>
        /// The type of animation to use
        /// </summary>
        public Easing? Easing { get; set; }

        /// <summary>
        /// Enable or disable all the animations that happen initially or during data update.
        /// </summary>
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// Speed at which animation runs.
        /// </summary>
        public double? Speed { get; set; }
    }

    /// <summary>
    /// Defines options for animations that run individually
    /// </summary>
    public class AnimateGradually
    {
        /// <summary>
        /// Speed at which gradual (one by one) animation runs.
        /// </summary>
        public double? Delay { get; set; }

        /// <summary>
        /// Gradually animate one by one every data in the series instead of animating all at once.
        /// </summary>
        public bool Enabled { get; set; } = true;
    }

    /// <summary>
    /// Defines options for animations that run as the chart data changes
    /// </summary>
    public class DynamicAnimation
    {
        /// <summary>
        /// Animate the chart when data is changed and chart is re-rendered.
        /// </summary>
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// Speed at which dynamic animation runs whenever data changes.
        /// </summary>
        public double? Speed { get; set; }
    }

    /// <summary>
    /// Defines options to use for generating a brush chart
    /// </summary>
    /// <remarks>
    /// Links:
    /// 
    /// <see href="https://apexcharts.com/docs/options/chart/brush">JavaScript Reference</see>
    /// </remarks>
    public class Brush
    {
        /// <summary>
        /// If set to true, the Y-axis will automatically scale based on the visible min/max range.
        /// </summary>
        /// <remarks>
        /// Note: One important configuration to set in a brush chart is the <see href="https://apexcharts.com/docs/options/chart/selection">chart.selection</see> option. The range which you set in <see cref="Chart.Selection"/> will act as brush in the brush chart
        /// </remarks>
        public bool? AutoScaleYaxis { get; set; }

        /// <summary>
        /// Turn on this option to enable brush chart options. After you enable brush, you need to set target chart ID to allow the brush chart to capture events on the target chart.
        /// </summary>
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// Chart ID of the target chart to sync the brush chart and the target chart. If you have an array of multiple chart IDs, use <see cref="Targets"/> property instead.
        /// </summary>
        public string Target { get; set; }

        /// <summary>
        /// Chart IDs of the target charts to sync the brush chart and the target charts. If you have a single chart ID, use <see cref="Target"/> property instead.
        /// </summary>
        public List<string> Targets { get; set; }
    }

    /// <summary>
    /// Defines how to style a drop shadow for current object
    /// </summary>
    public class DropShadow
    {
        /// <summary>
        /// Set blur distance for shadow
        /// </summary>
        public double? Blur { get; set; }

        /// <inheritdoc cref="ApexCharts.Color"/>
        /// <summary>
        /// Give a color to the shadow.
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// Enable a dropshadow for paths of the SVG
        /// </summary>
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// Set left offset for shadow
        /// </summary>
        public double? Left { get; set; }

        /// <summary>
        /// Set the opacity of shadow.
        /// </summary>
        public double? Opacity { get; set; }

        /// <summary>
        /// Set top offset for shadow
        /// </summary>
        public double? Top { get; set; }

        /// <summary>
        /// Provide series index on which the dropshadow should be enabled.
        /// </summary>
        public List<double> EnabledOnSeries { get; set; }
    }

    /// <summary>
    /// Defines localization options to use with the chart
    /// </summary>
    /// <remarks>
    /// Links:
    /// 
    /// <see href="https://apexcharts.com/docs/localization">JavaScript Documentation</see>,
    /// <see href="https://apexcharts.com/docs/options/chart/locales">JavaScript Reference</see>
    /// </remarks>
    public class ChartLocale
    {
        /// <summary>
        /// Name of the locale you will be defining options for. Can be en, fr, etc
        /// </summary>
        public string Name { get; set; }

        /// <inheritdoc cref="ApexCharts.LocaleOptions" />
        public LocaleOptions Options { get; set; }
    }

    /// <summary>
    /// Defines required properties to provide a localization
    /// </summary>
    public class LocaleOptions
    {
        /// <summary>
        /// Full names of days in your language
        /// </summary>
        public List<string> Days { get; set; }

        /// <summary>
        /// Full month names in your preferred language
        /// </summary>
        public List<string> Months { get; set; }

        /// <summary>
        /// Abbreviated day names
        /// </summary>
        public List<string> ShortDays { get; set; }

        /// <summary>
        /// Abbreviations of months
        /// </summary>
        public List<string> ShortMonths { get; set; }

        /// <inheritdoc cref="ApexCharts.LocaleToolbar" />
        public LocaleToolbar Toolbar { get; set; }
    }

    /// <summary>
    /// Defines tooltip text required to provide a localization
    /// </summary>
    public class LocaleToolbar
    {
        /// <summary>
        /// Tooltip title text which appears when you hover over menu icon
        /// </summary>
        public string Menu { get; set; }

        /// <summary>
        /// Tooltip title text which appears when you hover over pan icon
        /// </summary>
        public string Pan { get; set; }

        /// <summary>
        /// Tooltip title text which appears when you hover over reset icon
        /// </summary>
        public string Reset { get; set; }

        /// <summary>
        /// Tooltip title text which appears when you hover over selection icon
        /// </summary>
        public string Selection { get; set; }

        /// <summary>
        /// Tooltip title text which appears when you hover over selection zoom icon
        /// </summary>
        public string SelectionZoom { get; set; }

        /// <summary>
        /// Tooltip title text which appears when you hover over zoom in icon
        /// </summary>
        public string ZoomIn { get; set; }

        /// <summary>
        /// Tooltip title text which appears when you hover over zoom out icon
        /// </summary>
        public string ZoomOut { get; set; }

        /// <summary>
        /// Undocumented, this property exists in the TypeScript definition
        /// </summary>
        public string ExportToSVG { get; set; }

        /// <summary>
        /// Undocumented, this property exists in the TypeScript definition
        /// </summary>
        public string ExportToPNG { get; set; }

        /// <summary>
        /// Undocumented, this property exists in the TypeScript definition
        /// </summary>
        public string ExportToCSV { get; set; }
    }

    /// <summary>
    /// Defines options and styling to apply to the chart when items are selected within it
    /// </summary>
    /// <remarks>
    /// Links:
    /// 
    /// <see href="https://apexcharts.com/docs/options/chart/selection">JavaScript Reference</see>
    /// </remarks>
    public class Selection
    {
        /// <summary>
        /// To allow selection in axis charts. Selection will not be enabled for category x-axis charts, but only for charts having numeric x-axis. For eg., timeline charts.
        /// </summary>
        public bool Enabled { get; set; } = true;

        /// <inheritdoc cref="ApexCharts.SelectionFill" />
        public SelectionFill Fill { get; set; }

        /// <inheritdoc cref="ApexCharts.SelectionStroke" />
        public SelectionStroke Stroke { get; set; }

        /// <summary>
        /// Allow selection either on both x-axis, y-axis or on both axis.
        /// </summary>
        public AxisType Type { get; set; }

        /// <inheritdoc cref="ApexCharts.SelectionXaxis" />
        public SelectionXaxis Xaxis { get; set; }

        /// <inheritdoc cref="ApexCharts.SelectionYaxis" />
        public SelectionYaxis Yaxis { get; set; }
    }

    /// <summary>
    /// Defines styling options for the fill of selections
    /// </summary>
    public class SelectionFill
    {
        /// <summary>
        /// Background color of the selection rect which is drawn when user drags on the chart.
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Opacity of background color of the selection rect.
        /// </summary>
        public double? Opacity { get; set; }
    }

    /// <summary>
    /// Defines styling options for the stroke of selections
    /// </summary>
    public class SelectionStroke
    {
        /// <summary>
        /// Colors of selection border.
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Creates dashes in borders of svg path. Higher number creates more space between dashes in the border.
        /// </summary>
        public double? DashArray { get; set; }

        /// <summary>
        /// Opacity of selection border.
        /// </summary>
        public double? Opacity { get; set; }

        /// <summary>
        /// Border thickness of the selection rect.
        /// </summary>
        public double? Width { get; set; }
    }

    /// <summary>
    /// Defines parameters for selections on the X-axis
    /// </summary>
    public class SelectionXaxis
    {
        /// <summary>
        /// End value of x-axis. For a time-series chart, a timestamp should be provided.
        /// </summary>
        public double? Max { get; set; }

        /// <summary>
        /// Start value of x-axis. For a time-series chart, a timestamp should be provided
        /// </summary>
        public double? Min { get; set; }
    }

    /// <summary>
    /// Defines parameters for the selections on the Y-axis
    /// </summary>
    public class SelectionYaxis
    {
        /// <summary>
        /// End value of y-axis (if used in a multiple y-axes chart, this considers the 1st y-axis).
        /// </summary>
        public double? Max { get; set; }

        /// <summary>
        /// Start value of y-axis. (if used in a multiple y-axes chart, this considers the 1st y-axis).
        /// </summary>
        public double? Min { get; set; }
    }

    /// <summary>
    /// Defines options to use when creating sparklines for the chart
    /// </summary>
    /// <remarks>
    /// Links:
    /// 
    /// <see href="https://apexcharts.com/docs/options/chart/sparkline">JavaScript Reference</see>
    /// </remarks>
    public class ChartSparkline
    {
        /// <summary>
        /// Sparkline hides all the elements of the charts other than the primary paths. Helps to visualize data in small areas. <see href="https://apexcharts.com/javascript-chart-demos/sparklines/basic">Example</see>
        /// </summary>
        public bool Enabled { get; set; } = true;
    }

    /// <summary>
    /// Defines options for the toolbar to display in the top-right of the chart
    /// </summary>
    /// <remarks>
    /// Links:
    /// 
    /// <see href="https://apexcharts.com/docs/options/chart/toolbar">JavaScript Reference</see>
    /// </remarks>
    public class Toolbar
    {
        /// <summary>
        /// Automatically select one of the following tools when the chart loads.
        /// </summary>
        public AutoSelected? AutoSelected { get; set; }

        /// <summary>
        /// Sets the left offset of the toolbar.
        /// </summary>
        public double? OffsetX { get; set; }

        /// <summary>
        /// Sets the top offset of the toolbar.
        /// </summary>
        public double? OffsetY { get; set; }

        /// <summary>
        /// Display the toolbar / menu in the top right corner.
        /// </summary>
        public bool Show { get; set; } = true;

        /// <inheritdoc cref="ApexCharts.Tools" />
        public Tools Tools { get; set; }

        /// <inheritdoc cref="ApexCharts.ExportOptions" />
        public ExportOptions Export { get; set; }
    }

    /// <summary>
    /// Defines the options to display the toolbar with
    /// </summary>
    public class Tools
    {
        /// <inheritdoc cref="ApexCharts.ToolCustomIcon" />
        public List<ToolCustomIcon> CustomIcons { get; set; }

        /// <summary>
        /// Show the download menu / hamburger icon in the toolbar. 
        /// If you want to display a custom icon, you can provide HTML string in this property.
        /// 
        /// <code>
        /// download: true 
        /// download: '&lt;img src="/static/icons/download.png" class="ico-download" width="20"&gt;'
        /// </code>
        /// 
        /// ApexCharts has built-in support to allow exporting the chart to popular image formats like PNG or SVG and also allows exporting the chart data to a CSV file.
        /// By default, all XY charts have the toolbar enabled in them which has a hamburger icon at the top right corner. Clicking the hamburger icon opens a menu which has following options to download.
        /// </summary>
        public object Download { get; set; } = true;

        /// <summary>
        /// Show the panning icon in the toolbar. 
        /// If you want to display a custom icon for Pan, you can provide HTML string in this property.
        /// </summary>
        public object Pan { get; set; } = true;

        /// <summary>
        /// Reset the chart data to it's initial state after zommin/zoomout/panning. 
        /// If you want to display a custom icon for reset, you can provide HTML string in this property.
        /// </summary>
        public object Reset { get; set; } = true;

        /// <summary>
        /// Show the rectangle selection icon in the toolbar. 
        /// If you want to display a custom icon for selection, you can provide HTML string in this property.
        /// </summary>
        /// <remarks>
        /// Make sure to also enable <see cref="Chart.Selection"/> when showing the selection tool.
        /// </remarks>
        public object Selection { get; set; } = true;

        /// <summary>
        /// Show the zoom icon which is used for zooming by dragging selection on the chart area. 
        /// If you want to display a custom icon for zoom, you can provide HTML string in this property.
        /// </summary>
        public object Zoom { get; set; } = true;

        /// <summary>
        /// Show the zoom-in icon which zooms in 50% from the visible chart area. 
        /// If you want to display a custom icon for zoom-in, you can provide HTML string in this property.
        /// </summary>
        public object Zoomin { get; set; } = true;

        /// <summary>
        /// Show the zoom-out icon which zooms out 50% from the visible chart area. 
        /// If you want to display a custom icon for zoom-out, you can provide HTML string in this property.
        /// </summary>
        public object Zoomout { get; set; } = true;
    }

    /// <summary>
    /// Allows to add additional icon buttons in the toolbar. In the below example, index should be used to place at a particular position in the toolbar.
    /// </summary>
    public class ToolCustomIcon
    {
        /// <summary>
        /// Unique Id for the Custom Icon 
        /// </summary>
        [JsonIgnore]
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// URL for the icon to display
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// Ordering within the group of icons
        /// </summary>
        public double? Index { get; set; }

        /// <summary>
        /// Tooltip to display for the icon
        /// </summary>
        public string Title { get; set; } = "";

        /// <summary>
        /// A CSS class to apply to the icon
        /// </summary>
        public string Class { get; set; }

        /// <summary>
        /// Javascript function when the icon is clicked
        /// if a OnClick callback is registered this will be overwritten.
        /// </summary>
        [JsonConverter(typeof(FunctionStringConverter))]
        public string Click { get; set; }

        /// <summary>
        /// Callback when the icon is clicked
        /// </summary>
        [JsonIgnore]
        public Action<ToolCustomIcon> OnClick { get; set; }
    }

    /// <summary>
    /// Defines options to configure how zooming works on the chart
    /// </summary>
    /// <remarks>
    /// Links:
    /// 
    /// <see href="https://apexcharts.com/docs/options/chart/zoom">JavaScript Reference</see>
    /// </remarks>
    public class Zoom
    {
        /// <summary>
        /// When this option is turned on, the chart's y-axis re-scales to a new low and high based on the visible area. Helpful in situations where the user zoomed in to a small area to get a better view.
        /// </summary>
		/// <remarks>
		/// Known Issue: This option doesn't work in a multi-axis chart (when you have more than 1 y-axis)
		/// </remarks>
        public bool? AutoScaleYaxis { get; set; }

        /// <summary>
        /// To allow zooming in axis charts.
        /// </summary>
        /// <remarks>
        /// Note: In a category x-axis chart, to enable zooming, you will also need to set <see cref="XAxis.TickPlacement"/>: '<see cref="TickPlacement.On"/>'. Read more on the <see href="https://apexcharts.com/docs/zooming-in-category-x-axis">Category Axis Zoom</see> tutorial.
        /// </remarks>
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// Allow zooming either on both x-axis, y-axis or on both axis.
        /// </summary>
        /// <remarks>
        /// Known Issue: In <see href="https://apexcharts.com/javascript-chart-demos/line-charts/syncing-charts">synchronized charts</see>, <see cref="AxisType.Xy"/> or <see cref="AxisType.Y"/> will not update charts in sync, while <see cref="AxisType.X"/> will work correctly.
        /// </remarks>
        public AxisType? Type { get; set; }

        /// <inheritdoc cref="ApexCharts.ZoomedArea" />
        public ZoomedArea ZoomedArea { get; set; }
    }

    /// <summary>
    /// Zoomed Area is the area which is drawn when a user drags the mouse from one point to another
    /// </summary>
    public class ZoomedArea
    {
        /// <inheritdoc cref="ApexCharts.ZoomedAreaFill" />
        public ZoomedAreaFill Fill { get; set; }

        /// <inheritdoc cref="ApexCharts.ZoomedAreaStroke" />
        public ZoomedAreaStroke Stroke { get; set; }
    }

    /// <summary>
    /// Defines how to style the fill for the zoomed area of the chart
    /// </summary>
    public class ZoomedAreaFill
    {
        /// <summary>
        /// Background color of the selection zoomed area
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Sets the transparency level of the selection fill
        /// </summary>
        public double? Opacity { get; set; }
    }

    /// <summary>
    /// Defines how to style the stroke for the zoomed area of the chart
    /// </summary>
    public class ZoomedAreaStroke
    {
        /// <summary>
        /// Border color of the selection zoomed area
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Sets the transparency level of the selection border
        /// </summary>
        public double? Opacity { get; set; }

        /// <summary>
        /// Sets the width selection border
        /// </summary>
        public double? Width { get; set; }
    }

    /// <summary>
    /// Data Labels are the actual values which are passed in the series. You can add formatters which will allow you to modify values before displaying.
    /// </summary>
    /// <remarks>
    /// Links:
    /// 
    /// <see href="https://apexcharts.com/docs/datalabels">JavaScript Documentation</see>,
    /// <see href="https://apexcharts.com/docs/options/datalabels">JavaScript Reference</see>
    /// </remarks>
    public class DataLabels
    {
        /// <inheritdoc cref="ApexCharts.DataLabelsBackground"/>
        public DataLabelsBackground Background { get; set; }

        /// <summary>
        /// Similar to plotOptions.bar.distributed, this option makes each data-label discrete. So, when you provide an array of colors in datalabels.style.colors, the index in the colors array correlates with individual data-label index of all series.
        /// </summary>
        public bool? Distributed { get; set; }

        /// <inheritdoc cref="ApexCharts.DropShadow"/>
        public DropShadow DropShadow { get; set; }

        /// <summary>
        /// To determine whether to show dataLabels or not
        /// </summary>
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// Allows showing series only on specific series in a multi-series chart. For eg., if you have a line and a column chart, you can show dataLabels only on the line chart by specifying it's index in this array property.
        /// </summary>
        public List<double> EnabledOnSeries { get; set; }

        /// <summary>
        /// Sets the left offset for dataLabels
        /// </summary>
        public double? OffsetX { get; set; }

        /// <summary>
        /// Sets the top offset for dataLabels
        /// </summary>
        public double? OffsetY { get; set; }

        /// <inheritdoc cref="ApexCharts.DataLabelsStyle"/>
        public DataLabelsStyle Style { get; set; }

        /// <summary>
        /// The alignment of text relative to dataLabel's drawing position
        /// </summary>
        public TextAnchor? TextAnchor { get; set; }

        /// <summary>
        /// The formatter function allows you to modify the value before displaying. Example:
        /// 
        /// <code>
        /// formatter: function(value, { seriesIndex, dataPointIndex, w }) {
        ///     return w.config.series[seriesIndex].name + ":  " + value
        /// }
        /// </code>
        /// 
        /// In the code above, seriesIndex is useful in multi-series chart, while dataPointIndex is the index of data-point in that series. w is an object consisting all globals and configuration which can be utilized the way mentioned in the above code.
        /// </summary>
        [JsonConverter(typeof(FunctionStringConverter))]
        public string Formatter { get; set; }
    }

    /// <summary>
    /// Defines the styling options to apply to the background for data labels
    /// </summary>
    public class DataLabelsBackground
    {
        /// <summary>
        /// Border color of the background rect.
        /// </summary>
        public string BorderColor { get; set; }

        /// <summary>
        /// Border radius of the background rect.
        /// </summary>
        public double? BorderRadius { get; set; }

        /// <summary>
        /// Border width of the background rect.
        /// </summary>
        public double? BorderWidth { get; set; }

        /// <inheritdoc cref="ApexCharts.DropShadow" />
        public DropShadow DropShadow { get; set; }

        /// <summary>
        /// Should draw a background rectangle around the label
        /// </summary>
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// Color of the label when background is enabled. This will override the <see cref="DataLabelsStyle.Colors"/>.
        /// </summary>
        public string ForeColor { get; set; }

        /// <summary>
        /// Opacity of the background color.
        /// </summary>
        public double? Opacity { get; set; }

        /// <summary>
        /// The amount of padding to apply to the data label background
        /// </summary>
        public double? Padding { get; set; }
    }

    /// <summary>
    /// Defines the styling options to apply to the data labels
    /// </summary>
    public class DataLabelsStyle
    {
        /// <summary>
        /// Fore colors for the dataLabels. Accepts an array of string colors (['#333', '#999']) or an array of functions ([function(opts) { return '#333' }]) (Each index in the array corresponds to the series).
        /// </summary>
        public List<string> Colors { get; set; }

        /// <summary>
        /// Font family for the label
        /// </summary>
        public string FontFamily { get; set; }

        /// <summary>
        /// Font size for the label
        /// </summary>
        public string FontSize { get; set; }

        /// <summary>
        /// Font weight for the label. Can be String ('bold') or number (400/500)
        /// </summary>
        public object FontWeight { get; set; }
    }

    /// <summary>
    /// Class to define how the generated chart will be styled.
    /// </summary>
    /// <remarks>
    /// Links:
    /// 
    /// <see href="https://apexcharts.com/docs/options/fill">JavaScript Reference</see>
    /// </remarks>
    public class Fill
    {
        /// <summary>
        /// Colors to fill the svg paths. Each index in the array corresponds to the series-index. Example:
        /// 
        /// <code>
        /// fill: {
        ///     colors: ['#1A73E8', '#B32824']
        /// }
        /// </code>
        /// 
        /// Alternatively, if you are rendering a bar/pie/donut/radialBar chart, you can pass a function which returns color based on value. Example:
        /// 
        /// <code>
        /// fill: {
        ///     colors: [function({ value, seriesIndex, w }) {
        ///         if(value &lt; 55) {
        ///             return '#7E36AF'
        ///         } else if (value &gt;= 55 &amp;&amp; value &lt; 80) {
        ///             return '#164666'
        ///         } else {
        ///             return '#D9534F'
        ///         }
        ///     }]
        /// }
        /// </code>
        /// </summary>
        public List<string> Colors { get; set; }

        /// <inheritdoc cref="ApexCharts.FillGradient"/>
        public FillGradient Gradient { get; set; }

        /// <inheritdoc cref="ApexCharts.FillImage"/>
        public FillImage Image { get; set; }

        /// <inheritdoc cref="ApexCharts.Opacity"/>
        /// <summary>
        /// Opacity of the fill attribute.
        /// </summary>
        public Opacity Opacity { get; set; }

        /// <inheritdoc cref="ApexCharts.FillPattern"/>
        public FillPattern Pattern { get; set; }

        /// <inheritdoc cref="ApexCharts.FillTypeSelections"/>
        /// <summary>
        /// Whether to fill the paths with solid colors or gradient.
        /// </summary>
        public FillTypeSelections Type { get; set; }
    }

    /// <summary>
    /// Defines the styling options to use when filling a chart with a gradient. <see href="https://codepen.io/apexcharts/pen/GQmoXP">Example</see>
    /// </summary>
    public class FillGradient
    {
        /// <summary>
        /// Optional colors that ends the gradient to. The main colors array becomes the gradientFromColors and this array becomes the end colors of the gradient. Each index in the array corresponds to the series-index.
        /// </summary>
        public List<string> GradientToColors { get; set; }

        /// <summary>
        /// Inverse the start and end colors of the gradient.
        /// </summary>
        public bool? InverseColors { get; set; }

        /// <inheritdoc cref="ApexCharts.Opacity"/>
        /// <summary>
        /// Start color's opacity.
        /// </summary>
        public Opacity OpacityFrom { get; set; }

        /// <inheritdoc cref="ApexCharts.Opacity"/>
        /// <summary>
        /// End color's opacity
        /// </summary>
        public Opacity OpacityTo { get; set; }

        /// <summary>
        /// The option to use for shading the gradient
        /// </summary>
        public GradientShade? Shade { get; set; }

        /// <summary>
        /// Intensity of the gradient shade. Accepts from 0 to 1
        /// </summary>
        public double? ShadeIntensity { get; set; }

        /// <summary>
        /// Stops defines the ramp of colors to use on a gradient
        /// </summary>
        public List<double> Stops { get; set; }

        /// <summary>
        /// The type of gradient to generate
        /// </summary>
        public GradientType Type { get; set; }

        /// <inheritdoc cref="ApexCharts.FillGradientStops"/>
        public List<FillGradientStops> ColorStops { get; set; }
    }

    /// <summary>
    /// Override everything and define your own stops with unlimited color stops. <see href="https://codepen.io/apexcharts/pen/RvqdPb">Codepen Example</see>
    /// </summary>
    public class FillGradientStops
    {
        /// <summary>
        /// The location within the chart to apply the gradient stop at
        /// </summary>
        public double? Offset { get; set; }

        /// <summary>
        /// The color to apply at the gradient stop
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// The opacity to apply at the gradient stop. Accepts values from 0 to 1
        /// </summary>
        public double? Opacity { get; set; }
    }

    /// <summary>
    /// Defines the styling options to use when filling a chart with an image
    /// </summary>
    public class FillImage
    {
        /// <summary>
        /// Height of each image for all the series
        /// </summary>
        public double? Height { get; set; }

        /// <inheritdoc cref="ApexCharts.ImagePaths"/>
        /// <summary>
        /// The URL for each image to fill the chart series with
        /// </summary>
        public ImagePaths Src { get; set; }

        /// <summary>
        /// Width of each image for all the series
        /// </summary>
        public double? Width { get; set; }
    }

    /// <summary>
    /// Defines the styling options to use when filling a chart with a pattern
    /// </summary>
    public class FillPattern
    {
        /// <summary>
        /// Pattern height which will be repeated at this interval
        /// </summary>
        public double? Height { get; set; }

        /// <summary>
        /// Pattern lines width indicates the thickness of the stroke of pattern.
        /// </summary>
        public double? StrokeWidth { get; set; }

        /// <inheritdoc cref="ApexCharts.FillPatternStyleSelections"/>
        /// <summary>
        /// The type of pattern to fill the chart with
        /// </summary>
        public FillPatternStyleSelections Style { get; set; }

        /// <summary>
        /// Pattern width which will be repeated at this interval
        /// </summary>
        public double? Width { get; set; }
    }

    /// <summary>
    /// Grid is the plot area excluding legends, title, subtitle, x-axis, and y-axis. Grid's coordinates are used extensively in calculations in the chart in determining where to plot the actual chart elements.
    /// </summary>
    /// <remarks>
    /// Links:
    /// 
    /// <see href="https://apexcharts.com/docs/grid">JavaScript Documentation</see>,
    /// <see href="https://apexcharts.com/docs/options/grid">JavaScript Reference</see>
    /// </remarks>
    public class Grid
    {
        /// <summary>
        /// Colors of grid borders / lines
        /// </summary>
        public string BorderColor { get; set; }

        /// <inheritdoc cref="ApexCharts.GridColumn"/>
        public GridColumn Column { get; set; }

        /// <inheritdoc cref="ApexCharts.Padding"/>
        public Padding Padding { get; set; }

        /// <summary>
        /// Whether to place grid behind chart paths of in front.
        /// </summary>
        public GridPosition? Position { get; set; }

        /// <inheritdoc cref="ApexCharts.GridRow"/>
        public GridRow Row { get; set; }

        /// <summary>
        /// To show or hide grid area (including xaxis / yaxis)
        /// </summary>
        public bool? Show { get; set; }

        /// <summary>
        /// Creates dashes in borders of svg path. Higher number creates more space between dashes in the border.
        /// </summary>
        public double? StrokeDashArray { get; set; }

        /// <inheritdoc cref="ApexCharts.GridXAxis"/>
        public GridXAxis Xaxis { get; set; }

        /// <inheritdoc cref="ApexCharts.GridYAxis"/>
        public GridYAxis Yaxis { get; set; }
    }

    /// <summary>
    /// Defines how to style the columns for the grid
    /// </summary>
    public class GridColumn
    {
        /// <summary>
        /// Grid background colors filling in column pattern. Each column will be filled with colors based on the index in this array. If less colors are specified, colors are repeated.
        /// </summary>
        public List<string> Colors { get; set; }

        /// <summary>
        /// Opacity of the column background colors. Accepts values from 0 to 1
        /// </summary>
        public double? Opacity { get; set; }
    }

    /// <summary>
    /// Defines how to style the rows for the grid
    /// </summary>
    public class GridRow
    {
        /// <summary>
        /// Grid background colors filling in row pattern. Each row will be filled with colors based on the index in this array. If less colors are specified, colors are repeated.
        /// </summary>
        public List<string> Colors { get; set; }

        /// <summary>
        /// Opacity of the row background colors. Accepts values from 0 to 1
        /// </summary>
        public double? Opacity { get; set; }
    }

    /// <summary>
    /// Defines how to style the X-axis for the grid
    /// </summary>
    public class GridXAxis
    {
        /// <inheritdoc cref="ApexCharts.Lines" />
        public Lines Lines { get; set; }
    }

    /// <summary>
    /// Defines options for grid lines
    /// </summary>
    public class Lines
    {
        /// <summary>
        /// Undocumented, this property exists in the TypeScript definition
        /// </summary>
        public double? OffsetX { get; set; }

        /// <summary>
        /// Undocumented, this property exists in the TypeScript definition
        /// </summary>
        public double? OffsetY { get; set; }

        /// <summary>
        /// Whether to show / hide y-axis lines
        /// </summary>
        public bool? Show { get; set; }
    }

    /// <summary>
    /// Defines how to style the Y-axis for the grid
    /// </summary>
    public class GridYAxis
    {
        /// <inheritdoc cref="ApexCharts.Lines" />
        public Lines Lines { get; set; }
    }

    /// <summary>
    /// When there are multiple dataSeries in the chart, legends help to identify each dataSeries with a predefined symbol and name of the series.
    /// </summary>
    /// <remarks>
    /// Links:
    /// 
    /// <see href="https://apexcharts.github.io/Blazor-ApexCharts/features/legend">Blazor Example</see>,
    /// <see href="https://apexcharts.com/docs/legend">JavaScript Documentation</see>,
    /// <see href="https://apexcharts.com/docs/options/legend">JavaScript Reference</see>
    /// </remarks>
    public class Legend
    {
        /// <summary>
        /// Undocumented, this property exists in the TypeScript definition
        /// </summary>
        public LegendContainerMargin ContainerMargin { get; set; }

        /// <summary>
        /// Undocumented, this property exists in the TypeScript definition
        /// </summary>
        public string TextAnchor { get; set; }

        /// <summary>
        /// The floating option will take out the legend from the chart area and make it float above the chart.
        /// </summary>
        public bool? Floating { get; set; }

        /// <summary>
        /// Sets the font-family of legend text elements
        /// </summary>
        public string FontFamily { get; set; }

        /// <summary>
        /// Sets the fontSize of legend text elements
        /// </summary>
        public string FontSize { get; set; }

        /// <summary>
        /// Sets the font-weight of legend text elements
        /// </summary>
        public object FontWeight { get; set; }

        /// <summary>
        /// Sets the height for legend container
        /// </summary>
        public double? Height { get; set; }

        /// <summary>
        /// Available options for horizontal alignment
        /// </summary>
        public Align? HorizontalAlign { get; set; }

        /// <summary>
        /// Inverse the placement ordering of the legend items.
        /// </summary>
        public bool? InverseOrder { get; set; }

        /// <inheritdoc cref="ApexCharts.LegendItemMargin"/>
        public LegendItemMargin ItemMargin { get; set; }

        /// <inheritdoc cref="ApexCharts.LegendLabels"/>
        public LegendLabels Labels { get; set; }

        /// <inheritdoc cref="ApexCharts.LegendMarkers"/>
        public LegendMarkers Markers { get; set; }

        /// <summary>
        /// Sets the left offset of the marker
        /// </summary>
        public double? OffsetX { get; set; }

        /// <summary>
        /// Sets the top offset of the marker
        /// </summary>
        public double? OffsetY { get; set; }

        /// <inheritdoc cref="ApexCharts.LegendOnItemClick"/>
        public LegendOnItemClick OnItemClick { get; set; }

        /// <inheritdoc cref="ApexCharts.LegendOnItemHover"/>
        public LegendOnItemHover OnItemHover { get; set; }

        /// <summary>
        /// Available position options for legend
        /// </summary>
        public LegendPosition? Position { get; set; }

        /// <summary>
        /// Whether to show or hide the legend container.
        /// </summary>
        public bool? Show { get; set; }

        /// <summary>
        /// Allows you to hide a particular legend if it's series contains all null values.
        /// </summary>
        public bool? ShowForNullSeries { get; set; }

        /// <summary>
        /// Show legend even if there is just 1 series.
        /// </summary>
        public bool? ShowForSingleSeries { get; set; }

        /// <summary>
        /// Allows you to hide a particular legend if it's series contains all 0 values.
        /// </summary>
        public bool? ShowForZeroSeries { get; set; }

        /// <summary>
        /// Sets the width for legend container
        /// </summary>
        public double? Width { get; set; }

        /// <summary>
        /// A custom formatter function to append additional text to the legend series names. Example:
        /// 
        /// <code>
        /// legend: {
        ///     formatter: function(seriesName, opts) {
        ///         return [seriesName, " - ", opts.w.globals.series[opts.seriesIndex]]
        ///     }
        /// },
        /// </code>
        /// </summary>
        [JsonConverter(typeof(FunctionStringConverter))]
        public string Formatter { get; set; }

        /// <summary>
        /// A formatter function to allow showing data values in the legend while hovering on the chart. This can be useful when you have multiple series, and you don't want to show tooltips for each series together. Example:
        /// 
        /// <code>
        /// legend: {
        ///     tooltipHoverFormatter: function(seriesName, opts) {
        ///         return seriesName + ' - &lt;strong&gt;' + opts.w.globals.series[opts.seriesIndex][opts.dataPointIndex] + '&lt;/strong&gt;'
        ///     }
        /// },
        /// </code>
        /// </summary>
        /// <remarks>
        /// Note: This feature is only available in shared tooltips (when you have <see cref="Tooltip.Shared"/>: <see langword="true"/>).
        /// </remarks>
        [JsonConverter(typeof(FunctionStringConverter))]
        public string TooltipHoverFormatter { get; set; }

        /// <summary>
        /// Allows you to overwrite the default legend items with this customized set of labels. Please note that the click/hover events of the legend will stop working if you provide these custom legend labels.
        /// </summary>
        public List<string> CustomLegendItems { get; set; }
    }

    /// <summary>
    /// Undocumented, this class exists in the TypeScript definition
    /// </summary>
    public class LegendContainerMargin
    {
        /// <summary>
        /// Undocumented, this property exists in the TypeScript definition
        /// </summary>
        public double? Left { get; set; }

        /// <summary>
        /// Undocumented, this property exists in the TypeScript definition
        /// </summary>
        public double? Top { get; set; }
    }

    /// <summary>
    /// Defines the margin to apply to legend items
    /// </summary>
    public class LegendItemMargin
    {
        /// <summary>
        /// Horizontal margin for individual legend item.
        /// </summary>
        public double? Horizontal { get; set; }

        /// <summary>
        /// Vertical margin for individual legend item.
        /// </summary>
        public double? Vertical { get; set; }
    }

    /// <summary>
    /// Defines the styling to apply to legend item labels
    /// </summary>
    public class LegendLabels
    {
        /// <inheritdoc cref="ApexCharts.Color"/>
        /// <summary>
        /// Custom text colors for legend labels.
        /// </summary>
        public Color Colors { get; set; }

        /// <summary>
        /// Whether to use primary <see href="https://apexcharts.com/docs/colors">colors</see> or not.
        /// </summary>
        public bool? UseSeriesColors { get; set; }
    }

    /// <summary>
    /// Defines the styling to apply to legend item markers
    /// </summary>
    public class LegendMarkers
    {
        /// <summary>
        /// Fill Colors of the marker point.
        /// </summary>
        public List<string> FillColors { get; set; }

        /// <summary>
        /// Height of the marker.
        /// </summary>
        public double? Height { get; set; }

        /// <summary>
        /// Sets the left offset of the marker
        /// </summary>
        public double? OffsetX { get; set; }

        /// <summary>
        /// Sets the top offset of the marker
        /// </summary>
        public double? OffsetY { get; set; }

        /// <summary>
        /// Border Radius of the marker
        /// </summary>
        public double? Radius { get; set; }

        /// <summary>
        /// Stroke Color of the marker point.
        /// </summary>
        public string StrokeColor { get; set; }

        /// <summary>
        /// Stroke Size of the marker point.
        /// </summary>
        public double? StrokeWidth { get; set; }

        /// <summary>
        /// Width of the marker that appears before series name.
        /// </summary>
        public double? Width { get; set; }

        /// <summary>
        /// Custom HTML element to put in place of marker.
        /// 
        /// <code>
        /// markers: {
        ///     customHTML: function() {
        ///         return '&lt;span class="custom-marker"&gt;&lt;i class="fas fa-chart-pie"&gt;&lt;/i&gt;&lt;/span&gt;'
        ///     }
        /// }
        /// </code>
        /// </summary>
        [JsonConverter(typeof(FunctionStringConverter))]
        public string CustomHTML { get; set; }
    }

    /// <summary>
    /// Defines options to execute for when a legend item is clicked
    /// </summary>
    public class LegendOnItemClick
    {
        /// <summary>
        /// When clicked on legend item, it will toggle the visibility of the series in chart.
        /// </summary>
        public bool? ToggleDataSeries { get; set; }
    }

    /// <summary>
    /// Defines options to execute for when a legend item is hovered
    /// </summary>
    public class LegendOnItemHover
    {
        /// <summary>
        /// When hovered on legend item, it will highlight the paths of the hovered series in chart.
        /// </summary>
        public bool? HighlightDataSeries { get; set; }
    }

    /// <summary>
    /// Class to define the visual appearance of the data point markers
    /// </summary>
    /// <remarks>
    /// Links:
    /// 
    /// <see href="https://apexcharts.com/docs/options/markers">JavaScript Reference</see>
    /// </remarks>
    public class Markers
    {
        /// <inheritdoc cref="ApexCharts.Color"/>
        /// <summary>
        /// Sets the fill color(s) of the marker point.
        /// </summary>
        public Color Colors { get; set; }

        /// <summary>
        /// Allows you to target individual data-points and style particular marker differently. Example:
        /// 
        /// <code>
        /// markers: {
        ///     discrete: [{
        ///         seriesIndex: 0,
        ///         dataPointIndex: 7,
        ///         fillColor: '#e3e3e3',
        ///         strokeColor: '#fff',
        ///         size: 5,
        ///         shape: "circle"
        ///     }, {
        ///         seriesIndex: 2,
        ///         dataPointIndex: 11,
        ///         fillColor: '#f7f4f3',
        ///         strokeColor: '#eee',
        ///         size: 4,
        ///         shape: "square"
        ///     }]
        /// }
        /// </code>
        /// </summary>
        public List<MarkersDiscrete> Discrete { get; set; }

        /// <inheritdoc cref="ApexCharts.Opacity"/>
        /// <summary>
        /// Opacity of the marker fill color.
        /// </summary>
        public Opacity FillOpacity { get; set; }

        /// <inheritdoc cref="ApexCharts.MarkersHover"/>
        public MarkersHover Hover { get; set; }

        /// <summary>
        /// Sets the left offset of the marker
        /// </summary>
        public double? OffsetX { get; set; }

        /// <summary>
        /// Sets the top offset of the marker
        /// </summary>
        public double? OffsetY { get; set; }

        /// <summary>
        /// Radius of the marker (applies to square shape)
        /// </summary>
        public double? Radius { get; set; }

        /// <summary>
        /// Shape of the marker.
        /// </summary>
        public ShapeEnum? Shape { get; set; }

        /// <summary>
        /// Whether to show markers for null values in a line/area chart. If disabled, any null values present in line/area charts will not be visible.
        /// </summary>
        public bool? ShowNullDataPoints { get; set; }

        /// <inheritdoc cref="ApexCharts.Size"/>
        /// <summary>
        /// Size of the marker point.
        /// </summary>
        public Size Size { get; set; }

        /// <inheritdoc cref="ApexCharts.Color"/>
        /// <summary>
        /// Stroke Color of the marker.
        /// </summary>
        public Color StrokeColors { get; set; }

        /// <inheritdoc cref="ApexCharts.Size"/>
        /// <summary>
        /// Dashes in the border around marker. Higher number creates more space between dashes in the border.
        /// </summary>
        public Size StrokeDashArray { get; set; }

        /// <inheritdoc cref="ApexCharts.Opacity"/>
        /// <summary>
        /// Opacity of the border around marker.
        /// </summary>
        public Opacity StrokeOpacity { get; set; }

        /// <inheritdoc cref="ApexCharts.Size"/>
        /// <summary>
        /// Stroke Size of the marker.
        /// </summary>
        public Size StrokeWidth { get; set; }

        /// <summary>
        /// Undocumented, this property exists in the TypeScript definition
        /// </summary>
        public Size Width { get; set; }

        /// <summary>
        /// Undocumented, this property exists in the TypeScript definition
        /// </summary>
        public Size Height { get; set; }
    }

    /// <summary>
    /// Allows you to target individual data-points and style particular markers differently
    /// </summary>
    public class MarkersDiscrete
    {
        /// <summary>
        /// The index of the marker to apply styling to
        /// </summary>
        public double? DataPointIndex { get; set; }

        /// <summary>
        /// The color to fill the marker with
        /// </summary>
        public string FillColor { get; set; }

        /// <summary>
        /// The index within the data series to apply styling to
        /// </summary>
        public double? SeriesIndex { get; set; }

        /// <summary>
        /// The size to apply to the marker
        /// </summary>
        public double? Size { get; set; }

        /// <summary>
        /// The stroke color to apply to the marker
        /// </summary>
        public string StrokeColor { get; set; }

        /// <summary>
        /// The type of shape to use for the marker
        /// </summary>
        public ShapeEnum? Shape { get; set; }
    }

    /// <summary>
    /// Defines styling properties for when a marker is hovered
    /// </summary>
    public class MarkersHover
    {
        /// <summary>
        /// Fixed size of the marker when it is active
        /// </summary>
        public double? Size { get; set; }

        /// <summary>
        /// Unlike the fixed size, this option takes the original markers.size and increases/decreases the value based on it. So, if markers.size: 6, markers.hover.sizeOffset: 3 will make the marker's size 9 when hovered.
        /// </summary>
        public double? SizeOffset { get; set; }
    }

    /// <summary>
    /// Class to define information to display when no data is available for the chart. Useful when loading data from external sources.
    /// </summary>
    /// <remarks>
    /// Links:
    /// 
    /// <see href="https://apexcharts.github.io/Blazor-ApexCharts/features/chart-data">Blazor Example</see>,
    /// <see href="https://apexcharts.com/docs/options/nodata">JavaScript Reference</see>
    /// </remarks>
    public class NoData
    {
        /// <summary>
        /// Determines where to display the loading message
        /// </summary>
        public Align? Align { get; set; }

        /// <summary>
        /// Text offset from left
        /// </summary>
        public double? OffsetX { get; set; }

        /// <summary>
        /// Text offset from top
        /// </summary>
        public double? OffsetY { get; set; }

        /// <inheritdoc cref="ApexCharts.NoDataStyle"/>
        public NoDataStyle Style { get; set; }

        /// <summary>
        /// The text to display when no-data is available. Defaults to undefined which displays nothing.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Determines where to display the loading message
        /// </summary>
        public VerticalAlign? VerticalAlign { get; set; }
    }

    /// <summary>
    /// Styling options to apply when there is no data in the chart
    /// </summary>
    public class NoDataStyle
    {
        /// <summary>
        /// Fore color of the text
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Font family of the text
        /// </summary>
        public string FontFamily { get; set; }

        /// <summary>
        /// Font size of the text
        /// </summary>
        public string FontSize { get; set; }
    }

    /// <summary>
    /// Container class to store options for each specific type of chart
    /// </summary>
    /// <remarks>
    /// Links:
    /// 
    /// <see href="https://apexcharts.com/docs/options/plotoptions">JavaScript Reference</see>
    /// </remarks>
    public class PlotOptions
    {
        /// <inheritdoc cref="ApexCharts.PlotOptionsArea" />
        public PlotOptionsArea Area { get; set; }

        /// <inheritdoc cref="ApexCharts.PlotOptionsBar" />
        public PlotOptionsBar Bar { get; set; }

        /// <inheritdoc cref="ApexCharts.PlotOptionsBubble" />
        public PlotOptionsBubble Bubble { get; set; }

        /// <inheritdoc cref="ApexCharts.PlotOptionsCandlestick" />
        public PlotOptionsCandlestick Candlestick { get; set; }

        /// <inheritdoc cref="ApexCharts.PlotOptionsBoxPlot" />
        public PlotOptionsBoxPlot BoxPlot { get; set; }

        /// <inheritdoc cref="ApexCharts.PlotOptionsHeatmap" />
        public PlotOptionsHeatmap Heatmap { get; set; }

        /// <inheritdoc cref="ApexCharts.PlotOptionsPie" />
        public PlotOptionsPie Pie { get; set; }

        /// <inheritdoc cref="ApexCharts.PlotOptionsPolarArea" />
        public PlotOptionsPolarArea PolarArea { get; set; }

        /// <inheritdoc cref="ApexCharts.PlotOptionsRadar" />
        public PlotOptionsRadar Radar { get; set; }

        /// <inheritdoc cref="ApexCharts.PlotOptionsRadialBar" />
        public PlotOptionsRadialBar RadialBar { get; set; }

        /// <inheritdoc cref="ApexCharts.PlotOptionsTreemap" />
        public PlotOptionsTreemap Treemap { get; set; }

        /// <inheritdoc cref="ApexCharts.PlotOptionsLine" />
        public PlotOptionsLine Line { get; set; }
        
    }

    /// <summary>
    /// Defines options specific to <see cref="ChartType.Line"/>
    /// </summary>
    /// <remarks>
    /// Links:
    /// 
    /// <see href="https://apexcharts.com/docs/options/plotoptions/line">JavaScript Reference</see>
    /// </remarks>
    public class PlotOptionsLine
    {
        /// <summary>
        /// Indicates if the line should be rendered as a Slope Chart
        /// Default is false
        /// </summary>
        public bool? IsSlopeChart { get; set; }
    }

        /// <summary>
        /// Defines options specific to <see cref="ChartType.Area"/>
        /// </summary>
        /// <remarks>
        /// Links:
        /// 
        /// <see href="https://apexcharts.github.io/Blazor-ApexCharts/area-charts">Blazor Example</see>,
        /// <see href="https://apexcharts.com/docs/chart-types/area-chart">JavaScript Documentation</see>,
        /// <see href="https://apexcharts.com/docs/options/plotoptions/area">JavaScript Reference</see>
        /// </remarks>
        public class PlotOptionsArea
    {
        /// <summary>
        /// When negative values are present in the area chart, this option fill the area either from 0 (origin) or from the end of the chart as illustrated below.
        /// </summary>
        public AreaFillTo? FillTo { get; set; }
    }

    /// <summary>
    /// Defines options specific to <see cref="ChartType.BoxPlot"/>
    /// </summary>
    /// <remarks>
    /// Links:
    /// 
    /// <see href="https://apexcharts.github.io/Blazor-ApexCharts/boxplot-charts">Blazor Example</see>,
    /// <see href="https://apexcharts.com/docs/chart-types/boxplot">JavaScript Documentation</see>,
    /// <see href="https://apexcharts.com/docs/options/plotoptions/boxplot">JavaScript Reference</see>
    /// </remarks>
    public class PlotOptionsBoxPlot
    {
        /// <inheritdoc cref="ApexCharts.BloxPlotColors" />
        public BloxPlotColors Colors { get; set; }
    }

    /// <summary>
    /// Defines colors to apply to the box plot chart
    /// </summary>
    public class BloxPlotColors
    {
        /// <summary>
        /// Color for the upper quartile (Q3 to median) of the box plot.
        /// </summary>
        public string Upper { get; set; }

        /// <summary>
        /// Color for the lower quartile (median to Q1) of the box plot.
        /// </summary>
        public string Lower { get; set; }
    }

    /// <summary>
    /// Defines options specific to <see cref="ChartType.Bar"/>
    /// </summary>
    /// <remarks>
    /// Links:
    /// 
    /// <see href="https://apexcharts.github.io/Blazor-ApexCharts/bar-charts">Blazor Example</see>,
    /// <see href="https://apexcharts.com/docs/chart-types/bar-chart">JavaScript Documentation</see>,
    /// <see href="https://apexcharts.com/docs/options/plotoptions/bar">JavaScript Reference</see>
    /// </remarks>
    public class PlotOptionsBar
    {
        /// <summary>
        /// This option will turn a column chart into a horizontal bar chart.
        /// </summary>
        public bool? Horizontal { get; set; }

        /// <summary>
        /// Rounded corners around the bars/columns.
        /// </summary>
        public double? BorderRadius { get; set; }

        /// <summary>
        /// Whether to apply border-radius around both ends or only to single end
        /// </summary>
        public BorderRadiusApplication? BorderRadiusApplication { get; set; }

        /// <summary>
        /// Whether to apply border-radius to all bars or only to the data-set that is drawn last
        /// </summary>
        public BorderRadiusWhenStacked? BorderRadiusWhenStacked { get; set; }

        /// <summary>
        /// In column charts, columnWidth is the percentage of the available width in the grid-rect. Accepts '0%' to '100%'
        /// </summary>
        public string ColumnWidth { get; set; }

        /// <summary>
        /// In horizontal bar charts, barHeight is the percentage of the available height in the grid-rect. Accepts '0%' to '100%'
        /// </summary>
        public string BarHeight { get; set; }

        /// <summary>
        /// Turn this option to make the bars discrete. Each value indicates one bar per series.
        /// </summary>
        public bool? Distributed { get; set; }

        /// <summary>
        /// In a range-Bar / timeline chart, the bars should overlap over each other instead of stacking if this option is enabled.
        /// </summary>
        public bool? RangeBarOverlap { get; set; }

        /// <summary>
        /// In a multi-series range-Bar / timeline chart, group rows (horizontal bars) together instead of stacking up. Helpful when there are no overlapping rows but distinct values.
        /// </summary>
        public bool? RangeBarGroupRows { get; set; }

        /// <summary>
        /// In a non-stacked (grouped or clustered) bar chart, do not occupy space for those bars whose value is zero.
        /// </summary>
        public bool? HideZeroBarsWhenGrouped { get; set; }

        /// <summary>
        /// Is chart a dumbbell
        /// </summary>
        public bool? IsDumbbell { get; set; }

        /// <summary>
        /// When dumbbell chart is enabled, use this option to set custom colors for the dots at the starting and ending shape.
        /// </summary>
        public List<string> DumbbellColors { get; set; }

        /// <summary>
        /// Is chart funnel
        /// </summary>
        public bool? IsFunnel { get; set; }

        /// <summary>
        /// Chart is funnel 3D
        /// </summary>
        public bool? IsFunnel3d { get; set; }

        /// <inheritdoc cref="ApexCharts.PlotOptionsBarColors" />
        public PlotOptionsBarColors Colors { get; set; }

        /// <inheritdoc cref="ApexCharts.PlotOptionsBarDataLabels" />
        public PlotOptionsBarDataLabels DataLabels { get; set; }
    }

    /// <summary>
    /// Defines how to color the bar chart
    /// </summary>
    public class PlotOptionsBarColors
    {
        /// <summary>
        /// Custom colors for background rects. The number of colors in the array is repeated if fewer colors than data points are specified.
        /// </summary>
        public List<string> BackgroundBarColors { get; set; }

        /// <summary>
        /// Opacity for background colors of the bar
        /// </summary>
        public double? BackgroundBarOpacity { get; set; }

        /// <summary>
        /// Border radius for background rect of the bar
        /// </summary>
        public double? BackgroundBarRadius { get; set; }

        /// <inheritdoc cref="ApexCharts.PlotOptionsBarColorRange" />
        public List<PlotOptionsBarColorRange> Ranges { get; set; }
    }

    /// <summary>
    /// Sets the portion of the bar chart to color
    /// </summary>
    public class PlotOptionsBarColorRange
    {
        /// <summary>
        /// Color to fill the range with
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Value indicating range's upper limit
        /// </summary>
        public double? From { get; set; }

        /// <summary>
        /// Value indicating range's lower limit
        /// </summary>
        public double? To { get; set; }
    }

    /// <summary>
    /// Defines text to display with bars in the bar chart
    /// </summary>
    public class PlotOptionsBarDataLabels
    {
        /// <summary>
        /// When there are values that are very close to one another, this property prevents it by hiding overlapping labels. Note: This affects only stacked charts
        /// </summary>
        public bool? HideOverflowingLabels { get; set; }

        /// <summary>
        /// Maximum limit of data-labels that can be displayed on a bar chart. If data-points exceed this number, data-labels won't be shown.
        /// </summary>
        public double? MaxItems { get; set; }

        /// <summary>
        /// How to display the text
        /// </summary>
        public Orientation? Orientation { get; set; }

        /// <summary>
        /// Where to display the text
        /// </summary>
        public BarDataLabelPosition Position { get; set; }

        /// <inheritdoc cref="ApexCharts.BarTotalDataLabels" />
        public BarTotalDataLabels Total { get; set; }
    }

    /// <summary>
    /// Defines how to display total values for the bars in the bar chart
    /// </summary>
    public class BarTotalDataLabels
    {
        /// <summary>
        /// Enable total data-label in stacked bar charts which adds all the values of the category for those stacked data-sets
        /// </summary>
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// Applies a custom function for the total value. The function accepts 2 params where the 1st one is the value while the 2nd one is the config object.
        /// </summary>
        [JsonConverter(typeof(FunctionStringConverter))]
        public string Formatter { get; set; }

        /// <summary>
        /// Sets the left offset of the total label
        /// </summary>
        public double? OffsetX { get; set; }

        /// <summary>
        /// Sets the top offset of the total label
        /// </summary>
        public double? OffsetY { get; set; }

        /// <inheritdoc cref="ApexCharts.BarDataLabelsStyle" />
        public BarDataLabelsStyle Style { get; set; }
    }

    /// <summary>
    /// Defines how to style the bar chart total labels
    /// </summary>
    public class BarDataLabelsStyle
    {
        /// <summary>
        /// Color of the total label
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Font size of the total label
        /// </summary>
        public string FontSize { get; set; }

        /// <summary>
        /// Font family of the total label
        /// </summary>
        public string FontFamily { get; set; }

        /// <summary>
        /// Font-weight of the total label
        /// </summary>
        public object FontWeight { get; set; }
    }

    /// <summary>
    /// Defines options specific to <see cref="ChartType.Bubble"/>
    /// </summary>
    /// <remarks>
    /// Links:
    /// 
    /// <see href="https://apexcharts.github.io/Blazor-ApexCharts/bubble-charts">Blazor Example</see>,
    /// <see href="https://apexcharts.com/docs/options/plotoptions/bubble">JavaScript Reference</see>
    /// </remarks>
    public class PlotOptionsBubble
    {
        /// <summary>
        /// zScaling controls whether to draw the bubbles of different size (based on z value) or to draw same size bubbles (helpful in case your data-set contains values that extends to extreme ends)
        /// </summary>
        public bool? ZScaling { get; set; }

        /// <summary>
        /// Maximum radius size of a bubble. If a bubble value is too large to cover the chart, this size will be used.
        /// </summary>
        public double? MaxBubbleRadius { get; set; }

        /// <summary>
        /// Minimum radius size of a bubble. If a bubble value is too small to be displayed, this size will be used.
        /// </summary>
        public double? MinBubbleRadius { get; set; }
    }

    /// <summary>
    /// Defines options specific to <see cref="ChartType.Candlestick"/>
    /// </summary>
    /// <remarks>
    /// Links:
    /// 
    /// <see href="https://apexcharts.github.io/Blazor-ApexCharts/candlestick-charts">Blazor Example</see>,
    /// <see href="https://apexcharts.com/docs/chart-types/candlestick">JavaScript Documentation</see>,
    /// <see href="https://apexcharts.com/docs/options/plotoptions/candlestick">JavaScript Reference</see>
    /// </remarks>
    public class PlotOptionsCandlestick
    {
        /// <inheritdoc cref="ApexCharts.PlotOptionsCandlestickColors" />
        public PlotOptionsCandlestickColors Colors { get; set; }

        /// <inheritdoc cref="ApexCharts.PlotOptionsCandlestickWick" />
        public PlotOptionsCandlestickWick Wick { get; set; }
    }

    /// <summary>
    /// The colors to use for data points on the candlestick chart
    /// </summary>
    public class PlotOptionsCandlestickColors
    {
        /// <summary>
        /// Color for the downward candle when the value/price closed below where it opened. Usually, a red color is used for this downward candle.
        /// </summary>
        public string Downward { get; set; }

        /// <summary>
        /// Color for the upward candle when the value/price closed above where it opened. Usually, a green color is used for this upward candle.
        /// </summary>
        public string Upward { get; set; }
    }

    /// <summary>
    /// Defines how to style the wick portion of the candlestick chart
    /// </summary>
    public class PlotOptionsCandlestickWick
    {
        /// <summary>
        /// Use the same fill color for the wick. If this is false, the color of the wick falls back to the stroke.
        /// </summary>
        public bool? UseFillColor { get; set; }
    }

    /// <summary>
    /// Defines options specific to <see cref="ChartType.Heatmap"/>
    /// </summary>
    /// <remarks>
    /// Links:
    /// 
    /// <see href="https://apexcharts.github.io/Blazor-ApexCharts/heatmap-charts">Blazor Example</see>,
    /// <see href="https://apexcharts.com/docs/chart-types/heatmap-chart">JavaScript Documentation</see>,
    /// <see href="https://apexcharts.com/docs/options/plotoptions/heatmap">JavaScript Reference</see>
    /// </remarks>
    public class PlotOptionsHeatmap
    {
        /// <inheritdoc cref="ApexCharts.PlotOptionsHeatmapColorScale" />
        public PlotOptionsHeatmapColorScale ColorScale { get; set; }

        /// <summary>
        /// When turned on, each row in a heatmap will have it's own lowest and highest range and colors will be shaded for each series. Default value is turned off.
        /// </summary>
        public bool? Distributed { get; set; }

        /// <summary>
        /// Enable different shades of color depending on the value
        /// </summary>
        public bool? EnableShades { get; set; }

        /// <summary>
        /// Radius of the rectangle inside heatmap
        /// </summary>
        public double? Radius { get; set; }

        /// <summary>
        /// When enabled, it will reverse the shades for negatives but keep the positive shades as it is now. An example of such use-case would be in a profit/loss chart where darker reds mean larger losses, darker greens mean larger gains.
        /// </summary>
        public bool? ReverseNegativeShade { get; set; }

        /// <summary>
        /// The intensity of the shades generated for each value. Accepts from 0 to 1
        /// </summary>
        public double? ShadeIntensity { get; set; }

        /// <summary>
        /// If turned on, the stroke/border around the heatmap cell has the same color as the cell color.
        /// </summary>
        public bool? UseFillColorAsStroke { get; set; }
    }

    /// <summary>
    /// Defines how to color datapoints on the heatmap chart
    /// </summary>
    public class PlotOptionsHeatmapColorScale
    {
        /// <summary>
        /// In a multiple series heatmap, flip the color-scale to fill the heatmaps vertically instead of horizontally.
        /// </summary>
        public bool? Inverse { get; set; }

        /// <summary>
        /// Specify the higher range for heatmap color calculation.
        /// </summary>
        public double? Max { get; set; }

        /// <summary>
        /// Specify the lower range for heatmap color calculation.
        /// </summary>
        public double? Min { get; set; }

        /// <inheritdoc cref="ApexCharts.PlotOptionsHeatmapColorScaleRange" />
        public List<PlotOptionsHeatmapColorScaleRange> Ranges { get; set; }
    }

    /// <summary>
    /// Defines zones to apply a color to on the heatmap chart
    /// </summary>
    public class PlotOptionsHeatmapColorScaleRange
    {
        /// <summary>
        /// Background color to fill the range with.
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Fore Color of the text if data-labels is enabled.
        /// </summary>
        public string ForeColor { get; set; }

        /// <summary>
        /// Value indicating range's upper limit
        /// </summary>
        public double? From { get; set; }

        /// <summary>
        /// If a name is provided, it will be used in the legend. If it is not provided, the text falls back to {from} - {to}
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Value indicating range's lower limit
        /// </summary>
        public double? To { get; set; }
    }

    /// <summary>
    /// Defines options specific to <see cref="ChartType.Pie"/>
    /// </summary>
    /// <remarks>
    /// Links:
    /// 
    /// <see href="https://apexcharts.github.io/Blazor-ApexCharts/pie-charts">Blazor Example</see>,
    /// <see href="https://apexcharts.com/docs/chart-types/pie-donut">JavaScript Documentation</see>,
    /// <see href="https://apexcharts.com/docs/options/plotoptions/pie">JavaScript Reference</see>
    /// </remarks>
    public class PlotOptionsPie
    {
        /// <summary>
        /// A custom angle from which the pie/donut slices should start.
        /// </summary>
        public double? StartAngle { get; set; }

        /// <summary>
        /// A custom angle to which the pie/donut slices should end.
        /// </summary>
        public double? EndAngle { get; set; }

        /// <summary>
        /// Transform the scale of whole pie/donut overriding the default calculations. Try variations like 0.5 and 1.5 to see how it scales based on the default width/height of the pie
        /// </summary>
        public double? CustomScale { get; set; }

        /// <inheritdoc cref="ApexCharts.PieDataLabels" />
        /// <remarks>
        /// All additional formatting/styling settings for dataLabels has to be done in <see cref="ApexChartOptions{TItem}.DataLabels"/> configuration.
        /// </remarks>
        public PieDataLabels DataLabels { get; set; }

        /// <inheritdoc cref="ApexCharts.PlotOptionsDonut" />
        public PlotOptionsDonut Donut { get; set; }

        /// <summary>
        /// When clicked on the pie/donut slice, expand the slice to make it distinguished visually.
        /// </summary>
        public bool? ExpandOnClick { get; set; }

        /// <summary>
        /// Sets the left offset of the whole pie area
        /// </summary>
        public double? OffsetX { get; set; }

        /// <summary>
        /// Sets the top offset of the whole pie area
        /// </summary>
        public double? OffsetY { get; set; }
    }

    /// <summary>
    /// Defines how to display labels on the pie chart
    /// </summary>
    public class PieDataLabels
    {
        /// <summary>
        /// Minimum angle to allow data-labels to show. If the slice angle is less than this number, the label would not show to prevent overlapping issues.
        /// </summary>
        public double? MinAngleToShowLabel { get; set; }

        /// <summary>
        /// Offset by which labels will move outside / inside of the donut area
        /// </summary>
        public double? Offset { get; set; }
    }

    /// <summary>
    /// Defines options specific to <see cref="ChartType.Donut"/>. The <see cref="PlotOptionsPie"/> may also be used for donut charts.
    /// </summary>
    /// <remarks>
    /// 
    /// Links:
    /// 
    /// <see href="https://apexcharts.github.io/Blazor-ApexCharts/donut-charts">Blazor Example</see>,
    /// <see href="https://apexcharts.com/docs/chart-types/pie-donut">JavaScript Documentation</see>,
    /// <see href="https://apexcharts.com/docs/options/plotoptions/pie">JavaScript Reference</see>
    /// </remarks>
    public class PlotOptionsDonut
    {
        /// <summary>
        /// The background color of the pie
        /// </summary>
        public string Background { get; set; }

        /// <inheritdoc cref="ApexCharts.DonutLabels" />
        public DonutLabels Labels { get; set; }

        /// <summary>
        /// Donut / ring size in percentage relative to the total pie area.
        /// </summary>
        public string Size { get; set; }
    }

    /// <summary>
    /// Defines how to style text labels for each donut slice
    /// </summary>
    public class DonutLabels
    {
        /// <inheritdoc cref="ApexCharts.DonutLabelName" />
        public DonutLabelName Name { get; set; }

        /// <summary>
        /// Whether to display inner labels or not.
        /// </summary>
        public bool Show { get; set; } = true;

        /// <inheritdoc cref="ApexCharts.DonutLabelTotal" />
        public DonutLabelTotal Total { get; set; }

        /// <inheritdoc cref="ApexCharts.DonutLabelValue" />
        public DonutLabelValue Value { get; set; }
    }

    /// <summary>
    /// Defines how to style the name of a donut label
    /// </summary>
    public class DonutLabelName
    {
        /// <summary>
        /// Color of the name in the donut's label
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Font family of the name in donut's label
        /// </summary>
        public string FontFamily { get; set; }

        /// <summary>
        /// Font size of the name in donut's label
        /// </summary>
        public string FontSize { get; set; }

        /// <summary>
        /// Font-weight of the name in dataLabel
        /// </summary>
        public object FontWeight { get; set; }

        /// <summary>
        /// Sets the top offset for name
        /// </summary>
        public double? OffsetY { get; set; }

        /// <summary>
        /// Show the name of the respective bar associated with it's value
        /// </summary>
        public bool Show { get; set; } = true;

        /// <summary>
        /// A custom formatter function to apply on the name text in dataLabel
        /// </summary>
        [JsonConverter(typeof(FunctionStringConverter))]
        public string Formatter { get; set; }
    }

    /// <summary>
    /// Defines how to style the total label for a donut slice
    /// </summary>
    public class DonutLabelTotal
    {
        /// <summary>
        /// Color of the total label
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Font family of the total label
        /// </summary>
        public string FontFamily { get; set; }

        /// <summary>
        /// Font size of the total label
        /// </summary>
        public string FontSize { get; set; }

        /// <summary>
        /// Font-weight of the total label in dataLabel
        /// </summary>
        public object FontWeight { get; set; }

        /// <summary>
        /// Label for "total". Defaults to "Total"
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// Show the total of all the series in the inner area of radialBar
        /// </summary>
        public bool Show { get; set; } = true;

        /// <summary>
        /// Always show the total label and do not remove it even when user clicks/hovers over the slices.
        /// </summary>
        public bool? ShowAlways { get; set; }

        /// <summary>
        /// A custom formatter function to apply on the total value. It accepts one parameter w which contains the chart's config and global objects. Defaults to a total of all series percentage divided by the length of series.
        /// </summary>
        [JsonConverter(typeof(FunctionStringConverter))]
        public string Formatter { get; set; }
    }

    /// <summary>
    /// Defines how to style the value portion for a donut slice
    /// </summary>
    public class DonutLabelValue
    {
        /// <summary>
        /// Color of the value label in dataLabel
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Font family of the value label
        /// </summary>
        public string FontFamily { get; set; }

        /// <summary>
        /// Font size of the value label
        /// </summary>
        public string FontSize { get; set; }

        /// <summary>
        /// Font weight of the value label in dataLabel
        /// </summary>
        public object FontWeight { get; set; }

        /// <summary>
        /// Sets the top offset for value label
        /// </summary>
        public double? OffsetY { get; set; }

        /// <summary>
        /// Show the value label associated with the name label
        /// </summary>
        public bool Show { get; set; } = true;

        /// <summary>
        /// A custom formatter function to apply on the value label in dataLabel
        /// </summary>
        [JsonConverter(typeof(FunctionStringConverter))]
        public string Formatter { get; set; }
    }

    /// <summary>
    /// Defines options specific to <see cref="ChartType.PolarArea"/>
    /// </summary>
    /// <remarks>
    /// Links:
    /// 
    /// <see href="https://apexcharts.com/docs/options/plotoptions/polararea">JavaScript Reference</see>
    /// </remarks>
    public class PlotOptionsPolarArea
    {
        /// <inheritdoc cref="ApexCharts.PolarAreaRings" />
        public PolarAreaRings Rings { get; set; }

        /// <inheritdoc cref="ApexCharts.PolarAreaSpokes" />
        public PolarAreaSpokes Spokes { get; set; }
    }

    /// <summary>
    /// Defines how to style the rings in the polar area chart
    /// </summary>
    public class PolarAreaRings
    {
        /// <summary>
        /// The line/border color of the rings of the chart.
        /// </summary>
        public string StrokeColor { get; set; }

        /// <summary>
        /// Border width of the rings of polarArea chart.
        /// </summary>
        public double? StrokeWidth { get; set; }
    }

    /// <summary>
    /// Defines how to style the spokes in the polar area chart
    /// </summary>
    public class PolarAreaSpokes
    {
        /// <inheritdoc cref="ApexCharts.Color"/>
        /// <summary>
        /// The line/border color of the spokes of polarArea chart.
        /// </summary>
        public Color ConnectorColors { get; set; }

        /// <summary>
        /// Border width of the spokes of polarArea chart.
        /// </summary>
        public double? StrokeWidth { get; set; }
    }

    /// <summary>
    /// Defines options specific to <see cref="ChartType.Radar"/>
    /// </summary>
    /// <remarks>
    /// Links:
    /// 
    /// <see href="https://apexcharts.github.io/Blazor-ApexCharts/radar-charts">Blazor Example</see>,
    /// <see href="https://apexcharts.com/docs/chart-types/radar">JavaScript Documentation</see>,
    /// <see href="https://apexcharts.com/docs/options/plotoptions/radar">JavaScript Reference</see>
    /// </remarks>
    public class PlotOptionsRadar
    {
        /// <summary>
        /// Sets the left offset of the radar
        /// </summary>
        public double? OffsetX { get; set; }

        /// <summary>
        /// Sets the top offset of the radar
        /// </summary>
        public double? OffsetY { get; set; }

        /// <inheritdoc cref="ApexCharts.RadarPolygons" />
        public RadarPolygons Polygons { get; set; }

        /// <summary>
        /// A custom size for the inner radar. The default size calculation will be overrided with this
        /// </summary>
        public double? Size { get; set; }
    }

    /// <summary>
    /// The style options to apply to the radar chart
    /// </summary>
    public class RadarPolygons
    {
        /// <inheritdoc cref="ApexCharts.Color"/>
        /// <summary>
        /// The line color of the connector lines of the polygons.
        /// </summary>
        public Color ConnectorColors { get; set; }

        /// <inheritdoc cref="ApexCharts.RadarPolygonsFill" />
        public RadarPolygonsFill Fill { get; set; }

        /// <inheritdoc cref="ApexCharts.Color"/>
        /// <summary>
        /// The line/border color of the spokes of the chart excluding the connector lines.
        /// </summary>
        public Color StrokeColors { get; set; }

        /// <inheritdoc cref="ApexCharts.Size"/>
        /// <summary>
        /// Border width of the spokes of radar chart.
        /// </summary>
        public Size StrokeWidth { get; set; }
    }

    /// <summary>
    /// Defines which colors to fill the radar chart with
    /// </summary>
    public class RadarPolygonsFill
    {
        /// <summary>
        /// The list of colors to apply to the radar chart
        /// </summary>
        public List<string> Colors { get; set; }
    }

    /// <summary>
    /// Defines options specific to <see cref="ChartType.RadialBar"/>
    /// </summary>
    /// <remarks>
    /// Links:
    /// 
    /// <see href="https://apexcharts.github.io/Blazor-ApexCharts/radialbar-charts">Blazor Example</see>,
    /// <see href="https://apexcharts.com/docs/chart-types/radialbar-gauge">JavaScript Documentation</see>,
    /// <see href="https://apexcharts.com/docs/options/plotoptions/radialbar">JavaScript Reference</see>
    /// </remarks>
    public class PlotOptionsRadialBar
    {
        /// <inheritdoc cref="ApexCharts.RadialBarDataLabels" />
        public RadialBarDataLabels DataLabels { get; set; }

        /// <summary>
        /// Angle to which the radialBars should end. The sum of the startAngle and endAngle should not exceed 360.
        /// </summary>
        public double? EndAngle { get; set; }

        /// <inheritdoc cref="ApexCharts.Hollow" />
        public Hollow Hollow { get; set; }

        /// <summary>
        /// Whether to make the first value of series innermost or outermost
        /// </summary>
        public bool? InverseOrder { get; set; }

        /// <summary>
        /// Sets the left offset for radialBars
        /// </summary>
        public double? OffsetX { get; set; }

        /// <summary>
        /// Sets the top offset for radialBars
        /// </summary>
        public double? OffsetY { get; set; }

        /// <summary>
        /// Angle from which the radialBars should start
        /// </summary>
        public double? StartAngle { get; set; }

        /// <inheritdoc cref="ApexCharts.Track" />
        public Track Track { get; set; }
    }

    /// <summary>
    /// Defines how to style labels for the radial bar chart
    /// </summary>
    public class RadialBarDataLabels
    {
        /// <inheritdoc cref="ApexCharts.RadialBarDataLabelsName" />
        public RadialBarDataLabelsName Name { get; set; }

        /// <summary>
        /// Whether to display labels inside radialBars or not
        /// </summary>
        public bool? Show { get; set; }

        /// <inheritdoc cref="ApexCharts.RadialBarDataLabelsTotal" />
        public RadialBarDataLabelsTotal Total { get; set; }

        /// <inheritdoc cref="ApexCharts.RadialBarDataLabelsValue" />
        public RadialBarDataLabelsValue Value { get; set; }
    }

    /// <summary>
    /// Defines how to style the name for each section of the radial bar chart
    /// </summary>
    public class RadialBarDataLabelsName
    {
        /// <summary>
        /// Color of the name in dataLabel
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Font family of the name in dataLabel
        /// </summary>
        public string FontFamily { get; set; }

        /// <summary>
        /// Font size of the name in dataLabel
        /// </summary>
        public string FontSize { get; set; }

        /// <summary>
        /// Font-weight of the name in dataLabel
        /// </summary>
        public object FontWeight { get; set; }

        /// <summary>
        /// Sets the top offset for name
        /// </summary>
        public double? OffsetY { get; set; }

        /// <summary>
        /// Show the name of the respective bar associated with it's value
        /// </summary>
        public bool? Show { get; set; }
    }

    /// <summary>
    /// Defines how to style the total for each section of the radial bar chart
    /// </summary>
    public class RadialBarDataLabelsTotal
    {
        /// <summary>
        /// Color of the total label
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Font-family of the total label in dataLabel
        /// </summary>
        public string FontFamily { get; set; }

        /// <summary>
        /// Font-size of the total label in dataLabel
        /// </summary>
        public string FontSize { get; set; }

        /// <summary>
        /// Font-weight of the total label in dataLabel
        /// </summary>
        public object FontWeight { get; set; }

        /// <summary>
        /// Label for "total". Defaults to "Total"
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// Show the total of all the series in the inner area of radialBar
        /// </summary>
        public bool? Show { get; set; }

        /// <summary>
        /// A custom formatter function to apply on the total value. It accepts one parameter w which contains the chart's config and global objects. Defaults to a total of all series percentage divided by the length of series.
        /// </summary>
        [JsonConverter(typeof(FunctionStringConverter))]
        public string Formatter { get; set; }
    }

    /// <summary>
    /// Defines how to style the value for each section of the radial bar chart
    /// </summary>
    public class RadialBarDataLabelsValue
    {
        /// <summary>
        /// Color of the value label in dataLabel
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Font family of the value label in dataLabel
        /// </summary>
        public string FontFamily { get; set; }

        /// <summary>
        /// Font size of the value label in dataLabel
        /// </summary>
        public string FontSize { get; set; }

        /// <summary>
        /// Font weight of the value label in dataLabel
        /// </summary>
        public object FontWeight { get; set; }

        /// <summary>
        /// Sets the top offset for value label
        /// </summary>
        public double? OffsetY { get; set; }

        /// <summary>
        /// A custom formatter function to apply on the value label in dataLabel
        /// </summary>
        [JsonConverter(typeof(FunctionStringConverter))]
        public string Formatter { get; set; }

        /// <summary>
        /// Show the value label associated with the name label
        /// </summary>
        public bool? Show { get; set; }
    }

    /// <summary>
    /// Defines how to style the middle section of the radial bar chart
    /// </summary>
    public class Hollow
    {
        /// <summary>
        /// Background color for the hollow part of the radialBars
        /// </summary>
        public string Background { get; set; }

        /// <inheritdoc cref="ApexCharts.DropShadow" />
        public DropShadow DropShadow { get; set; }

        /// <summary>
        /// Optional image URL which can be displayed in the hollow area.
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// If true, the image doesn't exceeds the hollow area and is contained within.
        /// </summary>
        public bool? ImageClipped { get; set; }

        /// <summary>
        /// Height of the hollow image
        /// </summary>
        public double? ImageHeight { get; set; }

        /// <summary>
        /// Sets the left offset of hollow image
        /// </summary>
        public double? ImageOffsetX { get; set; }

        /// <summary>
        /// Sets the top offset of hollow image
        /// </summary>
        public double? ImageOffsetY { get; set; }

        /// <summary>
        /// Width of the hollow image
        /// </summary>
        public double? ImageWidth { get; set; }

        /// <summary>
        /// Spacing which will be subtracted from the available hollow size
        /// </summary>
        public double? Margin { get; set; }

        /// <summary>
        /// Determines where to place the grid for the chart
        /// </summary>
        public GridPosition? Position { get; set; }

        /// <summary>
        /// Size in percentage relative to the total available size of chart
        /// </summary>
        public string Size { get; set; }
    }

    /// <summary>
    /// Defines how to style the empty portions of the radial bar chart
    /// </summary>
    public class Track
    {
        /// <inheritdoc cref="ApexCharts.Color"/>
        /// <summary>
        /// Color of the track.
        /// </summary>
        public Color Background { get; set; }

        /// <inheritdoc cref="ApexCharts.DropShadow" />
        public DropShadow DropShadow { get; set; }

        /// <summary>
        /// Angle to which the track should end.
        /// </summary>
        public double? EndAngle { get; set; }

        /// <summary>
        /// Spacing between each track
        /// </summary>
        public double? Margin { get; set; }

        /// <summary>
        /// Opacity of the track
        /// </summary>
        public double? Opacity { get; set; }

        /// <summary>
        /// Show track under the bar lines.
        /// </summary>
        public bool? Show { get; set; }

        /// <summary>
        /// Angle from which the track should start.
        /// </summary>
        public double? StartAngle { get; set; }

        /// <summary>
        /// Please use <see cref="Width"/>
        /// </summary>
        [Obsolete("This property is obsolete. Use Width instead.", false)]
        public string StrokeWidth { get; set; }

        /// <summary>
        /// Width of the track
        /// </summary>
        public double? Width { get; set; }
    }

    /// <summary>
    /// You can configure different options for different screen sizes and ApexCharts will override the configuration based on breakpoints defined.
    /// </summary>
    /// <remarks>
    /// Links:
    /// 
    /// <see href="https://apexcharts.com/docs/responsive">JavaScript Documentation</see>,
    /// <see href="https://apexcharts.com/docs/options/responsive">JavaScript Reference</see>
    /// </remarks>
    public class Responsive
    {
        /// <summary>
        /// The breakpoint is the max screen width at which the original config object will be overrided by the responsive config object
        /// </summary>
        public double? Breakpoint { get; set; }

        /// <summary>
        /// The new configuration object that you would like to override on the existing default configuration object. All the options which you set normally can be set here. <see href="https://codepen.io/apexcharts/pen/ajpqJp">Example</see>
        /// </summary>
        public object Options { get; set; }
    }

    /// <summary>
    /// Class to define styles that are applied on various interaction states with the chart.
    /// </summary>
    /// <remarks>
    /// Links:
    /// 
    /// <see href="https://apexcharts.github.io/Blazor-ApexCharts/features/state">Blazor Example</see>,
    /// <see href="https://apexcharts.com/docs/options/states">JavaScript Reference</see>
    /// </remarks>
    public class States
    {
        /// <inheritdoc cref="ApexCharts.StatesActive" />
        public StatesActive Active { get; set; }

        /// <inheritdoc cref="ApexCharts.StatesHover" />
        public StatesHover Hover { get; set; }

        /// <inheritdoc cref="ApexCharts.StatesNormal" />
        public StatesNormal Normal { get; set; }
    }

    /// <summary>
    /// Defines styles to apply during the active state
    /// </summary>
    public class StatesActive
    {
        /// <summary>
        /// Whether to allow selection of multiple datapoints and give them active state or allow one dataPoint selection at a time.
        /// </summary>
        public bool? AllowMultipleDataPointsSelection { get; set; }

        /// <inheritdoc cref="ApexCharts.StatesFilter" />
        public StatesFilter Filter { get; set; }
    }

    /// <summary>
    /// Defines styles to apply during the hover state
    /// </summary>
    public class StatesHover
    {
        /// <inheritdoc cref="ApexCharts.StatesFilter" />
        public StatesFilter Filter { get; set; }
    }

    /// <summary>
    /// Defines styles to apply during the normal state
    /// </summary>
    public class StatesNormal
    {
        /// <inheritdoc cref="ApexCharts.StatesFilter" />
        public StatesFilter Filter { get; set; }
    }

    /// <summary>
    /// Defines the options to apply to the current state
    /// </summary>
    public class StatesFilter
    {
        /// <summary>
        /// The filter function to apply on hover state.
        /// </summary>
        public StatesFilterType Type { get; set; }

        /// <summary>
        /// A larger value intensifies the filter effect. Accepts values between 0 and 1
        /// </summary>
        public double? Value { get; set; }
    }

    /// <summary>
    /// Class to define how lines on charts should be generated.
    /// </summary>
    /// <remarks>
    /// Links:
    /// 
    /// <see href="https://apexcharts.com/docs/options/stroke">JavaScript Reference</see>
    /// </remarks>
    public class Stroke
    {
        /// <summary>
        /// Colors to fill the border for paths.
        /// </summary>
        public List<string> Colors { get; set; }

        /// <inheritdoc cref="ApexCharts.CurveSelections"/>
        /// <summary>
        /// In line / area charts, whether to draw smooth lines or straight lines.
        /// </summary>
        public CurveSelections Curve { get; set; }

        /// <inheritdoc cref="ApexCharts.Size"/>
        /// <summary>
        /// Creates dashes in borders of svg path. Higher number creates more space between dashes in the border.
        /// </summary>
        public Size DashArray { get; set; }

        /// <summary>
        /// For setting the starting and ending points of stroke
        /// </summary>
        public LineCap? LineCap { get; set; }

        /// <summary>
        /// To show or hide path-stroke / line
        /// </summary>
        public bool Show { get; set; } = true;

        /// <inheritdoc cref="ApexCharts.Size"/>
        /// <summary>
        /// Sets the width of border for svg path
        /// </summary>
        public Size Width { get; set; }

        /// <summary>
        /// Undocumented, this property exists in the TypeScript definition
        /// </summary>
        public Fill Fill { get; set; }
    }

    /// <summary>
    /// Class to define the visual appearance of the subtitle assigned to the chart
    /// </summary>
    /// <remarks>
    /// Links:
    /// 
    /// <see href="https://apexcharts.com/docs/options/subtitle">JavaScript Reference</see>
    /// </remarks>
    public class Subtitle
    {
        /// <summary>
        /// Alignment of subtitle relative to chart area.
        /// </summary>
        public Align? Align { get; set; }

        /// <summary>
        /// The floating option will take out the subtitle text from the chart area and make it float on top of the chart.
        /// </summary>
        public bool? Floating { get; set; }

        /// <summary>
        /// Vertical spacing around the subtitle text
        /// </summary>
        public double? Margin { get; set; }

        /// <summary>
        /// Sets the left offset for subtitle text
        /// </summary>
        public double? OffsetX { get; set; }

        /// <summary>
        /// Sets the top offset for subtitle text
        /// </summary>
        public double? OffsetY { get; set; }

        /// <inheritdoc cref="ApexCharts.SubtitleStyle" />
        public SubtitleStyle Style { get; set; }

        /// <summary>
        /// Text to display as a subtitle of chart
        /// </summary>
        public string Text { get; set; }
    }

    /// <summary>
    /// Defines the style options to apply to the subtitle
    /// </summary>
    public class SubtitleStyle
    {
        /// <summary>
        /// Fore color of the subtitle text
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Font Family of the subtitle text
        /// </summary>
        public string FontFamily { get; set; }

        /// <summary>
        /// Font Size of the subtitle text
        /// </summary>
        public string FontSize { get; set; }

        /// <summary>
        /// Font Weight of the subtitle text
        /// </summary>
        public object FontWeight { get; set; }
    }

    /// <summary>
    /// If you don't want to define your own colorPalette, choose a pre-defined palette in theme.palette property. ApexCharts has 10+ built in color palettes to choose from. To apply palette globally to all charts, set Apex.theme.palette property.
    /// </summary>
    /// <remarks>
    /// Links:
    /// 
    /// <see href="https://apexcharts.github.io/Blazor-ApexCharts/chart-themes">Blazor Example</see>,
    /// <see href="https://apexcharts.com/docs/themes">JavaScript Documentation</see>,
    /// <see href="https://apexcharts.com/docs/options/theme">JavaScript Reference</see>
    /// </remarks>
    public class Theme
    {
        /// <summary>
        /// Changing the theme.mode will also update the text and background colors of the chart.
        /// </summary>
        public Mode? Mode { get; set; }

        /// <inheritdoc cref="ApexCharts.ThemeMonochrome" />
        public ThemeMonochrome Monochrome { get; set; }

        /// <summary>
        /// Available palettes – palette1 to palette10
        /// </summary>
        public PaletteType? Palette { get; set; }
    }

    /// <summary>
    /// Single color is used as a base and shades are generated from that color.
    /// </summary>
    public class ThemeMonochrome
    {
        /// <summary>
        /// A hex color which will be used as the base color for generating shades
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Whether to enable monochrome theme option.
        /// </summary>
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// What should be the intensity while generating shades. Accepts from 0 to 1
        /// </summary>
        public double? ShadeIntensity { get; set; }

        /// <summary>
        /// The type of shade to apply
        /// </summary>
        public Mode? ShadeTo { get; set; }
    }

    /// <summary>
    /// Class to define the visual appearance of the title assigned to the chart
    /// </summary>
    /// <remarks>
    /// Links:
    /// 
    /// <see href="https://apexcharts.com/docs/options/title">JavaScript Reference</see>
    /// </remarks>
    public class Title
    {
        /// <summary>
        /// Alignment of title relative to chart area.
        /// </summary>
        public Align? Align { get; set; }

        /// <summary>
        /// The floating option will take out the title text from the chart area and make it float on top of the chart.
        /// </summary>
        public bool? Floating { get; set; }

        /// <summary>
        /// Vertical spacing around the title text
        /// </summary>
        public double? Margin { get; set; }

        /// <summary>
        /// Sets the left offset for title text
        /// </summary>
        public double? OffsetX { get; set; }

        /// <summary>
        /// Sets the top offset for title text
        /// </summary>
        public double? OffsetY { get; set; }

        /// <inheritdoc cref="ApexCharts.TitleStyle" />
        public TitleStyle Style { get; set; }

        /// <summary>
        /// Text to display as a title of chart
        /// </summary>
        public string Text { get; set; }
    }

    /// <summary>
    /// Defines the style options to apply to the title
    /// </summary>
    public class TitleStyle
    {
        /// <summary>
        /// Fore color of the title text
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Font Family of the title text
        /// </summary>
        public string FontFamily { get; set; }

        /// <summary>
        /// Font Size of the title text
        /// </summary>
        public string FontSize { get; set; }

        /// <summary>
        /// Font Weight of the title text
        /// </summary>
        public object FontWeight { get; set; }
    }

    /// <summary>
    /// Tooltip allows you to preview data when user hovers over the chart area.
    /// </summary>
    /// <remarks>
    /// Links:
    /// 
    /// <see href="https://apexcharts.github.io/Blazor-ApexCharts/features/tooltip">Blazor Example</see>,
    /// <see href="https://apexcharts.com/docs/tooltip">JavaScript Documentation</see>,
    /// <see href="https://apexcharts.com/docs/options/tooltip">JavaScript Reference</see>
    /// </remarks>
    public class Tooltip
    {

        /// <summary>
        /// Undocumented
        /// </summary>
        public bool? HideEmptySeries { get; set; }

        /// <summary>
        /// Undocumented
        /// </summary>
        public bool? HideEmptyShared { get; set; }

        /// <summary>
        /// Internal flag to indicate if there is a custom tooltip setup
        /// </summary>
        public bool CustomTooltip { get; internal set; }

        /// <inheritdoc cref="ApexCharts.CustomFunction"/>
        /// <summary>
        /// Draw a custom html tooltip instead of the default one based on the values provided in the function arguments.
        /// </summary>
        [JsonConverter(typeof(FunctionValueOrListConverterConverter))]
        public CustomFunction Custom { get; set; }

        /// <summary>
        /// Show tooltip when user hovers over chart area.
        /// </summary>
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// Show tooltip only on certain series in a multi-series chart. Provide indices of those series which you would like to be shown.
        /// </summary>
        public List<double> EnabledOnSeries { get; set; }

        /// <summary>
        /// When enabled, fill the tooltip background with the corresponding series color
        /// </summary>
        public bool? FillSeriesColor { get; set; }

        /// <inheritdoc cref="ApexCharts.TooltipFixed" />
        public TooltipFixed Fixed { get; set; }

        /// <summary>
        /// Follow user's cursor position instead of putting tooltip on actual data points.
        /// </summary>
        public bool? FollowCursor { get; set; }

        /// <summary>
        /// Show tooltip only when user hovers exactly over datapoint.
        /// </summary>
        public bool? Intersect { get; set; }

        /// <summary>
        /// In multiple series, when having shared tooltip, inverse the order of series (for better comparison in stacked charts).
        /// </summary>
        public bool? InverseOrder { get; set; }

        /// <inheritdoc cref="ApexCharts.TooltipItems" />
        public TooltipItems Items { get; set; }

        /// <inheritdoc cref="ApexCharts.TooltipMarker" />
        public TooltipMarker Marker { get; set; }

        /// <inheritdoc cref="ApexCharts.TooltipOnDatasetHover" />
        public TooltipOnDatasetHover OnDatasetHover { get; set; }

        /// <summary>
        /// When having multiple series, show a shared tooltip. If you have a DateTime x-axis and multiple series chart ‐ make sure all your series has the same "x" values for a shared tooltip to work smoothly.
        /// </summary>
        public bool? Shared { get; set; }

        /// <inheritdoc cref="ApexCharts.TooltipStyle" />
        public TooltipStyle Style { get; set; }

        /// <summary>
        /// The theme to apply to tooltips. If you further want to customize different background and forecolor of the tooltip, you should do it in CSS. Example:
        /// 
        /// <code>
        /// .apexcharts-tooltip {
        ///     background: #f3f3f3;
        ///     color: orange;
        /// }
        /// </code>
        /// </summary>
        public Mode? Theme { get; set; }

        /// <inheritdoc cref="ApexCharts.TooltipX" />
        public TooltipX X { get; set; }

        /// <inheritdoc cref="ApexCharts.TooltipY" />
        public TooltipY Y { get; set; }

        /// <inheritdoc cref="ApexCharts.TooltipZ" />
        public TooltipZ Z { get; set; }

        /// <summary>
        /// Undocumented, this property exists in the TypeScript definition
        /// </summary>
        public string CssClass { get; set; }
    }

    /// <summary>
    /// Defines options to create a fixed-position tooltip
    /// </summary>
    public class TooltipFixed
    {
        /// <summary>
        /// Set the tooltip to a fixed position
        /// </summary>
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// Sets the left offset for the tooltip container in fixed position
        /// </summary>
        public double? OffsetX { get; set; }

        /// <summary>
        /// Sets the top offset for the tooltip container in fixed position
        /// </summary>
        public double? OffsetY { get; set; }

        /// <summary>
        /// When having a fixed tooltip, select a predefined position.
        /// </summary>
        public TooltipPosition Position { get; set; } = TooltipPosition.TopRight;
    }

    /// <summary>
    /// Defines style options to apply to tooltips
    /// </summary>
    public class TooltipItems
    {
        /// <summary>
        /// The css property of each tooltip item container.
        /// </summary>
        public string Display { get; set; }
    }

    /// <summary>
    /// Defines options to apply to tooltip markers
    /// </summary>
    public class TooltipMarker
    {
        /// <summary>
        /// Undocumented, this property exists in the TypeScript definition
        /// </summary>
        public List<string> FillColors { get; set; }

        /// <summary>
        /// Whether to show the color coded marker shape in front of Series Name which helps to identify series in multiple datasets.
        /// </summary>
        public bool? Show { get; set; }
    }

    /// <summary>
    /// Defines options for hovering over the data set of the chart
    /// </summary>
    public class TooltipOnDatasetHover
    {
        /// <summary>
        /// When user hovers over a datapoint of a particular series, other series will be grayed out making the current series highlight.
        /// </summary>
        public bool? HighlightDataSeries { get; set; }
    }

    /// <summary>
    /// Defines style options to apply to tooltips
    /// </summary>
    public class TooltipStyle
    {
        /// <summary>
        /// Font-family to apply on tooltip texts
        /// </summary>
        public string FontFamily { get; set; }

        /// <summary>
        /// Font-size to apply on tooltip texts
        /// </summary>
        public string FontSize { get; set; }
    }

    /// <summary>
    /// Defines options to apply to the X-axis value of a tooltip
    /// </summary>
    public class TooltipX
    {
        /// <summary>
        /// The format of the x-axis value to show on the tooltip. To view how to format datetime Strings, view the <see href="https://apexcharts.com/docs/datetime">Datetime Formatter</see> guide.
        /// </summary>
        public string Format { get; set; }

        /// <summary>
        /// Whether to show the tooltip title (x-axis values) on tooltip or not
        /// </summary>
        public bool? Show { get; set; }

        /// <summary>
        /// A custom formatter function which you can override and display according to your needs (a use case can be a date formatted using complex moment.js functions)
        /// </summary>
        [JsonConverter(typeof(FunctionStringConverter))]
        public string Formatter { get; set; }
    }

    /// <summary>
    /// Defines options on how to format the title of a tooltip
    /// </summary>
    public class TooltipYTitle
    {
        /// <summary>
        /// The series name which appears besides values can be formatted using this function. Default behaviour is (seriesName) => returns seriesName
        /// </summary>
        [JsonConverter(typeof(FunctionStringConverter))]
        public string Formatter { get; set; }
    }

    /// <summary>
    /// Defines options to apply to the Y-axis value of a tooltip
    /// </summary>
    /// <remarks>
    /// In a multiple series, the tooltip.y property can accept array to target formatters of different series scales.
    /// </remarks>
    public class TooltipY
    {
        /// <inheritdoc cref="ApexCharts.TooltipYTitle" />
        public TooltipYTitle Title { get; set; }

        /// <summary>
        /// To format the Y-axis values of tooltip, you can define a custom formatter function. By default, these values will be formatted according yaxis.labels.formatter function which will be overrided by this function if you define it.
		/// 
		/// <code>
		/// tooltip: {
		///     y: {
		///         formatter: function(value, { series, seriesIndex, dataPointIndex, w }) {
		///             return value
		///         }
		///     }
		/// }
		/// </code>
        /// </summary>
        [JsonConverter(typeof(FunctionStringConverter))]
        public string Formatter { get; set; }
    }

    /// <summary>
    /// Defines options to  apply to the Z-axis value of a tooltip
    /// </summary>
    public class TooltipZ
    {
        /// <summary>
        /// A custom text for the z values of Bubble Series.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// To format the z values of a Bubble series, you can use this function.
        /// </summary>
        [JsonConverter(typeof(FunctionStringConverter))]
        public string Formatter { get; set; }
    }

    /// <summary>
    /// Class to customize the display of the X-axis on the generated chart
    /// </summary>
    /// <remarks>
    /// Links:
    /// 
    /// <see href="https://apexcharts.github.io/Blazor-ApexCharts/features/axis">Blazor Example</see>,
    /// <see href="https://apexcharts.com/docs/options/xaxis">JavaScript Reference</see>
    /// </remarks>
    public class XAxis
    {
        /// <inheritdoc cref="ApexCharts.AxisBorder" />
        public AxisBorder AxisBorder { get; set; }

        /// <inheritdoc cref="ApexCharts.AxisTicks" />
        public AxisTicks AxisTicks { get; set; }

        /// <summary>
        /// Categories are labels which are displayed on the x-axis.
        /// </summary>
        public object Categories { get; set; }

        /// <summary>
        /// Allows you to overwrite all the labels of the x-axis with these labels. Accepts an array of string values.
        /// </summary>
        public List<string> OverwriteCategories { get; set; }

        /// <inheritdoc cref="ApexCharts.AxisCrosshairs" />
        public AxisCrosshairs Crosshairs { get; set; }

        /// <summary>
        /// Setting this options takes the y-axis out of the plotting area. Much behaves like position: absolute property of CSS
        /// </summary>
        public bool? Floating { get; set; }

        /// <summary>
        /// The number of fractions to display when there are floating values on the x-axis numbers. Note: Works only in numeric type.
        /// </summary>
        public int? DecimalsInFloat { get; set; }

        /// <inheritdoc cref="ApexCharts.XAxisLabels" />
        public XAxisLabels Labels { get; set; }

        /// <summary>
        /// The highest number to be set for the x-axis. The graph drawing beyond this number will be clipped off
        /// </summary>
        public object Max { get; set; }

        /// <summary>
        /// The lowest number to be set for the x-axis. The graph drawing beyond this number will be clipped off
        /// </summary>
        public object Min { get; set; }

        /// <summary>
        /// stepSize refers to the interval between consecutive values on an x-axis. 
        /// It determines how the values on the axis are spaced or displayed. 
        /// If the step size is set to 10, the axis might display values like 0, 10, 20, 30, and so on.
        /// </summary>
        public object StepSize { get; set; }

        /// <summary>
        /// Sets the left offset for label
        /// </summary>
        public double? OffsetX { get; set; }

        /// <summary>
        /// Sets the top offset for label
        /// </summary>
        public double? OffsetY { get; set; }

        /// <summary>
        /// Setting this option allows you to change the x-axis position
        /// </summary>
        public XAxisPosition Position { get; set; }

        /// <summary>
        /// Range takes the max value of x-axis, subtracts the provided range value and gets the min value based on that. So, technically it helps to keep the same range when min and max values gets updated dynamically
        /// </summary>
        public double? Range { get; set; }

        /// <summary>
        /// Undocumented, this property exists in the TypeScript definition
        /// </summary>
        public bool? Sorted { get; set; }

        /// <summary>
        /// Number of Tick Intervals to show. Note: tickAmount doesn't have any effect when <see cref="Type"/> = <see cref="XAxisType.Datetime"/>
        /// </summary>
        /// <remarks>
        /// If you have a numeric xaxis <see cref="Type"/> = <see cref="XAxisType.Numeric"/>, you can specify tickAmount: 'dataPoints' which would make the number of ticks equal to the number of dataPoints in the chart.
        /// </remarks>
        public object TickAmount { get; set; }

        /// <summary>
        /// Whether to draw the ticks in between the data-points or on the data-points.
        /// </summary>
        /// <remarks>
        /// Note: tickPlacement only works for <see cref="Type"/>: <see cref="XAxisType.Category"/> charts and not for <see cref="XAxisType.Datetime"/> charts.
        /// </remarks>
        public TickPlacement? TickPlacement { get; set; }

        /// <inheritdoc cref="ApexCharts.AxisTitle" />
        public AxisTitle Title { get; set; }

        /// <inheritdoc cref="ApexCharts.AxisTooltip" />
        public AxisTooltip Tooltip { get; set; }

        /// <summary>
        /// Specifies the data type to use for the x-axis
        /// </summary>
        public XAxisType? Type { get; set; }

        /// <inheritdoc cref="ApexCharts.XAxisGroups" />
        public XAxisGroups Group { get; set; }
    }

    /// <summary>
    /// Defines options to apply to groups within the X-axis
    /// </summary>
    public class XAxisGroups
    {
        /// <inheritdoc cref="ApexCharts.XAxisGroup" />
        public List<XAxisGroup> Groups { get; set; }

        /// <inheritdoc cref="ApexCharts.XAxisGroupStyle" />
        public XAxisGroupStyle Style { get; set; }
    }

    /// <summary>
    /// Defines an individual group within the X-axis
    /// </summary>
    public class XAxisGroup
    {
        /// <summary>
        /// The name to apply to the group
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The number of columns in the group
        /// </summary>
        public int Cols { get; set; }
    }

    /// <summary>
    /// Defines styling to apply to X-axis groups
    /// </summary>
    public class XAxisGroupStyle
    {
        /// <inheritdoc cref="ApexCharts.Color"/>
        /// <summary>
        /// Fore color for the x-axis groups label.
        /// </summary>
        public Color Colors { get; set; }

        /// <summary>
        /// Font size for the x-axis group label
        /// </summary>
        public string FontSize { get; set; }

        /// <summary>
        /// Font family for the x-axis group label
        /// </summary>
        public string FontFamily { get; set; }

        /// <summary>
        /// Font-weight for the x-axis group label
        /// </summary>
        public string FontWeight { get; set; }

        /// <summary>
        /// A custom Css Class to give to the label elements
        /// </summary>
        public string CssClass { get; set; }
    }

    /// <summary>
    /// Defines styling to apply to the border of an axis
    /// </summary>
    public class AxisBorder
    {
        /// <summary>
        /// Color of the axis border
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Sets the left offset of the axis border
        /// </summary>
        public double? OffsetX { get; set; }

        /// <summary>
        /// Sets the top offset of the axis border
        /// </summary>
        public double? OffsetY { get; set; }

        /// <summary>
        /// Draw a border on the axis
        /// </summary>
        public bool? Show { get; set; }

        /// <summary>
        /// Sets the border height of the axis line
        /// </summary>
        public double? Height { get; set; }

        /// <summary>
        /// Sets the width of the axis line
        /// </summary>
        public double? Width { get; set; }

        /// <summary>
        /// Undocumented, this property exists in the TypeScript definition
        /// </summary>
        public double? StrokeWidth { get; set; }
    }

    /// <summary>
    /// Defines styling to apply to the ticks of an axis
    /// </summary>
    public class AxisTicks
    {
        /// <summary>
        /// The type of border to apply
        /// </summary>
        public BorderType BorderType { get; set; }

        /// <summary>
        /// Color of the ticks
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Height of the ticks
        /// </summary>
        public double? Height { get; set; }

        /// <summary>
        /// Sets the left offset of the ticks
        /// </summary>
        public double? OffsetX { get; set; }

        /// <summary>
        /// Sets the top offset of the ticks
        /// </summary>
        public double? OffsetY { get; set; }

        /// <summary>
        /// Draw ticks on the axis to specify intervals
        /// </summary>
        public bool? Show { get; set; }
    }

    /// <summary>
    /// Defines styling options to apply to chart crosshairs
    /// </summary>
    public class AxisCrosshairs
    {
        /// <inheritdoc cref="ApexCharts.CrosshairsDropShadow" />
        public CrosshairsDropShadow DropShadow { get; set; }

        /// <inheritdoc cref="ApexCharts.CrosshairsFill" />
        public CrosshairsFill Fill { get; set; }

        /// <summary>
        /// Opacity of the crosshairs
        /// </summary>
        public double? Opacity { get; set; }

        /// <summary>
        /// Where to place the crosshairs
        /// </summary>
        public GridPosition Position { get; set; }

        /// <summary>
        /// Show crosshairs on axis when user moves the mouse over chart area
        /// </summary>
        public bool? Show { get; set; }

        /// <inheritdoc cref="ApexCharts.CrosshairStroke" />
        public CrosshairStroke Stroke { get; set; }

        /// <summary>
        /// Defines the width to apply to the crosshairs
        /// </summary>
        /// <remarks>
        /// Options:
        /// 
        /// <list type="bullet">
        /// <item>Any number</item>
        /// <item>
        ///     <term>tickWidth</term>
        ///     <description>takes the tick intervals on axis and creates a crosshair of that width</description>
        /// </item>
        /// <item>
        ///     <term>barWidth</term>
        ///     <description>takes the barWidth and creates a crosshair of that width – only applicable to vertical bar charts</description>
        /// </item>
        /// </list>
        /// </remarks>
        public object Width { get; set; }
    }

    /// <summary>
    /// Defines how to style a shadow to display with the crosshairs
    /// </summary>
    public class CrosshairsDropShadow
    {
        /// <summary>
        /// Set blur distance for shadow
        /// </summary>
        public double? Blur { get; set; }

        /// <summary>
        /// Undocumented, this property exists in the TypeScript definition
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Enable a dropshadow for crosshairs
        /// </summary>
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// Set left offset for shadow
        /// </summary>
        public double? Left { get; set; }

        /// <summary>
        /// Set the opacity of shadow.
        /// </summary>
        public double? Opacity { get; set; }

        /// <summary>
        /// Set top offset for shadow
        /// </summary>
        public double? Top { get; set; }
    }

    /// <summary>
    /// Defines a styling to fill the crosshairs with
    /// </summary>
    public class CrosshairsFill
    {
        /// <summary>
        /// Fill color of crosshairs
        /// </summary>
        public string Color { get; set; }

        /// <inheritdoc cref="ApexCharts.FluffyGradient" />
        public FluffyGradient Gradient { get; set; }

        /// <summary>
        /// The type of fill to use in the crosshairs
        /// </summary>
        /// <remarks>
        /// Must use either <see cref="FillType.Solid"/> or <see cref="FillType.Gradient"/>
        /// </remarks>
        public FillType Type { get; set; }
    }

    /// <summary>
    /// Defines a styling for a gradient to fill the crosshairs with
    /// </summary>
    public class FluffyGradient
    {
        /// <summary>
        /// Crosshairs Gradient Color from
        /// </summary>
        public string ColorFrom { get; set; }

        /// <summary>
        /// Crosshairs Gradient Color to
        /// </summary>
        public string ColorTo { get; set; }

        /// <summary>
        /// Crosshairs fill opacity from
        /// </summary>
        public double? OpacityFrom { get; set; }

        /// <summary>
        /// Crosshairs fill opacity to
        /// </summary>
        public double? OpacityTo { get; set; }

        /// <summary>
        /// Stops defines the ramp of colors to use on a gradient
        /// </summary>
        public List<double> Stops { get; set; }
    }

    /// <summary>
    /// Defines how to style the border of the crosshairs
    /// </summary>
    public class CrosshairStroke
    {
        /// <summary>
        /// Border Color of crosshairs
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Creates dashes in borders of crosshairs. A higher number creates more space between dashes in the border.
        /// </summary>
        public double? DashArray { get; set; }

        /// <summary>
        /// Border Width of crosshairs
        /// </summary>
        public double? Width { get; set; }
    }

    /// <summary>
    /// Defines how to style labels on the X-axis
    /// </summary>
    public class XAxisLabels
    {
        /// <inheritdoc cref="ApexCharts.DatetimeFormatter" />
        public DatetimeFormatter DatetimeFormatter { get; set; }

        /// <summary>
        /// When turned on, local DateTime is converted into UTC. Turn it off if you supply date with timezone info and want to preserve it.
        /// </summary>
        public bool? DatetimeUTC { get; set; }

        /// <summary>
        /// Formats the datetime value based on the format specifier.
        /// </summary>
        /// <remarks>
        /// See the list of available <see href="https://apexcharts.com/docs/datetime">format specifiers</see>
        /// </remarks>
        public string Format { get; set; }

        /// <summary>
        /// Overrides everything and applies a custom function for the xaxis value. The function accepts 3 arguments. The first one is the default formatted value and the second one as the raw timestamp which you can pass to any datetime handling function to suit your needs. The 3rd argument is present in date-time xaxis which includes a dateFormatter as described in the code below.
		/// 
		/// <code>
		/// xaxis: {
		///     labels: {
		///         formatter: function(value, timestamp, opts) {
		///             return opts.dateFormatter(new Date(timestamp)).format("dd MMM")
		///         }
		///     }
		/// }
		/// </code>
        /// </summary>
        [JsonConverter(typeof(FunctionStringConverter))]
        public string Formatter { get; set; }

        /// <summary>
        /// When labels are too close and start to overlap on one another, this option prevents overlapping of the labels.
        /// </summary>
        public bool? HideOverlappingLabels { get; set; }

        /// <summary>
        /// Maximum height for the labels when they are rotated.
        /// </summary>
        public double? MaxHeight { get; set; }

        /// <summary>
        /// Minimum height for the labels
        /// </summary>
        public double? MinHeight { get; set; }

        /// <summary>
        /// Sets the left offset for label
        /// </summary>
        public double? OffsetX { get; set; }

        /// <summary>
        /// Sets the top offset for label
        /// </summary>
        public double? OffsetY { get; set; }

        /// <summary>
        /// Rotate angle for the x-axis labels
        /// </summary>
        public double? Rotate { get; set; }

        /// <summary>
        /// Whether to rotate the labels always or to rotate only when the texts don't fit the available width
        /// </summary>
        public bool? RotateAlways { get; set; }

        /// <summary>
        /// Show labels on x-axis
        /// </summary>
        public bool? Show { get; set; }

        /// <summary>
        /// By default, duplicate labels are not printed to prevent congested values in a datetime series. If you intentionally want to display the same values in x-axis labels, turn on this option
        /// </summary>
        public bool? ShowDuplicates { get; set; }

        /// <inheritdoc cref="ApexCharts.AxisLabelStyle" />
        public AxisLabelStyle Style { get; set; }

        /// <summary>
        /// Append ... to the text when it can't fit the available space and rotate is turned off
        /// </summary>
        public bool? Trim { get; set; }
    }

    /// <summary>
    /// Defines how to style labels on the Y-axis
    /// </summary>
    public class YAxisLabels
    {
        /// <inheritdoc cref="ApexCharts.DatetimeFormatter" />
        [Obsolete("Property has been removed", false)]
        public DatetimeFormatter DatetimeFormatter { get; set; }

        /// <summary>
        /// Applies a custom function for the yaxis value.
        /// 
        /// <code>
        /// yaxis: {
        ///     labels: {
        ///         formatter: function(val, index) {
        ///             return val.toFixed(2);
        ///         }
        ///     }
        /// }
        /// </code>
        /// </summary>
        /// <remarks>
        /// Note: In horizantal bar charts, the second parameters also contains additional data like dataPointIndex &amp; seriesIndex.
        /// </remarks>
        [JsonConverter(typeof(FunctionStringConverter))]
        public string Formatter { get; set; }

        /// <summary>
        /// Maximum width for the y-axis labels
        /// </summary>
        public double? MaxWidth { get; set; }

        /// <summary>
        /// Minimum width for the y-axis labels
        /// </summary>
        public double? MinWidth { get; set; }

        /// <summary>
        /// Sets the left offset for label
        /// </summary>
        public double? OffsetX { get; set; }

        /// <summary>
        /// Sets the top offset for label
        /// </summary>
        public double? OffsetY { get; set; }

        /// <summary>
        /// Rotate y-axis text label to a specific angle from it's center
        /// </summary>
        public double? Rotate { get; set; }

        /// <summary>
        /// Show labels on y-axis
        /// </summary>
        public bool? Show { get; set; }

        /// <inheritdoc cref="ApexCharts.AxisLabelStyle" />
        public AxisLabelStyle Style { get; set; }

        /// <summary>
        /// Alignment of Y-axis label relative to chart area.
        /// </summary>
        public Align? Align { get; set; }

        /// <summary>
        /// Undocumented, this property exists in the TypeScript definition
        /// </summary>
        public double? Padding { get; set; }
    }

    /// <summary>
    /// For the default timescale that is generated automatically based on the datetime difference, the below specifiers are used by default.
    /// </summary>
    public class DatetimeFormatter
    {
        /// <summary>
        /// Format specifier for the day of month.
        /// </summary>
        public string Day { get; set; }

        /// <summary>
        /// Format specifier for the hour of day.
        /// </summary>
        public string Hour { get; set; }

        /// <summary>
        /// Undocumented, this property exists in the TypeScript definition
        /// </summary>
        public string Minute { get; set; }

        /// <summary>
        /// Undocumented, this property exists in the TypeScript definition
        /// </summary>
        public string Second { get; set; }

        /// <summary>
        /// Format specifier for the month.
        /// </summary>
        public string Month { get; set; }

        /// <summary>
        /// Format specifier for the year.
        /// </summary>
        public string Year { get; set; }
    }

    /// <summary>
    /// Defines styling to apply to the axis labels
    /// </summary>
    public class AxisLabelStyle
    {
        /// <inheritdoc cref="ApexCharts.Color"/>
        /// <summary>
        /// Fore color for the axis label.
        /// </summary>
        public Color Colors { get; set; }

        /// <summary>
        /// A custom Css Class to give to the label elements
        /// </summary>
        public string CssClass { get; set; }

        /// <summary>
        /// Font family for the axis label
        /// </summary>
        public string FontFamily { get; set; }

        /// <summary>
        /// Font size for the axis label
        /// </summary>
        public string FontSize { get; set; }

        /// <summary>
        /// Font-weight for the axis label
        /// </summary>
        public object FontWeight { get; set; }
    }

    /// <inheritdoc/>
    public class XAxisTooltip : AxisTooltip { }

    /// <summary>
    /// Defines options to apply to X-axis tooltips
    /// </summary>
    public class AxisTooltip
    {
        /// <summary>
        /// Show tooltip on axis or not
        /// </summary>
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// Sets the top offset for axis tooltip
        /// </summary>
        public double? OffsetY { get; set; }

        /// <summary>
        /// A custom formatter function for the x-axis tooltip label. If undefined, the xaxis tooltip uses the default "X" value used in general tooltip. Example:
        /// 
        /// <code>
        /// xaxis: {
        ///     tooltip: {
        ///         formatter: function(val, opts) {
        ///             return val + "..."
        ///         }
        ///     }
        /// }
        /// </code>
        /// </summary>
        [JsonConverter(typeof(FunctionStringConverter))]
        public string Formatter { get; set; }

        /// <inheritdoc cref="ApexCharts.AxisTooltipStyle" />
        public AxisTooltipStyle Style { get; set; }
    }

    /// <summary>
    /// Defines options to apply to Y-axis tooltips
    /// </summary>
    public class YAxisTooltip
    {
        /// <summary>
        /// Show tooltip on y-axis
        /// </summary>
        public bool? Enabled { get; set; }

        /// <summary>
        /// Sets the top offset for y-axis tooltip
        /// </summary>
        public double? OffsetX { get; set; }
    }

    /// <summary>
    /// Defines styling to apply to axis tooltips
    /// </summary>
    public class AxisTooltipStyle
    {
        /// <summary>
        /// Font family for the axis tooltip text
        /// </summary>
        public string FontFamily { get; set; }

        /// <summary>
        /// Font size for the axis tooltip text
        /// </summary>
        public string FontSize { get; set; }
    }

    /// <summary>
    /// Class to customize the display of the Y-axis on the generated chart
    /// </summary>
    /// <remarks>
    /// Links:
    /// 
    /// <see href="https://apexcharts.github.io/Blazor-ApexCharts/features/axis">Blazor Example</see>,
    /// <see href="https://apexcharts.com/docs/options/yaxis">JavaScript Reference</see>
    /// </remarks>
    public class YAxis
    {
        /// <inheritdoc cref="ApexCharts.AxisBorder" />
        public AxisBorder AxisBorder { get; set; }

        /// <inheritdoc cref="ApexCharts.AxisTicks" />
        public AxisTicks AxisTicks { get; set; }

        /// <inheritdoc cref="ApexCharts.AxisCrosshairs" />
        public AxisCrosshairs Crosshairs { get; set; }

        /// <summary>
        /// The number of fractions to display when there are floating values in y-axis. Note: If you have defined a custom formatter function in yaxis.labels.formatter, this won't have any effect.
        /// </summary>
        public int? DecimalsInFloat { get; set; }

        /// <summary>
        /// etting this options takes the y-axis out of the plotting area. Much behaves like position: absolute property of CSS
        /// </summary>
        public bool? Floating { get; set; }

        /// <summary>
        /// If set to true, the y-axis scales are forced to generate nice looking rounded numbers even when min/max are provided. Turn this off if you manually set min/max and want it to be unchanged.
        /// </summary>
        public bool? ForceNiceScale { get; set; }

        /// <inheritdoc cref="ApexCharts.YAxisLabels" />
        public YAxisLabels Labels { get; set; }

        /// <summary>
        /// A non-linear scale when there is a large range of values.
        /// </summary>
        public bool? Logarithmic { get; set; }

        /// <summary>
        /// Controls the logarithmic base. Default is 10
        /// </summary>
        public decimal? LogBase { get; set; }

        /// <summary>
        /// The highest number to be set for the y-axis. The graph drawing beyond this number will be clipped off.
        /// </summary>
        /// <remarks>
        /// You can also pass a function here which should return a number.The function accepts an argument which by default is the biggest value in the y-axis.function(max) { return max }
        /// </remarks>
        public object Max { get; set; }

        /// <summary>
        /// The lowest number to be set for the y-axis. The graph drawing beyond this number will be clipped off.
        /// </summary>
        /// <remarks>
        /// You can also pass a function here which should return a number.The function accepts an argument which by default is the smallest value in the y-axis.function(min) { return min }
        /// </remarks>
        public object Min { get; set; }

        /// <summary>
        /// stepSize refers to the interval between consecutive values on an x-axis. 
        /// It determines how the values on the axis are spaced or displayed. 
        /// If the step size is set to 10, the axis might display values like 0, 10, 20, 30, and so on.
        /// </summary>
        public object StepSize { get; set; }

        /// <summary>
        /// When enabled, will draw the yaxis on the right side of the chart
        /// </summary>
        public bool? Opposite { get; set; }

        /// <summary>
        /// Flip the chart upside down making it inversed and draw the y-axis from bigger to smaller numbers.
        /// </summary>
        public bool? Reversed { get; set; }

        /// <summary>
        /// In a multiple y-axis chart, you can target the scale of a y-axis to a particular series by referencing through the seriesName. The series item which have the same name property will be used to calculate the scale of the y-axis.
        /// </summary>
        public SeriesName SeriesName { get; set; }

        /// <summary>
        /// Whether to display the y-axis or not.
        /// </summary>
        public bool? Show { get; set; }

        /// <summary>
        /// Whether to hide y-axis when user toggles series through legend.
        /// </summary>
        public bool? ShowAlways { get; set; }

        /// <summary>
        /// When turned off, it will hide the y-axis completely for a series which has no data or a series with all null values.
        /// </summary>
        public bool? ShowForNullSeries { get; set; }

        /// <summary>
        /// Number of Tick Intervals to show
        /// </summary>
        public double? TickAmount { get; set; }

        /// <inheritdoc cref="ApexCharts.AxisTitle" />
        public AxisTitle Title { get; set; }

        /// <inheritdoc cref="ApexCharts.YAxisTooltip" />
        public YAxisTooltip Tooltip { get; set; }
    }

    /// <summary>
    /// Defines options to apply to the axis title
    /// </summary>
    public class AxisTitle
    {
        /// <summary>
        /// Sets the left offset for axis title.
        /// </summary>
        public double? OffsetX { get; set; }

        /// <summary>
        /// Sets the top offset for the axis title.
        /// </summary>
        public double? OffsetY { get; set; }

        /// <summary>
        /// Rotate the axis title either 90 or -90.
        /// </summary>
        public double? Rotate { get; set; }

        /// <inheritdoc cref="ApexCharts.AxisTitleStyle" />
        public AxisTitleStyle Style { get; set; }

        /// <summary>
        /// Give the axis a title which will be displayed below the axis labels by default.
        /// </summary>
        public MultiLineText Text { get; set; }
    }

    /// <summary>
    /// Defines styling to apply to the axis title
    /// </summary>
    public class AxisTitleStyle
    {
        /// <summary>
        /// Fore color of the axis title
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// A custom CSS Class to give to the axis title
        /// </summary>
        public string CssClass { get; set; }

        /// <summary>
        /// Font family for the axis title
        /// </summary>
        public string FontFamily { get; set; }

        /// <summary>
        /// Font size for the axis title
        /// </summary>
        public string FontSize { get; set; }

        /// <summary>
        /// Font-weight for the axis title
        /// </summary>
        public object FontWeight { get; set; }
    }

#pragma warning disable CS1591 // Enum values are self-explanatory
    /// <summary>
    /// A list of easing options for animations
    /// </summary>
    public enum Easing
    {
        Easein,
        Easeinout,
        Easeout,
        Linear
    };

    /// <summary>
    /// A list of stacking options for stacked charts
    /// </summary>
    public enum StackType
    {
        Normal,
        [EnumMember(Value = "100%")]
        Percent100
    };

    /// <summary>
    /// A list of toolbar items to select when the chart is loaded
    /// </summary>
    public enum AutoSelected
    {
        Pan,
        Selection,
        Zoom
    };

    /// <summary>
    /// A list of chart types available to create
    /// </summary>
    public enum ChartType
    {
        Area,
        Bar,
        Bubble,
        Candlestick,
        Donut,
        Heatmap,
        Line,
        Pie,
        PolarArea,
        Radar,
        RadialBar,
        RangeBar,
        Scatter,
        Treemap,
        BoxPlot,
        RangeArea
    };

    /// <summary>
    /// A list of options to enable zooming on charts
    /// </summary>
    public enum AxisType
    {
        X,
        Xy,
        Y
    };

    /// <summary>
    /// A list of options to align data labels
    /// </summary>
    public enum TextAnchor
    {
        End,
        Middle,
        Start
    };

    /// <summary>
    /// A list of options to position generated gridss
    /// </summary>
    public enum GridPosition
    {
        Back,
        Front
    };

    /// <summary>
    /// A list of alignment options
    /// </summary>
    public enum Align
    {
        Center,
        Left,
        Right
    };

    /// <summary>
    /// A list of areas to place the legend for charts
    /// </summary>
    public enum LegendPosition
    {
        Bottom,
        Left,
        Right,
        Top
    };

    /// <summary>
    /// A list of sides to place the labels for data point annotations
    /// </summary>
    public enum LabelPosition
    {
        Left,
        Right
    };

    /// <summary>
    /// A list of options to place labels for bar charts
    /// </summary>
    public enum BarDataLabelPosition
    {
        Top,
        Center,
        Bottom
    };

    /// <summary>
    /// A list of options to place fixed tooltips
    /// </summary>
    public enum TooltipPosition
    {
        TopLeft,
        TopRight,
        BottomLeft,
        BottomRight
    };

    /// <summary>
    /// A list of options to specify where to place the X-Axis
    /// </summary>
    public enum XAxisPosition
    {
        Bottom,
        Top
    };

    /// <summary>
    /// A list of shapes to generate data point markers with
    /// </summary>
    public enum ShapeEnum
    {
        Circle,
        Square,
        Rect
    };

    /// <summary>
    /// A list of alignment options for vertical placement
    /// </summary>
    public enum VerticalAlign
    {
        Bottom,
        Middle,
        Top
    };

    /// <summary>
    /// A list of orientations for rotation
    /// </summary>
    public enum Orientation
    {
        Horizontal,
        Vertical
    };

    /// <summary>
    /// A list of options for formatting data labels
    /// </summary>
    public enum Format
    {
        Scale,
        Truncate
    };

    /// <summary>
    /// A list of theme options to use
    /// </summary>
    public enum Mode
    {
        Dark,
        Light
    };

    /// <summary>
    /// A list of data types available for X-axis values
    /// </summary>
    public enum XAxisType
    {
        Category,
        Datetime,
        Numeric
    };

    /// <summary>
    /// A list of options for styling borders
    /// </summary>
    public enum BorderType
    {
        Solid,
        Dotted
    };

    /// <summary>
    /// A list of fill options for charts
    /// </summary>
    public enum FillType
    {
        Solid,
        Gradient,
        Pattern,
        Image
    }

    /// <summary>
    /// A list of gradient options for chart fills
    /// </summary>
    public enum GradientShade
    {
        Light,
        Dark
    }

    /// <summary>
    /// A list of gradient types for chart fills
    /// </summary>
    public enum GradientType
    {
        Horizontal,
        Vertical,
        Diagonal1,
        Diagonal2
    }

    /// <summary>
    /// A list of fill options for pattern-based fills
    /// </summary>
    public enum FillPatternStyle
    {
        VerticalLines,
        HorizontalLines,
        SlantedLines,
        Squares,
        Circles
    }

    /// <summary>
    /// A list of fill options
    /// </summary>
    public enum AreaFillTo
    {
        End,
        Origin
    }

    /// <summary>
    /// A list of border radius options
    /// </summary>
    public enum BorderRadiusApplication
    {
        Around,
        End
    }

    /// <summary>
    /// A list of border radius options
    /// </summary>
    public enum BorderRadiusWhenStacked
    {
        All,
        Last
    }

    /// <summary>
    /// A list of shading options to apply to various data point states
    /// </summary>
    public enum StatesFilterType
    {
        none,
        lighten,
        darken
    }

    /// <summary>
    /// A list of placement options for ticks
    /// </summary>
    public enum TickPlacement
    {
        On,
        Between
    }
#pragma warning restore CS1591

    /// <summary>
    /// A list of ways to generate lines
    /// </summary>
    public enum Curve
    {
        /// <summary>
        /// Connects the points in a curve fashion. Also known as spline
        /// </summary>
        Smooth,

        /// <summary>
        /// Points are connected by horizontal and vertical line segments, looking like steps of a staircase.
        ///  Step is drawn after each data point
        /// </summary>
        Stepline,

        /// <summary>
        /// Connect the points in straight lines.
        /// </summary>
        Straight,

        /// <summary>
        /// Connects the points to create a monotone cubic spline
        /// </summary>
        MonotoneCubic,

        /// <summary>
        /// Points are connected by horizontal and vertical line segments, looking like steps of a staircase.
        ///  Step is drawn before each data point
        /// </summary>
        Linestep
    };

    /// <summary>
    /// A list of shapes to use for starting and ending points
    /// </summary>
    public enum LineCap
    {
        /// <summary>
        /// Ends the stroke with a 90-degree angle
        /// </summary>
        Butt,

        /// <summary>
        /// Ends the path-stroke with a radius that smooths out the start and end points
        /// </summary>
        Round,

        /// <summary>
        /// Similar to butt except that it extends the stroke beyond the length of the path
        /// </summary>
        Square
    };
}
