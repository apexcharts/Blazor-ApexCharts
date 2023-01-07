using System.Collections.Generic;
using System;
using System.Runtime.Serialization;
using System.Linq;

namespace ApexCharts
{
    public class ApexChartOptions<TItem> where TItem : class
    {
        public bool Debug { get; set; }
        public bool HasDataPointSelection { get; internal set; }
        public bool HasDataPointEnter { get; internal set; }
        public bool HasDataPointLeave { get; internal set; }
        public bool HasLegendClick { get; internal set; }
        public bool HasSelection { get; internal set; }
        public bool HasBrushScrolled { get; internal set; }
        public bool HasZoomed { get; internal set; }

        /// <summary>
        /// Annotations options
        /// See https://apexcharts.com/docs/options/annotations/
        /// </summary>
        public Annotations Annotations { get; set; }

        /// <summary>
        /// Main Chart options
        /// See https://apexcharts.com/docs/options/chart/
        /// </summary>
        public Chart Chart { get; set; } = new();

        public List<string> Colors { get; set; }

        /// <summary>
        /// Chart Datalabels options
        /// See https://apexcharts.com/docs/options/datalabels/
        /// </summary>
        public DataLabels DataLabels { get; set; }

        public Fill Fill { get; set; }

        /// <summary>
        /// Plot X and Y grid options
        /// See https://apexcharts.com/docs/options/grid/
        /// </summary>
        public Grid Grid { get; set; }

        public List<string> Labels { get; set; }

        /// <summary>
        /// Chart Legend configuration options.
        /// See https://apexcharts.com/docs/options/legend/
        /// </summary>
        public Legend Legend { get; set; }

        /// <summary>
        /// Marker configuration options.
        /// See https://apexcharts.com/docs/options/markers/
        /// </summary>
        public Markers Markers { get; set; }

        /// <summary>
        /// NoData configuration options.
        /// See https://apexcharts.com/docs/options/nodata/
        /// </summary>
        public NoData NoData { get; set; }

        /// <summary>
        /// PlotOptions for specifying chart-type-specific configuration.
        /// See https://apexcharts.com/docs/options/plotoptions/bar/
        /// </summary>
        public PlotOptions PlotOptions { get; set; }

        /// <summary>
        /// PlotOptions for chart.
        /// See https://apexcharts.com/docs/options/responsive/
        /// </summary>
        public List<Responsive> Responsive { get; set; }

        /// <summary>
        /// Series for specifying chart-type-specific configuration.
        /// See https://apexcharts.com/docs/options/series/
        /// </summary>
        public List<Series<TItem>> Series { get; set; }


        public ForecastDataPoints ForecastDataPoints { get; set; }

        public States States { get; set; }

        /// <summary>
        /// Options for the line drawn on line and area charts.
        /// See https://apexcharts.com/docs/options/stroke/
        /// </summary>
        public Stroke Stroke { get; set; }

        /// <summary>
        /// Chart Sub Title options
        /// See https://apexcharts.com/docs/options/title/
        /// </summary>
        public Subtitle Subtitle { get; set; }

        public Theme Theme { get; set; }

        /// <summary>
        /// Chart Title options
        /// See https://apexcharts.com/docs/options/title/
        /// </summary>
        public Title Title { get; set; }

        /// <summary>
        /// Chart Tooltip options
        /// See https://apexcharts.com/docs/options/tooltip/
        /// </summary>
        public Tooltip Tooltip { get; set; }

        /// <summary>
        /// X Axis options
        /// See https://apexcharts.com/docs/options/xaxis/
        /// </summary>
        public XAxis Xaxis { get; set; }

        /// <summary>
        /// Y Axis options
        /// See https://apexcharts.com/docs/options/yaxis/
        /// </summary>
        public List<YAxis> Yaxis { get; set; }
    }

    public class ForecastDataPoints
    {
        public int Count { get; set; }
        public double? FillOpacity { get; set; }
        public double? StrokeWidth { get; set; }
        public double? DashArray { get; set; }
    }



    public class PlotOptionsTreemap
    {
        public bool? EnableShades { get; set; }
        public double? ShadeIntensity { get; set; }
        public bool? Distributed { get; set; }
        public bool? ReverseNegativeShade { get; set; }
        public bool? UseFillColorAsStroke { get; set; }
        public TreemapColorScale ColorScale { get; set; }
    }

