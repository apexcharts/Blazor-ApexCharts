using System.Collections.Generic;
using System;
using System.Runtime.Serialization;

namespace ApexCharts
{
    public class ApexChartOptions<TItem>
    {
        public bool Debug { get; set; }
        
        /// <summary>
        /// Annotations options
        /// See https://apexcharts.com/docs/options/annotations/
        /// </summary>
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

        public List<object> SeriesNonXAxis { get; internal set; }

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

        public Chart()
        {
            ChartId = Guid.NewGuid();
        }
        public Animations Animations { get; set; }
        public string Background { get; set; }
        public Brush Brush { get; set; }
        public string DefaultLocale { get; set; }
        public DropShadow DropShadow { get; set; }
        public Dictionary<string, object> Events { get; set; }
        public string FontFamily { get; set; }
        public string ForeColor { get; set; }
        public string Group { get; set; }

        public Guid ChartId { get; set; }
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
        public ChartType Type { get; set; }
        public object Width { get; set; }
        public object Height { get; set; }
        public Zoom Zoom { get; set; }
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
        public List<object> Colors { get; set; }
        public FillGradient Gradient { get; set; }
        public FillImage Image { get; set; }
        public Opacity? Opacity { get; set; }
        public FillPattern Pattern { get; set; }
        public Color? Type { get; set; }
    }

    public class FillGradient
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

    public class FillImage
    {
        public double? Height { get; set; }
        public Color? Src { get; set; }
        public double? Width { get; set; }
    }

    public class FillPattern
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
    public class Grid
    {
        public string BorderColor { get; set; }
        public GridColumn Column { get; set; }
        public Padding Padding { get; set; }
        public GridPosition? Position { get; set; }
        public GridRow Row { get; set; }
        public bool? Show { get; set; }
        public double? StrokeDashArray { get; set; }
        public GridXaxis Xaxis { get; set; }
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

    public class GridXaxis
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
        public Color? Colors { get; set; }
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

    public class MarkersDiscrete
    {
        public double? DataPointIndex { get; set; }
        public string FillColor { get; set; }
        public double? SeriesIndex { get; set; }
        public double? Size { get; set; }
        public string StrokeColor { get; set; }
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
        public PlotOptionsBar Bar { get; set; }
        public PlotOptionsBubble Bubble { get; set; }
        public PlotOptionsCandlestick Candlestick { get; set; }
        public PlotOptionsHeatmap Heatmap { get; set; }
        public PlotOptionsPie Pie { get; set; }
        public PlotOptionsPolarArea PolarArea { get; set; }
        public PlotOptionsRadar Radar { get; set; }
        public PlotOptionsRadialBar RadialBar { get; set; }
    }

    public class PlotOptionsBar
    {
        public string BarHeight { get; set; }
        public PlotOptionsBarColors Colors { get; set; }
        public string ColumnWidth { get; set; }
        public PlotOptionsBarDataLabels DataLabels { get; set; }
        public bool? Distributed { get; set; }
        public Shape? EndingShape { get; set; }
        public bool? Horizontal { get; set; }
        public bool? RangeBarOverlap { get; set; }
        public Shape? StartingShape { get; set; }
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
        public Color? ConnectorColors { get; set; }
        public TentacledFill Fill { get; set; }
        public Color? StrokeColors { get; set; }
        public Color? StrokeWidth { get; set; }
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
    }

    public class RadialBarDataLabelsValue
    {
        public string Color { get; set; }
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
        public object FontWeight { get; set; }
        public double? OffsetY { get; set; }
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
        public PurpleFilter Filter { get; set; }
    }

    public class PurpleFilter
    {
        public string Type { get; set; }
        public double? Value { get; set; }
    }

    public class StatesHover
    {
        public FluffyFilter Filter { get; set; }
    }

    public class FluffyFilter
    {
        public string Type { get; set; }
        public double? Value { get; set; }
    }

    public class StatesNormal
    {
        public TentacledFilter Filter { get; set; }
    }

    public class TentacledFilter
    {
        public string Type { get; set; }
        public double? Value { get; set; }
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
        public PaletteType Palette { get; set; }
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
        public StickyStyle Style { get; set; }
        public string Text { get; set; }
    }

    public class StickyStyle
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
        //public Custom? Custom { get; set; }
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
        public IndigoStyle Style { get; set; }
        public string Theme { get; set; }
        public TooltipX X { get; set; }
        public object Y { get; set; }
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

    public class IndigoStyle
    {
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
    }

    public class TooltipX
    {
        public string Format { get; set; }
        public bool? Show { get; set; }
    }

    public class PurpleY
    {
        public Dictionary<string, object> Title { get; set; }
    }

   
    public class TooltipZ
    {
        public string Title { get; set; }
    }

    /// <summary>
    /// X Axis options
    /// See https://apexcharts.com/docs/options/xaxis/
    /// </summary>
    public class XAxis
    {
        public XaxisAxisBorder AxisBorder { get; set; }
        public XaxisAxisTicks AxisTicks { get; set; }
        public object Categories { get; set; }
        public XaxisCrosshairs Crosshairs { get; set; }
        public bool? Floating { get; set; }
        public XaxisLabels Labels { get; set; }
        public object Max { get; set; }
        public object Min { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public string Position { get; set; }
        public double? Range { get; set; }
        public bool? Sorted { get; set; }
        public object TickAmount { get; set; }
        public string TickPlacement { get; set; }
        public XaxisTitle Title { get; set; }
        public XaxisTooltip Tooltip { get; set; }
        public XaxisType? Type { get; set; }
    }

    public class XaxisAxisBorder
    {
        public string Color { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public bool? Show { get; set; }
        public double? Height { get; set; }
    }

