using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ApexCharts
{




    public partial class ApexChartOptions
    {
        public Annotations Annotations { get; set; }

        /// <summary>
        /// Main Chart options
        /// See https://apexcharts.com/docs/options/chart/
        /// </summary>
        public Chart Chart { get; set; }

        public List<string> Colors { get; set; }

        /// <summary>
        /// Chart Datalabels options
        /// See https://apexcharts.com/docs/options/datalabels/
        /// </summary>
        public DataLabels DataLabels { get; set; }

        public ApexChartsApexOptionsFill Fill { get; set; }

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

        public ApexChartsApexOptionsMarkers Markers { get; set; }
        public NoData NoData { get; set; }

        /// <summary>
        /// PlotOptions for specifying chart-type-specific configuration.
        /// See https://apexcharts.com/docs/options/plotoptions/bar/
        /// </summary>
        public PlotOptions PlotOptions { get; set; }

        public List<Responsive> Responsive { get; set; }

        public List<Series> Series { get; set; }

        public List<object> SeriesNonXAxis { get; internal set; }

        public States States { get; set; }

        /// <summary>
        /// Options for the line drawn on line and area charts.
        /// See https://apexcharts.com/docs/options/stroke/
        /// </summary>
        public ApexChartsApexOptionsStroke Stroke { get; set; }

        /// <summary>
        /// Chart Title options
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
        public ApexChartsApexOptionsTooltip Tooltip { get; set; }

        /// <summary>
        /// X Axis options
        /// See https://apexcharts.com/docs/options/xaxis/
        /// </summary>
        public XAxis Xaxis { get; set; }

        /// <summary>
        /// Y Axis options
        /// See https://apexcharts.com/docs/options/yaxis/
        /// </summary>
        public List<Yaxis> Yaxis { get; set; }
    }

    public partial class Annotations
    {
        public List<AnnotationsImage> Images { get; set; }
        public List<AnnotationsPoint> Points { get; set; }
        public string Position { get; set; }
        public List<AnnotationsShape> Shapes { get; set; }
        public List<AnnotationsText> Texts { get; set; }
        public List<AnnotationsXaxi> Xaxis { get; set; }
        public List<AnnotationsYaxi> Yaxis { get; set; }
    }

    public partial class AnnotationsImage
    {
        public double? Height { get; set; }
        public string Path { get; set; }
        public double? Width { get; set; }
        public double? X { get; set; }
        public double? Y { get; set; }
    }

    public partial class AnnotationsPoint
    {
        public PurpleImage Image { get; set; }
        public PurpleLabel Label { get; set; }
        public PurpleMarker Marker { get; set; }
        public double? SeriesIndex { get; set; }
        public FontWeight? X { get; set; }
        public double? Y { get; set; }
        public double? YAxisIndex { get; set; }
    }

    public partial class PurpleImage
    {
        public double? Height { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public string Path { get; set; }
        public double? Width { get; set; }
    }

    public partial class PurpleLabel
    {
        public string BorderColor { get; set; }
        public double? BorderWidth { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public string Orientation { get; set; }
        public string Position { get; set; }
        public PurpleStyle Style { get; set; }
        public string Text { get; set; }
        public string TextAnchor { get; set; }
    }

    public partial class PurpleStyle
    {
        public string Background { get; set; }
        public string Color { get; set; }
        public string CssClass { get; set; }
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
        public FontWeight? FontWeight { get; set; }
        public PurplePadding Padding { get; set; }
    }

    public partial class PurplePadding
    {
        public double? Bottom { get; set; }
        public double? Left { get; set; }
        public double? Right { get; set; }
        public double? Top { get; set; }
    }

    public partial class PurpleMarker
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

    public partial class AnnotationsShape
    {
        public string BackgroundColor { get; set; }
        public string BorderColor { get; set; }
        public double? BorderRadius { get; set; }
        public double? BorderWidth { get; set; }
        public double? Height { get; set; }
        public double? Opacity { get; set; }
        public string Type { get; set; }
        public FontWeight? Width { get; set; }
        public double? X { get; set; }
        public double? Y { get; set; }
    }

    public partial class AnnotationsText
    {
        public string BackgroundColor { get; set; }
        public string BorderColor { get; set; }
        public double? BorderRadius { get; set; }
        public double? BorderWidth { get; set; }
        public string FontFamily { get; set; }
        public FontWeight? FontSize { get; set; }
        public FontWeight? FontWeight { get; set; }
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

    public partial class AnnotationsXaxi
    {
        public string BorderColor { get; set; }
        public double? BorderWidth { get; set; }
        public string FillColor { get; set; }
        public FluffyLabel Label { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public double? Opacity { get; set; }
        public double? StrokeDashArray { get; set; }
        public X? X { get; set; }
        public X? X2 { get; set; }
    }

    public partial class FluffyLabel
    {
        public string BorderColor { get; set; }
        public double? BorderWidth { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public string Orientation { get; set; }
        public string Position { get; set; }
        public FluffyStyle Style { get; set; }
        public string Text { get; set; }
        public string TextAnchor { get; set; }
    }

    public partial class FluffyStyle
    {
        public string Background { get; set; }
        public string Color { get; set; }
        public string CssClass { get; set; }
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
        public FontWeight? FontWeight { get; set; }
        public FluffyPadding Padding { get; set; }
    }

    public partial class FluffyPadding
    {
        public double? Bottom { get; set; }
        public double? Left { get; set; }
        public double? Right { get; set; }
        public double? Top { get; set; }
    }

    public partial class AnnotationsYaxi
    {
        public string BorderColor { get; set; }
        public double? BorderWidth { get; set; }
        public string FillColor { get; set; }
        public TentacledLabel Label { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public double? Opacity { get; set; }
        public double? StrokeDashArray { get; set; }
        public X? Y { get; set; }
        public X? Y2 { get; set; }
        public double? YAxisIndex { get; set; }
    }

    public partial class TentacledLabel
    {
        public string BorderColor { get; set; }
        public double? BorderWidth { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public string Orientation { get; set; }
        public string Position { get; set; }
        public TentacledStyle Style { get; set; }
        public string Text { get; set; }
        public string TextAnchor { get; set; }
    }

    public partial class TentacledStyle
    {
        public string Background { get; set; }
        public string Color { get; set; }
        public string CssClass { get; set; }
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
        public FontWeight? FontWeight { get; set; }
        public TentacledPadding Padding { get; set; }
    }

    public partial class TentacledPadding
    {
        public double? Bottom { get; set; }
        public double? Left { get; set; }
        public double? Right { get; set; }
        public double? Top { get; set; }
    }

    /// <summary>
    /// Main Chart options
    /// See https://apexcharts.com/docs/options/chart/
    /// </summary>
    public partial class Chart
    {
        public ChartAnimations Animations { get; set; }
        public string Background { get; set; }
        public ChartBrush Brush { get; set; }
        public string DefaultLocale { get; set; }
        public ChartDropShadow DropShadow { get; set; }
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
        public ChartSelection Selection { get; set; }
        public ChartSparkline Sparkline { get; set; }
        public bool? Stacked { get; set; }
        public StackType? StackType { get; set; }
        public ChartToolbar Toolbar { get; set; }
        public ChartType Type { get; set; }
        public object Width { get; set; }
        public object Height { get; set; }
        public ChartZoom Zoom { get; set; }
    }

    public partial class ChartAnimations
    {
        public PurpleAnimateGradually AnimateGradually { get; set; }
        public PurpleDynamicAnimation DynamicAnimation { get; set; }
        public Easing? Easing { get; set; }
        public bool? Enabled { get; set; }
        public double? Speed { get; set; }
    }

    public partial class PurpleAnimateGradually
    {
        public double? Delay { get; set; }
        public bool? Enabled { get; set; }
    }

    public partial class PurpleDynamicAnimation
    {
        public bool? Enabled { get; set; }
        public double? Speed { get; set; }
    }

    public partial class ChartBrush
    {
        public bool? AutoScaleYaxis { get; set; }
        public bool? Enabled { get; set; }
        public string Target { get; set; }
    }

    public partial class ChartDropShadow
    {
        public double? Blur { get; set; }
        public string Color { get; set; }
        public bool? Enabled { get; set; }
        public double? Left { get; set; }
        public double? Opacity { get; set; }
        public double? Top { get; set; }
        public List<double> EnabledOnSeries { get; set; }
    }

    public partial class ChartLocale
    {
        public string Name { get; set; }
        public PurpleOptions Options { get; set; }
    }

    public partial class PurpleOptions
    {
        public List<string> Days { get; set; }
        public List<string> Months { get; set; }
        public List<string> ShortDays { get; set; }
        public List<string> ShortMonths { get; set; }
        public PurpleToolbar Toolbar { get; set; }
    }

    public partial class PurpleToolbar
    {
        public string Download { get; set; }
        public string Pan { get; set; }
        public string Reset { get; set; }
        public string Selection { get; set; }
        public string SelectionZoom { get; set; }
        public string ZoomIn { get; set; }
        public string ZoomOut { get; set; }
    }

    public partial class ChartSelection
    {
        public bool? Enabled { get; set; }
        public PurpleFill Fill { get; set; }
        public PurpleStroke Stroke { get; set; }
        public string Type { get; set; }
        public PurpleXaxis Xaxis { get; set; }
        public PurpleYaxis Yaxis { get; set; }
    }

    public partial class PurpleFill
    {
        public string Color { get; set; }
        public double? Opacity { get; set; }
    }

    public partial class PurpleStroke
    {
        public string Color { get; set; }
        public double? DashArray { get; set; }
        public double? Opacity { get; set; }
        public double? Width { get; set; }
    }

    public partial class PurpleXaxis
    {
        public double? Max { get; set; }
        public double? Min { get; set; }
    }

    public partial class PurpleYaxis
    {
        public double? Max { get; set; }
        public double? Min { get; set; }
    }

    public partial class ChartSparkline
    {
        public bool? Enabled { get; set; }
    }

    public partial class ChartToolbar
    {
        public AutoSelected? AutoSelected { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public bool? Show { get; set; }
        public PurpleTools Tools { get; set; }
    }

    public partial class PurpleTools
    {
        public List<PurpleCustomIcon> CustomIcons { get; set; }
        public Download? Download { get; set; }
        public Download? Pan { get; set; }
        public Download? Reset { get; set; }
        public Download? Selection { get; set; }
        public Download? Zoom { get; set; }
        public Download? Zoomin { get; set; }
        public Download? Zoomout { get; set; }
    }

    public partial class PurpleCustomIcon
    {
        public string Icon { get; set; }
        public double? Index { get; set; }
        public string Title { get; set; }
    }

    public partial class ChartZoom
    {
        public bool? AutoScaleYaxis { get; set; }
        public bool? Enabled { get; set; }
        public ZoomType? Type { get; set; }
        public PurpleZoomedArea ZoomedArea { get; set; }
    }

    public partial class PurpleZoomedArea
    {
        public FluffyFill Fill { get; set; }
        public FluffyStroke Stroke { get; set; }
    }

    public partial class FluffyFill
    {
        public string Color { get; set; }
        public double? Opacity { get; set; }
    }

    public partial class FluffyStroke
    {
        public string Color { get; set; }
        public double? Opacity { get; set; }
        public double? Width { get; set; }
    }

    /// <summary>
    /// Chart Datalabels options
    /// See https://apexcharts.com/docs/options/datalabels/
    /// </summary>
    public partial class DataLabels
    {
        public DataLabelsBackground Background { get; set; }
        public bool? Distributed { get; set; }
        public DataLabelsDropShadow DropShadow { get; set; }
        public bool? Enabled { get; set; }
        public List<double> EnabledOnSeries { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public DataLabelsStyle Style { get; set; }
        public TextAnchor? TextAnchor { get; set; }
    }

    public partial class DataLabelsBackground
    {
        public string BorderColor { get; set; }
        public double? BorderRadius { get; set; }
        public double? BorderWidth { get; set; }
        public PurpleDropShadow DropShadow { get; set; }
        public bool? Enabled { get; set; }
        public string ForeColor { get; set; }
        public double? Opacity { get; set; }
        public double? Padding { get; set; }
    }

    public partial class PurpleDropShadow
    {
        public double? Blur { get; set; }
        public string Color { get; set; }
        public bool? Enabled { get; set; }
        public double? Left { get; set; }
        public double? Opacity { get; set; }
        public double? Top { get; set; }
    }

    public partial class DataLabelsDropShadow
    {
        public double? Blur { get; set; }
        public string Color { get; set; }
        public bool? Enabled { get; set; }
        public double? Left { get; set; }
        public double? Opacity { get; set; }
        public double? Top { get; set; }
    }

    public partial class DataLabelsStyle
    {
        public List<string> Colors { get; set; }
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
        public FontWeight? FontWeight { get; set; }
    }

    public partial class ApexChartsApexOptionsFill
    {
        public List<object> Colors { get; set; }
        public PurpleGradient Gradient { get; set; }
        public FillImage Image { get; set; }
        public Opacity? Opacity { get; set; }
        public FillPattern Pattern { get; set; }
        public Color? Type { get; set; }
    }

    public partial class PurpleGradient
    {
        public List<string> GradientToColors { get; set; }
        public bool? InverseColors { get; set; }
        public double? OpacityFrom { get; set; }
        public double? OpacityTo { get; set; }
        public string Shade { get; set; }
        public double? ShadeIntensity { get; set; }
        public List<double> Stops { get; set; }
        public string Type { get; set; }
    }

    public partial class FillImage
    {
        public double? Height { get; set; }
        public Color? Src { get; set; }
        public double? Width { get; set; }
    }

    public partial class FillPattern
    {
        public double? Height { get; set; }
        public double? StrokeWidth { get; set; }
        public Color? Style { get; set; }
        public double? Width { get; set; }
    }

    /// <summary>
    /// Plot X and Y grid options
    /// See https://apexcharts.com/docs/options/grid/
    /// </summary>
    public partial class Grid
    {
        public string BorderColor { get; set; }
        public GridColumn Column { get; set; }
        public GridPadding Padding { get; set; }
        public GridPosition? Position { get; set; }
        public GridRow Row { get; set; }
        public bool? Show { get; set; }
        public double? StrokeDashArray { get; set; }
        public GridXaxis Xaxis { get; set; }
        public GridYaxis Yaxis { get; set; }
    }

    public partial class GridColumn
    {
        public List<string> Colors { get; set; }
        public double? Opacity { get; set; }
    }

    public partial class GridPadding
    {
        public double? Bottom { get; set; }
        public double? Left { get; set; }
        public double? Right { get; set; }
        public double? Top { get; set; }
    }

    public partial class GridRow
    {
        public List<string> Colors { get; set; }
        public double? Opacity { get; set; }
    }

    public partial class GridXaxis
    {
        public PurpleLines Lines { get; set; }
    }

    public partial class PurpleLines
    {
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public bool? Show { get; set; }
    }

    public partial class GridYaxis
    {
        public FluffyLines Lines { get; set; }
    }

    public partial class FluffyLines
    {
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public bool? Show { get; set; }
    }

    /// <summary>
    /// Chart Legend configuration options.
    /// See https://apexcharts.com/docs/options/legend/
    /// </summary>
    public partial class Legend
    {
        public LegendContainerMargin ContainerMargin { get; set; }
        public bool? Floating { get; set; }
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
        public FontWeight? FontWeight { get; set; }
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

    public partial class LegendContainerMargin
    {
        public double? Left { get; set; }
        public double? Top { get; set; }
    }

    public partial class LegendItemMargin
    {
        public double? Horizontal { get; set; }
        public double? Vertical { get; set; }
    }

    public partial class LegendLabels
    {
        public Color? Colors { get; set; }
        public bool? UseSeriesColors { get; set; }
    }

    public partial class LegendMarkers
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

    public partial class LegendOnItemClick
    {
        public bool? ToggleDataSeries { get; set; }
    }

    public partial class LegendOnItemHover
    {
        public bool? HighlightDataSeries { get; set; }
    }

    public partial class ApexChartsApexOptionsMarkers
    {
        public List<string> Colors { get; set; }
        public List<MarkersDiscrete> Discrete { get; set; }
        public Opacity? FillOpacity { get; set; }
        public MarkersHover Hover { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public double? Radius { get; set; }
        public ShapeEnum? Shape { get; set; }
        public bool? ShowNullDataPoints { get; set; }
        public double? Size { get; set; }
        public Color? StrokeColors { get; set; }
        public Opacity? StrokeDashArray { get; set; }
        public Opacity? StrokeOpacity { get; set; }
        public Opacity? StrokeWidth { get; set; }
    }

    public partial class MarkersDiscrete
    {
        public double? DataPointIndex { get; set; }
        public string FillColor { get; set; }
        public double? SeriesIndex { get; set; }
        public double? Size { get; set; }
        public string StrokeColor { get; set; }
    }

    public partial class MarkersHover
    {
        public double? Size { get; set; }
        public double? SizeOffset { get; set; }
    }

    public partial class NoData
    {
        public Align? Align { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public NoDataStyle Style { get; set; }
        public string Text { get; set; }
        public VerticalAlign? VerticalAlign { get; set; }
    }

    public partial class NoDataStyle
    {
        public string Color { get; set; }
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
    }

    /// <summary>
    /// PlotOptions for specifying chart-type-specific configuration.
    /// See https://apexcharts.com/docs/options/plotoptions/bar/
    /// </summary>
    public partial class PlotOptions
    {
        public PlotOptionsBar Bar { get; set; }
        public PlotOptionsBubble Bubble { get; set; }
        public PlotOptionsCandlestick Candlestick { get; set; }
        public PlotOptionsHeatmap Heatmap { get; set; }
        public PlotOptionsPie Pie { get; set; }
        public PlotOptionsPolarArea PolarArea { get; set; }
        public PlotOptionsRadar Radar { get; set; }
        public PlotOptionsRadialBar RadialBar { get; set; }
    }

    public partial class PlotOptionsBar
    {
        public string BarHeight { get; set; }
        public PurpleColors Colors { get; set; }
        public string ColumnWidth { get; set; }
        public PurpleDataLabels DataLabels { get; set; }
        public bool? Distributed { get; set; }
        public IngShape? EndingShape { get; set; }
        public bool? Horizontal { get; set; }
        public bool? RangeBarOverlap { get; set; }
        public IngShape? StartingShape { get; set; }
    }

    public partial class PurpleColors
    {
        public List<string> BackgroundBarColors { get; set; }
        public double? BackgroundBarOpacity { get; set; }
        public double? BackgroundBarRadius { get; set; }
        public List<PurpleRange> Ranges { get; set; }
    }

    public partial class PurpleRange
    {
        public string Color { get; set; }
        public double? From { get; set; }
        public double? To { get; set; }
    }

    public partial class PurpleDataLabels
    {
        public bool? HideOverflowingLabels { get; set; }
        public double? MaxItems { get; set; }
        public Orientation? Orientation { get; set; }
        public string Position { get; set; }
    }

    public partial class PlotOptionsBubble
    {
        public double? MaxBubbleRadius { get; set; }
        public double? MinBubbleRadius { get; set; }
    }

    public partial class PlotOptionsCandlestick
    {
        public FluffyColors Colors { get; set; }
        public PurpleWick Wick { get; set; }
    }

    public partial class FluffyColors
    {
        public string Downward { get; set; }
        public string Upward { get; set; }
    }

    public partial class PurpleWick
    {
        public bool? UseFillColor { get; set; }
    }

    public partial class PlotOptionsHeatmap
    {
        public PurpleColorScale ColorScale { get; set; }
        public bool? Distributed { get; set; }
        public bool? EnableShades { get; set; }
        public double? Radius { get; set; }
        public bool? ReverseNegativeShade { get; set; }
        public double? ShadeIntensity { get; set; }
        public bool? UseFillColorAsStroke { get; set; }
    }

    public partial class PurpleColorScale
    {
        public bool? Inverse { get; set; }
        public double? Max { get; set; }
        public double? Min { get; set; }
        public List<FluffyRange> Ranges { get; set; }
    }

    public partial class FluffyRange
    {
        public string Color { get; set; }
        public string ForeColor { get; set; }
        public double? From { get; set; }
        public string Name { get; set; }
        public double? To { get; set; }
    }

    public partial class PlotOptionsPie
    {
        public double? CustomScale { get; set; }
        public FluffyDataLabels DataLabels { get; set; }
        public PlotOptionsDonut Donut { get; set; }
        public bool? ExpandOnClick { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
    }

    public partial class FluffyDataLabels
    {
        public double? MinAngleToShowLabel { get; set; }
        public double? Offset { get; set; }
    }

    public partial class PlotOptionsDonut
    {
        public string Background { get; set; }
        public DonutLabels Labels { get; set; }
        public string Size { get; set; }
    }

    public partial class DonutLabels
    {
        public DonutLabelName Name { get; set; }
        public bool Show { get; set; } = true;
        public DonutLabelTotal Total { get; set; }
        public DonutLabelValue Value { get; set; }
    }

    public partial class DonutLabelName
    {
        public string Color { get; set; }
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
        public FontWeight? FontWeight { get; set; }
        public double? OffsetY { get; set; }
        public bool Show { get; set; } = true;
    }

    public partial class DonutLabelTotal
    {
        public string Color { get; set; }
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
        public FontWeight? FontWeight { get; set; }
        public string Label { get; set; }
        public bool Show { get; set; } = true;
        public bool? ShowAlways { get; set; }
    }

    public partial class DonutLabelValue
    {
        public string Color { get; set; }
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
        public FontWeight? FontWeight { get; set; }
        public double? OffsetY { get; set; }
        public bool Show { get; set; } = true;
    }

    public partial class PlotOptionsPolarArea
    {
        public PolarAreaRings Rings { get; set; }
    }

    public partial class PolarAreaRings
    {
        public string StrokeColor { get; set; }
        public double? StrokeWidth { get; set; }
    }

    public partial class PlotOptionsRadar
    {
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public PurplePolygons Polygons { get; set; }
        public double? Size { get; set; }
    }

    public partial class PurplePolygons
    {
        public Color? ConnectorColors { get; set; }
        public TentacledFill Fill { get; set; }
        public Color? StrokeColors { get; set; }
        public Color? StrokeWidth { get; set; }
    }

    public partial class TentacledFill
    {
        public List<string> Colors { get; set; }
    }

    public partial class PlotOptionsRadialBar
    {
        public TentacledDataLabels DataLabels { get; set; }
        public double? EndAngle { get; set; }
        public PurpleHollow Hollow { get; set; }
        public bool? InverseOrder { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public double? StartAngle { get; set; }
        public PurpleTrack Track { get; set; }
    }

    public partial class TentacledDataLabels
    {
        public FluffyName Name { get; set; }
        public bool? Show { get; set; }
        public FluffyTotal Total { get; set; }
        public FluffyValue Value { get; set; }
    }

    public partial class FluffyName
    {
        public string Color { get; set; }
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
        public FontWeight? FontWeight { get; set; }
        public double? OffsetY { get; set; }
        public bool? Show { get; set; }
    }

    public partial class FluffyTotal
    {
        public string Color { get; set; }
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
        public FontWeight? FontWeight { get; set; }
        public string Label { get; set; }
        public bool? Show { get; set; }
    }

    public partial class FluffyValue
    {
        public string Color { get; set; }
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
        public FontWeight? FontWeight { get; set; }
        public double? OffsetY { get; set; }
        public bool? Show { get; set; }
    }

    public partial class PurpleHollow
    {
        public string Background { get; set; }
        public FluffyDropShadow DropShadow { get; set; }
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

    public partial class FluffyDropShadow
    {
        public double? Blur { get; set; }
        public string Color { get; set; }
        public bool? Enabled { get; set; }
        public double? Left { get; set; }
        public double? Opacity { get; set; }
        public double? Top { get; set; }
    }

    public partial class PurpleTrack
    {
        public string Background { get; set; }
        public TentacledDropShadow DropShadow { get; set; }
        public double? EndAngle { get; set; }
        public double? Margin { get; set; }
        public double? Opacity { get; set; }
        public bool? Show { get; set; }
        public double? StartAngle { get; set; }
        public string StrokeWidth { get; set; }
    }

    public partial class TentacledDropShadow
    {
        public double? Blur { get; set; }
        public string Color { get; set; }
        public bool? Enabled { get; set; }
        public double? Left { get; set; }
        public double? Opacity { get; set; }
        public double? Top { get; set; }
    }

    public partial class Responsive
    {
        public double? Breakpoint { get; set; }
        public object Options { get; set; }
    }


    public partial class PurpleDatum
    {
        public string FillColor { get; set; }
        public string StrokeColor { get; set; }
        public object X { get; set; }
        public object Y { get; set; }
    }

    public partial class States
    {
        public StatesActive Active { get; set; }
        public StatesHover Hover { get; set; }
        public StatesNormal Normal { get; set; }
    }

    public partial class StatesActive
    {
        public bool? AllowMultipleDataPointsSelection { get; set; }
        public PurpleFilter Filter { get; set; }
    }

    public partial class PurpleFilter
    {
        public string Type { get; set; }
        public double? Value { get; set; }
    }

    public partial class StatesHover
    {
        public FluffyFilter Filter { get; set; }
    }

    public partial class FluffyFilter
    {
        public string Type { get; set; }
        public double? Value { get; set; }
    }

    public partial class StatesNormal
    {
        public TentacledFilter Filter { get; set; }
    }

    public partial class TentacledFilter
    {
        public string Type { get; set; }
        public double? Value { get; set; }
    }

    /// <summary>
    /// Options for the line drawn on line and area charts.
    /// See https://apexcharts.com/docs/options/stroke/
    /// </summary>
    public partial class ApexChartsApexOptionsStroke
    {
        public List<string> Colors { get; set; }
        public Curve? Curve { get; set; }
        public Opacity? DashArray { get; set; }
        public LineCap? LineCap { get; set; }
        public bool? Show { get; set; }
        public Opacity? Width { get; set; }
    }

    /// <summary>
    /// Chart Title options
    /// See https://apexcharts.com/docs/options/title/
    /// </summary>
    public partial class Subtitle
    {
        public Align? Align { get; set; }
        public bool? Floating { get; set; }
        public double? Margin { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public SubtitleStyle Style { get; set; }
        public string Text { get; set; }
    }

    public partial class SubtitleStyle
    {
        public string Color { get; set; }
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
        public FontWeight? FontWeight { get; set; }
    }

    public partial class Theme
    {
        public Mode? Mode { get; set; }
        public ThemeMonochrome Monochrome { get; set; }
        public PaletteType Palette { get; set; }
    }

   

    public partial class ThemeMonochrome
    {
        public string Color { get; set; }
        public bool? Enabled { get; set; }
        public double? ShadeIntensity { get; set; }
        public Mode? ShadeTo { get; set; }
    }

    /// <summary>
    /// Chart Title options
    /// See https://apexcharts.com/docs/options/title/
    /// </summary>
    public partial class Title
    {
        public Align? Align { get; set; }
        public bool? Floating { get; set; }
        public double? Margin { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public StickyStyle Style { get; set; }
        public string Text { get; set; }
    }

    public partial class StickyStyle
    {
        public string Color { get; set; }
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
        public FontWeight? FontWeight { get; set; }
    }

    /// <summary>
    /// Chart Tooltip options
    /// See https://apexcharts.com/docs/options/tooltip/
    /// </summary>
    public partial class ApexChartsApexOptionsTooltip
    {
        public Custom? Custom { get; set; }
        public bool? Enabled { get; set; }
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
        public IndigoStyle Style { get; set; }
        public string Theme { get; set; }
        public TooltipX X { get; set; }
        public TooltipY? Y { get; set; }
        public TooltipZ Z { get; set; }
    }

    public partial class TooltipFixed
    {
        public bool? Enabled { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public string Position { get; set; }
    }

    public partial class TooltipItems
    {
        public string Display { get; set; }
    }

    public partial class TooltipMarker
    {
        public List<string> FillColors { get; set; }
        public bool? Show { get; set; }
    }

    public partial class TooltipOnDatasetHover
    {
        public bool? HighlightDataSeries { get; set; }
    }

    public partial class IndigoStyle
    {
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
    }

    public partial class TooltipX
    {
        public string Format { get; set; }
        public bool? Show { get; set; }
    }

    public partial class PurpleY
    {
        public Dictionary<string, object> Title { get; set; }
    }

    public partial class FluffyY
    {
        public Dictionary<string, object> Title { get; set; }
    }

    public partial class TooltipZ
    {
        public string Title { get; set; }
    }

    /// <summary>
    /// X Axis options
    /// See https://apexcharts.com/docs/options/xaxis/
    /// </summary>
    public  class XAxis
    {
        public XaxisAxisBorder AxisBorder { get; set; }
        public XaxisAxisTicks AxisTicks { get; set; }
        public object Categories { get; set; }
        public XaxisCrosshairs Crosshairs { get; set; }
        public bool? Floating { get; set; }
        public XaxisLabels Labels { get; set; }
        public double? Max { get; set; }
        public double? Min { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public string Position { get; set; }
        public double? Range { get; set; }
        public bool? Sorted { get; set; }
        public TickAmountUnion? TickAmount { get; set; }
        public string TickPlacement { get; set; }
        public XaxisTitle Title { get; set; }
        public XaxisTooltip Tooltip { get; set; }
        public XaxisType? Type { get; set; }
    }

    public partial class XaxisAxisBorder
    {
        public string Color { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public bool? Show { get; set; }
        public double? StrokeWidth { get; set; }
    }

    public partial class XaxisAxisTicks
    {
        public string BorderType { get; set; }
        public string Color { get; set; }
        public double? Height { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public bool? Show { get; set; }
    }

    public partial class XaxisCrosshairs
    {
        public StickyDropShadow DropShadow { get; set; }
        public StickyFill Fill { get; set; }
        public double? Opacity { get; set; }
        public string Position { get; set; }
        public bool? Show { get; set; }
        public TentacledStroke Stroke { get; set; }
        public FontWeight? Width { get; set; }
    }

    public partial class StickyDropShadow
    {
        public double? Blur { get; set; }
        public string Color { get; set; }
        public bool? Enabled { get; set; }
        public double? Left { get; set; }
        public double? Opacity { get; set; }
        public double? Top { get; set; }
    }

    public partial class StickyFill
    {
        public string Color { get; set; }
        public FluffyGradient Gradient { get; set; }
        public string Type { get; set; }
    }

    public partial class FluffyGradient
    {
        public string ColorFrom { get; set; }
        public string ColorTo { get; set; }
        public double? OpacityFrom { get; set; }
        public double? OpacityTo { get; set; }
        public List<double> Stops { get; set; }
    }

    public partial class TentacledStroke
    {
        public string Color { get; set; }
        public double? DashArray { get; set; }
        public double? Width { get; set; }
    }

    public partial class XaxisLabels
    {
        public PurpleDatetimeFormatter DatetimeFormatter { get; set; }
        public bool? DatetimeUtc { get; set; }
        public string Format { get; set; }
        public bool? HideOverlappingLabels { get; set; }
        public double? MaxHeight { get; set; }
        public double? MinHeight { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public double? Rotate { get; set; }
        public bool? RotateAlways { get; set; }
        public bool? Show { get; set; }
        public bool? ShowDuplicates { get; set; }
        public IndecentStyle Style { get; set; }
        public bool? Trim { get; set; }
    }

    public partial class PurpleDatetimeFormatter
    {
        public string Day { get; set; }
        public string Hour { get; set; }
        public string Minute { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
    }

    public partial class IndecentStyle
    {
        public Color? Colors { get; set; }
        public string CssClass { get; set; }
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
        public FontWeight? FontWeight { get; set; }
    }

    public partial class XaxisTitle
    {
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public HilariousStyle Style { get; set; }
        public string Text { get; set; }
    }

    public partial class HilariousStyle
    {
        public string Color { get; set; }
        public string CssClass { get; set; }
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
        public FontWeight? FontWeight { get; set; }
    }

    public partial class XaxisTooltip
    {
        public bool? Enabled { get; set; }
        public double? OffsetY { get; set; }
        public AmbitiousStyle Style { get; set; }
    }

    public partial class AmbitiousStyle
    {
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
    }

    public partial class Yaxis
    {
        public YaxiAxisBorder AxisBorder { get; set; }
        public YaxiAxisTicks AxisTicks { get; set; }
        public YaxiCrosshairs Crosshairs { get; set; }
        public double? DecimalsInFloat { get; set; }
        public bool? Floating { get; set; }
        public bool? ForceNiceScale { get; set; }
        public YaxiLabels Labels { get; set; }
        public bool? Logarithmic { get; set; }
        public Max? Max { get; set; }
        public Max? Min { get; set; }
        public bool? Opposite { get; set; }
        public bool? Reversed { get; set; }
        public string SeriesName { get; set; }
        public bool? Show { get; set; }
        public bool? ShowAlways { get; set; }
        public bool? ShowForNullSeries { get; set; }
        public double? TickAmount { get; set; }
        public YaxisTitle Title { get; set; }
        public YaxiTooltip Tooltip { get; set; }
    }

    public partial class YaxiAxisBorder
    {
        public string Color { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public bool? Show { get; set; }
        public double? Width { get; set; }
    }

    public partial class YaxiAxisTicks
    {
        public string Color { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public bool? Show { get; set; }
        public double? Width { get; set; }
    }

    public partial class YaxiCrosshairs
    {
        public string Position { get; set; }
        public bool? Show { get; set; }
        public StickyStroke Stroke { get; set; }
    }

    public partial class StickyStroke
    {
        public string Color { get; set; }
        public double? DashArray { get; set; }
        public double? Width { get; set; }
    }

    public partial class YaxiLabels
    {
        public Align? Align { get; set; }
        public double? MaxWidth { get; set; }
        public double? MinWidth { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public double? Padding { get; set; }
        public double? Rotate { get; set; }
        public bool? Show { get; set; }
        public CunningStyle Style { get; set; }
    }

    public partial class CunningStyle
    {
        public string Colors { get; set; }
        public string CssClass { get; set; }
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
        public FontWeight? FontWeight { get; set; }
    }

    public partial class YaxiTitle
    {
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public double? Rotate { get; set; }
        public MagentaStyle Style { get; set; }
        public string Text { get; set; }
    }

    public partial class MagentaStyle
    {
        public string Color { get; set; }
        public string CssClass { get; set; }
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
        public FontWeight? FontWeight { get; set; }
    }

    public partial class YaxiTooltip
    {
        public bool? Enabled { get; set; }
        public double? OffsetX { get; set; }
    }

    public partial class YaxisYaxis
    {
        public YaxisAxisBorder AxisBorder { get; set; }
        public YaxisAxisTicks AxisTicks { get; set; }
        public YaxisCrosshairs Crosshairs { get; set; }
        public double? DecimalsInFloat { get; set; }
        public bool? Floating { get; set; }
        public bool? ForceNiceScale { get; set; }
        public YaxisLabels Labels { get; set; }
        public bool? Logarithmic { get; set; }
        public Max? Max { get; set; }
        public Max? Min { get; set; }
        public bool? Opposite { get; set; }
        public bool? Reversed { get; set; }
        public string SeriesName { get; set; }
        public bool? Show { get; set; }
        public bool? ShowAlways { get; set; }
        public bool? ShowForNullSeries { get; set; }
        public double? TickAmount { get; set; }
        public YaxisTitle Title { get; set; }
        public YaxisTooltip Tooltip { get; set; }
    }

    public partial class YaxisAxisBorder
    {
        public string Color { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public bool? Show { get; set; }
        public double? Width { get; set; }
    }

    public partial class YaxisAxisTicks
    {
        public string Color { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public bool? Show { get; set; }
        public double? Width { get; set; }
    }

    public partial class YaxisCrosshairs
    {
        public string Position { get; set; }
        public bool? Show { get; set; }
        public IndigoStroke Stroke { get; set; }
    }

    public partial class IndigoStroke
    {
        public string Color { get; set; }
        public double? DashArray { get; set; }
        public double? Width { get; set; }
    }

    public partial class YaxisLabels
    {
        public Align? Align { get; set; }
        public double? MaxWidth { get; set; }
        public double? MinWidth { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public double? Padding { get; set; }
        public double? Rotate { get; set; }
        public bool? Show { get; set; }
        public FriskyStyle Style { get; set; }
    }

    public partial class FriskyStyle
    {
        public string Colors { get; set; }
        public string CssClass { get; set; }
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
        public FontWeight? FontWeight { get; set; }
    }

    public partial class YaxisTitle
    {
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public double? Rotate { get; set; }
        public MischievousStyle Style { get; set; }
        public string Text { get; set; }
    }

    public partial class MischievousStyle
    {
        public string Color { get; set; }
        public string CssClass { get; set; }
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
        public FontWeight? FontWeight { get; set; }
    }

    public partial class YaxisTooltip
    {
        public bool? Enabled { get; set; }
        public double? OffsetX { get; set; }
    }

    public partial class ApexDropShadow
    {
        public double? Blur { get; set; }
        public string Color { get; set; }
        public bool? Enabled { get; set; }
        public double? Left { get; set; }
        public double? Opacity { get; set; }
        public double? Top { get; set; }
    }

    public partial class ApexChart
    {
        public ApexChartAnimations Animations { get; set; }
        public string Background { get; set; }
        public ApexChartBrush Brush { get; set; }
        public string DefaultLocale { get; set; }
        public ApexChartDropShadow DropShadow { get; set; }
        public Dictionary<string, object> Events { get; set; }
        public string FontFamily { get; set; }
        public string ForeColor { get; set; }
        public string Group { get; set; }
        public FontWeight? Height { get; set; }
        public string Id { get; set; }
        public List<ApexChartLocale> Locales { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public double? ParentHeightOffset { get; set; }
        public bool? RedrawOnParentResize { get; set; }
        public ApexChartSelection Selection { get; set; }
        public ApexChartSparkline Sparkline { get; set; }
        public bool? Stacked { get; set; }
        public StackType? StackType { get; set; }
        public ApexChartToolbar Toolbar { get; set; }
        public ChartType? Type { get; set; }
        public FontWeight? Width { get; set; }
        public ApexChartZoom Zoom { get; set; }
    }

    public partial class ApexChartAnimations
    {
        public FluffyAnimateGradually AnimateGradually { get; set; }
        public FluffyDynamicAnimation DynamicAnimation { get; set; }
        public Easing? Easing { get; set; }
        public bool? Enabled { get; set; }
        public double? Speed { get; set; }
    }

    public partial class FluffyAnimateGradually
    {
        public double? Delay { get; set; }
        public bool? Enabled { get; set; }
    }

    public partial class FluffyDynamicAnimation
    {
        public bool? Enabled { get; set; }
        public double? Speed { get; set; }
    }

    public partial class ApexChartBrush
    {
        public bool? AutoScaleYaxis { get; set; }
        public bool? Enabled { get; set; }
        public string Target { get; set; }
    }

    public partial class ApexChartDropShadow
    {
        public double? Blur { get; set; }
        public string Color { get; set; }
        public bool? Enabled { get; set; }
        public double? Left { get; set; }
        public double? Opacity { get; set; }
        public double? Top { get; set; }
        public List<double> EnabledOnSeries { get; set; }
    }

    public partial class ApexChartLocale
    {
        public string Name { get; set; }
        public FluffyOptions Options { get; set; }
    }

    public partial class FluffyOptions
    {
        public List<string> Days { get; set; }
        public List<string> Months { get; set; }
        public List<string> ShortDays { get; set; }
        public List<string> ShortMonths { get; set; }
        public FluffyToolbar Toolbar { get; set; }
    }

    public partial class FluffyToolbar
    {
        public string Download { get; set; }
        public string Pan { get; set; }
        public string Reset { get; set; }
        public string Selection { get; set; }
        public string SelectionZoom { get; set; }
        public string ZoomIn { get; set; }
        public string ZoomOut { get; set; }
    }

    public partial class ApexChartSelection
    {
        public bool? Enabled { get; set; }
        public IndigoFill Fill { get; set; }
        public IndecentStroke Stroke { get; set; }
        public string Type { get; set; }
        public FluffyXaxis Xaxis { get; set; }
        public FluffyYaxis Yaxis { get; set; }
    }

    public partial class IndigoFill
    {
        public string Color { get; set; }
        public double? Opacity { get; set; }
    }

    public partial class IndecentStroke
    {
        public string Color { get; set; }
        public double? DashArray { get; set; }
        public double? Opacity { get; set; }
        public double? Width { get; set; }
    }

    public partial class FluffyXaxis
    {
        public double? Max { get; set; }
        public double? Min { get; set; }
    }

    public partial class FluffyYaxis
    {
        public double? Max { get; set; }
        public double? Min { get; set; }
    }

    public partial class ApexChartSparkline
    {
        public bool? Enabled { get; set; }
    }

    public partial class ApexChartToolbar
    {
        public AutoSelected? AutoSelected { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public bool? Show { get; set; }
        public FluffyTools Tools { get; set; }
    }

    public partial class FluffyTools
    {
        public List<FluffyCustomIcon> CustomIcons { get; set; }
        public Download? Download { get; set; }
        public Download? Pan { get; set; }
        public Download? Reset { get; set; }
        public Download? Selection { get; set; }
        public Download? Zoom { get; set; }
        public Download? Zoomin { get; set; }
        public Download? Zoomout { get; set; }
    }

    public partial class FluffyCustomIcon
    {
        public string Icon { get; set; }
        public double? Index { get; set; }
        public string Title { get; set; }
    }

    public partial class ApexChartZoom
    {
        public bool? AutoScaleYaxis { get; set; }
        public bool? Enabled { get; set; }
        public ZoomType? Type { get; set; }
        public FluffyZoomedArea ZoomedArea { get; set; }
    }

    public partial class FluffyZoomedArea
    {
        public IndecentFill Fill { get; set; }
        public HilariousStroke Stroke { get; set; }
    }

    public partial class IndecentFill
    {
        public string Color { get; set; }
        public double? Opacity { get; set; }
    }

    public partial class HilariousStroke
    {
        public string Color { get; set; }
        public double? Opacity { get; set; }
        public double? Width { get; set; }
    }

    public partial class ApexStates
    {
        public ApexStatesActive Active { get; set; }
        public ApexStatesHover Hover { get; set; }
        public ApexStatesNormal Normal { get; set; }
    }

    public partial class ApexStatesActive
    {
        public bool? AllowMultipleDataPointsSelection { get; set; }
        public StickyFilter Filter { get; set; }
    }

    public partial class StickyFilter
    {
        public string Type { get; set; }
        public double? Value { get; set; }
    }

    public partial class ApexStatesHover
    {
        public IndigoFilter Filter { get; set; }
    }

    public partial class IndigoFilter
    {
        public string Type { get; set; }
        public double? Value { get; set; }
    }

    public partial class ApexStatesNormal
    {
        public IndecentFilter Filter { get; set; }
    }

    public partial class IndecentFilter
    {
        public string Type { get; set; }
        public double? Value { get; set; }
    }

    public partial class ApexTitleSubtitle
    {
        public Align? Align { get; set; }
        public bool? Floating { get; set; }
        public double? Margin { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public ApexTitleSubtitleStyle Style { get; set; }
        public string Text { get; set; }
    }

    public partial class ApexTitleSubtitleStyle
    {
        public string Color { get; set; }
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
        public FontWeight? FontWeight { get; set; }
    }

    /// <summary>
    /// Chart Series options.
    /// Use ApexNonAxisChartSeries for Pie and Donut charts.
    /// See https://apexcharts.com/docs/options/series/
    ///
    /// According to the documentation at
    /// https://apexcharts.com/docs/series/
    /// Section 1: data can be a list of single numbers
    /// Sections 2.1 and 3.1: data can be a list of tuples of two numbers
    /// Sections 2.2 and 3.2: data can be a list of objects where x is a string
    /// and y is a number
    /// And according to the demos, data can contain null.
    /// https://apexcharts.com/javascript-chart-demos/line-charts/null-values/
    /// </summary>
    //public partial class ApexAxisChartSeries
    //{
    //    public List<ApexAxisChartSeryDatum> Data { get; set; }
    //    public string Name { get; set; }
    //    public string Type { get; set; }
    //}

    public partial class FluffyDatum
    {
        public string FillColor { get; set; }
        public string StrokeColor { get; set; }
        public object X { get; set; }
        public object Y { get; set; }
    }

    public partial class ApexStroke
    {
        public List<string> Colors { get; set; }
        public Curve? Curve { get; set; }
        public Opacity? DashArray { get; set; }
        public LineCap? LineCap { get; set; }
        public bool? Show { get; set; }
        public Opacity? Width { get; set; }
    }

    public partial class ApexAnnotations
    {
        public List<ApexAnnotationsImage> Images { get; set; }
        public List<ApexAnnotationsPoint> Points { get; set; }
        public string Position { get; set; }
        public List<ApexAnnotationsShape> Shapes { get; set; }
        public List<ApexAnnotationsText> Texts { get; set; }
        public List<ApexAnnotationsXaxi> Xaxis { get; set; }
        public List<ApexAnnotationsYaxi> Yaxis { get; set; }
    }

    public partial class ApexAnnotationsImage
    {
        public double? Height { get; set; }
        public string Path { get; set; }
        public double? Width { get; set; }
        public double? X { get; set; }
        public double? Y { get; set; }
    }

    public partial class ApexAnnotationsPoint
    {
        public FluffyImage Image { get; set; }
        public StickyLabel Label { get; set; }
        public FluffyMarker Marker { get; set; }
        public double? SeriesIndex { get; set; }
        public FontWeight? X { get; set; }
        public double? Y { get; set; }
        public double? YAxisIndex { get; set; }
    }

    public partial class FluffyImage
    {
        public double? Height { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public string Path { get; set; }
        public double? Width { get; set; }
    }

    public partial class StickyLabel
    {
        public string BorderColor { get; set; }
        public double? BorderWidth { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public string Orientation { get; set; }
        public string Position { get; set; }
        public BraggadociousStyle Style { get; set; }
        public string Text { get; set; }
        public string TextAnchor { get; set; }
    }

    public partial class BraggadociousStyle
    {
        public string Background { get; set; }
        public string Color { get; set; }
        public string CssClass { get; set; }
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
        public FontWeight? FontWeight { get; set; }
        public StickyPadding Padding { get; set; }
    }

    public partial class StickyPadding
    {
        public double? Bottom { get; set; }
        public double? Left { get; set; }
        public double? Right { get; set; }
        public double? Top { get; set; }
    }

    public partial class FluffyMarker
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

    public partial class ApexAnnotationsShape
    {
        public string BackgroundColor { get; set; }
        public string BorderColor { get; set; }
        public double? BorderRadius { get; set; }
        public double? BorderWidth { get; set; }
        public double? Height { get; set; }
        public double? Opacity { get; set; }
        public string Type { get; set; }
        public FontWeight? Width { get; set; }
        public double? X { get; set; }
        public double? Y { get; set; }
    }

    public partial class ApexAnnotationsText
    {
        public string BackgroundColor { get; set; }
        public string BorderColor { get; set; }
        public double? BorderRadius { get; set; }
        public double? BorderWidth { get; set; }
        public string FontFamily { get; set; }
        public FontWeight? FontSize { get; set; }
        public FontWeight? FontWeight { get; set; }
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

    public partial class ApexAnnotationsXaxi
    {
        public string BorderColor { get; set; }
        public double? BorderWidth { get; set; }
        public string FillColor { get; set; }
        public IndigoLabel Label { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public double? Opacity { get; set; }
        public double? StrokeDashArray { get; set; }
        public X? X { get; set; }
        public X? X2 { get; set; }
    }

    public partial class IndigoLabel
    {
        public string BorderColor { get; set; }
        public double? BorderWidth { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public string Orientation { get; set; }
        public string Position { get; set; }
        public Style1 Style { get; set; }
        public string Text { get; set; }
        public string TextAnchor { get; set; }
    }

    public partial class Style1
    {
        public string Background { get; set; }
        public string Color { get; set; }
        public string CssClass { get; set; }
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
        public FontWeight? FontWeight { get; set; }
        public IndigoPadding Padding { get; set; }
    }

    public partial class IndigoPadding
    {
        public double? Bottom { get; set; }
        public double? Left { get; set; }
        public double? Right { get; set; }
        public double? Top { get; set; }
    }

    public partial class ApexAnnotationsYaxi
    {
        public string BorderColor { get; set; }
        public double? BorderWidth { get; set; }
        public string FillColor { get; set; }
        public IndecentLabel Label { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public double? Opacity { get; set; }
        public double? StrokeDashArray { get; set; }
        public X? Y { get; set; }
        public X? Y2 { get; set; }
        public double? YAxisIndex { get; set; }
    }

    public partial class IndecentLabel
    {
        public string BorderColor { get; set; }
        public double? BorderWidth { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public string Orientation { get; set; }
        public string Position { get; set; }
        public Style2 Style { get; set; }
        public string Text { get; set; }
        public string TextAnchor { get; set; }
    }

    public partial class Style2
    {
        public string Background { get; set; }
        public string Color { get; set; }
        public string CssClass { get; set; }
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
        public FontWeight? FontWeight { get; set; }
        public IndecentPadding Padding { get; set; }
    }

    public partial class IndecentPadding
    {
        public double? Bottom { get; set; }
        public double? Left { get; set; }
        public double? Right { get; set; }
        public double? Top { get; set; }
    }

    public partial class AnnotationLabel
    {
        public string BorderColor { get; set; }
        public double? BorderWidth { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public string Orientation { get; set; }
        public string Position { get; set; }
        public AnnotationLabelStyle Style { get; set; }
        public string Text { get; set; }
        public string TextAnchor { get; set; }
    }

    public partial class AnnotationLabelStyle
    {
        public string Background { get; set; }
        public string Color { get; set; }
        public string CssClass { get; set; }
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
        public FontWeight? FontWeight { get; set; }
        public HilariousPadding Padding { get; set; }
    }

    public partial class HilariousPadding
    {
        public double? Bottom { get; set; }
        public double? Left { get; set; }
        public double? Right { get; set; }
        public double? Top { get; set; }
    }

    public partial class AnnotationStyle
    {
        public string Background { get; set; }
        public string Color { get; set; }
        public string CssClass { get; set; }
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
        public FontWeight? FontWeight { get; set; }
        public AnnotationStylePadding Padding { get; set; }
    }

    public partial class AnnotationStylePadding
    {
        public double? Bottom { get; set; }
        public double? Left { get; set; }
        public double? Right { get; set; }
        public double? Top { get; set; }
    }

    public partial class XAxisAnnotations
    {
        public string BorderColor { get; set; }
        public double? BorderWidth { get; set; }
        public string FillColor { get; set; }
        public XAxisAnnotationsLabel Label { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public double? Opacity { get; set; }
        public double? StrokeDashArray { get; set; }
        public X? X { get; set; }
        public X? X2 { get; set; }
    }

    public partial class XAxisAnnotationsLabel
    {
        public string BorderColor { get; set; }
        public double? BorderWidth { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public string Orientation { get; set; }
        public string Position { get; set; }
        public Style3 Style { get; set; }
        public string Text { get; set; }
        public string TextAnchor { get; set; }
    }

    public partial class Style3
    {
        public string Background { get; set; }
        public string Color { get; set; }
        public string CssClass { get; set; }
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
        public FontWeight? FontWeight { get; set; }
        public AmbitiousPadding Padding { get; set; }
    }

    public partial class AmbitiousPadding
    {
        public double? Bottom { get; set; }
        public double? Left { get; set; }
        public double? Right { get; set; }
        public double? Top { get; set; }
    }

    public partial class YAxisAnnotations
    {
        public string BorderColor { get; set; }
        public double? BorderWidth { get; set; }
        public string FillColor { get; set; }
        public YAxisAnnotationsLabel Label { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public double? Opacity { get; set; }
        public double? StrokeDashArray { get; set; }
        public X? Y { get; set; }
        public X? Y2 { get; set; }
        public double? YAxisIndex { get; set; }
    }

    public partial class YAxisAnnotationsLabel
    {
        public string BorderColor { get; set; }
        public double? BorderWidth { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public string Orientation { get; set; }
        public string Position { get; set; }
        public Style4 Style { get; set; }
        public string Text { get; set; }
        public string TextAnchor { get; set; }
    }

    public partial class Style4
    {
        public string Background { get; set; }
        public string Color { get; set; }
        public string CssClass { get; set; }
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
        public FontWeight? FontWeight { get; set; }
        public CunningPadding Padding { get; set; }
    }

    public partial class CunningPadding
    {
        public double? Bottom { get; set; }
        public double? Left { get; set; }
        public double? Right { get; set; }
        public double? Top { get; set; }
    }

    public partial class PointAnnotations
    {
        public PointAnnotationsImage Image { get; set; }
        public PointAnnotationsLabel Label { get; set; }
        public PointAnnotationsMarker Marker { get; set; }
        public double? SeriesIndex { get; set; }
        public FontWeight? X { get; set; }
        public double? Y { get; set; }
        public double? YAxisIndex { get; set; }
    }

    public partial class PointAnnotationsImage
    {
        public double? Height { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public string Path { get; set; }
        public double? Width { get; set; }
    }

    public partial class PointAnnotationsLabel
    {
        public string BorderColor { get; set; }
        public double? BorderWidth { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public string Orientation { get; set; }
        public string Position { get; set; }
        public Style5 Style { get; set; }
        public string Text { get; set; }
        public string TextAnchor { get; set; }
    }

    public partial class Style5
    {
        public string Background { get; set; }
        public string Color { get; set; }
        public string CssClass { get; set; }
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
        public FontWeight? FontWeight { get; set; }
        public MagentaPadding Padding { get; set; }
    }

    public partial class MagentaPadding
    {
        public double? Bottom { get; set; }
        public double? Left { get; set; }
        public double? Right { get; set; }
        public double? Top { get; set; }
    }

    public partial class PointAnnotationsMarker
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

    public partial class ShapeAnnotations
    {
        public string BackgroundColor { get; set; }
        public string BorderColor { get; set; }
        public double? BorderRadius { get; set; }
        public double? BorderWidth { get; set; }
        public double? Height { get; set; }
        public double? Opacity { get; set; }
        public string Type { get; set; }
        public FontWeight? Width { get; set; }
        public double? X { get; set; }
        public double? Y { get; set; }
    }

    public partial class TextAnnotations
    {
        public string BackgroundColor { get; set; }
        public string BorderColor { get; set; }
        public double? BorderRadius { get; set; }
        public double? BorderWidth { get; set; }
        public string FontFamily { get; set; }
        public FontWeight? FontSize { get; set; }
        public FontWeight? FontWeight { get; set; }
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

    public partial class ImageAnnotations
    {
        public double? Height { get; set; }
        public string Path { get; set; }
        public double? Width { get; set; }
        public double? X { get; set; }
        public double? Y { get; set; }
    }

    public partial class ApexLocale
    {
        public string Name { get; set; }
        public ApexLocaleOptions Options { get; set; }
    }

    public partial class ApexLocaleOptions
    {
        public List<string> Days { get; set; }
        public List<string> Months { get; set; }
        public List<string> ShortDays { get; set; }
        public List<string> ShortMonths { get; set; }
        public TentacledToolbar Toolbar { get; set; }
    }

    public partial class TentacledToolbar
    {
        public string Download { get; set; }
        public string Pan { get; set; }
        public string Reset { get; set; }
        public string Selection { get; set; }
        public string SelectionZoom { get; set; }
        public string ZoomIn { get; set; }
        public string ZoomOut { get; set; }
    }

    public partial class ApexPlotOptions
    {
        public ApexPlotOptionsBar Bar { get; set; }
        public ApexPlotOptionsBubble Bubble { get; set; }
        public ApexPlotOptionsCandlestick Candlestick { get; set; }
        public ApexPlotOptionsHeatmap Heatmap { get; set; }
        public ApexPlotOptionsPie Pie { get; set; }
        public ApexPlotOptionsPolarArea PolarArea { get; set; }
        public ApexPlotOptionsRadar Radar { get; set; }
        public ApexPlotOptionsRadialBar RadialBar { get; set; }
    }

    public partial class ApexPlotOptionsBar
    {
        public string BarHeight { get; set; }
        public TentacledColors Colors { get; set; }
        public string ColumnWidth { get; set; }
        public StickyDataLabels DataLabels { get; set; }
        public bool? Distributed { get; set; }
        public IngShape? EndingShape { get; set; }
        public bool? Horizontal { get; set; }
        public bool? RangeBarOverlap { get; set; }
        public IngShape? StartingShape { get; set; }
    }

    public partial class TentacledColors
    {
        public List<string> BackgroundBarColors { get; set; }
        public double? BackgroundBarOpacity { get; set; }
        public double? BackgroundBarRadius { get; set; }
        public List<TentacledRange> Ranges { get; set; }
    }

    public partial class TentacledRange
    {
        public string Color { get; set; }
        public double? From { get; set; }
        public double? To { get; set; }
    }

    public partial class StickyDataLabels
    {
        public bool? HideOverflowingLabels { get; set; }
        public double? MaxItems { get; set; }
        public Orientation? Orientation { get; set; }
        public string Position { get; set; }
    }

    public partial class ApexPlotOptionsBubble
    {
        public double? MaxBubbleRadius { get; set; }
        public double? MinBubbleRadius { get; set; }
    }

    public partial class ApexPlotOptionsCandlestick
    {
        public StickyColors Colors { get; set; }
        public FluffyWick Wick { get; set; }
    }

    public partial class StickyColors
    {
        public string Downward { get; set; }
        public string Upward { get; set; }
    }

    public partial class FluffyWick
    {
        public bool? UseFillColor { get; set; }
    }

    public partial class ApexPlotOptionsHeatmap
    {
        public FluffyColorScale ColorScale { get; set; }
        public bool? Distributed { get; set; }
        public bool? EnableShades { get; set; }
        public double? Radius { get; set; }
        public bool? ReverseNegativeShade { get; set; }
        public double? ShadeIntensity { get; set; }
        public bool? UseFillColorAsStroke { get; set; }
    }

    public partial class FluffyColorScale
    {
        public bool? Inverse { get; set; }
        public double? Max { get; set; }
        public double? Min { get; set; }
        public List<StickyRange> Ranges { get; set; }
    }

    public partial class StickyRange
    {
        public string Color { get; set; }
        public string ForeColor { get; set; }
        public double? From { get; set; }
        public string Name { get; set; }
        public double? To { get; set; }
    }

    public partial class ApexPlotOptionsPie
    {
        public double? CustomScale { get; set; }
        public IndigoDataLabels DataLabels { get; set; }
        public FluffyDonut Donut { get; set; }
        public bool? ExpandOnClick { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
    }

    public partial class IndigoDataLabels
    {
        public double? MinAngleToShowLabel { get; set; }
        public double? Offset { get; set; }
    }

    public partial class FluffyDonut
    {
        public string Background { get; set; }
        public FluffyLabels Labels { get; set; }
        public string Size { get; set; }
    }

    public partial class FluffyLabels
    {
        public TentacledName Name { get; set; }
        public bool? Show { get; set; }
        public TentacledTotal Total { get; set; }
        public TentacledValue Value { get; set; }
    }

    public partial class TentacledName
    {
        public string Color { get; set; }
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
        public FontWeight? FontWeight { get; set; }
        public double? OffsetY { get; set; }
        public bool? Show { get; set; }
    }

    public partial class TentacledTotal
    {
        public string Color { get; set; }
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
        public FontWeight? FontWeight { get; set; }
        public string Label { get; set; }
        public bool? Show { get; set; }
        public bool? ShowAlways { get; set; }
    }

    public partial class TentacledValue
    {
        public string Color { get; set; }
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
        public FontWeight? FontWeight { get; set; }
        public double? OffsetY { get; set; }
        public bool? Show { get; set; }
    }

    public partial class ApexPlotOptionsPolarArea
    {
        public FluffyRings Rings { get; set; }
    }

    public partial class FluffyRings
    {
        public string StrokeColor { get; set; }
        public double? StrokeWidth { get; set; }
    }

    public partial class ApexPlotOptionsRadar
    {
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public FluffyPolygons Polygons { get; set; }
        public double? Size { get; set; }
    }

    public partial class FluffyPolygons
    {
        public Color? ConnectorColors { get; set; }
        public HilariousFill Fill { get; set; }
        public Color? StrokeColors { get; set; }
        public Color? StrokeWidth { get; set; }
    }

    public partial class HilariousFill
    {
        public List<string> Colors { get; set; }
    }

    public partial class ApexPlotOptionsRadialBar
    {
        public IndecentDataLabels DataLabels { get; set; }
        public double? EndAngle { get; set; }
        public FluffyHollow Hollow { get; set; }
        public bool? InverseOrder { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public double? StartAngle { get; set; }
        public FluffyTrack Track { get; set; }
    }

    public partial class IndecentDataLabels
    {
        public StickyName Name { get; set; }
        public bool? Show { get; set; }
        public StickyTotal Total { get; set; }
        public StickyValue Value { get; set; }
    }

    public partial class StickyName
    {
        public string Color { get; set; }
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
        public FontWeight? FontWeight { get; set; }
        public double? OffsetY { get; set; }
        public bool? Show { get; set; }
    }

    public partial class StickyTotal
    {
        public string Color { get; set; }
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
        public FontWeight? FontWeight { get; set; }
        public string Label { get; set; }
        public bool? Show { get; set; }
    }

    public partial class StickyValue
    {
        public string Color { get; set; }
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
        public FontWeight? FontWeight { get; set; }
        public double? OffsetY { get; set; }
        public bool? Show { get; set; }
    }

    public partial class FluffyHollow
    {
        public string Background { get; set; }
        public IndigoDropShadow DropShadow { get; set; }
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

    public partial class IndigoDropShadow
    {
        public double? Blur { get; set; }
        public string Color { get; set; }
        public bool? Enabled { get; set; }
        public double? Left { get; set; }
        public double? Opacity { get; set; }
        public double? Top { get; set; }
    }

    public partial class FluffyTrack
    {
        public string Background { get; set; }
        public IndecentDropShadow DropShadow { get; set; }
        public double? EndAngle { get; set; }
        public double? Margin { get; set; }
        public double? Opacity { get; set; }
        public bool? Show { get; set; }
        public double? StartAngle { get; set; }
        public string StrokeWidth { get; set; }
    }

    public partial class IndecentDropShadow
    {
        public double? Blur { get; set; }
        public string Color { get; set; }
        public bool? Enabled { get; set; }
        public double? Left { get; set; }
        public double? Opacity { get; set; }
        public double? Top { get; set; }
    }

    public partial class ApexFill
    {
        public List<object> Colors { get; set; }
        public ApexFillGradient Gradient { get; set; }
        public ApexFillImage Image { get; set; }
        public Opacity? Opacity { get; set; }
        public ApexFillPattern Pattern { get; set; }
        public Color? Type { get; set; }
    }

    public partial class ApexFillGradient
    {
        public List<string> GradientToColors { get; set; }
        public bool? InverseColors { get; set; }
        public double? OpacityFrom { get; set; }
        public double? OpacityTo { get; set; }
        public string Shade { get; set; }
        public double? ShadeIntensity { get; set; }
        public List<double> Stops { get; set; }
        public string Type { get; set; }
    }

    public partial class ApexFillImage
    {
        public double? Height { get; set; }
        public Color? Src { get; set; }
        public double? Width { get; set; }
    }

    public partial class ApexFillPattern
    {
        public double? Height { get; set; }
        public double? StrokeWidth { get; set; }
        public Color? Style { get; set; }
        public double? Width { get; set; }
    }

    public partial class ApexLegend
    {
        public ApexLegendContainerMargin ContainerMargin { get; set; }
        public bool? Floating { get; set; }
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
        public FontWeight? FontWeight { get; set; }
        public double? Height { get; set; }
        public Align? HorizontalAlign { get; set; }
        public bool? InverseOrder { get; set; }
        public ApexLegendItemMargin ItemMargin { get; set; }
        public ApexLegendLabels Labels { get; set; }
        public ApexLegendMarkers Markers { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public ApexLegendOnItemClick OnItemClick { get; set; }
        public ApexLegendOnItemHover OnItemHover { get; set; }
        public LegendPosition? Position { get; set; }
        public bool? Show { get; set; }
        public bool? ShowForNullSeries { get; set; }
        public bool? ShowForSingleSeries { get; set; }
        public bool? ShowForZeroSeries { get; set; }
        public string TextAnchor { get; set; }
        public double? Width { get; set; }
    }

    public partial class ApexLegendContainerMargin
    {
        public double? Left { get; set; }
        public double? Top { get; set; }
    }

    public partial class ApexLegendItemMargin
    {
        public double? Horizontal { get; set; }
        public double? Vertical { get; set; }
    }

    public partial class ApexLegendLabels
    {
        public Color? Colors { get; set; }
        public bool? UseSeriesColors { get; set; }
    }

    public partial class ApexLegendMarkers
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

    public partial class ApexLegendOnItemClick
    {
        public bool? ToggleDataSeries { get; set; }
    }

    public partial class ApexLegendOnItemHover
    {
        public bool? HighlightDataSeries { get; set; }
    }

    public partial class ApexDiscretePoint
    {
        public double? DataPointIndex { get; set; }
        public string FillColor { get; set; }
        public double? SeriesIndex { get; set; }
        public double? Size { get; set; }
        public string StrokeColor { get; set; }
    }

    public partial class ApexMarkers
    {
        public List<string> Colors { get; set; }
        public List<ApexMarkersDiscrete> Discrete { get; set; }
        public Opacity? FillOpacity { get; set; }
        public ApexMarkersHover Hover { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public double? Radius { get; set; }
        public ShapeUnion? Shape { get; set; }
        public bool? ShowNullDataPoints { get; set; }
        public Opacity? Size { get; set; }
        public Color? StrokeColors { get; set; }
        public Opacity? StrokeDashArray { get; set; }
        public Opacity? StrokeOpacity { get; set; }
        public Opacity? StrokeWidth { get; set; }
    }

    public partial class ApexMarkersDiscrete
    {
        public double? DataPointIndex { get; set; }
        public string FillColor { get; set; }
        public double? SeriesIndex { get; set; }
        public double? Size { get; set; }
        public string StrokeColor { get; set; }
    }

    public partial class ApexMarkersHover
    {
        public double? Size { get; set; }
        public double? SizeOffset { get; set; }
    }

    public partial class ApexNoData
    {
        public Align? Align { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public ApexNoDataStyle Style { get; set; }
        public string Text { get; set; }
        public VerticalAlign? VerticalAlign { get; set; }
    }

    public partial class ApexNoDataStyle
    {
        public string Color { get; set; }
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
    }

    public partial class ApexDataLabels
    {
        public ApexDataLabelsBackground Background { get; set; }
        public bool? Distributed { get; set; }
        public ApexDataLabelsDropShadow DropShadow { get; set; }
        public bool? Enabled { get; set; }
        public List<double> EnabledOnSeries { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public ApexDataLabelsStyle Style { get; set; }
        public TextAnchor? TextAnchor { get; set; }
    }

    public partial class ApexDataLabelsBackground
    {
        public string BorderColor { get; set; }
        public double? BorderRadius { get; set; }
        public double? BorderWidth { get; set; }
        public HilariousDropShadow DropShadow { get; set; }
        public bool? Enabled { get; set; }
        public string ForeColor { get; set; }
        public double? Opacity { get; set; }
        public double? Padding { get; set; }
    }

    public partial class HilariousDropShadow
    {
        public double? Blur { get; set; }
        public string Color { get; set; }
        public bool? Enabled { get; set; }
        public double? Left { get; set; }
        public double? Opacity { get; set; }
        public double? Top { get; set; }
    }

    public partial class ApexDataLabelsDropShadow
    {
        public double? Blur { get; set; }
        public string Color { get; set; }
        public bool? Enabled { get; set; }
        public double? Left { get; set; }
        public double? Opacity { get; set; }
        public double? Top { get; set; }
    }

    public partial class ApexDataLabelsStyle
    {
        public List<string> Colors { get; set; }
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
        public FontWeight? FontWeight { get; set; }
    }

    public partial class ApexResponsive
    {
        public double? Breakpoint { get; set; }
        public object Options { get; set; }
    }

    public partial class ApexTooltipY
    {
        public Dictionary<string, object> Title { get; set; }
    }

    public partial class ApexTooltip
    {
        public Custom? Custom { get; set; }
        public bool? Enabled { get; set; }
        public List<double> EnabledOnSeries { get; set; }
        public bool? FillSeriesColor { get; set; }
        public ApexTooltipFixed Fixed { get; set; }
        public bool? FollowCursor { get; set; }
        public bool? Intersect { get; set; }
        public bool? InverseOrder { get; set; }
        public ApexTooltipItems Items { get; set; }
        public ApexTooltipMarker Marker { get; set; }
        public ApexTooltipOnDatasetHover OnDatasetHover { get; set; }
        public bool? Shared { get; set; }
        public ApexTooltipStyle Style { get; set; }
        public string Theme { get; set; }
        public ApexTooltipX X { get; set; }
        public ApexTooltipYUnion? Y { get; set; }
        public ApexTooltipZ Z { get; set; }
    }

    public partial class ApexTooltipFixed
    {
        public bool? Enabled { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public string Position { get; set; }
    }

    public partial class ApexTooltipItems
    {
        public string Display { get; set; }
    }

    public partial class ApexTooltipMarker
    {
        public List<string> FillColors { get; set; }
        public bool? Show { get; set; }
    }

    public partial class ApexTooltipOnDatasetHover
    {
        public bool? HighlightDataSeries { get; set; }
    }

    public partial class ApexTooltipStyle
    {
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
    }

    public partial class ApexTooltipX
    {
        public string Format { get; set; }
        public bool? Show { get; set; }
    }

    public partial class TentacledY
    {
        public Dictionary<string, object> Title { get; set; }
    }

    public partial class StickyY
    {
        public Dictionary<string, object> Title { get; set; }
    }

    public partial class ApexTooltipZ
    {
        public string Title { get; set; }
    }

    public partial class ApexXAxis
    {
        public ApexXAxisAxisBorder AxisBorder { get; set; }
        public ApexXAxisAxisTicks AxisTicks { get; set; }
        public object Categories { get; set; }
        public ApexXAxisCrosshairs Crosshairs { get; set; }
        public bool? Floating { get; set; }
        public ApexXAxisLabels Labels { get; set; }
        public double? Max { get; set; }
        public double? Min { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public string Position { get; set; }
        public double? Range { get; set; }
        public bool? Sorted { get; set; }
        public TickAmountUnion? TickAmount { get; set; }
        public string TickPlacement { get; set; }
        public ApexXAxisTitle Title { get; set; }
        public ApexXAxisTooltip Tooltip { get; set; }
        public XaxisType? Type { get; set; }
    }

    public partial class ApexXAxisAxisBorder
    {
        public string Color { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public bool? Show { get; set; }
        public double? StrokeWidth { get; set; }
    }

    public partial class ApexXAxisAxisTicks
    {
        public string BorderType { get; set; }
        public string Color { get; set; }
        public double? Height { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public bool? Show { get; set; }
    }

    public partial class ApexXAxisCrosshairs
    {
        public AmbitiousDropShadow DropShadow { get; set; }
        public AmbitiousFill Fill { get; set; }
        public double? Opacity { get; set; }
        public string Position { get; set; }
        public bool? Show { get; set; }
        public AmbitiousStroke Stroke { get; set; }
        public FontWeight? Width { get; set; }
    }

    public partial class AmbitiousDropShadow
    {
        public double? Blur { get; set; }
        public string Color { get; set; }
        public bool? Enabled { get; set; }
        public double? Left { get; set; }
        public double? Opacity { get; set; }
        public double? Top { get; set; }
    }

    public partial class AmbitiousFill
    {
        public string Color { get; set; }
        public TentacledGradient Gradient { get; set; }
        public string Type { get; set; }
    }

    public partial class TentacledGradient
    {
        public string ColorFrom { get; set; }
        public string ColorTo { get; set; }
        public double? OpacityFrom { get; set; }
        public double? OpacityTo { get; set; }
        public List<double> Stops { get; set; }
    }

    public partial class AmbitiousStroke
    {
        public string Color { get; set; }
        public double? DashArray { get; set; }
        public double? Width { get; set; }
    }

    public partial class ApexXAxisLabels
    {
        public FluffyDatetimeFormatter DatetimeFormatter { get; set; }
        public bool? DatetimeUtc { get; set; }
        public string Format { get; set; }
        public bool? HideOverlappingLabels { get; set; }
        public double? MaxHeight { get; set; }
        public double? MinHeight { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public double? Rotate { get; set; }
        public bool? RotateAlways { get; set; }
        public bool? Show { get; set; }
        public bool? ShowDuplicates { get; set; }
        public Style6 Style { get; set; }
        public bool? Trim { get; set; }
    }

    public partial class FluffyDatetimeFormatter
    {
        public string Day { get; set; }
        public string Hour { get; set; }
        public string Minute { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
    }

    public partial class Style6
    {
        public Color? Colors { get; set; }
        public string CssClass { get; set; }
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
        public FontWeight? FontWeight { get; set; }
    }

    public partial class ApexXAxisTitle
    {
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public Style7 Style { get; set; }
        public string Text { get; set; }
    }

    public partial class Style7
    {
        public string Color { get; set; }
        public string CssClass { get; set; }
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
        public FontWeight? FontWeight { get; set; }
    }

    public partial class ApexXAxisTooltip
    {
        public bool? Enabled { get; set; }
        public double? OffsetY { get; set; }
        public Style8 Style { get; set; }
    }

    public partial class Style8
    {
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
    }

    //public partial class ApexYAxis
    //{
    //    public ApexYAxisAxisBorder AxisBorder { get; set; }
    //    public ApexYAxisAxisTicks AxisTicks { get; set; }
    //    public ApexYAxisCrosshairs Crosshairs { get; set; }
    //    public double? DecimalsInFloat { get; set; }
    //    public bool? Floating { get; set; }
    //    public bool? ForceNiceScale { get; set; }
    //    public ApexYAxisLabels Labels { get; set; }
    //    public bool? Logarithmic { get; set; }
    //    public Max? Max { get; set; }
    //    public Max? Min { get; set; }
    //    public bool? Opposite { get; set; }
    //    public bool? Reversed { get; set; }
    //    public string SeriesName { get; set; }
    //    public bool? Show { get; set; }
    //    public bool? ShowAlways { get; set; }
    //    public bool? ShowForNullSeries { get; set; }
    //    public double? TickAmount { get; set; }
    //    public ApexYAxisTitle Title { get; set; }
    //    public ApexYAxisTooltip Tooltip { get; set; }
    //}

    //public partial class ApexYAxisAxisBorder
    //{
    //    public string Color { get; set; }
    //    public double? OffsetX { get; set; }
    //    public double? OffsetY { get; set; }
    //    public bool? Show { get; set; }
    //    public double? Width { get; set; }
    //}

    //public partial class ApexYAxisAxisTicks
    //{
    //    public string Color { get; set; }
    //    public double? OffsetX { get; set; }
    //    public double? OffsetY { get; set; }
    //    public bool? Show { get; set; }
    //    public double? Width { get; set; }
    //}

    //public partial class ApexYAxisCrosshairs
    //{
    //    public string Position { get; set; }
    //    public bool? Show { get; set; }
    //    public CunningStroke Stroke { get; set; }
    //}

    public partial class CunningStroke
    {
        public string Color { get; set; }
        public double? DashArray { get; set; }
        public double? Width { get; set; }
    }

    public partial class ApexYAxisLabels
    {
        public Align? Align { get; set; }
        public double? MaxWidth { get; set; }
        public double? MinWidth { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public double? Padding { get; set; }
        public double? Rotate { get; set; }
        public bool? Show { get; set; }
        public Style9 Style { get; set; }
    }

    public partial class Style9
    {
        public string Colors { get; set; }
        public string CssClass { get; set; }
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
        public FontWeight? FontWeight { get; set; }
    }

    public partial class ApexYAxisTitle
    {
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public double? Rotate { get; set; }
        public Style10 Style { get; set; }
        public string Text { get; set; }
    }

    public partial class Style10
    {
        public string Color { get; set; }
        public string CssClass { get; set; }
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
        public FontWeight? FontWeight { get; set; }
    }

    public partial class ApexYAxisTooltip
    {
        public bool? Enabled { get; set; }
        public double? OffsetX { get; set; }
    }

    public partial class ApexGrid
    {
        public string BorderColor { get; set; }
        public ApexGridColumn Column { get; set; }
        public ApexGridPadding Padding { get; set; }
        public GridPosition? Position { get; set; }
        public ApexGridRow Row { get; set; }
        public bool? Show { get; set; }
        public double? StrokeDashArray { get; set; }
        public ApexGridXaxis Xaxis { get; set; }
        public ApexGridYaxis Yaxis { get; set; }
    }

    public partial class ApexGridColumn
    {
        public List<string> Colors { get; set; }
        public double? Opacity { get; set; }
    }

    public partial class ApexGridPadding
    {
        public double? Bottom { get; set; }
        public double? Left { get; set; }
        public double? Right { get; set; }
        public double? Top { get; set; }
    }

    public partial class ApexGridRow
    {
        public List<string> Colors { get; set; }
        public double? Opacity { get; set; }
    }

    public partial class ApexGridXaxis
    {
        public TentacledLines Lines { get; set; }
    }

    public partial class TentacledLines
    {
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public bool? Show { get; set; }
    }

    public partial class ApexGridYaxis
    {
        public StickyLines Lines { get; set; }
    }

    public partial class StickyLines
    {
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public bool? Show { get; set; }
    }

    public partial class ApexTheme
    {
        public Mode? Mode { get; set; }
        public ApexThemeMonochrome Monochrome { get; set; }
        public string Palette { get; set; }
    }

    public partial class ApexThemeMonochrome
    {
        public string Color { get; set; }
        public bool? Enabled { get; set; }
        public double? ShadeIntensity { get; set; }
        public Mode? ShadeTo { get; set; }
    }

    public enum Easing { Easein, Easeinout, Easeout, Linear };

    public enum StackType { Normal, The100 };

    public enum AutoSelected { Pan, Selection, Zoom };

    public enum ChartType { Area, Bar, Bubble, Candlestick, Donut, Heatmap, Histogram, Line, Pie, PolarArea, Radar, RadialBar, RangeBar, Scatter };

    public enum ZoomType { X, Xy, Y };

    public enum TextAnchor { End, Middle, Start };

    public enum GridPosition { Back, Front };

    public enum Align { Center, Left, Right };

    public enum LegendPosition { Bottom, Left, Right, Top };

    public enum ShapeEnum { Circle, Square };

    public enum VerticalAlign { Bottom, Middle, Top };

    public enum Orientation { Horizontal, Vertical };

    public enum IngShape { Flat, Rounded };

    public enum Curve { Smooth, Stepline, Straight };

    public enum LineCap { Butt, Round, Square };

    public enum Mode { Dark, Light };

    public enum TickAmountEnum { DataPoints };

    public enum XaxisType { Category, Datetime, Numeric };

    public partial struct FontWeight
    {
        public double? Double;
        public string String;

        public static implicit operator FontWeight(double Double) => new FontWeight { Double = Double };
        public static implicit operator FontWeight(string String) => new FontWeight { String = String };
    }

    public partial struct X
    {
        public double? Double;
        public string String;

        public static implicit operator X(double Double) => new X { Double = Double };
        public static implicit operator X(string String) => new X { String = String };
        public bool IsNull => Double == null && String == null;
    }

    public partial struct Download
    {
        public bool? Bool;
        public string String;

        public static implicit operator Download(bool Bool) => new Download { Bool = Bool };
        public static implicit operator Download(string String) => new Download { String = String };
    }

    public partial struct Color
    {
        public string String;
        public List<string> StringArray;

        public static implicit operator Color(string String) => new Color { String = String };
        public static implicit operator Color(List<string> StringArray) => new Color { StringArray = StringArray };
    }

    public partial struct Opacity
    {
        public double? Double;
        public List<double> DoubleArray;

        public static implicit operator Opacity(double Double) => new Opacity { Double = Double };
        public static implicit operator Opacity(List<double> DoubleArray) => new Opacity { DoubleArray = DoubleArray };
    }

    public partial struct ShapeUnion
    {
        public ShapeEnum? Enum;
        public List<string> StringArray;

        public static implicit operator ShapeUnion(ShapeEnum Enum) => new ShapeUnion { Enum = Enum };
        public static implicit operator ShapeUnion(List<string> StringArray) => new ShapeUnion { StringArray = StringArray };
    }

    public partial struct DatumDatum
    {
        public double? Double;
        public List<double?> UnionArray;

        public static implicit operator DatumDatum(double Double) => new DatumDatum { Double = Double };
        public static implicit operator DatumDatum(List<double?> UnionArray) => new DatumDatum { UnionArray = UnionArray };
        public bool IsNull => UnionArray == null && Double == null;
    }

    public partial struct SeriesDatum
    {
        public List<DatumDatum> AnythingArray;
        public double? Double;
        public PurpleDatum PurpleDatum;

        public static implicit operator SeriesDatum(List<DatumDatum> AnythingArray) => new SeriesDatum { AnythingArray = AnythingArray };
        public static implicit operator SeriesDatum(double Double) => new SeriesDatum { Double = Double };
        public static implicit operator SeriesDatum(PurpleDatum PurpleDatum) => new SeriesDatum { PurpleDatum = PurpleDatum };
        public bool IsNull => AnythingArray == null && PurpleDatum == null && Double == null;
    }

    public partial struct SeriesElement
    {
        public double? Double;
        public Series SeriesClass;

        public static implicit operator SeriesElement(double Double) => new SeriesElement { Double = Double };
        public static implicit operator SeriesElement(Series SeriesClass) => new SeriesElement { SeriesClass = SeriesClass };
    }

    public partial struct Custom
    {
        public Dictionary<string, object> AnythingMap;
        public List<Dictionary<string, object>> AnythingMapArray;

        public static implicit operator Custom(Dictionary<string, object> AnythingMap) => new Custom { AnythingMap = AnythingMap };
        public static implicit operator Custom(List<Dictionary<string, object>> AnythingMapArray) => new Custom { AnythingMapArray = AnythingMapArray };
    }

    public partial struct TooltipY
    {
        public FluffyY FluffyY;
        public List<PurpleY> PurpleYArray;

        public static implicit operator TooltipY(FluffyY FluffyY) => new TooltipY { FluffyY = FluffyY };
        public static implicit operator TooltipY(List<PurpleY> PurpleYArray) => new TooltipY { PurpleYArray = PurpleYArray };
    }

    public partial struct TickAmountUnion
    {
        public double? Double;
        public TickAmountEnum? Enum;

        public static implicit operator TickAmountUnion(double Double) => new TickAmountUnion { Double = Double };
        public static implicit operator TickAmountUnion(TickAmountEnum Enum) => new TickAmountUnion { Enum = Enum };
    }

    public partial struct Max
    {
        public Dictionary<string, object> AnythingMap;
        public double? Double;

        public static implicit operator Max(Dictionary<string, object> AnythingMap) => new Max { AnythingMap = AnythingMap };
        public static implicit operator Max(double Double) => new Max { Double = Double };
    }

    //public partial struct Yaxis_Old
    //{
    //    public List<YaxisYaxi> YaxisYaxiArray;
    //    public YaxisYaxis YaxisYaxis;

    //    public static implicit operator Yaxis(List<YaxisYaxi> YaxisYaxiArray) => new Yaxis { YaxisYaxiArray = YaxisYaxiArray };
    //    public static implicit operator Yaxis(YaxisYaxis YaxisYaxis) => new Yaxis { YaxisYaxis = YaxisYaxis };
    //}

    public partial struct ApexAxisChartSeryDatum
    {
        public List<DatumDatum> AnythingArray;
        public double? Double;
        public FluffyDatum FluffyDatum;

        public static implicit operator ApexAxisChartSeryDatum(List<DatumDatum> AnythingArray) => new ApexAxisChartSeryDatum { AnythingArray = AnythingArray };
        public static implicit operator ApexAxisChartSeryDatum(double Double) => new ApexAxisChartSeryDatum { Double = Double };
        public static implicit operator ApexAxisChartSeryDatum(FluffyDatum FluffyDatum) => new ApexAxisChartSeryDatum { FluffyDatum = FluffyDatum };
        public bool IsNull => AnythingArray == null && FluffyDatum == null && Double == null;
    }

    public partial struct ApexTooltipYUnion
    {
        public StickyY StickyY;
        public List<TentacledY> TentacledYArray;

        public static implicit operator ApexTooltipYUnion(StickyY StickyY) => new ApexTooltipYUnion { StickyY = StickyY };
        public static implicit operator ApexTooltipYUnion(List<TentacledY> TentacledYArray) => new ApexTooltipYUnion { TentacledYArray = TentacledYArray };
    }
}