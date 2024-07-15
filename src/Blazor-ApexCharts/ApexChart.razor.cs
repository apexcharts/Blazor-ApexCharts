using ApexCharts.Internal;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace ApexCharts
{
    /// <summary>
    /// Main component to create an Apex chart in Blazor
    /// </summary>
    /// <typeparam name="TItem">The data type of the items to display in the chart</typeparam>
    public partial class ApexChart<TItem> : IDisposable where TItem : class
    {
        [Inject] private IJSRuntime jsRuntime { get; set; }

        /// <summary>
        /// Used to contain the data within the chart
        /// </summary>
        [Parameter] public RenderFragment ChildContent { get; set; }

        /// <inheritdoc cref="Tooltip"/>
        [Parameter] public RenderFragment<HoverData<TItem>> ApexPointTooltip { get; set; }

        /// <summary>
        /// The options to customize the chart with
        /// </summary>
        /// <remarks>
        /// Each instance of this component must have its own options object
        /// </remarks>
        [Parameter] public ApexChartOptions<TItem> Options { get; set; } = new ApexChartOptions<TItem>();

        /// <inheritdoc cref="Title.Text"/>
        [Parameter] public string Title { get; set; }

        /// <inheritdoc cref="XAxis.Type"/>
        [Parameter] public XAxisType? XAxisType { get; set; }

        /// <summary>
        /// Specifies whether to enable debug mode
        /// </summary>
        [Parameter] public bool Debug { get; set; }

        /// <inheritdoc cref="Chart.Width"/>
        [Parameter] public object Width { get; set; }

        /// <inheritdoc cref="Chart.Height"/>
        [Parameter] public object Height { get; set; }

        /// <summary>
        /// Fires when user clicks on the x-axis labels.
        /// </summary>
        /// <remarks>
        /// Links:
        /// 
        /// <see href="https://apexcharts.github.io/Blazor-ApexCharts/events/xaxis-label-click">Blazor Example</see>,
        /// <see href="https://apexcharts.com/docs/options/chart/events#xAxisLabelClick">JavaScript Documentation</see>
        /// </remarks>
        [Parameter] public EventCallback<XAxisLabelClicked<TItem>> OnXAxisLabelClick { get; set; }

        /// <summary>
        /// Fires when user clicks on the markers.
        /// </summary>
        /// <remarks>
        /// Links:
        /// 
        /// <see href="https://apexcharts.github.io/Blazor-ApexCharts/events/marker-click">Blazor Example</see>,
        /// <see href="https://apexcharts.com/docs/options/chart/events#markerClick">JavaScript Documentation</see>
        /// </remarks>
        [Parameter] public EventCallback<SelectedData<TItem>> OnMarkerClick { get; set; }

        /// <summary>
        /// Fires when user clicks on a datapoint (bar/column/marker/bubble/donut-slice).
        /// </summary>
        /// <remarks>
        /// Links:
        /// 
        /// <see href="https://apexcharts.github.io/Blazor-ApexCharts/events/data-point-selection">Blazor Example</see>,
        /// <see href="https://apexcharts.com/docs/options/chart/events#dataPointSelection">JavaScript Documentation</see>
        /// </remarks>
        [Parameter] public EventCallback<SelectedData<TItem>> OnDataPointSelection { get; set; }

        /// <summary>
        /// Fires when user's mouse enter on a datapoint (bar/column/marker/bubble/donut-slice).
        /// </summary>
        /// <remarks>
        /// Links:
        /// 
        /// <see href="https://apexcharts.github.io/Blazor-ApexCharts/events/data-point-hover">Blazor Example</see>,
        /// <see href="https://apexcharts.com/docs/options/chart/events#dataPointMouseEnter">JavaScript Documentation</see>
        /// </remarks>
        [Parameter] public EventCallback<HoverData<TItem>> OnDataPointEnter { get; set; }

        /// <summary>
        /// MouseLeave event for a datapoint (bar/column/marker/bubble/donut-slice).
        /// </summary>
        /// <remarks>
        /// Links:
        /// 
        /// <see href="https://apexcharts.github.io/Blazor-ApexCharts/events/data-point-hover">Blazor Example</see>,
        /// <see href="https://apexcharts.com/docs/options/chart/events#dataPointMouseLeave">JavaScript Documentation</see>
        /// </remarks>
        [Parameter] public EventCallback<HoverData<TItem>> OnDataPointLeave { get; set; }

        /// <summary>
        /// Fires when user clicks on legend.
        /// </summary>
        /// <remarks>
        /// Links:
        /// 
        /// <see href="https://apexcharts.github.io/Blazor-ApexCharts/events/legend-click">Blazor Example</see>,
        /// <see href="https://apexcharts.com/docs/options/chart/events#legendClick">JavaScript Documentation</see>
        /// </remarks>
        [Parameter] public EventCallback<LegendClicked<TItem>> OnLegendClicked { get; set; }

        /// <summary>
        /// Fires when user selects rect using the selection tool.
        /// </summary>
        /// <remarks>
        /// Links:
        /// 
        /// <see href="https://apexcharts.github.io/Blazor-ApexCharts/events/selection">Blazor Example</see>,
        /// <see href="https://apexcharts.com/docs/options/chart/events#selection">JavaScript Documentation</see>
        /// </remarks>
        [Parameter] public EventCallback<SelectionData<TItem>> OnSelection { get; set; }

        /// <summary>
        /// Fires when user drags the brush in a brush chart.
        /// </summary>
        /// <remarks>
        /// Links:
        /// 
        /// <see href="https://apexcharts.github.io/Blazor-ApexCharts/events/brush-scrolled">Blazor Example</see>,
        /// <see href="https://apexcharts.com/docs/options/chart/events#brushScrolled">JavaScript Documentation</see>
        /// </remarks>
        [Parameter] public EventCallback<SelectionData<TItem>> OnBrushScrolled { get; set; }

        /// <summary>
        /// Fires when user zooms in/out the chart using either the selection zooming tool or zoom in/out buttons.
        /// </summary>
        /// <remarks>
        /// Links:
        /// 
        /// <see href="https://apexcharts.github.io/Blazor-ApexCharts/events/zoomed">Blazor Example</see>,
        /// <see href="https://apexcharts.com/docs/options/chart/events#zoomed">JavaScript Documentation</see>
        /// </remarks>
        [Parameter] public EventCallback<ZoomedData<TItem>> OnZoomed { get; set; }

        /// <summary>
        /// Fires when the chart’s initial animation is finished.
        /// </summary>
        /// <remarks>
        /// Links:
        /// 
        /// <see href="https://apexcharts.com/docs/options/chart/events#animationEnd">JavaScript Documentation</see>
        /// </remarks>
        [Parameter] public EventCallback OnAnimationEnd { get; set; }

        /// <summary>
        /// Fires before the chart has been drawn on screen.
        /// </summary>
        /// <remarks>
        /// Links:
        /// 
        /// <see href="https://apexcharts.com/docs/options/chart/events#beforeMount">JavaScript Documentation</see>
        /// </remarks>
        [Parameter] public EventCallback OnBeforeMount { get; set; }

        /// <summary>
        /// Fires after the chart has been drawn on screen.
        /// </summary>
        /// <remarks>
        /// Links:
        /// 
        /// <see href="https://apexcharts.com/docs/options/chart/events#mounted">JavaScript Documentation</see>
        /// </remarks>
        [Parameter] public EventCallback OnMounted { get; set; }

        /// <summary>
        /// Fires when the chart has been dynamically updated either with <see cref="UpdateOptionsAsync(bool, bool, bool, ZoomOptions)"/> or <see cref="UpdateSeriesAsync(bool)"/> functions.
        /// </summary>
        /// <remarks>
        /// Links:
        /// 
        /// <see href="https://apexcharts.com/docs/options/chart/events#updated">JavaScript Documentation</see>
        /// </remarks>
        [Parameter] public EventCallback OnUpdated { get; set; }

        /// <summary>
        /// Fires when user moves mouse on any area of the chart.
        /// </summary>
        /// <remarks>
        /// Links:
        /// 
        /// <see href="https://apexcharts.com/docs/options/chart/events#mousemove">JavaScript Documentation</see>
        /// </remarks>
        [Parameter] public EventCallback<SelectedData<TItem>> OnMouseMove { get; set; }

        /// <summary>
        /// Fires when user moves mouse outside chart area (exclusing axis).
        /// </summary>
        /// <remarks>
        /// Links:
        /// 
        /// <see href="https://apexcharts.com/docs/options/chart/events#mouseleave">JavaScript Documentation</see>
        /// </remarks>
        [Parameter] public EventCallback OnMouseLeave { get; set; }

        /// <summary>
        /// Fires when user clicks on any area of the chart.
        /// </summary>
        /// <remarks>
        /// Links:
        /// 
        /// <see href="https://apexcharts.com/docs/options/chart/events#click">JavaScript Documentation</see>
        /// </remarks>
        [Parameter] public EventCallback<SelectedData<TItem>> OnClick { get; set; }

        /// <summary>
        /// This function, if defined, runs just before zooming in/out of the chart allowing you to set a custom range for zooming in/out.
        /// </summary>
        /// <remarks>
        /// Links:
        /// 
        /// <see href="https://apexcharts.com/docs/options/chart/events#beforeZoom">JavaScript Documentation</see>
        /// </remarks>
        [Parameter] public Func<SelectionXAxis, SelectionXAxis> OnBeforeZoom { get; set; }

        /// <summary>
        /// This function, if defined, runs just before the user hits the HOME button on the toolbar to reset the chart to it’s original state. The function allows you to set a custom axes range for the initial view of the chart.
        /// </summary>
        /// <remarks>
        /// Links:
        /// 
        /// <see href="https://apexcharts.com/docs/options/chart/events#beforeResetZoom">JavaScript Documentation</see>
        /// </remarks>
        [Parameter] public Func<object, SelectionXAxis> OnBeforeResetZoom { get; set; }

        /// <summary>
        /// Fires when user scrolls using the pan tool. The 2nd argument includes information of the new xaxis generated after scrolling.
        /// </summary>
        /// <remarks>
        /// Links:
        /// 
        /// <see href="https://apexcharts.com/docs/options/chart/events#scrolled">JavaScript Documentation</see>
        /// </remarks>
        [Parameter] public EventCallback<SelectionData<TItem>> OnScrolled { get; set; }

        /// <summary>
        /// Fires when <see cref="RenderAsync"/> completes
        /// </summary>
        [Parameter] public EventCallback OnRendered { get; set; }

        /// <summary>
        /// Fires when user click an annotation label.
        /// </summary>
        /// <remarks>
        /// Links:
        ///
        /// <see href="https://apexcharts.com/docs/options/annotations/">JavaScript Documentation</see>
        /// </remarks>
        [Parameter] public EventCallback<AnnotationEvent<TItem>> OnAnnotationLabelClick { get; set; }

        /// <summary>
        /// Fires when user mouse enters an annotation label.
        /// </summary>
        /// <remarks>
        /// Links:
        ///
        /// <see href="https://apexcharts.com/docs/options/annotations/">JavaScript Documentation</see>
        /// </remarks>
        [Parameter] public EventCallback<AnnotationEvent<TItem>> OnAnnotationLabelMouseEnter { get; set; }

        /// <summary>
        /// Fires when user mouse leaves an annotation label.
        /// </summary>
        /// <remarks>
        /// Links:
        ///
        /// <see href="https://apexcharts.com/docs/options/annotations/">JavaScript Documentation</see>
        /// </remarks>
        [Parameter] public EventCallback<AnnotationEvent<TItem>> OnAnnotationLabelMouseLeave { get; set; }


        /// <summary>
        /// Fires when user click an annotation point.
        /// </summary>
        /// <remarks>
        /// Links:
        ///
        /// <see href="https://apexcharts.com/docs/options/annotations/">JavaScript Documentation</see>
        /// </remarks>
        [Parameter] public EventCallback<AnnotationEvent<TItem>> OnAnnotationPointClick { get; set; }

        /// <summary>
        /// Fires when user mouse enters an annotation point.
        /// </summary>
        /// <remarks>
        /// Links:
        ///
        /// <see href="https://apexcharts.com/docs/options/annotations/">JavaScript Documentation</see>
        /// </remarks>
        [Parameter] public EventCallback<AnnotationEvent<TItem>> OnAnnotationPointMouseEnter { get; set; }

        /// <summary>
        /// Fires when user mouse leaves an annotation point.
        /// </summary>
        /// <remarks>
        /// Links:
        ///
        /// <see href="https://apexcharts.com/docs/options/annotations/">JavaScript Documentation</see>
        /// </remarks>
        [Parameter] public EventCallback<AnnotationEvent<TItem>> OnAnnotationPointMouseLeave { get; set; }


        /// <summary>
        /// A custom function to execute for generating Y-axis labels. Only supported in Blazor WebAssembly!
        /// </summary>
        /// <remarks>
        /// Links:
        /// 
        /// <see href="https://apexcharts.github.io/Blazor-ApexCharts/features/formatters">Blazor Example</see>
        /// </remarks>
        [Parameter] public Func<decimal, string> FormatYAxisLabel { get; set; }

        /// <summary>
        /// A custom function to execute for generating X-axis labels. Only supported in Blazor WebAssembly!
        /// </summary>
        /// <remarks>
        /// Links:
        /// 
        /// <see href="https://apexcharts.github.io/Blazor-ApexCharts/features/formatters">Blazor Example</see>
        /// </remarks>
        [Parameter] public Func<decimal, string> FormatXAxisLabel { get; set; }

        /// <summary>
        /// Enables merging multiple data points into a single item in the chart
        /// </summary>
        /// <remarks>
        /// Links:
        /// 
        /// <see href="https://apexcharts.github.io/Blazor-ApexCharts/features/group-points">Blazor Example</see>
        /// </remarks>
        [Parameter] public GroupPoints GroupPoints { get; set; }

        /// <summary>
        /// The collection of data series to display on the chart
        /// </summary>
        public List<IApexSeries<TItem>> Series => apexSeries;

        private ElementReference ChartContainer { get; set; }
        private List<IApexSeries<TItem>> apexSeries = new();
        private bool isReady;
        private bool forceRender = true;
        private string chartId;
        private HoverData<TItem> tooltipData;
        private JSHandler<TItem> JSHandler;
        private IJSObjectReference blazor_apexchart;

        /// <inheritdoc cref="Chart.Id"/>
        public string ChartId => chartId;

        /// <inheritdoc/>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender && isReady == false)
            {
                var javascriptPath = "./_content/Blazor-ApexCharts/js/blazor-apexcharts.js?ver=3";
                if (!string.IsNullOrWhiteSpace(Options?.Blazor?.JavascriptPath)) { javascriptPath = Options.Blazor.JavascriptPath; }

                // load Module ftom ES6 script
                IJSObjectReference module = await jsRuntime.InvokeAsync<IJSObjectReference>("import", javascriptPath);
                // load the  blazor_apexchart parent, currently window! to be compatyble with JS interop calls e.g blazor_apexchart.dataUri                                                                                                    
                blazor_apexchart = await module.InvokeAsync<IJSObjectReference>("get_apexcharts");


                isReady = true;
                JSHandler = new JSHandler<TItem>(this);
            }

            if (isReady && forceRender)
            {
                await RenderChartAsync();
            }
        }

        /// <inheritdoc/>
        protected override void OnParametersSet()
        {
            if (Options == null) { Options = new ApexChartOptions<TItem>(); }
            if (Options.Chart == null) { Options.Chart = new Chart(); }

            if (string.IsNullOrEmpty(chartId))
            {
                if (Options.Chart.Id != null)
                {
                    chartId = Options.Chart.Id;
                }
                else
                {
                    chartId = Guid.NewGuid().ToString("N");
                }
            }

            Options.Chart.Id = chartId;
            Options.Debug = Debug;

            if (Width != null)
            {
                Options.Chart.Width = Width;
            }

            if (Height != null)
            {
                Options.Chart.Height = Height;
            }

            if (XAxisType != null)
            {
                if (Options.Xaxis == null) { Options.Xaxis = new XAxis(); }
                Options.Xaxis.Type = XAxisType;
            }

            if (string.IsNullOrEmpty(Title))
            {
                Options.Title = null;
            }
            else
            {
                if (Options.Title == null) { Options.Title = new Title(); }
                Options.Title.Text = Title;
            }
        }

        private async ValueTask<TValue> InvokeJsAsync<TValue>(string identifier, params object[] args)
        {
            if (blazor_apexchart == null) { return default; }

            try
            {
                return await blazor_apexchart.InvokeAsync<TValue>(identifier, args);
            }
            catch (Exception ex) when (ex is ObjectDisposedException || ex is JSDisconnectedException)
            {
                return default;
            }
        }

        private async ValueTask InvokeVoidJsAsync(string identifier, params object[] args)
        {
            if (blazor_apexchart == null) { return; }

            try
            {
                if (blazor_apexchart is IJSInProcessObjectReference jsInProcessRuntime)
                {
                    jsInProcessRuntime.InvokeVoid(identifier, args);
                }
                else
                {
                    await blazor_apexchart.InvokeVoidAsync(identifier, args);
                }
            }
            catch (Exception ex) when (ex is ObjectDisposedException || ex is JSDisconnectedException)
            { }
        }

        internal void AddSeries(IApexSeries<TItem> series)
        {
            if (!apexSeries.Contains(series))
            {
                apexSeries.Add(series);
            }
        }

        internal void RemoveSeries(IApexSeries<TItem> series)
        {
            if (apexSeries.Contains(series))
            {
                apexSeries.Remove(series);
            }
        }

        internal bool IsNoAxisChart
        {
            get
            {
                return Options?.Chart?.Type == ChartType.Pie ||
               Options?.Chart?.Type == ChartType.Donut ||
               Options?.Chart?.Type == ChartType.PolarArea ||
               Options?.Chart?.Type == ChartType.RadialBar;
            }
        }

        private void SetSeriesColors()
        {
            if (Options?.Series == null) { return; }
            if (apexSeries.All(e => (e.Color == null))) { return; }

            var colors = new List<string>();

            foreach (var series in Options.Series)
            {
                colors.Add(series.ApexSeries.Color ?? "#d3d3d3"); //Default is light gray
            }

            Options.Colors = colors;
        }

        private void SetSeriesStroke()
        {
            if (Options?.Series == null) { return; }
            if (apexSeries.All(e => (e.Stroke == null))) { return; }

            if (Options.Stroke == null) { Options.Stroke = new Stroke(); }

            var strokeWidths = new List<int>();
            var strokeColors = new List<string>();
            var strokeDash = new List<int>();

            foreach (var series in Options.Series)
            {
                strokeWidths.Add(series.ApexSeries.Stroke?.Width ?? 4);
                strokeColors.Add(series.ApexSeries.Stroke?.Color ?? "#d3d3d3"); //Default is light gray
                strokeDash.Add(series.ApexSeries.Stroke?.DashSpace ?? 0);
            }

            //Not sure if this is a good idea, right now only here for backward compability
            if (Options.Colors == null || !Options.Colors.Any())
            {
                Options.Colors = strokeColors;
            }

            Options.Stroke.Width = strokeWidths;
            Options.Stroke.Colors = strokeColors;
            Options.Stroke.DashArray = strokeDash;
        }

        private void SetDataLabels()
        {
            if (Options?.Series == null || !Options.Series.Any()) { return; }

            if (Options.DataLabels == null) { Options.DataLabels = new DataLabels(); }

            if (Options.Series.First().ApexSeries.GetChartType() != ChartType.Radar)
            {
                if (Options.DataLabels.EnabledOnSeries == null) { Options.DataLabels.EnabledOnSeries = new List<double>(); }

                foreach (var series in Options.Series)
                {
                    var index = Options.Series.FindIndex(e => e == series);

                    if (series.ApexSeries.ShowDataLabels)
                    {
                        if (!Options.DataLabels.EnabledOnSeries.Contains(index))
                        {
                            Options.DataLabels.EnabledOnSeries.Add(index);
                        }
                    }
                    else
                    {
                        if (Options.DataLabels.EnabledOnSeries.Contains(index))
                        {
                            Options.DataLabels.EnabledOnSeries.Remove(index);
                        }
                    }
                }
            }

            if (Options.Series.Select(e => e.ApexSeries).Any(e => e.ShowDataLabels))
            {
                Options.DataLabels.Enabled = true;
            }
            else
            {
                Options.DataLabels.Enabled = false;
            }
        }

        private void UpdateDataForNoAxisCharts()
        {
            if (!IsNoAxisChart)
            {
                Options.Labels = null;
                return;
            };

            if (Options.Series == null || !Options.Series.Any()) { return; }
            if (Options.Series.Count > 1)
            {
                throw new SystemException("Only one series is allowed when chart has no axis");
            }

            var noAxisSeries = Options.Series.First();
            Options.Labels = noAxisSeries.Data.Select(e => e.X?.ToString()).ToList();

            var colors = noAxisSeries.Data.Where(e => !string.IsNullOrWhiteSpace(e.FillColor)).Select(e => e.FillColor).ToList();
            if (colors.Any())
            {
                Options.Colors = colors;
            }
        }

        private bool ShouldFixDataSelection()
        {
            if ((!OnDataPointSelection.HasDelegate && !OnDataPointEnter.HasDelegate && ApexPointTooltip == null) || !Options.Series.Any()) { return false; }

            if (Options.Chart?.Type != null && Options.Chart.Type == ChartType.Line || Options.Chart.Type == ChartType.Area || Options.Chart.Type == ChartType.Radar)
            {
                return true;
            }

            if (Options.Series.Any(e => e.Type == MixedType.Line || e.Type == MixedType.Area))
            {
                return true;
            }

            return false;
        }

        private void FixLineDataSelection()
        {
            if (ShouldFixDataSelection())
            {
                if (Options.Tooltip == null) { Options.Tooltip = new Tooltip(); }
                if (Options.Markers == null) { Options.Markers = new Markers(); }

                if (Options.Markers.Size == null || !Options.Markers.Size.Any() || Options.Markers.Size.Any(x => x <= 0))
                {
                    Options.Markers.Size = 5;
                }

                Options.Tooltip.Intersect = true;
                Options.Tooltip.Shared = false;
            }
        }

        private string Serialize(object data)
        {
            return JsonSerializer.Serialize(data, ChartSerializer.GetOptions<TItem>());
        }

        /// <summary>
        /// The render() method is responsible for drawing the chart on the page. It is the primary method that has to be called after configuring the options.
        /// </summary>
        /// <remarks>
        /// Links:
        /// 
        /// <see href="https://apexcharts.github.io/Blazor-ApexCharts/methods/render">Blazor Example</see>,
        /// <see href="https://apexcharts.com/docs/methods/#render">JavaScript Documentation</see>
        /// </remarks>
        public virtual async Task RenderAsync()
        {
            await RenderChartAsync();
        }

        /// <summary>
        /// The addPointAnnotation() method can be used to draw annotations after chart is rendered.
        /// </summary>
        /// <param name="annotationsPoint">This function accepts the same parameters as it accepts in the point annotations config.</param>
        /// <param name="pushToMemory">When enabled, it preserves the annotations in subsequent chart updates. If you don't want it to be saved for the next updates, turn off this option</param>
        /// <remarks>
        /// Links:
        /// 
        /// <see href="https://apexcharts.github.io/Blazor-ApexCharts/methods/annotations">Blazor Example</see>,
        /// <see href="https://apexcharts.com/docs/methods/#addpointannotation">JavaScript Documentation</see>
        /// </remarks>
        public virtual async Task AddPointAnnotationAsync(AnnotationsPoint annotationsPoint, bool pushToMemory)
        {
            var json = Serialize(annotationsPoint);
            await InvokeVoidJsAsync("blazor_apexchart.addPointAnnotation", Options.Chart.Id, json, pushToMemory);
        }

        /// <summary>
        /// This method allows you to append new data to the series array. If you have existing multiple series, provide the new array in the same indexed order.
        /// </summary>
        /// <param name="items">The data array to append the existing series datasets</param>
        /// <remarks>
        /// Links:
        /// 
        /// <see href="https://apexcharts.github.io/Blazor-ApexCharts/methods/append-data">Blazor Example</see>,
        /// <see href="https://apexcharts.com/docs/methods/#appendData">JavaScript Documentation</see>
        /// </remarks>
        public virtual async Task AppendDataAsync(IEnumerable<TItem> items)
        {
            if (IsNoAxisChart && Series.Any())
            {
                var series = Series.First();

                var allItems = items.ToList();
                allItems.AddRange(series.Items);

                var dataPoints = Series.First().GenerateDataPoints(allItems).Select(e => ((DataPoint<TItem>)e));

                var appendData = new AppendNoAxisData
                {
                    Data = dataPoints.Where(e => e.Y != null).Select(e => e.Y)
                };

                await InvokeVoidJsAsync("blazor_apexchart.appendData", Options.Chart.Id, Serialize(appendData));
                return;
            }

            var seriesList = new List<AppendData<TItem>>();

            foreach (var apxSeries in Options.Series)
            {
                var data = apxSeries.ApexSeries.GenerateDataPoints(items);
                var updatedData = apxSeries.Data.ToList();
                updatedData.AddRange(data);
                apxSeries.Data = updatedData;

                seriesList.Add(new AppendData<TItem>
                {
                    Data = data
                }); ;
            }

            var json = Serialize(seriesList);
            await InvokeVoidJsAsync("blazor_apexchart.appendData", Options.Chart.Id, json);
        }

        /// <summary>
        /// This method allows you to append new data to the series arrays, referencing each series by name
        /// </summary>
        /// <param name="items">The dictinoary with the data array to append the existing series datasets, using the key as the series name.</param>
        /// <remarks>
        /// Links:
        /// 
        /// <see href="https://apexcharts.github.io/Blazor-ApexCharts/methods/append-data">Blazor Example</see>,
        /// <see href="https://apexcharts.com/docs/methods/#appendData">JavaScript Documentation</see>
        /// </remarks>
        public virtual async Task AppendDataBySeriesNameAsync(Dictionary<string, IEnumerable<TItem>> items)
        {
            if (IsNoAxisChart)
            {
                throw new Exception($"{typeof(ApexChart<TItem>)}.{nameof(AppendDataBySeriesNameAsync)}: Operation not valid for no axis charts.");
            }

            var seriesList = new List<AppendData<TItem>>();

            foreach (var apxSeries in Options.Series)
            {
                if (items.ContainsKey(apxSeries.Name))
                {
                    var data = apxSeries.ApexSeries.GenerateDataPoints(items[apxSeries.Name]);
                    var updatedData = apxSeries.Data.ToList();
                    updatedData.AddRange(data);
                    apxSeries.Data = updatedData;

                    seriesList.Add(new AppendData<TItem>
                    {
                        Data = data
                    }); ;
                }
            }

            var json = Serialize(seriesList);
            await InvokeVoidJsAsync("blazor_apexchart.appendData", Options.Chart.Id, json);
        }

        /// <summary>
        /// Manually zoom into the chart with the start and end X values.
        /// </summary>
        /// <param name="zoomOptions">Undefined</param>
        /// <remarks>
        /// Links:
        /// 
        /// <see href="https://apexcharts.github.io/Blazor-ApexCharts/methods/zoomx">Blazor Example</see>,
        /// <see href="https://apexcharts.com/docs/methods/#zoomX">JavaScript Documentation</see>
        /// </remarks>
        public virtual async Task ZoomXAsync(ZoomOptions zoomOptions)
        {
            if (zoomOptions == null) { throw new ArgumentNullException(nameof(zoomOptions)); }
            await InvokeVoidJsAsync("blazor_apexchart.zoomX", Options.Chart.Id, zoomOptions.Start, zoomOptions.End);
        }

        /// <summary>
        /// Manually zoom into the chart with the start and end X values.
        /// </summary>
        /// <param name="start">The starting x-axis value. Accepts timestamp or a number</param>
        /// <param name="end">The ending x-axis value. Accepts timestamp or a number</param>
        /// <remarks>
        /// Links:
        /// 
        /// <see href="https://apexcharts.github.io/Blazor-ApexCharts/methods/zoomx">Blazor Example</see>,
        /// <see href="https://apexcharts.com/docs/methods/#zoomX">JavaScript Documentation</see>
        /// </remarks>
        public virtual async Task ZoomXAsync(decimal start, decimal end)
        {
            await InvokeVoidJsAsync("blazor_apexchart.zoomX", Options.Chart.Id, start, end);
        }

        /// <summary>
        /// Resets all toggled series and bring back the chart to its original state.
        /// </summary>
        /// <param name="shouldUpdateChart">After resetting the series, the chart data should update and return to it's original series.</param>
        /// <param name="shouldResetZoom">If the user has zoomed in when this method is called, the zoom level should also reset.</param>
        public virtual async Task SetLocaleAsync(string name)
        {
            await InvokeVoidJsAsync("blazor_apexchart.setLocale", Options.Chart.Id, name);
        }


        /// <summary>
        /// Resets all toggled series and bring back the chart to its original state.
        /// </summary>
        /// <param name="shouldUpdateChart">After resetting the series, the chart data should update and return to it's original series.</param>
        /// <param name="shouldResetZoom">If the user has zoomed in when this method is called, the zoom level should also reset.</param>
        /// <remarks>
        /// Links:
        /// 
        /// <see href="https://apexcharts.com/docs/methods/#resetSeries">JavaScript Documentation</see>
        /// </remarks>
        public virtual async Task ResetSeriesAsync(bool shouldUpdateChart, bool shouldResetZoom)
        {
            await InvokeVoidJsAsync("blazor_apexchart.resetSeries", Options.Chart.Id, shouldUpdateChart, shouldResetZoom);
        }

        /// <summary>
        /// The dataURI() method is used to get base64 dataURI. Then this dataURI can be used to generate PDF using jsPDF
        /// </summary>
        /// <param name="dataUriOptions">The options object accepts scale or width property which allows to adjust the size of the exported graphic. scale/width both accepts a number.</param>
        /// <remarks>
        /// Links:
        /// 
        /// <see href="https://apexcharts.github.io/Blazor-ApexCharts/methods/data-uri">Blazor Example</see>,
        /// <see href="https://apexcharts.com/docs/methods/#dataURI">JavaScript Documentation</see>
        /// </remarks>
        public virtual async Task<string> GetDataUriAsync(DataUriOptions dataUriOptions)
        {
            var json = Serialize(dataUriOptions);
            var result = await InvokeJsAsync<DataUriResult>("blazor_apexchart.dataUri", Options.Chart.Id, json);
            return result.ImgURI;
        }

        /// <summary>
        /// The addXaxisAnnotation() method can be used to draw annotations after chart is rendered.
        /// </summary>
        /// <param name="annotationsXAxis">This function accepts the same parameters as it accepts in the annotations x-axis config.</param>
        /// <param name="pushToMemory">When enabled, it preserves the annotations in subsequent chart updates. If you don't want it to be saved for the next updates, turn off this option</param>
        /// <remarks>
        /// Links:
        /// 
        /// <see href="https://apexcharts.github.io/Blazor-ApexCharts/methods/annotations">Blazor Example</see>,
        /// <see href="https://apexcharts.com/docs/methods/#addxaxisannotation">JavaScript Documentation</see>
        /// </remarks>
        public virtual async Task AddXAxisAnnotationAsync(AnnotationsXAxis annotationsXAxis, bool pushToMemory)
        {
            var json = Serialize(annotationsXAxis);
            await InvokeVoidJsAsync("blazor_apexchart.addXaxisAnnotation", Options.Chart.Id, json, pushToMemory);
        }

        /// <summary>
        /// The addYaxisAnnotation() method can be used to draw annotations after chart is rendered.
        /// </summary>
        /// <param name="annotationsYAxis">This function accepts the same parameters as it accepts in the annotations y-axis config.</param>
        /// <param name="pushToMemory">When enabled, it preserves the annotations in subsequent chart updates. If you don't want it to be saved for the next updates, turn off this option</param>
        /// <remarks>
        /// Links:
        /// 
        /// <see href="https://apexcharts.github.io/Blazor-ApexCharts/methods/annotations">Blazor Example</see>,
        /// <see href="https://apexcharts.com/docs/methods/#addyaxisannotation">JavaScript Documentation</see>
        /// </remarks>
        public virtual async Task AddYAxisAnnotationAsync(AnnotationsYAxis annotationsYAxis, bool pushToMemory)
        {
            var json = Serialize(annotationsYAxis);
            await InvokeVoidJsAsync("blazor_apexchart.addYaxisAnnotation", Options.Chart.Id, json, pushToMemory);
        }

        /// <summary>
        /// The clearAnnotations() method is used to delete all annotation elements which are added dynamically using the three methods stated above.
        /// </summary>
        /// <remarks>
        /// Links:
        /// 
        /// <see href="https://apexcharts.github.io/Blazor-ApexCharts/methods/annotations">Blazor Example</see>,
        /// <see href="https://apexcharts.com/docs/methods/#clearAnnotations">JavaScript Documentation</see>
        /// </remarks>
        public virtual async Task ClearAnnotationsAsync()
        {
            await InvokeVoidJsAsync("blazor_apexchart.clearAnnotations", Options.Chart.Id);
        }

        /// <summary>
        /// The removeAnnotation() method can be used to delete any previously added annotations. Only annotations which are added by external methods (addPointAnnotation, addXaxisAnnotation, addYaxisAnnotation) are affected. Annotations defined in the options/config object are not affected.
        /// </summary>
        /// <param name="id">The unqiue identifier of the annotation which was created using the addPointAnnotation, addXaxisAnnotation or addYaxisAnnotation methods.</param>
        /// <remarks>
        /// Links:
        /// 
        /// <see href="https://apexcharts.com/docs/methods/#removeAnnotation">JavaScript Documentation</see>
        /// </remarks>
        public virtual async Task RemoveAnnotationAsync(string id)
        {
            await InvokeVoidJsAsync("blazor_apexchart.removeAnnotation", Options.Chart.Id, id);
        }

        /// <summary>
        /// This method allows you to update the configuration object by passing the options as the first parameter. The new config object is merged with the existing config object preserving the existing configuration.
        /// </summary>
        /// <param name="redrawPaths">When the chart is re-rendered, should it draw from the existing paths or completely redraw the chart paths from the beginning. By default, the chart is re-rendered from the existing paths</param>
        /// <param name="animate">Should the chart animate on re-rendering</param>
        /// <param name="updateSyncedCharts">All the charts in a group should also update when one chart in a group is updated.</param>
        /// <param name="zoom">Undefined</param>
        /// <remarks>
        /// Links:
        /// 
        /// <see href="https://apexcharts.github.io/Blazor-ApexCharts/methods/update-options">Blazor Example</see>,
        /// <see href="https://apexcharts.com/docs/methods/#updateOptions">JavaScript Documentation</see>
        /// </remarks>
        public virtual async Task UpdateOptionsAsync(bool redrawPaths, bool animate, bool updateSyncedCharts, ZoomOptions zoom = null)
        {
            await Task.Yield();
            PrepareChart();
            var json = Serialize(Options);
            await InvokeVoidJsAsync("blazor_apexchart.updateOptions", Options.Chart.Id, json, redrawPaths, animate, updateSyncedCharts, zoom);
        }

        /// <summary>
        /// Allows you to update the series array overriding the existing one. If you want to append series to existing series, use the appendSeries() method
        /// </summary>
        /// <param name="animate">Should the chart animate on re-rendering</param>
        /// <remarks>
        /// Links:
        /// 
        /// <see href="https://apexcharts.github.io/Blazor-ApexCharts/methods/update-series">Blazor Example</see>,
        /// <see href="https://apexcharts.com/docs/methods/#updateSeries">JavaScript Documentation</see>
        /// </remarks>
        public virtual async Task UpdateSeriesAsync(bool animate = true)
        {
            await Task.Yield();
            SetSeries();
            UpdateDataForNoAxisCharts();
            var jsonSeries = Serialize(Options.Series);
            await InvokeVoidJsAsync("blazor_apexchart.updateSeries", Options.Chart.Id, jsonSeries, animate);
        }

        /// <summary>
        /// This method allows you to append a new series to the existing ones.
        /// </summary>
        /// <param name="newSeries">The series object to append the existing one</param>
        /// <param name="animate">Should the chart animate on re-rendering</param>
        /// <param name="overwriteInitialSeries">Overwrite initial series</param>
        /// <remarks>
        /// Links:
        /// 
        /// <see href="https://apexcharts.github.io/Blazor-ApexCharts/methods/update-series">Blazor Example</see>,
        /// <see href="https://apexcharts.com/docs/methods/#updateSeries">JavaScript Documentation</see>
        /// </remarks>
        public virtual async Task AppendSeriesAsync(IApexSeries<TItem> newSeries, bool animate = true, bool overwriteInitialSeries = true)
        {
            await Task.Yield();
            UpdateDataForNoAxisCharts();

            var series = new Series<TItem>
            {
                Data = newSeries.GenerateDataPoints(newSeries.Items),
                Name = newSeries.Name,
                Group = newSeries.Group,
                ApexSeries = newSeries
            };

            Options.Series.Add(series);

            var jsonSeries = Serialize(series);
            await InvokeVoidJsAsync("blazor_apexchart.appendSeries", Options.Chart.Id, jsonSeries, animate, overwriteInitialSeries);
        }

        /// <summary>
        /// This method allows you to select/deselect a data-point of a particular series.
        /// </summary>
        /// <param name="seriesIndex">Index of the series array. If you are rendering a pie/donut/radialBar chart, this acts as dataPointIndex and is the only thing you have to provide as pie/donut/radialBar don't support multi-series chart.</param>
        /// <param name="dataPointIndex">Index of the data-point in the series selected in previous argument. Not required in pie/donut/radialBar</param>
        /// <remarks>
        /// Links:
        /// 
        /// <see href="https://apexcharts.github.io/Blazor-ApexCharts/methods/toggle-data-point">Blazor Example</see>,
        /// <see href="https://apexcharts.com/docs/methods/#toggleDataPointSelection">JavaScript Documentation</see>
        /// </remarks>
        public virtual async Task ToggleDataPointSelectionAsync(int seriesIndex, int? dataPointIndex)
        {
            await InvokeVoidJsAsync("blazor_apexchart.toggleDataPointSelection", Options.Chart.Id, seriesIndex, dataPointIndex);
        }

        /// <inheritdoc cref="IApexSeries{TItem}.Toggle"/>
        /// <param name="seriesName">The series name which you want to toggle visibility for.</param>
        public virtual async Task ToggleSeriesAsync(string seriesName)
        {
            await InvokeVoidJsAsync("blazor_apexchart.toggleSeries", Options.Chart.Id, seriesName);
        }

        /// <inheritdoc cref="IApexSeries{TItem}.Show"/>
        /// <param name="seriesName">The series name which you want to show.</param>
        public virtual async Task ShowSeriesAsync(string seriesName)
        {
            await InvokeVoidJsAsync("blazor_apexchart.showSeries", Options.Chart.Id, seriesName);
        }

        /// <inheritdoc cref="IApexSeries{TItem}.Hide"/>
        /// <param name="seriesName">The series name which you want to hide.</param>
        public virtual async Task HideSeriesAsync(string seriesName)
        {
            await InvokeVoidJsAsync("blazor_apexchart.hideSeries", Options.Chart.Id, seriesName);
        }

        private void SetCustomIcons()
        {
            var customIcons = Options?.Chart?.Toolbar?.Tools?.CustomIcons?.Where(e => e.OnClick != null);
            if (customIcons == null) { return; }

            foreach (var customIcon in customIcons)
            {
                var script = @"function (chart, options, e) {
                                options.config.dotNetObject.invokeMethodAsync('JSCustomIconClick', '" + customIcon.Id.ToString("N") + @"');
                                }";
                customIcon.Click = script;
            }
        }

        private void PrepareChart()
        {
            CheckChart();
            SetSeries();
            SetSeriesColors();
            SetSeriesStroke();
            SetDataLabels();
            SetCustomIcons();
            FixLineDataSelection();
            UpdateDataForNoAxisCharts();
            SetDotNetFormatters();
            SetCustomTooltip();
            SetAnnotationLabelEvents();
            SetAnnotationPointEvents();


        }

        private void CheckChart()
        {
            var jsInProcess = jsRuntime is IJSInProcessRuntime;
            if (OnBeforeZoom != null && !jsInProcess)
            {
                throw new NotSupportedException("Event 'OnBeforeZoom' is not suported in blazor server");
            }

            if (OnBeforeResetZoom != null && !jsInProcess)
            {
                throw new NotSupportedException("Event 'OnBeforeResetZoom' is not suported in blazor server");
            }
        }

        private void SetAnnotationPointEvents()
        {
            if (!OnAnnotationPointClick.HasDelegate && !OnAnnotationPointMouseEnter.HasDelegate && !OnAnnotationPointMouseLeave.HasDelegate)
            {
                return;
            }

            var annotations = Options?.Annotations?.Points;
            if (annotations == null || !annotations.Any()) { return; }

            foreach (var annotation in annotations)
            {
                SetAnnotationPointEvent(annotation);
            }
        }

        private void SetAnnotationLabelEvents()
        {
            if (!OnAnnotationLabelClick.HasDelegate && !OnAnnotationLabelMouseEnter.HasDelegate && !OnAnnotationLabelMouseLeave.HasDelegate)
            {
                return;
            }

            var annotations = Options?.Annotations?.GetAllAnnotations();
            if (annotations == null || !annotations.Any()) { return; }

            foreach (var annotation in annotations)
            {
                SetAnnotationLabelEvent(annotation);
            }
        }

        private void SetAnnotationPointEvent(AnnotationsPoint annotation)
        {

            if (string.IsNullOrWhiteSpace(annotation.Id))
            {
                annotation.Id = Guid.NewGuid().ToString();
            }

            if (OnAnnotationLabelClick.HasDelegate)
            {
                annotation.SetEventFunction(AnnotationEventType.Click);
            }

            if (OnAnnotationLabelMouseEnter.HasDelegate)
            {
                annotation.SetEventFunction(AnnotationEventType.MouseEnter);
            }

            if (OnAnnotationLabelMouseLeave.HasDelegate)
            {
                annotation.SetEventFunction(AnnotationEventType.MouseLeave);
            }
        }

        private void SetAnnotationLabelEvent(IAnnotation annotation)
        {

            if (string.IsNullOrWhiteSpace(annotation.Id))
            {
                annotation.Id = Guid.NewGuid().ToString();
            }

            if (OnAnnotationLabelClick.HasDelegate)
            {
                annotation.Label.SetEventFunction(AnnotationEventType.Click);
            }

            if (OnAnnotationLabelMouseEnter.HasDelegate)
            {
                annotation.Label.SetEventFunction(AnnotationEventType.MouseEnter);
            }

            if (OnAnnotationLabelMouseLeave.HasDelegate)
            {
                annotation.Label.SetEventFunction(AnnotationEventType.MouseLeave);
            }
        }


        private void SetCustomTooltip()
        {
            if (ApexPointTooltip == null) { return; }
            if (Options.Tooltip == null) { Options.Tooltip = new Tooltip(); }
            Options.Tooltip.CustomTooltip = true;
        }

        private async Task RenderChartAsync()
        {
            if (!apexSeries.Any())
            {
                return;
            }

            await Task.Yield();
            forceRender = false;
            PrepareChart();

            await InvokeVoidJsAsync("blazor_apexchart.renderChart", JSHandler.ObjectReference, ChartContainer, JsonSerializer.Serialize(Options, ChartSerializer.GetOptions<TItem>()), new JSEvents()
            {
                HasDataPointSelection = OnDataPointSelection.HasDelegate,
                HasDataPointEnter = OnDataPointEnter.HasDelegate || ApexPointTooltip != null,
                HasDataPointLeave = OnDataPointLeave.HasDelegate,
                HasLegendClick = OnLegendClicked.HasDelegate,
                HasMarkerClick = OnMarkerClick.HasDelegate,
                HasXAxisLabelClick = OnXAxisLabelClick.HasDelegate,
                HasSelection = OnSelection.HasDelegate,
                HasBrushScrolled = OnBrushScrolled.HasDelegate,
                HasZoomed = OnZoomed.HasDelegate,
                HasAnimationEnd = OnAnimationEnd.HasDelegate,
                HasBeforeMount = OnBeforeMount.HasDelegate,
                HasMounted = OnMounted.HasDelegate,
                HasUpdated = OnUpdated.HasDelegate,
                HasMouseLeave = OnMouseLeave.HasDelegate,
                HasMouseMove = OnMouseMove.HasDelegate,
                HasClick = OnClick.HasDelegate,
                HasBeforeZoom = OnBeforeZoom != null,
                HasBeforeResetZoom = OnBeforeResetZoom != null,
                HasScrolled = OnScrolled.HasDelegate
            });

            await OnRendered.InvokeAsync();
        }

        private void SetDotNetFormatters()
        {
            if (FormatYAxisLabel != null)
            {
                if (Options.Yaxis == null) { Options.Yaxis = new List<YAxis>(); }
                if (!Options.Yaxis.Any()) { Options.Yaxis.Add(new YAxis()); }

                var yAxis = Options.Yaxis.First();

                yAxis.Labels ??= new YAxisLabels();

                yAxis.Labels.Formatter = @"function (value, index, w) {
                                          return window.blazor_apexchart.getYAxisLabel(value, index, w);
                                         }";
            }

            if (FormatXAxisLabel != null)
            {
                if (Options.Xaxis == null) { Options.Xaxis = new XAxis(); }

                var xAxis = Options.Xaxis;

                xAxis.Labels ??= new XAxisLabels();

                xAxis.Labels.Formatter = @"function (value, index, w) {
                                          return window.blazor_apexchart.getXAxisLabel(value, index, w);
                                         }";
            }
        }

        private void SetSeries()
        {
            Options.Series = new List<Series<TItem>>();
            var isMixed = apexSeries.Select(e => e.GetChartType()).Distinct().Count() > 1;

            foreach (var apxSeries in apexSeries)
            {
                var series = new Series<TItem>
                {
                    Data = apxSeries.GenerateDataPoints(apxSeries.Items),
                    Name = apxSeries.Name,
                    Group = apxSeries.Group,
                    ApexSeries = apxSeries
                };

                Options.Series.Add(series);

                var seriesChartType = apxSeries.GetChartType();

                if (!isMixed)
                {
                    Options.Chart.Type = seriesChartType;
                }
                else
                {
                    series.Type = GetMixedChartType(seriesChartType);
                }
            }
        }

        private MixedType GetMixedChartType(ChartType chartType)
        {
            switch (chartType)
            {
                case ChartType.Line:
                    return MixedType.Line;
                case ChartType.Scatter:
                    return MixedType.Scatter;
                case ChartType.Area:
                    return MixedType.Area;
                case ChartType.Bubble:
                    return MixedType.Bubble;
                case ChartType.Candlestick:
                    return MixedType.Candlestick;
                case ChartType.BoxPlot:
                    return MixedType.BoxPlot;
                case ChartType.RangeArea:
                    return MixedType.RangeArea;
                case ChartType.Bar:
                    if (Options?.PlotOptions?.Bar?.Horizontal == true)
                    {
                        return MixedType.Bar;
                    }
                    else
                    {
                        return MixedType.Column;
                    }
                default:
                    throw new Exception($"Chart Type {chartType} can not be mixed");
            }
        }

        /// <inheritdoc/>
        public virtual void Dispose()
        {
            GC.SuppressFinalize(this);

            if (Options.Chart?.Id != null && isReady && blazor_apexchart != null)
            {
                try
                {
                    InvokeAsync(async () => { await InvokeVoidJsAsync("blazor_apexchart.destroyChart", Options.Chart.Id); });
                    InvokeAsync(async () => { await blazor_apexchart.DisposeAsync(); });
                }
                catch (Exception ex) when (ex is ObjectDisposedException || ex is JSDisconnectedException)
                { }


            }
            JSHandler?.Dispose();
        }

        internal void UpdateTooltipData(HoverData<TItem> tooltipData)
        {
            this.tooltipData = tooltipData;
            StateHasChanged();
        }
    }
}