    public class XaxisAxisTicks
    {
        public string BorderType { get; set; }
        public string Color { get; set; }
        public double? Height { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public bool? Show { get; set; }
    }

    public class XaxisCrosshairs
    {
        public StickyDropShadow DropShadow { get; set; }
        public StickyFill Fill { get; set; }
        public double? Opacity { get; set; }
        public string Position { get; set; }
        public bool? Show { get; set; }
        public TentacledStroke Stroke { get; set; }
        public object Width { get; set; }
    }

    public class StickyDropShadow
    {
        public double? Blur { get; set; }
        public string Color { get; set; }
        public bool Enabled { get; set; } = true;
        public double? Left { get; set; }
        public double? Opacity { get; set; }
        public double? Top { get; set; }
    }

    public class StickyFill
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

    public class TentacledStroke
    {
        public string Color { get; set; }
        public double? DashArray { get; set; }
        public double? Width { get; set; }
    }

    public class XaxisLabels
    {
        public PurpleDatetimeFormatter DatetimeFormatter { get; set; }
        public bool? DatetimeUTC { get; set; }
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

    public class PurpleDatetimeFormatter
    {
        public string Day { get; set; }
        public string Hour { get; set; }
        public string Minute { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
    }

    public class IndecentStyle
    {
        public Color? Colors { get; set; }
        public string CssClass { get; set; }
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
        public object FontWeight { get; set; }
    }

    public class XaxisTitle
    {
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public XAxistTitleStyle Style { get; set; }
        public string Text { get; set; }
    }

    public class XAxistTitleStyle
    {
        public string Color { get; set; }
        public string CssClass { get; set; }
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
        public object FontWeight { get; set; }
    }

    public class XaxisTooltip
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
        public YaxiAxisBorder AxisBorder { get; set; }
        public YaxiAxisTicks AxisTicks { get; set; }
        public YAxisCrosshairs Crosshairs { get; set; }
        public double? DecimalsInFloat { get; set; }
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
        public YaxisTitle Title { get; set; }
        public YaxiTooltip Tooltip { get; set; }
    }

    public class YaxiAxisBorder
    {
        public string Color { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public bool? Show { get; set; }
        public double? Width { get; set; }
    }

    public class YaxiAxisTicks
    {
        public string Color { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public bool? Show { get; set; }
        public double? Width { get; set; }
    }

    public class YAxisCrosshairs
    {
        public string Position { get; set; }
        public bool? Show { get; set; }
        public YAxisCrosshairsStroke Stroke { get; set; }
    }

    public class YAxisCrosshairsStroke
    {
        public string Color { get; set; }
        public double? DashArray { get; set; }
        public double? Width { get; set; }
    }

    public class YAxisLabels
    {
        public Align? Align { get; set; }
        public double? MaxWidth { get; set; }
        public double? MinWidth { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public double? Padding { get; set; }
        public double? Rotate { get; set; }
        public bool? Show { get; set; }
        public YAxisLabelStyle Style { get; set; }
    }

    public class YAxisLabelStyle
    {
        public string Colors { get; set; }
        public string CssClass { get; set; }
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
        public object FontWeight { get; set; }
    }

    public class YaxiTitle
    {
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public double? Rotate { get; set; }
        public MagentaStyle Style { get; set; }
        public string Text { get; set; }
    }

    public class MagentaStyle
    {
        public string Color { get; set; }
        public string CssClass { get; set; }
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
        public object FontWeight { get; set; }
    }

    public class YaxiTooltip
    {
        public bool Enabled { get; set; } = true;
        public double? OffsetX { get; set; }
    }

    public class YaxisYaxis
    {
        public YaxisAxisBorder AxisBorder { get; set; }
        public YaxisAxisTicks AxisTicks { get; set; }
        public YaxisCrosshairs Crosshairs { get; set; }
        public double? DecimalsInFloat { get; set; }
        public bool? Floating { get; set; }
        public bool? ForceNiceScale { get; set; }
        public YaxisLabels Labels { get; set; }
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
        public YaxisTitle Title { get; set; }
        public YaxisTooltip Tooltip { get; set; }
    }

    public class YaxisAxisBorder
    {
        public string Color { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public bool? Show { get; set; }
        public double? Width { get; set; }
    }

    public class YaxisAxisTicks
    {
        public string Color { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public bool? Show { get; set; }
        public double? Width { get; set; }
    }

    public class YaxisCrosshairs
    {
        public string Position { get; set; }
        public bool? Show { get; set; }
        public IndigoStroke Stroke { get; set; }
    }

    public class IndigoStroke
    {
        public string Color { get; set; }
        public double? DashArray { get; set; }
        public double? Width { get; set; }
    }

    public class YaxisLabels
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

    public class FriskyStyle
    {
        public string Colors { get; set; }
        public string CssClass { get; set; }
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
        public object FontWeight { get; set; }
    }

    public class YaxisTitle
    {
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public double? Rotate { get; set; }
        public YAxisTitleStyle Style { get; set; }
        public string Text { get; set; }
    }

    public class YAxisTitleStyle
    {
        public string Color { get; set; }
        public string CssClass { get; set; }
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
        public object FontWeight { get; set; }
    }

    public class YaxisTooltip
    {
        public bool Enabled { get; set; } = true;
        public double? OffsetX { get; set; }
    }

    public class ApexDropShadow
    {
        public double? Blur { get; set; }
        public string Color { get; set; }
        public bool Enabled { get; set; } = true;
        public double? Left { get; set; }
        public double? Opacity { get; set; }
        public double? Top { get; set; }
    }

    public class ApexChart
    {
        public ApexChartAnimations Animations { get; set; }
        public string Background { get; set; }
        public ApexChartBrush Brush { get; set; }
        public string DefaultLocale { get; set; }
        public DropShadow DropShadow { get; set; }
        public Dictionary<string, object> Events { get; set; }
        public string FontFamily { get; set; }
        public string ForeColor { get; set; }
        public string Group { get; set; }
        public object Height { get; set; }
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
        public object Width { get; set; }
        public ApexChartZoom Zoom { get; set; }
    }