    public class TreemapColorScale
    {
        public bool? Inverse { get; set; }
        public List<TreemapRanges> Ranges { get; set; }
        public double? Min { get; set; }
        public double? Max { get; set; }
    }

    public class TreemapRanges
    {
        public double? From { get; set; }
        public double? To { get; set; }
        public string Color { get; set; }
        public string ForeColor { get; set; }
        public string Name { get; set; }
    }

    public class Annotations
    {
        public List<AnnotationsImage> Images { get; set; }
        public List<AnnotationsPoint> Points { get; set; }
        public string Position { get; set; }
        public List<AnnotationsShape> Shapes { get; set; }
        public List<AnnotationsText> Texts { get; set; }
        public List<AnnotationsXAxis> Xaxis { get; set; }
        public List<AnnotationsYAxis> Yaxis { get; set; }
    }

    public class AnnotationsImage
    {
        public double? Height { get; set; }
        public string Path { get; set; }
        public double? Width { get; set; }
        public double? X { get; set; }
        public double? Y { get; set; }
    }

    public class AnnotationsPoint
    {
        public string Id { get; set; }
        public AnnotationsPointImage Image { get; set; }
        public Label Label { get; set; }
        public AnnotationMarker Marker { get; set; }
        public double? SeriesIndex { get; set; }
        public object X { get; set; }
        public double? Y { get; set; }
        public double? YAxisIndex { get; set; }
    }

