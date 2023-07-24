using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace ApexCharts
{
	/// <summary>
	/// Main class to configure options that are to be serialized and passed to the JavaScript.
	/// </summary>
	/// <typeparam name="TItem">The data type to be used in the chart to create data points.</typeparam>
	public class ApexChartOptions<TItem> where TItem : class
	{
		public bool Debug { get; set; }

		public bool HasDataPointSelection { get; internal set; }

		public bool HasDataPointEnter { get; internal set; }

		public bool HasDataPointLeave { get; internal set; }

		public bool HasLegendClick { get; internal set; }

		public bool HasMarkerClick { get; internal set; }

		public bool HasXAxisLabelClick { get; internal set; }

		public bool HasSelection { get; internal set; }

		public bool HasBrushScrolled { get; internal set; }

		public bool HasZoomed { get; internal set; }

		/// <inheritdoc cref="ApexCharts.Annotations" />
		public Annotations Annotations { get; set; }

		/// <inheritdoc cref="ApexCharts.Chart" />
		public Chart Chart { get; set; } = new();

		/// <summary>
		/// Colors for the chart’s series. When all colors of the array are used, it starts from the beginning.
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

		/// <inheritdoc cref="ApexCharts.ForecastDataPoints" />
		public ForecastDataPoints ForecastDataPoints { get; set; }

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
		/// When turned on, each series in a treemap will have it’s own lowest and highest range and colors will be shaded for each series. Default value is false.
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
		/// Value indicating range’s upper limit
		/// </summary>
		public double? From { get; set; }

		/// <summary>
		/// Value indicating range’s lower limit
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
		/// Undefined
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

		/// <summary>
		/// Undefined
		/// </summary>
		public string Position { get; set; }

		/// <inheritdoc cref="ApexCharts.AnnotationsShape" />
		public List<AnnotationsShape> Shapes { get; set; }

		/// <inheritdoc cref="ApexCharts.AnnotationsText" />
		public List<AnnotationsText> Texts { get; set; }

		/// <inheritdoc cref="ApexCharts.AnnotationsXAxis" />
		public List<AnnotationsXAxis> Xaxis { get; set; }

		/// <inheritdoc cref="ApexCharts.AnnotationsYAxis" />
		public List<AnnotationsYAxis> Yaxis { get; set; }
	}

	/// <summary>
	/// Defines details for images to use on the data annotations
	/// </summary>
	public class AnnotationsImage
	{
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
		/// Left position for the image relative to the element specified in appendTo property
		/// </summary>
		public double? X { get; set; }

		/// <summary>
		/// Top position for the image relative to the element specified in appendTo property
		/// </summary>
		public double? Y { get; set; }
	}

	/// <summary>
	/// Defines details for individual data annotation points
	/// </summary>
	public class AnnotationsPoint
	{
		/// <summary>
		/// Undefined
		/// </summary>
		public string Id { get; set; }

		/// <inheritdoc cref="ApexCharts.AnnotationsPointImage" />
		public AnnotationsPointImage Image { get; set; }

		/// <inheritdoc cref="ApexCharts.Label" />
		public Label Label { get; set; }

		/// <inheritdoc cref="ApexCharts.AnnotationMarker" />
		public AnnotationMarker Marker { get; set; }

		/// <summary>
		/// In a multiple series, you will have to specify which series the annotation’s y value belongs to. Not required for single series
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
		/// When there are multiple y-axis, setting this options will put the point annotation for that particular y-axis’ y value
		/// </summary>
		public double? YAxisIndex { get; set; }
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

	public class Label
	{
		public string BorderColor { get; set; }
		public double? BorderWidth { get; set; }
		public double? OffsetX { get; set; }
		public double? OffsetY { get; set; }
		public string Orientation { get; set; }
		public string Position { get; set; }
		public Style Style { get; set; }
		public string Text { get; set; }
		public string TextAnchor { get; set; }
	}

	public class Style
	{
		public string Background { get; set; }
		public string Color { get; set; }
		public string CssClass { get; set; }
		public string FontFamily { get; set; }
		public string FontSize { get; set; }
		public object FontWeight { get; set; }
		public Padding Padding { get; set; }
	}

	public class Padding
	{
		public double? Bottom { get; set; }
		public double? Left { get; set; }
		public double? Right { get; set; }
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
		public string Shape { get; set; }

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

#pragma warning disable CS1591 // Documentation not available for current version of ApexCharts.js
	/// <summary>
	/// Undefined
	/// </summary>
	public class AnnotationsShape
	{
		public string BackgroundColor { get; set; }
		public string BorderColor { get; set; }
		public double? BorderRadius { get; set; }
		public double? BorderWidth { get; set; }
		public double? Height { get; set; }
		public double? Opacity { get; set; }
		public string Type { get; set; }
		public object Width { get; set; }
		public double? X { get; set; }
		public double? Y { get; set; }
	}
#pragma warning restore CS1591

	/// <summary>
	/// Defines how to style the individual data point annotations and their text
	/// </summary>
	public class AnnotationsText
	{
		/// <summary>
		/// Undefined
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
		/// Undefined
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
		public string TextAnchor { get; set; }

		/// <summary>
		/// X (left) position for the text relative to the element specified in appendTo property
		/// </summary>
		public double? X { get; set; }

		/// <summary>
		/// Y (top) position for the text relative to the element specified in appendTo property
		/// </summary>
		public double? Y { get; set; }
	}

	/// <summary>
	/// Defines how to style the X-Axis for a data annotation point
	/// </summary>
	public class AnnotationsXAxis
	{
		/// <summary>
		/// Undefined
		/// </summary>
		public string Id { get; set; }

		/// <summary>
		/// Color of the annotation line
		/// </summary>
		public string BorderColor { get; set; }

		/// <summary>
		/// Undefined
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
	public class AnnotationsYAxis
	{
		/// <summary>
		/// Undefined
		/// </summary>
		public string Id { get; set; }

		/// <summary>
		/// Color of the annotation line
		/// </summary>
		public string BorderColor { get; set; }

		/// <summary>
		/// 
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
		/// <inheritdoc cref="ApexCharts.Animations" />
		public Animations Animations { get; set; }

		/// <summary>
		/// Background color for the chart area. If you want to set background with css, use .apexcharts-canvas to set it.
		/// </summary>
		public string Background { get; set; }

		/// <inheritdoc cref="ApexCharts.Brush" />
		public Brush Brush { get; set; }

		/// <summary>
		/// defaultLocale is the preselected language that the chart should render initially and the selected locale has to be present in the locales array. Read more in the localization guide.
		/// </summary>
		public string DefaultLocale { get; set; }

		/// <inheritdoc cref="ApexCharts.DropShadow" />
		public DropShadow DropShadow { get; set; }

		/// <summary>
		/// A collection of JavaScript functions to execute on specific interations with the chart. Available keys are:
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
		/// A chart group is created to perform interactive operations at the same time in all the charts. In case you want to create synchronized charts, you will need to provide this property.
		/// </summary>
		public string Group { get; set; }

		/// <summary>
		/// A chart ID is a unique identifier that will be used in calling certain ApexCharts methods. You will also need chart.id to be set in case you want to use any of the following functionalities.
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

		/// <inheritdoc cref="ApexCharts.Selection" />
		public Selection Selection { get; set; }

		/// <inheritdoc cref="ApexCharts.ChartSparkline" />
		public ChartSparkline Sparkline { get; set; }

		/// <summary>
		/// Enables stacked option for axis charts.
		/// </summary>
		/// <remarks>
		/// A stacked chart works only for same chart types and won’t work in combo/mixed charts combinations. So, an area series combined with a column series will not work.
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
		/// Accepts Number (400) OR String (‘400px’)
		/// </remarks>
		public object Width { get; set; }

		/// <summary>
		/// Height of the chart. The default value ‘auto’ is calculated based on the golden ratio 1.618 which roughly translates to a 16:10 aspect ratio. Examples:
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

	public class ExportOptions
	{
		public ExportCSV Csv { get; set; }
		public ExportSvg Svg { get; set; }
		public ExportPng Png { get; set; }
	}

	public class ExportSvg
	{
		public string Filename { get; set; }
	}

	public class ExportPng
	{
		public string Filename { get; set; }
	}

	public class ExportCSV
	{
		public string Filename { get; set; }
		public string ColumnDelimiter { get; set; }
		public string HeaderCategory { get; set; }
		public string HeaderValue { get; set; }
		public string DateFormatter { get; set; }
	}

	public class Animations
	{
		public AnimateGradually AnimateGradually { get; set; }
		public DynamicAnimation DynamicAnimation { get; set; }
		public Easing? Easing { get; set; }
		public bool Enabled { get; set; } = true;
		public double? Speed { get; set; }
	}

	public class AnimateGradually
	{
		public double? Delay { get; set; }
		public bool Enabled { get; set; } = true;
	}

	public class DynamicAnimation
	{
		public bool Enabled { get; set; } = true;
		public double? Speed { get; set; }
	}

	public class Brush
	{
		public bool? AutoScaleYaxis { get; set; }
		public bool Enabled { get; set; } = true;
		public string Target { get; set; }
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

		/// <summary>
		/// Give a color to the shadow. If array is provided, each series can have different shadow color
		/// </summary>
		public string Color { get; set; }

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

	public class ChartLocale
	{
		public string Name { get; set; }
		public LocaleOptions Options { get; set; }
	}

	public class LocaleOptions
	{
		public List<string> Days { get; set; }
		public List<string> Months { get; set; }
		public List<string> ShortDays { get; set; }
		public List<string> ShortMonths { get; set; }
		public LocaleToolbar Toolbar { get; set; }
	}

	public class LocaleToolbar
	{
		public string Download { get; set; }
		public string Pan { get; set; }
		public string Reset { get; set; }
		public string Selection { get; set; }
		public string SelectionZoom { get; set; }
		public string ZoomIn { get; set; }
		public string ZoomOut { get; set; }
	}

	public class Selection
	{
		public bool Enabled { get; set; } = true;
		public SelectionFill Fill { get; set; }
		public SelectionStroke Stroke { get; set; }
		public string Type { get; set; }
		public SelectionXaxis Xaxis { get; set; }
		public SelectionYaxis Yaxis { get; set; }
	}

	public class SelectionFill
	{
		public string Color { get; set; }
		public double? Opacity { get; set; }
	}

	public class SelectionStroke
	{
		public string Color { get; set; }
		public double? DashArray { get; set; }
		public double? Opacity { get; set; }
		public double? Width { get; set; }
	}

	public class SelectionXaxis
	{
		public double? Max { get; set; }
		public double? Min { get; set; }
	}

	public class SelectionYaxis
	{
		public double? Max { get; set; }
		public double? Min { get; set; }
	}

	public class ChartSparkline
	{
		public bool Enabled { get; set; } = true;
	}

	public class Toolbar
	{
		public AutoSelected? AutoSelected { get; set; }
		public double? OffsetX { get; set; }
		public double? OffsetY { get; set; }
		public bool Show { get; set; } = true;
		public Tools Tools { get; set; }
		public ExportOptions Export { get; set; }
	}

	public class Tools
	{
		public List<ToolCustomIcon> CustomIcons { get; set; }
		public bool Download { get; set; } = true;
		public bool Pan { get; set; } = true;
		public bool Reset { get; set; } = true;
		public bool Selection { get; set; } = true;
		public bool Zoom { get; set; } = true;
		public bool Zoomin { get; set; } = true;
		public bool Zoomout { get; set; } = true;
	}

	public class ToolCustomIcon
	{
		public string Icon { get; set; }
		public double? Index { get; set; }
		public string Title { get; set; }
	}

	public class Zoom
	{
		public bool? AutoScaleYaxis { get; set; }
		public bool Enabled { get; set; } = true;
		public ZoomType? Type { get; set; }
		public ZoomedArea ZoomedArea { get; set; }
	}

	public class ZoomedArea
	{
		public ZoomedAreaFill Fill { get; set; }
		public ZoomedAreaStroke Stroke { get; set; }
	}

	public class ZoomedAreaFill
	{
		public string Color { get; set; }
		public double? Opacity { get; set; }
	}

	public class ZoomedAreaStroke
	{
		public string Color { get; set; }
		public double? Opacity { get; set; }
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
		/// Allows showing series only on specific series in a multi-series chart. For eg., if you have a line and a column chart, you can show dataLabels only on the line chart by specifying it’s index in this array property.
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
		/// The alignment of text relative to dataLabel’s drawing position
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
		public string Formatter { get; set; }
	}

	public class DataLabelsBackground
	{
		public string BorderColor { get; set; }
		public double? BorderRadius { get; set; }
		public double? BorderWidth { get; set; }
		public DropShadow DropShadow { get; set; }
		public bool Enabled { get; set; } = true;
		public string ForeColor { get; set; }
		public double? Opacity { get; set; }
		public double? Padding { get; set; }
	}

	public class DataLabelsStyle
	{
		public List<string> Colors { get; set; }
		public string FontFamily { get; set; }
		public string FontSize { get; set; }
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

		/// <summary>
		/// Opacity of the fill attribute.
		/// </summary>
		public List<double> Opacity { get; set; }

		/// <inheritdoc cref="ApexCharts.FillPattern"/>
		public FillPattern Pattern { get; set; }

		/// <summary>
		/// Whether to fill the paths with solid colors or gradient.
		/// </summary>
		public List<FillType> Type { get; set; }
	}

	public enum FillType
	{
		Solid,
		Gradient,
		Pattern,
		Image
	}
	public enum GradientShade
	{
		Light,
		Dark
	}

	public enum GradientType
	{
		Horizontal,
		Vertical,
		Diagonal1,
		Diagonal2
	}

	public class FillGradient
	{
		public List<string> GradientToColors { get; set; }
		public bool? InverseColors { get; set; }
		public double? OpacityFrom { get; set; }
		public double? OpacityTo { get; set; }
		public GradientShade? Shade { get; set; }
		public double? ShadeIntensity { get; set; }
		public List<double> Stops { get; set; }
		public GradientType Type { get; set; }
	}

	public class FillImage
	{
		public double? Height { get; set; }
		public List<string> Src { get; set; }
		public double? Width { get; set; }
	}

	public class FillPattern
	{
		public double? Height { get; set; }
		public double? StrokeWidth { get; set; }
		public FillPatternStyle? Style { get; set; }
		public double? Width { get; set; }
	}

	public enum FillPatternStyle
	{
		VerticalLines,
		HorizontalLines,
		SlantedLines,
		Squares,
		Circles
	}

	/// <summary>
	/// Grid is the plot area excluding legends, title, subtitle, x-axis, and y-axis. Grid’s coordinates are used extensively in calculations in the chart in determining where to plot the actual chart elements.
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

	public class GridColumn
	{
		public List<string> Colors { get; set; }
		public double? Opacity { get; set; }
	}

	public class GridRow
	{
		public List<string> Colors { get; set; }
		public double? Opacity { get; set; }
	}

	public class GridXAxis
	{
		public Lines Lines { get; set; }
	}

	public class Lines
	{
		public double? OffsetX { get; set; }
		public double? OffsetY { get; set; }
		public bool? Show { get; set; }
	}

	public class GridYAxis
	{
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
#pragma warning disable CS1591 // Documentation not available for obsolete properties
		[Obsolete("This property is no longer availabe")]
		public LegendContainerMargin ContainerMargin { get; set; }

		[Obsolete("This property is no longer availabe")]
		public string TextAnchor { get; set; }
#pragma warning restore CS1591

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
		/// Allows you to hide a particular legend if it’s series contains all null values.
		/// </summary>
		public bool? ShowForNullSeries { get; set; }

		/// <summary>
		/// Show legend even if there is just 1 series.
		/// </summary>
		public bool? ShowForSingleSeries { get; set; }

		/// <summary>
		/// Allows you to hide a particular legend if it’s series contains all 0 values.
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
		public string Formatter { get; set; }

		/// <summary>
		/// A formatter function to allow showing data values in the legend while hovering on the chart. This can be useful when you have multiple series, and you don’t want to show tooltips for each series together. Example:
		/// 
		/// <code>
		/// legend: {
		///     tooltipHoverFormatter: function(seriesName, opts) {
		///         return seriesName + ' - &lt;strong&gt;' + opts.w.globals.series[opts.seriesIndex][opts.dataPointIndex] + '&lt;/strong&gt;'
		///     }
		/// },
		/// </code>
		/// </summary>
		public string TooltipHoverFormatter { get; set; }

		/// <summary>
		/// Allows you to overwrite the default legend items with this customized set of labels. Please note that the click/hover events of the legend will stop working if you provide these custom legend labels.
		/// </summary>
		public List<string> CustomLegendItems { get; set; }
	}

	public class LegendContainerMargin
	{
		public double? Left { get; set; }
		public double? Top { get; set; }
	}

	public class LegendItemMargin
	{
		public double? Horizontal { get; set; }
		public double? Vertical { get; set; }
	}

	public class LegendLabels
	{
		public Color Colors { get; set; }
		public bool? UseSeriesColors { get; set; }
	}

	public class LegendMarkers
	{
		public List<string> FillColors { get; set; }
		public double? Height { get; set; }
		public double? OffsetX { get; set; }
		public double? OffsetY { get; set; }
		public double? Radius { get; set; }
		public string StrokeColor { get; set; }
		public double? StrokeWidth { get; set; }
		public double? Width { get; set; }
	}

	public class LegendOnItemClick
	{
		public bool? ToggleDataSeries { get; set; }
	}

	public class LegendOnItemHover
	{
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
		/// <summary>
		/// Sets the fill color(s) of the marker point.
		/// </summary>
		public List<string> Colors { get; set; }

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

		/// <inheritdoc cref="JsonSize"/>
		/// <remarks>
		/// For single-series charts
		/// </remarks>
		[JsonIgnore]
		public double? Size { get; set; }

		/// <inheritdoc cref="JsonSize"/>
		/// <remarks>
		/// For multi-series charts
		/// </remarks>
		[JsonIgnore]
		public List<double> Sizes { get; set; }

		/// <summary>
		/// Size of the marker point. In a multi-series chart, you can provide an array of numbers to display different size of markers on different series.
		/// </summary>
		[JsonPropertyName("size")]
		public object JsonSize
		{
			get
			{
				if (Sizes is not null)
					return Sizes;

				return Size;
			}
		}

		/// <summary>
		/// Stroke Color of the marker. Accepts a single color or an array of colors in a multi-series chart.
		/// </summary>
		public Color StrokeColors { get; set; }

		/// <summary>
		/// Dashes in the border around marker. Higher number creates more space between dashes in the border.
		/// </summary>
		public Opacity StrokeDashArray { get; set; }

		/// <summary>
		/// Opacity of the border around marker.
		/// </summary>
		public Opacity StrokeOpacity { get; set; }

		/// <summary>
		/// Stroke Size of the marker.
		/// </summary>
		public Opacity StrokeWidth { get; set; }
	}

	public class MarkersDiscrete
	{
		public double? DataPointIndex { get; set; }
		public string FillColor { get; set; }
		public double? SeriesIndex { get; set; }
		public double? Size { get; set; }
		public string StrokeColor { get; set; }
		public ShapeEnum? Shape { get; set; }
	}

	public class MarkersHover
	{
		public double? Size { get; set; }
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

	public class NoDataStyle
	{
		public string Color { get; set; }
		public string FontFamily { get; set; }
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
		/// <inheritdoc cref="ApexCharts.PlotOptionsBoxPlot" />
		public PlotOptionsBoxPlot BoxPlot { get; set; }

		/// <inheritdoc cref="ApexCharts.PlotOptionsBar" />
		public PlotOptionsBar Bar { get; set; }

		/// <inheritdoc cref="ApexCharts.PlotOptionsBubble" />
		public PlotOptionsBubble Bubble { get; set; }

		/// <inheritdoc cref="ApexCharts.PlotOptionsCandlestick" />
		public PlotOptionsCandlestick Candlestick { get; set; }

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

		/// <inheritdoc cref="ApexCharts.PlotOptionsArea" />
		public PlotOptionsArea Area { get; set; }
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

	public enum AreaFillTo
	{
		End,
		Origin
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
		/// Rounded corners around the bars/columns. Note: This option is available since v3.26.0
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
		/// In column charts, columnWidth is the percentage of the available width in the grid-rect. Accepts ‘0%’ to ‘100%’
		/// </summary>
		public string ColumnWidth { get; set; }

		/// <summary>
		/// In horizontal bar charts, barHeight is the percentage of the available height in the grid-rect. Accepts ‘0%’ to ‘100%’
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

		/// <inheritdoc cref="ApexCharts.PlotOptionsBarColors" />
		public PlotOptionsBarColors Colors { get; set; }

		/// <inheritdoc cref="ApexCharts.PlotOptionsBarDataLabels" />
		public PlotOptionsBarDataLabels DataLabels { get; set; }

#pragma warning disable CS1591 // Documentation not available for obsolete properties
		[Obsolete("Deprecated since 3.24.0")]
		public Shape? EndingShape { get; set; }

		[Obsolete("Deprecated since 3.24.0")]
		public Shape? StartingShape { get; set; }
#pragma warning restore CS1591
	}

	public enum BorderRadiusApplication
	{
		Around,
		End
	}

	public enum BorderRadiusWhenStacked
	{
		All,
		Last
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
		/// Value indicating range’s upper limit
		/// </summary>
		public double? From { get; set; }

		/// <summary>
		/// Value indicating range’s lower limit
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
		/// Maximum limit of data-labels that can be displayed on a bar chart. If data-points exceed this number, data-labels won’t be shown.
		/// </summary>
		public double? MaxItems { get; set; }

		/// <summary>
		/// How to display the text
		/// </summary>
		public Orientation? Orientation { get; set; }

		/// <summary>
		/// Where to display the text
		/// </summary>
		public string Position { get; set; }

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
		/// When turned on, each row in a heatmap will have it’s own lowest and highest range and colors will be shaded for each series. Default value is turned off.
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
		/// Value indicating range’s upper limit
		/// </summary>
		public double? From { get; set; }

		/// <summary>
		/// If a name is provided, it will be used in the legend. If it is not provided, the text falls back to {from} - {to}
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Value indicating range’s lower limit
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
		/// Color of the name in the donut’s label
		/// </summary>
		public string Color { get; set; }

		/// <summary>
		/// Font family of the name in donut’s label
		/// </summary>
		public string FontFamily { get; set; }

		/// <summary>
		/// Font size of the name in donut’s label
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
		/// Show the name of the respective bar associated with it’s value
		/// </summary>
		public bool Show { get; set; } = true;
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
		/// A custom formatter function to apply on the total value. It accepts one parameter w which contains the chart’s config and global objects. Defaults to a total of all series percentage divided by the length of series.
		/// </summary>
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

		/// <inheritdoc cref="ApexCharts.PurplePolygons" />
		public PurplePolygons Polygons { get; set; }

		/// <summary>
		/// A custom size for the inner radar. The default size calculation will be overrided with this
		/// </summary>
		public double? Size { get; set; }
	}

	/// <summary>
	/// The style options to apply to the radar chart
	/// </summary>
	public class PurplePolygons
	{
		/// <summary>
		/// The line color of the connector lines of the polygons. If you want to pass more than 1 color, you can pass an array instead of a String. connectorColors: '#e8e8e8' and connectorColors: ['#e8e8e8', '#f1f1f1'] both are valid.
		/// </summary>
		public Color ConnectorColors { get; set; }

		/// <inheritdoc cref="ApexCharts.TentacledFill" />
		public TentacledFill Fill { get; set; }

		/// <summary>
		/// The line/border color of the spokes of the chart excluding the connector lines. If you want to pass more than 1 color, you can pass an array instead of a String. strokeColors: '#e8e8e8' and strokeColors: ['#e8e8e8', '#f1f1f1'] both are valid.
		/// </summary>
		public Color StrokeColors { get; set; }

		/// <summary>
		/// Border width of the spokes of radar chart.
		/// </summary>
		public Color StrokeWidth { get; set; }
	}

	/// <summary>
	/// Defines which colors to fill the radar chart with
	/// </summary>
	public class TentacledFill
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
		/// Show the name of the respective bar associated with it’s value
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
		/// A custom formatter function to apply on the total value. It accepts one parameter w which contains the chart’s config and global objects. Defaults to a total of all series percentage divided by the length of series.
		/// </summary>
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
		/// If true, the image doesn’t exceeds the hollow area and is contained within.
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
		/// <summary>
		/// Color of the track. If you want different color for each track, you can pass an array of colors.
		/// </summary>
		public string Background { get; set; }

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
		/// Undefined
		/// </summary>
		public string StrokeWidth { get; set; }
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
		/// The new configuration object that you would like to override on the existing default configuration object. All the options which you set normally can be set here
		/// </summary>
		public object Options { get; set; }
	}

	public class PurpleDatum
	{
		public string FillColor { get; set; }
		public string StrokeColor { get; set; }
		public object X { get; set; }
		public object Y { get; set; }
	}

	/// <summary>
	/// Class to define stypes that are applied on various interaction states with the chart.
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

	public class StatesActive
	{
		public bool? AllowMultipleDataPointsSelection { get; set; }
		public StatesFilter Filter { get; set; }
	}


	public class StatesHover
	{
		public StatesFilter Filter { get; set; }
	}


	public class StatesNormal
	{
		public StatesFilter Filter { get; set; }
	}

	public class StatesFilter
	{
		public StatesFilterType Type { get; set; }
		public double? Value { get; set; }
	}

	public enum StatesFilterType
	{
		none,
		lighten,
		darken
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

		/// <inheritdoc cref="JsonCurve"/>
		/// <remarks>
		/// For single-series charts
		/// </remarks>
		[JsonIgnore]
		public Curve? Curve { get; set; }

		/// <inheritdoc cref="JsonCurve"/>
		/// <remarks>
		/// For multi-series charts
		/// </remarks>
		[JsonIgnore]
		public List<Curve> Curves { get; set; }

		/// <summary>
		/// In line / area charts, whether to draw smooth lines or straight lines. You can also pass an array in stroke.curve, where each index corresponds to the series-index in multi-series charts.
		/// </summary>
		[JsonPropertyName("curve")]
		public object JsonCurve
		{
			get
			{
				if (Curves is not null)
					return Curves;

				return Curve;
			}
		}

		/// <summary>
		/// Creates dashes in borders of svg path. Higher number creates more space between dashes in the border.
		/// </summary>
		public object DashArray { get; set; }

		/// <summary>
		/// For setting the starting and ending points of stroke
		/// </summary>
		public LineCap? LineCap { get; set; }

		/// <summary>
		/// To show or hide path-stroke / line
		/// </summary>
		public bool Show { get; set; } = true;

		/// <summary>
		/// Sets the width of border for svg path
		/// </summary>
		public object Width { get; set; }
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

	public class SubtitleStyle
	{
		public string Color { get; set; }
		public string FontFamily { get; set; }
		public string FontSize { get; set; }
		public object FontWeight { get; set; }
	}

	/// <summary>
	/// If you don’t want to define your own colorPalette, choose a pre-defined palette in theme.palette property. ApexCharts has 10+ built in color palettes to choose from. To apply palette globally to all charts, set Apex.theme.palette property.
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

	public class ThemeMonochrome
	{
		public string Color { get; set; }
		public bool Enabled { get; set; } = true;
		public double? ShadeIntensity { get; set; }
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

	public class TitleStyle
	{
		public string Color { get; set; }
		public string FontFamily { get; set; }
		public string FontSize { get; set; }
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
		/// Draw a custom html tooltip instead of the default one based on the values provided in the function arguments. Example:
		/// 
		/// <code>
		/// tooltip: {
		///     custom: function({series, seriesIndex, dataPointIndex, w}) {
		///         return '&lt;div class="arrow_box"&gt;' +
		///             '&lt;span&gt;' + series[seriesIndex][dataPointIndex] + '&lt;/span&gt;' +
		///             '&lt;/div&gt;'
		///     }
		/// }
		/// </code>
		/// </summary>
		public string Custom { get; set; }

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
		/// Follow user’s cursor position instead of putting tooltip on actual data points.
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
		public string Theme { get; set; }

		/// <inheritdoc cref="ApexCharts.TooltipX" />
		public TooltipX X { get; set; }

		/// <inheritdoc cref="ApexCharts.TooltipY" />
		public TooltipY Y { get; set; }

		/// <inheritdoc cref="ApexCharts.TooltipZ" />
		public TooltipZ Z { get; set; }
	}

	public class TooltipFixed
	{
		public bool Enabled { get; set; } = true;
		public double? OffsetX { get; set; }
		public double? OffsetY { get; set; }
		public string Position { get; set; }
	}

	public class TooltipItems
	{
		public string Display { get; set; }
	}

	public class TooltipMarker
	{
		public List<string> FillColors { get; set; }
		public bool? Show { get; set; }
	}

	public class TooltipOnDatasetHover
	{
		public bool? HighlightDataSeries { get; set; }
	}

	public class TooltipStyle
	{
		public string FontFamily { get; set; }
		public string FontSize { get; set; }
	}

	public class TooltipX
	{
		public string Format { get; set; }
		public bool? Show { get; set; }
		public string Formatter { get; set; }
	}

	public class PurpleY
	{
		public Dictionary<string, object> Title { get; set; }
	}

	public class TooltipYTitle
	{
		public string Formatter { get; set; }
	}

	public class TooltipY
	{
		public TooltipYTitle Title { get; set; }
		public string Formatter { get; set; }
	}

	public class TooltipZ
	{
		public string Title { get; set; }
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
		public string Position { get; set; }

		/// <summary>
		/// Range takes the max value of x-axis, subtracts the provided range value and gets the min value based on that. So, technically it helps to keep the same range when min and max values gets updated dynamically
		/// </summary>
		public double? Range { get; set; }

		/// <summary>
		/// Undefined
		/// </summary>
		public bool? Sorted { get; set; }

		/// <summary>
		/// Number of Tick Intervals to show. Note: tickAmount doesn’t have any effect when xaxis.type = datetime
		/// </summary>
		/// <remarks>
		/// f you have a numeric xaxis xaxis.type = 'numeric', you can specify tickAmount: 'dataPoints' which would make the number of ticks equal to the number of dataPoints in the chart.
		/// </remarks>
		public object TickAmount { get; set; }

		/// <summary>
		/// Whether to draw the ticks in between the data-points or on the data-points.
		/// </summary>
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

	public class XAxisGroups
	{
		public List<XAxisGroup> Groups { get; set; }
		public XAxisGroupStyle Style { get; set; }
	}

	public class XAxisGroup
	{
		public string Title { get; set; }
		public int Cols { get; set; }
	}

	public class XAxisGroupStyle
	{
		public List<string> Colors { get; set; }
		public string FontSize { get; set; }
		public string FontFamily { get; set; }
		public string FontWeight { get; set; }
		public string CssClass { get; set; }
	}

	public enum TickPlacement
	{
		On,
		Beteween
	}

	public class AxisBorder
	{
		public string Color { get; set; }
		public double? OffsetX { get; set; }
		public double? OffsetY { get; set; }
		public bool? Show { get; set; }
		public double? Height { get; set; }
	}

	public class AxisTicks
	{
		public string BorderType { get; set; }
		public string Color { get; set; }
		public double? Height { get; set; }
		public double? OffsetX { get; set; }
		public double? OffsetY { get; set; }
		public bool? Show { get; set; }
	}

	public class AxisCrosshairs
	{
		public CrosshairsDropShadow DropShadow { get; set; }
		public CrosshairsFill Fill { get; set; }
		public double? Opacity { get; set; }
		public string Position { get; set; }
		public bool? Show { get; set; }
		public CrosshairStroke Stroke { get; set; }
		public object Width { get; set; }
	}

	public class CrosshairsDropShadow
	{
		public double? Blur { get; set; }
		public string Color { get; set; }
		public bool Enabled { get; set; } = true;
		public double? Left { get; set; }
		public double? Opacity { get; set; }
		public double? Top { get; set; }
	}

	public class CrosshairsFill
	{
		public string Color { get; set; }
		public FluffyGradient Gradient { get; set; }
		public string Type { get; set; }
	}

	public class FluffyGradient
	{
		public string ColorFrom { get; set; }
		public string ColorTo { get; set; }
		public double? OpacityFrom { get; set; }
		public double? OpacityTo { get; set; }
		public List<double> Stops { get; set; }
	}

	public class CrosshairStroke
	{
		public string Color { get; set; }
		public double? DashArray { get; set; }
		public double? Width { get; set; }
	}

	public class XAxisLabels
	{
		public DatetimeFormatter DatetimeFormatter { get; set; }
		public bool? DatetimeUTC { get; set; }
		public string Format { get; set; }
		public string Formatter { get; set; }
		public bool? HideOverlappingLabels { get; set; }
		public double? MaxHeight { get; set; }
		public double? MinHeight { get; set; }
		public double? OffsetX { get; set; }
		public double? OffsetY { get; set; }
		public double? Rotate { get; set; }
		public bool? RotateAlways { get; set; }
		public bool? Show { get; set; }
		public bool? ShowDuplicates { get; set; }
		public AxisLabelStyle Style { get; set; }
		public bool? Trim { get; set; }
	}

	public class YAxisLabels
	{
		public DatetimeFormatter DatetimeFormatter { get; set; }
		public bool? DatetimeUTC { get; set; }
		public string Format { get; set; }
		public string Formatter { get; set; }
		public bool? HideOverlappingLabels { get; set; }
		public double? MaxWidth { get; set; }
		public double? MinWidth { get; set; }
		public double? OffsetX { get; set; }
		public double? OffsetY { get; set; }
		public double? Rotate { get; set; }
		public bool? RotateAlways { get; set; }
		public bool? Show { get; set; }
		public bool? ShowDuplicates { get; set; }
		public AxisLabelStyle Style { get; set; }
		public bool? Trim { get; set; }
	}

	public class DatetimeFormatter
	{
		public string Day { get; set; }
		public string Hour { get; set; }
		public string Minute { get; set; }
		public string Month { get; set; }
		public string Year { get; set; }
	}

	public class AxisLabelStyle
	{
		public Color Colors { get; set; }
		public string CssClass { get; set; }
		public string FontFamily { get; set; }
		public string FontSize { get; set; }
		public object FontWeight { get; set; }
	}


	public class AxisTooltip
	{
		public bool Enabled { get; set; } = true;
		public double? OffsetY { get; set; }
		public XAxisTooltipStyle Style { get; set; }
	}

	public class XAxisTooltipStyle
	{
		public string FontFamily { get; set; }
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
		/// The number of fractions to display when there are floating values in y-axis. Note: If you have defined a custom formatter function in yaxis.labels.formatter, this won’t have any effect.
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
		public string SeriesName { get; set; }

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

		/// <inheritdoc cref="ApexCharts.AxisTooltip" />
		public AxisTooltip Tooltip { get; set; }
	}

	public class AxisTitle
	{
		public double? OffsetX { get; set; }
		public double? OffsetY { get; set; }
		public double? Rotate { get; set; }
		public AxisTitleStyle Style { get; set; }
		public string Text { get; set; }
	}

	public class AxisTitleStyle
	{
		public string Color { get; set; }
		public string CssClass { get; set; }
		public string FontFamily { get; set; }
		public string FontSize { get; set; }
		public object FontWeight { get; set; }
	}

	public enum Easing
	{
		Easein,
		Easeinout,
		Easeout,
		Linear
	};

	public enum StackType
	{
		Normal,
		[EnumMember(Value = "100%")]
		Percent100
	};

	public enum AutoSelected
	{
		Pan,
		Selection,
		Zoom
	};

	public enum ChartType
	{
		Area,
		Bar,
		Bubble,
		Candlestick,
		Donut,
		Heatmap,
		Histogram,
		Line,
		Pie,
		PolarArea,
		Radar,
		RadialBar,
		RangeBar,
		Scatter,
		Treemap,
		BoxPlot,
		RangeArea,
	};

	public enum ZoomType
	{
		X,
		Xy,
		Y
	};

	public enum TextAnchor
	{
		End,
		Middle,
		Start
	};

	public enum GridPosition
	{
		Back,
		Front
	};

	public enum Align
	{
		Center,
		Left,
		Right
	};

	public enum LegendPosition
	{
		Bottom,
		Left,
		Right,
		Top
	};

	public enum ShapeEnum
	{
		Circle,
		Square,
		Rect
	};

	public enum VerticalAlign
	{
		Bottom,
		Middle,
		Top
	};

	public enum Orientation
	{
		Horizontal,
		Vertical
	};

	public enum Shape
	{
		Flat,
		Rounded
	};

	public enum Curve
	{
		Smooth,
		Stepline,
		Straight
	};

	public enum LineCap
	{
		Butt,
		Round,
		Square
	};

	public enum Mode
	{
		Dark,
		Light
	};

	public enum TickAmountEnum
	{
		DataPoints
	};

	public enum XAxisType
	{
		Category,
		Datetime,
		Numeric
	};

#pragma warning disable CS1591 // Primarily for internal use to enable multi-data-type values
	public class ValueOrList<T>
	{
		private readonly T _str;
		private readonly List<T> _list;
		public bool IsList { get; private set; }

		public T GetValue => _str;
		public IEnumerable<T> GetList => _list;

		public ValueOrList(T value)
		{
			_str = value;
		}

		public ValueOrList(IEnumerable<T> list)
		{
			IsList = true;
			_list = list.ToList();
		}
	}

	public class Color : ValueOrList<string>
	{
		public Color(string value) : base(value)
		{
		}
		public Color(IEnumerable<string> list) : base(list)
		{
		}
	}

	public class Opacity : ValueOrList<double>
	{
		public Opacity(double value) : base(value)
		{
		}
		public Opacity(IEnumerable<double> list) : base(list)
		{
		}
	}
#pragma warning restore CS1591
}