    public class ApexChartAnimations
    {
        public FluffyAnimateGradually AnimateGradually { get; set; }
        public FluffyDynamicAnimation DynamicAnimation { get; set; }
        public Easing? Easing { get; set; }
        public bool Enabled { get; set; } = true;
        public double? Speed { get; set; }
    }

    public class FluffyAnimateGradually
    {
        public double? Delay { get; set; }
        public bool Enabled { get; set; } = true;
    }

    public class FluffyDynamicAnimation
    {
        public bool Enabled { get; set; } = true;
        public double? Speed { get; set; }
    }

    public class ApexChartBrush
    {
        public bool? AutoScaleYaxis { get; set; }
        public bool Enabled { get; set; } = true;
        public string Target { get; set; }
    }


    public class ApexChartLocale
    {
        public string Name { get; set; }
        public FluffyOptions Options { get; set; }
    }

    public class FluffyOptions
    {
        public List<string> Days { get; set; }
        public List<string> Months { get; set; }
        public List<string> ShortDays { get; set; }
        public List<string> ShortMonths { get; set; }
        public FluffyToolbar Toolbar { get; set; }
    }

    public class FluffyToolbar
    {
        public string Download { get; set; }
        public string Pan { get; set; }
        public string Reset { get; set; }
        public string Selection { get; set; }
        public string SelectionZoom { get; set; }
        public string ZoomIn { get; set; }
        public string ZoomOut { get; set; }
    }

    public class ApexChartSelection
    {
        public bool Enabled { get; set; } = true;
        public IndigoFill Fill { get; set; }
        public IndecentStroke Stroke { get; set; }
        public string Type { get; set; }
        public FluffyXaxis Xaxis { get; set; }
        public FluffyYaxis Yaxis { get; set; }
    }

    public class IndigoFill
    {
        public string Color { get; set; }
        public double? Opacity { get; set; }
    }

    public class IndecentStroke
    {
        public string Color { get; set; }
        public double? DashArray { get; set; }
        public double? Opacity { get; set; }
        public double? Width { get; set; }
    }

    public class FluffyXaxis
    {
        public double? Max { get; set; }
        public double? Min { get; set; }
    }

    public class FluffyYaxis
    {
        public double? Max { get; set; }
        public double? Min { get; set; }
    }

    public class ApexChartSparkline
    {
        public bool Enabled { get; set; } = true;
    }

    public class ApexChartToolbar
    {
        public AutoSelected? AutoSelected { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public bool? Show { get; set; }
        public FluffyTools Tools { get; set; }
    }

    public class FluffyTools
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

    public class FluffyCustomIcon
    {
        public string Icon { get; set; }
        public double? Index { get; set; }
        public string Title { get; set; }
    }

    public class ApexChartZoom
    {
        public bool? AutoScaleYaxis { get; set; }
        public bool Enabled { get; set; } = true;
        public ZoomType? Type { get; set; }
        public FluffyZoomedArea ZoomedArea { get; set; }
    }

    public class FluffyZoomedArea
    {
        public IndecentFill Fill { get; set; }
        public HilariousStroke Stroke { get; set; }
    }

    public class IndecentFill
    {
        public string Color { get; set; }
        public double? Opacity { get; set; }
    }

    public class HilariousStroke
    {
        public string Color { get; set; }
        public double? Opacity { get; set; }
        public double? Width { get; set; }
    }

    public class ApexStates
    {
        public ApexStatesActive Active { get; set; }
        public ApexStatesHover Hover { get; set; }
        public ApexStatesNormal Normal { get; set; }
    }

    public class ApexStatesActive
    {
        public bool? AllowMultipleDataPointsSelection { get; set; }
        public StickyFilter Filter { get; set; }
    }

    public class StickyFilter
    {
        public string Type { get; set; }
        public double? Value { get; set; }
    }

    public class ApexStatesHover
    {
        public IndigoFilter Filter { get; set; }
    }

    public class IndigoFilter
    {
        public string Type { get; set; }
        public double? Value { get; set; }
    }

    public class ApexStatesNormal
    {
        public IndecentFilter Filter { get; set; }
    }

    public class IndecentFilter
    {
        public string Type { get; set; }
        public double? Value { get; set; }
    }

    public class ApexTitleSubtitle
    {
        public Align? Align { get; set; }
        public bool? Floating { get; set; }
        public double? Margin { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public ApexTitleSubtitleStyle Style { get; set; }
        public string Text { get; set; }
    }

    public class ApexTitleSubtitleStyle
    {
        public string Color { get; set; }
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
        public object FontWeight { get; set; }
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
    //public class ApexAxisChartSeries
    //{
    //    public List<ApexAxisChartSeryDatum> Data { get; set; }
    //    public string Name { get; set; }
    //    public string Type { get; set; }
    //}

    public class FluffyDatum
    {
        public string FillColor { get; set; }
        public string StrokeColor { get; set; }
        public object X { get; set; }
        public object Y { get; set; }
    }

    public class ApexStroke
    {
        public List<string> Colors { get; set; }
        public Curve? Curve { get; set; }
        public Opacity? DashArray { get; set; }
        public LineCap? LineCap { get; set; }
        public bool? Show { get; set; }
        public Opacity? Width { get; set; }
    }

    public class ApexAnnotations
    {
        public List<ApexAnnotationsImage> Images { get; set; }
        public List<ApexAnnotationsPoint> Points { get; set; }
        public string Position { get; set; }
        public List<ApexAnnotationsShape> Shapes { get; set; }
        public List<ApexAnnotationsText> Texts { get; set; }
        public List<ApexAnnotationsXaxi> Xaxis { get; set; }
        public List<ApexAnnotationsYAxis> Yaxis { get; set; }
    }