    public class AnnotationsPointImage
    {
        public double? Height { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public string Path { get; set; }
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

    public class AnnotationMarker
    {
        public string CssClass { get; set; }
        public string FillColor { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public double? Radius { get; set; }
        public string Shape { get; set; }
        public double? Size { get; set; }
        public string StrokeColor { get; set; }
        public double? StrokeWidth { get; set; }
    }

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

    public class AnnotationsText
    {
        public string BackgroundColor { get; set; }
        public string BorderColor { get; set; }
        public double? BorderRadius { get; set; }
        public double? BorderWidth { get; set; }
        public string FontFamily { get; set; }
        public object FontSize { get; set; }
        public object FontWeight { get; set; }
        public string ForeColor { get; set; }
        public double? PaddingBottom { get; set; }
        public double? PaddingLeft { get; set; }
        public double? PaddingRight { get; set; }
        public double? PaddingTop { get; set; }
        public string Text { get; set; }
        public string TextAnchor { get; set; }
        public double? X { get; set; }
        public double? Y { get; set; }
    }

    public class AnnotationsXAxis
    {
        public string Id { get; set; }
        public string BorderColor { get; set; }
        public double? BorderWidth { get; set; }
        public string FillColor { get; set; }
        public Label Label { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public double? Opacity { get; set; }
        public double? StrokeDashArray { get; set; }
        public object X { get; set; }
        public object X2 { get; set; }
    }



    public class AnnotationsYAxis
    {
        public string Id { get; set; }
        public string BorderColor { get; set; }
        public double? BorderWidth { get; set; }
        public string FillColor { get; set; }
        public Label Label { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public double? Opacity { get; set; }
        public double? StrokeDashArray { get; set; }
        public object Y { get; set; }
        public object Y2 { get; set; }
        public double? YAxisIndex { get; set; }
    }

    /// <summary>
    /// Main Chart options
    /// See https://apexcharts.com/docs/options/chart/
    /// </summary>
    public class Chart
    {
        public Animations Animations { get; set; }
        public string Background { get; set; }
        public Brush Brush { get; set; }
        public string DefaultLocale { get; set; }
        public DropShadow DropShadow { get; set; }
        public Dictionary<string, object> Events { get; set; }
        public string FontFamily { get; set; }
        public string ForeColor { get; set; }
        public string Group { get; set; }
        public string Id { get; set; }
        public List<ChartLocale> Locales { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public double? ParentHeightOffset { get; set; }
        public bool? RedrawOnParentResize { get; set; }
        public Selection Selection { get; set; }
        public ChartSparkline Sparkline { get; set; }
        public bool? Stacked { get; set; }
        public StackType? StackType { get; set; }
        public Toolbar Toolbar { get; set; }
        public ChartType? Type { get; set; }
        public object Width { get; set; }
        public object Height { get; set; }
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

    public class DropShadow
    {
        public double? Blur { get; set; }
        public string Color { get; set; }
        public bool Enabled { get; set; } = true;
        public double? Left { get; set; }
        public double? Opacity { get; set; }
        public double? Top { get; set; }
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
    /// Chart Datalabels options
    /// See https://apexcharts.com/docs/options/datalabels/
    /// </summary>
    public class DataLabels
    {
        public DataLabelsBackground Background { get; set; }
        public bool? Distributed { get; set; }
        public DropShadow DropShadow { get; set; }
        public bool Enabled { get; set; } = true;
        public List<double> EnabledOnSeries { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public DataLabelsStyle Style { get; set; }
        public TextAnchor? TextAnchor { get; set; }
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

    public class Fill
    {
        public List<string> Colors { get; set; }
        public FillGradient Gradient { get; set; }
        public FillImage Image { get; set; }
        public List<double> Opacity { get; set; }
        public FillPattern Pattern { get; set; }
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
    /// Plot X and Y grid options
    /// See https://apexcharts.com/docs/options/grid/
    /// </summary>
    public class Grid
    {
        public string BorderColor { get; set; }
        public GridColumn Column { get; set; }
        public Padding Padding { get; set; }
        public GridPosition? Position { get; set; }
        public GridRow Row { get; set; }
        public bool? Show { get; set; }
        public double? StrokeDashArray { get; set; }
        public GridXAxis Xaxis { get; set; }
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
    /// Chart Legend configuration options.
    /// See https://apexcharts.com/docs/options/legend/
    /// </summary>
    public class Legend
    {
        public LegendContainerMargin ContainerMargin { get; set; }
        public bool? Floating { get; set; }
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
        public object FontWeight { get; set; }
        public double? Height { get; set; }
        public Align? HorizontalAlign { get; set; }
        public bool? InverseOrder { get; set; }
        public LegendItemMargin ItemMargin { get; set; }
        public LegendLabels Labels { get; set; }
        public LegendMarkers Markers { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public LegendOnItemClick OnItemClick { get; set; }
        public LegendOnItemHover OnItemHover { get; set; }
        public LegendPosition? Position { get; set; }
        public bool? Show { get; set; }
        public bool? ShowForNullSeries { get; set; }
        public bool? ShowForSingleSeries { get; set; }
        public bool? ShowForZeroSeries { get; set; }
        public string TextAnchor { get; set; }
        public double? Width { get; set; }
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

    public class Markers
    {
        public List<string> Colors { get; set; }
        public List<MarkersDiscrete> Discrete { get; set; }
        public Opacity FillOpacity { get; set; }
        public MarkersHover Hover { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public double? Radius { get; set; }
        public ShapeEnum? Shape { get; set; }
        public bool? ShowNullDataPoints { get; set; }
        public double? Size { get; set; }
        public Color StrokeColors { get; set; }
        public Opacity StrokeDashArray { get; set; }
        public Opacity StrokeOpacity { get; set; }
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

    public class NoData
    {
        public Align? Align { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public NoDataStyle Style { get; set; }
        public string Text { get; set; }
        public VerticalAlign? VerticalAlign { get; set; }
    }

    public class NoDataStyle
    {
        public string Color { get; set; }
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
    }

    /// <summary>
    /// PlotOptions for specifying chart-type-specific configuration.
    /// See https://apexcharts.com/docs/options/plotoptions/bar/
    /// </summary>
    public class PlotOptions
    {
        public PlotOptionsBoxPlot BoxPlot { get; set; }
        public PlotOptionsBar Bar { get; set; }
        public PlotOptionsBubble Bubble { get; set; }
        public PlotOptionsCandlestick Candlestick { get; set; }
        public PlotOptionsHeatmap Heatmap { get; set; }
        public PlotOptionsPie Pie { get; set; }
        public PlotOptionsPolarArea PolarArea { get; set; }
        public PlotOptionsRadar Radar { get; set; }
        public PlotOptionsRadialBar RadialBar { get; set; }
        public PlotOptionsTreemap Treemap { get; set; }
        public PlotOptionsArea Area { get; set; }
    }

    public class PlotOptionsArea
    {
        public AreaFillTo? FillTo { get; set; }

    }

    public enum AreaFillTo
    {
        End,
        Origin
    }

    public class PlotOptionsBoxPlot
    {
        public BloxPlotColors Colors { get; set; }
    }

    public class BloxPlotColors
    {
        public string Upper { get; set; }
        public string Lower { get; set; }
    }

    public class PlotOptionsBar
    {
        public bool? Horizontal { get; set; }
        public double? BorderRadius { get; set; }

        public BorderRadiusApplication? BorderRadiusApplication { get; set; }
        public BorderRadiusWhenStacked? BorderRadiusWhenStacked { get; set; }

        public string ColumnWidth { get; set; }
        public string BarHeight { get; set; }
        public bool? Distributed { get; set; }
        public bool? RangeBarOverlap { get; set; }
        public bool? RangeBarGroupRows { get; set; }

        public PlotOptionsBarColors Colors { get; set; }

        public PlotOptionsBarDataLabels DataLabels { get; set; }

        [Obsolete("Deprecated since 3.24.0")]
        public Shape? EndingShape { get; set; }

        [Obsolete("Deprecated since 3.24.0")]
        public Shape? StartingShape { get; set; }
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

    public class PlotOptionsBarColors
    {
        public List<string> BackgroundBarColors { get; set; }
        public double? BackgroundBarOpacity { get; set; }
        public double? BackgroundBarRadius { get; set; }
        public List<PlotOptionsBarColorRange> Ranges { get; set; }
    }

    public class PlotOptionsBarColorRange
    {
        public string Color { get; set; }
        public double? From { get; set; }
        public double? To { get; set; }
    }

    public class PlotOptionsBarDataLabels
    {
        public bool? HideOverflowingLabels { get; set; }
        public double? MaxItems { get; set; }
        public Orientation? Orientation { get; set; }
        public string Position { get; set; }
        public BarTotalDataLabels Total { get; set; }
    }

    public class BarTotalDataLabels
    {
        public bool Enabled { get; set; } = true;
        public string Formatter { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public BarDataLabelsStyle Style { get; set; }
    }

    public class BarDataLabelsStyle
    {
        public string Color { get; set; }
        public string FontSize { get; set; }
        public string FontFamily { get; set; }
        public object FontWeight { get; set; }
    }

    public class PlotOptionsBubble
    {
        public double? MaxBubbleRadius { get; set; }
        public double? MinBubbleRadius { get; set; }
    }

    public class PlotOptionsCandlestick
    {
        public PlotOptionsCandlestickColors Colors { get; set; }
        public PlotOptionsCandlestickWick Wick { get; set; }
    }

    public class PlotOptionsCandlestickColors
    {
        public string Downward { get; set; }
        public string Upward { get; set; }
    }

    public class PlotOptionsCandlestickWick
    {
        public bool? UseFillColor { get; set; }
    }

    public class PlotOptionsHeatmap
    {
        public PlotOptionsHeatmapColorScale ColorScale { get; set; }
        public bool? Distributed { get; set; }
        public bool? EnableShades { get; set; }
        public double? Radius { get; set; }
        public bool? ReverseNegativeShade { get; set; }
        public double? ShadeIntensity { get; set; }
        public bool? UseFillColorAsStroke { get; set; }
    }

    public class PlotOptionsHeatmapColorScale
    {
        public bool? Inverse { get; set; }
        public double? Max { get; set; }
        public double? Min { get; set; }
        public List<PlotOptionsHeatmapColorScaleRange> Ranges { get; set; }
    }

    public class PlotOptionsHeatmapColorScaleRange
    {
        public string Color { get; set; }
        public string ForeColor { get; set; }
        public double? From { get; set; }
        public string Name { get; set; }
        public double? To { get; set; }
    }

    public class PlotOptionsPie
    {
        public double? CustomScale { get; set; }
        public PieDataLabels DataLabels { get; set; }
        public PlotOptionsDonut Donut { get; set; }
        public bool? ExpandOnClick { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
    }

    public class PieDataLabels
    {
        public double? MinAngleToShowLabel { get; set; }
        public double? Offset { get; set; }
    }

    public class PlotOptionsDonut
    {
        public string Background { get; set; }
        public DonutLabels Labels { get; set; }
        public string Size { get; set; }
    }

    public class DonutLabels
    {
        public DonutLabelName Name { get; set; }
        public bool Show { get; set; } = true;
        public DonutLabelTotal Total { get; set; }
        public DonutLabelValue Value { get; set; }
    }

    public class DonutLabelName
    {
        public string Color { get; set; }
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
        public object FontWeight { get; set; }
        public double? OffsetY { get; set; }
        public bool Show { get; set; } = true;
    }

    public class DonutLabelTotal
    {
        public string Color { get; set; }
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
        public object FontWeight { get; set; }
        public string Label { get; set; }
        public bool Show { get; set; } = true;
        public bool? ShowAlways { get; set; }
    }

    public class DonutLabelValue
    {
        public string Color { get; set; }
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
        public object FontWeight { get; set; }
        public double? OffsetY { get; set; }
        public bool Show { get; set; } = true;
    }

    public class PlotOptionsPolarArea
    {
        public PolarAreaRings Rings { get; set; }
    }

    public class PolarAreaRings
    {
        public string StrokeColor { get; set; }
        public double? StrokeWidth { get; set; }
    }

    public class PlotOptionsRadar
    {
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public PurplePolygons Polygons { get; set; }
        public double? Size { get; set; }
    }

    public class PurplePolygons
    {
        public Color ConnectorColors { get; set; }
        public TentacledFill Fill { get; set; }
        public Color StrokeColors { get; set; }
        public Color StrokeWidth { get; set; }
    }

    public class TentacledFill
    {
        public List<string> Colors { get; set; }
    }

    public class PlotOptionsRadialBar
    {
        public RadialBarDataLabels DataLabels { get; set; }
        public double? EndAngle { get; set; }
        public Hollow Hollow { get; set; }
        public bool? InverseOrder { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public double? StartAngle { get; set; }
        public Track Track { get; set; }
    }

    public class RadialBarDataLabels
    {
        public RadialBarDataLabelsName Name { get; set; }
        public bool? Show { get; set; }
        public RadialBarDataLabelsTotal Total { get; set; }
        public RadialBarDataLabelsValue Value { get; set; }
    }

    public class RadialBarDataLabelsName
    {
        public string Color { get; set; }
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
        public object FontWeight { get; set; }
        public double? OffsetY { get; set; }
        public bool? Show { get; set; }
    }

    public class RadialBarDataLabelsTotal
    {
        public string Color { get; set; }
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
        public object FontWeight { get; set; }
        public string Label { get; set; }
        public bool? Show { get; set; }
        public string Formatter { get; set; }
    }

    public class RadialBarDataLabelsValue
    {
        public string Color { get; set; }
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
        public object FontWeight { get; set; }
        public double? OffsetY { get; set; }
        public string Formatter { get; set; }
        public bool? Show { get; set; }
    }

    public class Hollow
    {
        public string Background { get; set; }
        public DropShadow DropShadow { get; set; }
        public string Image { get; set; }
        public bool? ImageClipped { get; set; }
        public double? ImageHeight { get; set; }
        public double? ImageOffsetX { get; set; }
        public double? ImageOffsetY { get; set; }
        public double? ImageWidth { get; set; }
        public double? Margin { get; set; }
        public GridPosition? Position { get; set; }
        public string Size { get; set; }
    }


    public class Track
    {
        public string Background { get; set; }
        public DropShadow DropShadow { get; set; }
        public double? EndAngle { get; set; }
        public double? Margin { get; set; }
        public double? Opacity { get; set; }
        public bool? Show { get; set; }
        public double? StartAngle { get; set; }
        public string StrokeWidth { get; set; }
    }

    public class Responsive
    {
        public double? Breakpoint { get; set; }
        public object Options { get; set; }
    }


    public class PurpleDatum
    {
        public string FillColor { get; set; }
        public string StrokeColor { get; set; }
        public object X { get; set; }
        public object Y { get; set; }
    }

    public class States
    {
        public StatesActive Active { get; set; }
        public StatesHover Hover { get; set; }
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
    /// Options for the line drawn on line and area charts.
    /// See https://apexcharts.com/docs/options/stroke/
    /// </summary>
    public class Stroke
    {
        public List<string> Colors { get; set; }
        public Curve? Curve { get; set; }
        public object DashArray { get; set; }
        public LineCap? LineCap { get; set; }
        public bool Show { get; set; } = true;
        public object Width { get; set; }
    }

    /// <summary>
    /// Chart Title options
    /// See https://apexcharts.com/docs/options/title/
    /// </summary>
    public class Subtitle
    {
        public Align? Align { get; set; }
        public bool? Floating { get; set; }
        public double? Margin { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public SubtitleStyle Style { get; set; }
        public string Text { get; set; }
    }

    public class SubtitleStyle
    {
        public string Color { get; set; }
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
        public object FontWeight { get; set; }
    }

    public class Theme
    {
        public Mode? Mode { get; set; }
        public ThemeMonochrome Monochrome { get; set; }
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
    /// Chart Title options
    /// See https://apexcharts.com/docs/options/title/
    /// </summary>
    public class Title
    {
        public Align? Align { get; set; }
        public bool? Floating { get; set; }
        public double? Margin { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public TitleStyle Style { get; set; }
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
    /// Chart Tooltip options
    /// See https://apexcharts.com/docs/options/tooltip/
    /// </summary>
    public class Tooltip
    {
        public string Custom { get; set; }
        public bool Enabled { get; set; } = true;
        public List<double> EnabledOnSeries { get; set; }
        public bool? FillSeriesColor { get; set; }
        public TooltipFixed Fixed { get; set; }
        public bool? FollowCursor { get; set; }
        public bool? Intersect { get; set; }
        public bool? InverseOrder { get; set; }
        public TooltipItems Items { get; set; }
        public TooltipMarker Marker { get; set; }
        public TooltipOnDatasetHover OnDatasetHover { get; set; }
        public bool? Shared { get; set; }
        public TooltipStyle Style { get; set; }
        public string Theme { get; set; }
        public TooltipX X { get; set; }
        public TooltipY Y { get; set; }
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
    /// X Axis options
    /// See https://apexcharts.com/docs/options/xaxis/
    /// </summary>
    public class XAxis
    {
        public AxisBorder AxisBorder { get; set; }
        public AxisTicks AxisTicks { get; set; }
        public object Categories { get; set; }
        public AxisCrosshairs Crosshairs { get; set; }
        public bool? Floating { get; set; }
        public int? DecimalsInFloat { get; set; }
        public XAxisLabels Labels { get; set; }
        public object Max { get; set; }
        public object Min { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public string Position { get; set; }
        public double? Range { get; set; }
        public bool? Sorted { get; set; }
        public object TickAmount { get; set; }
        public TickPlacement? TickPlacement { get; set; }
        public AxisTitle Title { get; set; }
        public AxisTooltip Tooltip { get; set; }
        public XAxisType? Type { get; set; }
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

    public class YAxis
    {
        public AxisBorder AxisBorder { get; set; }
        public AxisTicks AxisTicks { get; set; }
        public AxisCrosshairs Crosshairs { get; set; }
        public int? DecimalsInFloat { get; set; }
        public bool? Floating { get; set; }
        public bool? ForceNiceScale { get; set; }
        public YAxisLabels Labels { get; set; }
        public bool? Logarithmic { get; set; }
        public object Max { get; set; }
        public object Min { get; set; }
        public bool? Opposite { get; set; }
        public bool? Reversed { get; set; }
        public string SeriesName { get; set; }
        public bool? Show { get; set; }
        public bool? ShowAlways { get; set; }
        public bool? ShowForNullSeries { get; set; }
        public double? TickAmount { get; set; }
        public AxisTitle Title { get; set; }
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




    public enum Easing { Easein, Easeinout, Easeout, Linear };

    public enum StackType
    {
        Normal,
        [EnumMember(Value = "100%")]
        Percent100
    };

    public enum AutoSelected { Pan, Selection, Zoom };

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
    };

    public enum ZoomType { X, Xy, Y };

    public enum TextAnchor { End, Middle, Start };

    public enum GridPosition { Back, Front };

    public enum Align { Center, Left, Right };

    public enum LegendPosition { Bottom, Left, Right, Top };

    public enum ShapeEnum { Circle, Square, Rect };

    public enum VerticalAlign { Bottom, Middle, Top };

    public enum Orientation { Horizontal, Vertical };

    public enum Shape { Flat, Rounded };

    public enum Curve { Smooth, Stepline, Straight };

    public enum LineCap { Butt, Round, Square };

    public enum Mode { Dark, Light };

    public enum TickAmountEnum { DataPoints };

    public enum XAxisType { Category, Datetime, Numeric };

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
}