    public class ApexAnnotationsImage
    {
        public double? Height { get; set; }
        public string Path { get; set; }
        public double? Width { get; set; }
        public double? X { get; set; }
        public double? Y { get; set; }
    }

    public class ApexAnnotationsPoint
    {
        public FluffyImage Image { get; set; }
        public StickyLabel Label { get; set; }
        public FluffyMarker Marker { get; set; }
        public double? SeriesIndex { get; set; }
        public object X { get; set; }
        public double? Y { get; set; }
        public double? YAxisIndex { get; set; }
    }

    public class FluffyImage
    {
        public double? Height { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public string Path { get; set; }
        public double? Width { get; set; }
    }

    public class StickyLabel
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

    public class BraggadociousStyle
    {
        public string Background { get; set; }
        public string Color { get; set; }
        public string CssClass { get; set; }
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
        public object FontWeight { get; set; }
        public Padding Padding { get; set; }
    }


    public class FluffyMarker
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

    public class ApexAnnotationsShape
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

    public class ApexAnnotationsText
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

    public class ApexAnnotationsXaxi
    {
        public string BorderColor { get; set; }
        public double? BorderWidth { get; set; }
        public string FillColor { get; set; }
        public IndigoLabel Label { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public double? Opacity { get; set; }
        public double? StrokeDashArray { get; set; }
        public object X { get; set; }
        public object X2 { get; set; }
    }

    public class IndigoLabel
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

    public class Style1
    {
        public string Background { get; set; }
        public string Color { get; set; }
        public string CssClass { get; set; }
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
        public object FontWeight { get; set; }
        public Padding Padding { get; set; }
    }


    public class ApexAnnotationsYAxis
    {
        public string BorderColor { get; set; }
        public double? BorderWidth { get; set; }
        public string FillColor { get; set; }
        public IndecentLabel Label { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public double? Opacity { get; set; }
        public double? StrokeDashArray { get; set; }
        public object Y { get; set; }
        public object Y2 { get; set; }
        public double? YAxisIndex { get; set; }
    }

    public class IndecentLabel
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

    public class Style2
    {
        public string Background { get; set; }
        public string Color { get; set; }
        public string CssClass { get; set; }
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
        public object FontWeight { get; set; }
        public Padding Padding { get; set; }
    }



    public class AnnotationLabel
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

    public class AnnotationLabelStyle
    {
        public string Background { get; set; }
        public string Color { get; set; }
        public string CssClass { get; set; }
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
        public object FontWeight { get; set; }
        public Padding Padding { get; set; }
    }


    public class AnnotationStyle
    {
        public string Background { get; set; }
        public string Color { get; set; }
        public string CssClass { get; set; }
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
        public object FontWeight { get; set; }
        public Padding Padding { get; set; }
    }


    public class XAxisAnnotations
    {
        public string BorderColor { get; set; }
        public double? BorderWidth { get; set; }
        public string FillColor { get; set; }
        public XAxisAnnotationsLabel Label { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public double? Opacity { get; set; }
        public double? StrokeDashArray { get; set; }
        public object X { get; set; }
        public object X2 { get; set; }
    }

    public class XAxisAnnotationsLabel
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

    public class Style3
    {
        public string Background { get; set; }
        public string Color { get; set; }
        public string CssClass { get; set; }
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
        public object FontWeight { get; set; }
        public Padding Padding { get; set; }
    }


    public class YAxisAnnotations
    {
        public string BorderColor { get; set; }
        public double? BorderWidth { get; set; }
        public string FillColor { get; set; }
        public YAxisAnnotationsLabel Label { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public double? Opacity { get; set; }
        public double? StrokeDashArray { get; set; }
        public object Y { get; set; }
        public object Y2 { get; set; }
        public double? YAxisIndex { get; set; }
    }

    public class YAxisAnnotationsLabel
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

    public class Style4
    {
        public string Background { get; set; }
        public string Color { get; set; }
        public string CssClass { get; set; }
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
        public object FontWeight { get; set; }
        public Padding Padding { get; set; }
    }



    public class PointAnnotations
    {
        public PointAnnotationsImage Image { get; set; }
        public PointAnnotationsLabel Label { get; set; }
        public PointAnnotationsMarker Marker { get; set; }
        public double? SeriesIndex { get; set; }
        public object X { get; set; }
        public double? Y { get; set; }
        public double? YAxisIndex { get; set; }
    }

    public class PointAnnotationsImage
    {
        public double? Height { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public string Path { get; set; }
        public double? Width { get; set; }
    }

    public class PointAnnotationsLabel
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

    public class Style5
    {
        public string Background { get; set; }
        public string Color { get; set; }
        public string CssClass { get; set; }
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
        public object FontWeight { get; set; }
        public Padding Padding { get; set; }
    }


    public class PointAnnotationsMarker
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

    public class ShapeAnnotations
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

    public class TextAnnotations
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

    public class ImageAnnotations
    {
        public double? Height { get; set; }
        public string Path { get; set; }
        public double? Width { get; set; }
        public double? X { get; set; }
        public double? Y { get; set; }
    }

    public class ApexLocale
    {
        public string Name { get; set; }
        public ApexLocaleOptions Options { get; set; }
    }

    public class ApexLocaleOptions
    {
        public List<string> Days { get; set; }
        public List<string> Months { get; set; }
        public List<string> ShortDays { get; set; }
        public List<string> ShortMonths { get; set; }
        public TentacledToolbar Toolbar { get; set; }
    }

    public class TentacledToolbar
    {
        public string Download { get; set; }
        public string Pan { get; set; }
        public string Reset { get; set; }
        public string Selection { get; set; }
        public string SelectionZoom { get; set; }
        public string ZoomIn { get; set; }
        public string ZoomOut { get; set; }
    }

    public class ApexPlotOptions
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

    public class ApexPlotOptionsBar
    {
        public string BarHeight { get; set; }
        public TentacledColors Colors { get; set; }
        public string ColumnWidth { get; set; }
        public StickyDataLabels DataLabels { get; set; }
        public bool? Distributed { get; set; }
        public Shape? EndingShape { get; set; }
        public bool? Horizontal { get; set; }
        public bool? RangeBarOverlap { get; set; }
        public Shape? StartingShape { get; set; }
    }

    public class TentacledColors
    {
        public List<string> BackgroundBarColors { get; set; }
        public double? BackgroundBarOpacity { get; set; }
        public double? BackgroundBarRadius { get; set; }
        public List<TentacledRange> Ranges { get; set; }
    }

    public class TentacledRange
    {
        public string Color { get; set; }
        public double? From { get; set; }
        public double? To { get; set; }
    }

    public class StickyDataLabels
    {
        public bool? HideOverflowingLabels { get; set; }
        public double? MaxItems { get; set; }
        public Orientation? Orientation { get; set; }
        public string Position { get; set; }
    }

    public class ApexPlotOptionsBubble
    {
        public double? MaxBubbleRadius { get; set; }
        public double? MinBubbleRadius { get; set; }
    }

    public class ApexPlotOptionsCandlestick
    {
        public StickyColors Colors { get; set; }
        public FluffyWick Wick { get; set; }
    }

    public class StickyColors
    {
        public string Downward { get; set; }
        public string Upward { get; set; }
    }

    public class FluffyWick
    {
        public bool? UseFillColor { get; set; }
    }

    public class ApexPlotOptionsHeatmap
    {
        public FluffyColorScale ColorScale { get; set; }
        public bool? Distributed { get; set; }
        public bool? EnableShades { get; set; }
        public double? Radius { get; set; }
        public bool? ReverseNegativeShade { get; set; }
        public double? ShadeIntensity { get; set; }
        public bool? UseFillColorAsStroke { get; set; }
    }

    public class FluffyColorScale
    {
        public bool? Inverse { get; set; }
        public double? Max { get; set; }
        public double? Min { get; set; }
        public List<StickyRange> Ranges { get; set; }
    }

    public class StickyRange
    {
        public string Color { get; set; }
        public string ForeColor { get; set; }
        public double? From { get; set; }
        public string Name { get; set; }
        public double? To { get; set; }
    }

    public class ApexPlotOptionsPie
    {
        public double? CustomScale { get; set; }
        public IndigoDataLabels DataLabels { get; set; }
        public FluffyDonut Donut { get; set; }
        public bool? ExpandOnClick { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
    }

    public class IndigoDataLabels
    {
        public double? MinAngleToShowLabel { get; set; }
        public double? Offset { get; set; }
    }

    public class FluffyDonut
    {
        public string Background { get; set; }
        public FluffyLabels Labels { get; set; }
        public string Size { get; set; }
    }

    public class FluffyLabels
    {
        public TentacledName Name { get; set; }
        public bool? Show { get; set; }
        public TentacledTotal Total { get; set; }
        public TentacledValue Value { get; set; }
    }

    public class TentacledName
    {
        public string Color { get; set; }
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
        public object FontWeight { get; set; }
        public double? OffsetY { get; set; }
        public bool? Show { get; set; }
    }

    public class TentacledTotal
    {
        public string Color { get; set; }
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
        public object FontWeight { get; set; }
        public string Label { get; set; }
        public bool? Show { get; set; }
        public bool? ShowAlways { get; set; }
    }

    public class TentacledValue
    {
        public string Color { get; set; }
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
        public object FontWeight { get; set; }
        public double? OffsetY { get; set; }
        public bool? Show { get; set; }
    }

    public class ApexPlotOptionsPolarArea
    {
        public FluffyRings Rings { get; set; }
    }

    public class FluffyRings
    {
        public string StrokeColor { get; set; }
        public double? StrokeWidth { get; set; }
    }

    public class ApexPlotOptionsRadar
    {
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public FluffyPolygons Polygons { get; set; }
        public double? Size { get; set; }
    }

    public class FluffyPolygons
    {
        public Color? ConnectorColors { get; set; }
        public HilariousFill Fill { get; set; }
        public Color? StrokeColors { get; set; }
        public Color? StrokeWidth { get; set; }
    }

    public class HilariousFill
    {
        public List<string> Colors { get; set; }
    }

    public class ApexPlotOptionsRadialBar
    {
        public IndecentDataLabels DataLabels { get; set; }
        public double? EndAngle { get; set; }
        public FluffyHollow Hollow { get; set; }
        public bool? InverseOrder { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public double? StartAngle { get; set; }
        public Track Track { get; set; }
    }

    public class IndecentDataLabels
    {
        public StickyName Name { get; set; }
        public bool? Show { get; set; }
        public StickyTotal Total { get; set; }
        public StickyValue Value { get; set; }
    }

    public class StickyName
    {
        public string Color { get; set; }
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
        public object FontWeight { get; set; }
        public double? OffsetY { get; set; }
        public bool? Show { get; set; }
    }

    public class StickyTotal
    {
        public string Color { get; set; }
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
        public object FontWeight { get; set; }
        public string Label { get; set; }
        public bool? Show { get; set; }
    }

    public class StickyValue
    {
        public string Color { get; set; }
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
        public object FontWeight { get; set; }
        public double? OffsetY { get; set; }
        public bool? Show { get; set; }
    }

    public class FluffyHollow
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



    public class ApexFill
    {
        public List<object> Colors { get; set; }
        public ApexFillGradient Gradient { get; set; }
        public ApexFillImage Image { get; set; }
        public Opacity? Opacity { get; set; }
        public ApexFillPattern Pattern { get; set; }
        public Color? Type { get; set; }
    }

    public class ApexFillGradient
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

    public class ApexFillImage
    {
        public double? Height { get; set; }
        public Color? Src { get; set; }
        public double? Width { get; set; }
    }

    public class ApexFillPattern
    {
        public double? Height { get; set; }
        public double? StrokeWidth { get; set; }
        public Color? Style { get; set; }
        public double? Width { get; set; }
    }

    public class ApexLegend
    {
        public ApexLegendContainerMargin ContainerMargin { get; set; }
        public bool? Floating { get; set; }
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
        public object FontWeight { get; set; }
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

    public class ApexLegendContainerMargin
    {
        public double? Left { get; set; }
        public double? Top { get; set; }
    }

    public class ApexLegendItemMargin
    {
        public double? Horizontal { get; set; }
        public double? Vertical { get; set; }
    }

    public class ApexLegendLabels
    {
        public Color? Colors { get; set; }
        public bool? UseSeriesColors { get; set; }
    }

    public class ApexLegendMarkers
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

    public class ApexLegendOnItemClick
    {
        public bool? ToggleDataSeries { get; set; }
    }

    public class ApexLegendOnItemHover
    {
        public bool? HighlightDataSeries { get; set; }
    }

    public class ApexDiscretePoint
    {
        public double? DataPointIndex { get; set; }
        public string FillColor { get; set; }
        public double? SeriesIndex { get; set; }
        public double? Size { get; set; }
        public string StrokeColor { get; set; }
    }

    public class ApexMarkers
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

    public class ApexMarkersDiscrete
    {
        public double? DataPointIndex { get; set; }
        public string FillColor { get; set; }
        public double? SeriesIndex { get; set; }
        public double? Size { get; set; }
        public string StrokeColor { get; set; }
    }

    public class ApexMarkersHover
    {
        public double? Size { get; set; }
        public double? SizeOffset { get; set; }
    }

    public class ApexNoData
    {
        public Align? Align { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public ApexNoDataStyle Style { get; set; }
        public string Text { get; set; }
        public VerticalAlign? VerticalAlign { get; set; }
    }

    public class ApexNoDataStyle
    {
        public string Color { get; set; }
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
    }

    public class ApexDataLabels
    {
        public ApexDataLabelsBackground Background { get; set; }
        public bool? Distributed { get; set; }
        public ApexDataLabelsDropShadow DropShadow { get; set; }
        public bool Enabled { get; set; } = true;
        public List<double> EnabledOnSeries { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public ApexDataLabelsStyle Style { get; set; }
        public TextAnchor? TextAnchor { get; set; }
    }

    public class ApexDataLabelsBackground
    {
        public string BorderColor { get; set; }
        public double? BorderRadius { get; set; }
        public double? BorderWidth { get; set; }
        public HilariousDropShadow DropShadow { get; set; }
        public bool Enabled { get; set; } = true;
        public string ForeColor { get; set; }
        public double? Opacity { get; set; }
        public double? Padding { get; set; }
    }

    public class HilariousDropShadow
    {
        public double? Blur { get; set; }
        public string Color { get; set; }
        public bool Enabled { get; set; } = true;
        public double? Left { get; set; }
        public double? Opacity { get; set; }
        public double? Top { get; set; }
    }

    public class ApexDataLabelsDropShadow
    {
        public double? Blur { get; set; }
        public string Color { get; set; }
        public bool Enabled { get; set; } = true;
        public double? Left { get; set; }
        public double? Opacity { get; set; }
        public double? Top { get; set; }
    }

    public class ApexDataLabelsStyle
    {
        public List<string> Colors { get; set; }
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
        public object FontWeight { get; set; }
    }

    public class ApexResponsive
    {
        public double? Breakpoint { get; set; }
        public object Options { get; set; }
    }

    public class ApexTooltipY
    {
        public Dictionary<string, object> Title { get; set; }
    }

    public class ApexTooltip
    {
        //public Custom? Custom { get; set; }
        public bool Enabled { get; set; } = true;
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

    public class ApexTooltipFixed
    {
        public bool Enabled { get; set; } = true;
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public string Position { get; set; }
    }

    public class ApexTooltipItems
    {
        public string Display { get; set; }
    }

    public class ApexTooltipMarker
    {
        public List<string> FillColors { get; set; }
        public bool? Show { get; set; }
    }

    public class ApexTooltipOnDatasetHover
    {
        public bool? HighlightDataSeries { get; set; }
    }

    public class ApexTooltipStyle
    {
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
    }

    public class ApexTooltipX
    {
        public string Format { get; set; }
        public bool? Show { get; set; }
    }

    public class TentacledY
    {
        public Dictionary<string, object> Title { get; set; }
    }

    public class StickyY
    {
        public Dictionary<string, object> Title { get; set; }
    }

    public class ApexTooltipZ
    {
        public string Title { get; set; }
    }

    public class ApexXAxis
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
        public object TickAmount { get; set; }
        public string TickPlacement { get; set; }
        public ApexXAxisTitle Title { get; set; }
        public ApexXAxisTooltip Tooltip { get; set; }
        public XaxisType? Type { get; set; }
    }

    public class ApexXAxisAxisBorder
    {
        public string Color { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public bool? Show { get; set; }
        public double? StrokeWidth { get; set; }
    }

    public class ApexXAxisAxisTicks
    {
        public string BorderType { get; set; }
        public string Color { get; set; }
        public double? Height { get; set; }
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public bool? Show { get; set; }
    }

    public class ApexXAxisCrosshairs
    {
        public AmbitiousDropShadow DropShadow { get; set; }
        public AmbitiousFill Fill { get; set; }
        public double? Opacity { get; set; }
        public string Position { get; set; }
        public bool? Show { get; set; }
        public AmbitiousStroke Stroke { get; set; }
        public object Width { get; set; }
    }

    public class AmbitiousDropShadow
    {
        public double? Blur { get; set; }
        public string Color { get; set; }
        public bool Enabled { get; set; } = true;
        public double? Left { get; set; }
        public double? Opacity { get; set; }
        public double? Top { get; set; }
    }

    public class AmbitiousFill
    {
        public string Color { get; set; }
        public TentacledGradient Gradient { get; set; }
        public string Type { get; set; }
    }

    public class TentacledGradient
    {
        public string ColorFrom { get; set; }
        public string ColorTo { get; set; }
        public double? OpacityFrom { get; set; }
        public double? OpacityTo { get; set; }
        public List<double> Stops { get; set; }
    }

    public class AmbitiousStroke
    {
        public string Color { get; set; }
        public double? DashArray { get; set; }
        public double? Width { get; set; }
    }

    public class ApexXAxisLabels
    {
        public FluffyDatetimeFormatter DatetimeFormatter { get; set; }
        public bool? DatetimeUTC { get; set; }
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

    public class FluffyDatetimeFormatter
    {
        public string Day { get; set; }
        public string Hour { get; set; }
        public string Minute { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
    }

    public class Style6
    {
        public Color? Colors { get; set; }
        public string CssClass { get; set; }
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
        public object FontWeight { get; set; }
    }

    public class ApexXAxisTitle
    {
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public Style7 Style { get; set; }
        public string Text { get; set; }
    }

    public class Style7
    {
        public string Color { get; set; }
        public string CssClass { get; set; }
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
        public object FontWeight { get; set; }
    }

    public class ApexXAxisTooltip
    {
        public bool Enabled { get; set; } = true;
        public double? OffsetY { get; set; }
        public Style8 Style { get; set; }
    }

    public class Style8
    {
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
    }

    //public class ApexYAxis
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

    //public class ApexYAxisAxisBorder
    //{
    //    public string Color { get; set; }
    //    public double? OffsetX { get; set; }
    //    public double? OffsetY { get; set; }
    //    public bool? Show { get; set; }
    //    public double? Width { get; set; }
    //}

    //public class ApexYAxisAxisTicks
    //{
    //    public string Color { get; set; }
    //    public double? OffsetX { get; set; }
    //    public double? OffsetY { get; set; }
    //    public bool? Show { get; set; }
    //    public double? Width { get; set; }
    //}

    //public class ApexYAxisCrosshairs
    //{
    //    public string Position { get; set; }
    //    public bool? Show { get; set; }
    //    public CunningStroke Stroke { get; set; }
    //}

    public class CunningStroke
    {
        public string Color { get; set; }
        public double? DashArray { get; set; }
        public double? Width { get; set; }
    }

    public class ApexYAxisLabels
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

    public class Style9
    {
        public string Colors { get; set; }
        public string CssClass { get; set; }
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
        public object FontWeight { get; set; }
    }

    public class ApexYAxisTitle
    {
        public double? OffsetX { get; set; }
        public double? OffsetY { get; set; }
        public double? Rotate { get; set; }
        public Style10 Style { get; set; }
        public string Text { get; set; }
    }

    public class Style10
    {
        public string Color { get; set; }
        public string CssClass { get; set; }
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
        public object FontWeight { get; set; }
    }

    public class ApexYAxisTooltip
    {
        public bool Enabled { get; set; } = true;
        public double? OffsetX { get; set; }
    }

    public class ApexGrid
    {
        public string BorderColor { get; set; }
        public ApexGridColumn Column { get; set; }
        public Padding Padding { get; set; }
        public GridPosition? Position { get; set; }
        public ApexGridRow Row { get; set; }
        public bool? Show { get; set; }
        public double? StrokeDashArray { get; set; }
        public ApexGridXaxis Xaxis { get; set; }
        public ApexGridYaxis Yaxis { get; set; }
    }

    public class ApexGridColumn
    {
        public List<string> Colors { get; set; }
        public double? Opacity { get; set; }
    }

    public class ApexGridRow
    {
        public List<string> Colors { get; set; }
        public double? Opacity { get; set; }
    }

    public class ApexGridXaxis
    {
        public Lines Lines { get; set; }
    }
  
    public class ApexGridYaxis
    {
        public Lines Lines { get; set; }
    }

    public class ApexTheme
    {
        public Mode? Mode { get; set; }
        public ApexThemeMonochrome Monochrome { get; set; }
        public string Palette { get; set; }
    }

    public class ApexThemeMonochrome
    {
        public string Color { get; set; }
        public bool Enabled { get; set; } = true;
        public double? ShadeIntensity { get; set; }
        public Mode? ShadeTo { get; set; }
    }

    public enum Easing { Easein, Easeinout, Easeout, Linear };

    public enum StackType { 
        Normal,
        [EnumMember(Value = "100%")]
        Percent100 };

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

    public enum Shape { Flat, Rounded };

    public enum Curve { Smooth, Stepline, Straight };

    public enum LineCap { Butt, Round, Square };

    public enum Mode { Dark, Light };

    public enum TickAmountEnum { DataPoints };

    public enum XaxisType { Category, Datetime, Numeric };

    //public struct FontWeight
    //{
    //    public double? Double;
    //    public string String;

    //    public static implicit operator FontWeight(double Double) => new FontWeight { Double = Double };
    //    public static implicit operator FontWeight(string String) => new FontWeight { String = String };
    //}

    //public struct X
    //{
    //    public double? Double;
    //    public string String;

    //    public static implicit operator X(double Double) => new X { Double = Double };
    //    public static implicit operator X(string String) => new X { String = String };
    //    public bool IsNull => Double == null && String == null;
    //}

    public struct Download
    {
        public bool? Bool;
        public string String;

        public static implicit operator Download(bool Bool) => new Download { Bool = Bool };
        public static implicit operator Download(string String) => new Download { String = String };
    }

    public struct Color
    {
        public string String;
        public List<string> StringArray;

        public static implicit operator Color(string String) => new Color { String = String };
        public static implicit operator Color(List<string> StringArray) => new Color { StringArray = StringArray };
    }

    public struct Opacity
    {
        public double? Double;
        public List<double> DoubleArray;

        public static implicit operator Opacity(double Double) => new Opacity { Double = Double };
        public static implicit operator Opacity(List<double> DoubleArray) => new Opacity { DoubleArray = DoubleArray };
    }

    public struct ShapeUnion
    {
        public ShapeEnum? Enum;
        public List<string> StringArray;

        public static implicit operator ShapeUnion(ShapeEnum Enum) => new ShapeUnion { Enum = Enum };
        public static implicit operator ShapeUnion(List<string> StringArray) => new ShapeUnion { StringArray = StringArray };
    }

    public struct DatumDatum
    {
        public double? Double;
        public List<double?> UnionArray;

        public static implicit operator DatumDatum(double Double) => new DatumDatum { Double = Double };
        public static implicit operator DatumDatum(List<double?> UnionArray) => new DatumDatum { UnionArray = UnionArray };
        public bool IsNull => UnionArray == null && Double == null;
    }

    //public struct SeriesDatum
    //{
    //    public List<DatumDatum> AnythingArray;
    //    public double? Double;
    //    public PurpleDatum PurpleDatum;

    //    public static implicit operator SeriesDatum(List<DatumDatum> AnythingArray) => new SeriesDatum { AnythingArray = AnythingArray };
    //    public static implicit operator SeriesDatum(double Double) => new SeriesDatum { Double = Double };
    //    public static implicit operator SeriesDatum(PurpleDatum PurpleDatum) => new SeriesDatum { PurpleDatum = PurpleDatum };
    //    public bool IsNull => AnythingArray == null && PurpleDatum == null && Double == null;
    //}

    //public struct SeriesElement
    //{
    //    public double? Double;
    //    public Series SeriesClass;

    //    public static implicit operator SeriesElement(double Double) => new SeriesElement { Double = Double };
    //    public static implicit operator SeriesElement(Series SeriesClass) => new SeriesElement { SeriesClass = SeriesClass };
    //}

    //public struct Custom
    //{
    //    public Dictionary<string, object> AnythingMap;
    //    public List<Dictionary<string, object>> AnythingMapArray;

    //    public static implicit operator Custom(Dictionary<string, object> AnythingMap) => new Custom { AnythingMap = AnythingMap };
    //    public static implicit operator Custom(List<Dictionary<string, object>> AnythingMapArray) => new Custom { AnythingMapArray = AnythingMapArray };
    //}

    //public struct TooltipY
    //{
    //    public FluffyY FluffyY;
    //    public List<PurpleY> PurpleYArray;

    //    public static implicit operator TooltipY(FluffyY FluffyY) => new TooltipY { FluffyY = FluffyY };
    //    public static implicit operator TooltipY(List<PurpleY> PurpleYArray) => new TooltipY { PurpleYArray = PurpleYArray };
    //}

    //public struct TickAmountUnion
    //{
    //    public double? Double;
    //    public TickAmountEnum? Enum;

    //    public static implicit operator TickAmountUnion(double Double) => new TickAmountUnion { Double = Double };
    //    public static implicit operator TickAmountUnion(TickAmountEnum Enum) => new TickAmountUnion { Enum = Enum };
    //}

    //public struct Max
    //{
    //    public Dictionary<string, object> AnythingMap;
    //    public double? Double;

    //    public static implicit operator Max(Dictionary<string, object> AnythingMap) => new Max { AnythingMap = AnythingMap };
    //    public static implicit operator Max(double Double) => new Max { Double = Double };
    //}

    //public struct Yaxis_Old
    //{
    //    public List<YaxisYaxi> YaxisYaxiArray;
    //    public YaxisYaxis YaxisYaxis;

    //    public static implicit operator Yaxis(List<YaxisYaxi> YaxisYaxiArray) => new Yaxis { YaxisYaxiArray = YaxisYaxiArray };
    //    public static implicit operator Yaxis(YaxisYaxis YaxisYaxis) => new Yaxis { YaxisYaxis = YaxisYaxis };
    //}

    public struct ApexAxisChartSeryDatum
    {
        public List<DatumDatum> AnythingArray;
        public double? Double;
        public FluffyDatum FluffyDatum;

        public static implicit operator ApexAxisChartSeryDatum(List<DatumDatum> AnythingArray) => new ApexAxisChartSeryDatum { AnythingArray = AnythingArray };
        public static implicit operator ApexAxisChartSeryDatum(double Double) => new ApexAxisChartSeryDatum { Double = Double };
        public static implicit operator ApexAxisChartSeryDatum(FluffyDatum FluffyDatum) => new ApexAxisChartSeryDatum { FluffyDatum = FluffyDatum };
        public bool IsNull => AnythingArray == null && FluffyDatum == null && Double == null;
    }

    public struct ApexTooltipYUnion
    {
        public StickyY StickyY;
        public List<TentacledY> TentacledYArray;

        public static implicit operator ApexTooltipYUnion(StickyY StickyY) => new ApexTooltipYUnion { StickyY = StickyY };
        public static implicit operator ApexTooltipYUnion(List<TentacledY> TentacledYArray) => new ApexTooltipYUnion { TentacledYArray = TentacledYArray };
    }
}