using ApexCharts.Series;
using BlazorApexCharts;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
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
        [Inject] private IServiceProvider ServiceProvider { get; set; }
        [Inject] private IJSRuntime JSRuntime { get; set; }

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
        /// <see href="https://apexcharts.com/docs/options/chart/events">JavaScript Documentation</see>
        /// </remarks>
        [Parameter] public EventCallback<XAxisLabelClicked<TItem>> OnXAxisLabelClick { get; set; }

        /// <summary>
        /// Fires when user clicks on the markers.
        /// </summary>
        /// <remarks>
        /// Links:
        /// 
        /// <see href="https://apexcharts.github.io/Blazor-ApexCharts/events/marker-click">Blazor Example</see>,
        /// <see href="https://apexcharts.com/docs/options/chart/events">JavaScript Documentation</see>
        /// </remarks>
        [Parameter] public EventCallback<SelectedData<TItem>> OnMarkerClick { get; set; }

        /// <summary>
        /// Fires when user clicks on a datapoint (bar/column/marker/bubble/donut-slice).
        /// </summary>
        /// <remarks>
        /// Links:
        /// 
        /// <see href="https://apexcharts.github.io/Blazor-ApexCharts/events/data-point-selection">Blazor Example</see>,
        /// <see href="https://apexcharts.com/docs/options/chart/events">JavaScript Documentation</see>
        /// </remarks>
        [Parameter] public EventCallback<SelectedData<TItem>> OnDataPointSelection { get; set; }

        /// <summary>
        /// Fires when user's mouse enter on a datapoint (bar/column/marker/bubble/donut-slice).
        /// </summary>
        /// <remarks>
        /// Links:
        /// 
        /// <see href="https://apexcharts.github.io/Blazor-ApexCharts/events/data-point-hover">Blazor Example</see>,
        /// <see href="https://apexcharts.com/docs/options/chart/events">JavaScript Documentation</see>
        /// </remarks>
        [Parameter] public EventCallback<HoverData<TItem>> OnDataPointEnter { get; set; }

        /// <summary>
        /// MouseLeave event for a datapoint (bar/column/marker/bubble/donut-slice).
        /// </summary>
        /// <remarks>
        /// Links:
        /// 
        /// <see href="https://apexcharts.github.io/Blazor-ApexCharts/events/data-point-hover">Blazor Example</see>,
        /// <see href="https://apexcharts.com/docs/options/chart/events">JavaScript Documentation</see>
        /// </remarks>
        [Parameter] public EventCallback<HoverData<TItem>> OnDataPointLeave { get; set; }

        /// <summary>
        /// Fires when user clicks on legend.
        /// </summary>
        /// <remarks>
        /// Links:
        /// 
        /// <see href="https://apexcharts.github.io/Blazor-ApexCharts/events/legend-click">Blazor Example</see>,
        /// <see href="https://apexcharts.com/docs/options/chart/events">JavaScript Documentation</see>
        /// </remarks>
        [Parameter] public EventCallback<LegendClicked<TItem>> OnLegendClicked { get; set; }

        /// <summary>
        /// Fires when user selects rect using the selection tool.
        /// </summary>
        /// <remarks>
        /// Links:
        /// 
        /// <see href="https://apexcharts.github.io/Blazor-ApexCharts/events/selection">Blazor Example</see>,
        /// <see href="https://apexcharts.com/docs/options/chart/events">JavaScript Documentation</see>
        /// </remarks>
        [Parameter] public EventCallback<SelectionData<TItem>> OnSelection { get; set; }

        /// <summary>
        /// Fires when user drags the brush in a brush chart.
        /// </summary>
        /// <remarks>
        /// Links:
        /// 
        /// <see href="https://apexcharts.github.io/Blazor-ApexCharts/events/brush-scrolled">Blazor Example</see>,
        /// <see href="https://apexcharts.com/docs/options/chart/events">JavaScript Documentation</see>
        /// </remarks>
        [Parameter] public EventCallback<SelectionData<TItem>> OnBrushScrolled { get; set; }

        /// <summary>
        /// Fires when user zooms in/out the chart using either the selection zooming tool or zoom in/out buttons.
        /// </summary>
        /// <remarks>
        /// Links:
        /// 
        /// <see href="https://apexcharts.github.io/Blazor-ApexCharts/events/zoomed">Blazor Example</see>,
        /// <see href="https://apexcharts.com/docs/options/chart/events">JavaScript Documentation</see>
        /// </remarks>
        [Parameter] public EventCallback<ZoomedData<TItem>> OnZoomed { get; set; }

        /// <summary>
        /// Fires when <see cref="RenderAsync"/> completes
        /// </summary>
        [Parameter] public EventCallback OnRendered { get; set; }

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
        /// Enables merging multiple data points into a single item in the chart
        /// </summary>
        /// <remarks>
        /// Links:
        /// 
        /// <see href="https://apexcharts.github.io/Blazor-ApexCharts/features/group-points">Blazor Example</see>
        /// </remarks>
        [Parameter] public GroupPoints GroupPoints { get; set; }

        private ChartSerializer chartSerializer = new();

        /// <summary>
        /// The collection of data series to display on the chart
        /// </summary>
        public List<IApexSeries<TItem>> Series => apexSeries;

        private DotNetObjectReference<ApexChart<TItem>> ObjectReference;
        private ElementReference ChartContainer { get; set; }
        private List<IApexSeries<TItem>> apexSeries = new();
        private bool isReady;
        private bool forceRender = true;
        private string chartId;
        private HoverData<TItem> tooltipData;

        /// <inheritdoc cref="Chart.Id"/>
        public string ChartId => chartId;

        /// <inheritdoc/>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender && isReady == false)
            {
                isReady = true;
                ObjectReference = DotNetObjectReference.Create(this);
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

                if ((Options.Markers.Size == null || Options.Markers.Size <= 0) && (Options.Markers.Sizes == null || !Options.Markers.Sizes.Any()))
                {
                    Options.Markers.Size = 5;
                }

                Options.Tooltip.Intersect = true;
                Options.Tooltip.Shared = false;
            }
        }

        private string Serialize(object data)
        {
            var serializerOptions = chartSerializer.GetOptions<TItem>();
            var json = JsonSerializer.Serialize(data, serializerOptions);
            return json;
        }

#pragma warning disable CS1591 // Documentation not available for obsolete properties
        [Obsolete("Please use RenderAsync(), this method will be removed in future versions")]
        public void SetRerenderChart()
        {
            forceRender = true;
        }
#pragma warning restore CS1591

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
            await JSRuntime.InvokeVoidAsync("blazor_apexchart.addPointAnnotation", Options.Chart.Id, json, pushToMemory);
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

                await JSRuntime.InvokeVoidAsync("blazor_apexchart.appendData", Options.Chart.Id, Serialize(appendData));
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
            await JSRuntime.InvokeVoidAsync("blazor_apexchart.appendData", Options.Chart.Id, json);
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
            await JSRuntime.InvokeVoidAsync("blazor_apexchart.zoomX", Options.Chart.Id, zoomOptions.Start, zoomOptions.End);
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
            await JSRuntime.InvokeVoidAsync("blazor_apexchart.zoomX", Options.Chart.Id, start, end);
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
            await JSRuntime.InvokeVoidAsync("blazor_apexchart.resetSeries", Options.Chart.Id, shouldUpdateChart, shouldResetZoom);
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
            var result = await JSRuntime.InvokeAsync<DataUriResult>("blazor_apexchart.dataUri", Options.Chart.Id, json);
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
            await JSRuntime.InvokeVoidAsync("blazor_apexchart.addXaxisAnnotation", Options.Chart.Id, json, pushToMemory);
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
            await JSRuntime.InvokeVoidAsync("blazor_apexchart.addYaxisAnnotation", Options.Chart.Id, json, pushToMemory);
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
            await JSRuntime.InvokeVoidAsync("blazor_apexchart.clearAnnotations", Options.Chart.Id);
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
            await JSRuntime.InvokeVoidAsync("blazor_apexchart.removeAnnotation", Options.Chart.Id, id);
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
            PrepareChart();
            var json = Serialize(Options);
            await JSRuntime.InvokeVoidAsync("blazor_apexchart.updateOptions", Options.Chart.Id, json, redrawPaths, animate, updateSyncedCharts, zoom);
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
            SetSeries();
            UpdateDataForNoAxisCharts();
            var jsonSeries = Serialize(Options.Series);
            await JSRuntime.InvokeVoidAsync("blazor_apexchart.updateSeries", Options.Chart.Id, jsonSeries, animate);
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
            await JSRuntime.InvokeVoidAsync("blazor_apexchart.toggleDataPointSelection", Options.Chart.Id, seriesIndex, dataPointIndex);
        }

        /// <inheritdoc cref="IApexSeries{TItem}.Toggle"/>
        /// <param name="seriesName">The series name which you want to toggle visibility for.</param>
        public virtual async Task ToggleSeriesAsync(string seriesName)
        {
            await JSRuntime.InvokeVoidAsync("blazor_apexchart.toggleSeries", Options.Chart.Id, seriesName);
        }

        /// <inheritdoc cref="IApexSeries{TItem}.Show"/>
        /// <param name="seriesName">The series name which you want to show.</param>
        public virtual async Task ShowSeriesAsync(string seriesName)
        {
            await JSRuntime.InvokeVoidAsync("blazor_apexchart.showSeries", Options.Chart.Id, seriesName);
        }

        /// <inheritdoc cref="IApexSeries{TItem}.Hide"/>
        /// <param name="seriesName">The series name which you want to hide.</param>
        public virtual async Task HideSeriesAsync(string seriesName)
        {
            await JSRuntime.InvokeVoidAsync("blazor_apexchart.hideSeries", Options.Chart.Id, seriesName);
        }

        private void PrepareChart()
        {
            SetSeries();
            SetSeriesColors();
            SetSeriesStroke();
            SetDataLabels();
            FixLineDataSelection();
            UpdateDataForNoAxisCharts();
            SetDotNetFormatters();
            SetEvents();
            SetCustomTooltip();
        }

        private void SetCustomTooltip()
        {
            if (ApexPointTooltip == null) { return; }
            if (Options.Tooltip == null) { Options.Tooltip = new Tooltip(); }

            var customTooltip = @"function({series, seriesIndex, dataPointIndex, w}) {
                                var sourceId = 'apex-tooltip-' + w.globals.chartID;
                                var source = document.getElementById(sourceId);
                                if (source) {
                                return source.innerHTML;
                                }
                                return '...'
                                }";

            Options.Tooltip.Custom = customTooltip;
        }

        private void SetEvents()
        {
            Options.HasDataPointSelection = OnDataPointSelection.HasDelegate;
            Options.HasDataPointEnter = OnDataPointEnter.HasDelegate || ApexPointTooltip != null;
            Options.HasDataPointLeave = OnDataPointLeave.HasDelegate;
            Options.HasLegendClick = OnLegendClicked.HasDelegate;
            Options.HasMarkerClick = OnMarkerClick.HasDelegate;
            Options.HasXAxisLabelClick = OnXAxisLabelClick.HasDelegate;
            Options.HasSelection = OnSelection.HasDelegate;
            Options.HasBrushScrolled = OnBrushScrolled.HasDelegate;
            Options.HasZoomed = OnZoomed.HasDelegate;
        }

        private async Task RenderChartAsync()
        {
            forceRender = false;
            PrepareChart();
            var jsonOptions = Serialize(Options);
            await JSRuntime.InvokeVoidAsync("blazor_apexchart.renderChart", ObjectReference, ChartContainer, jsonOptions);
            await OnRendered.InvokeAsync();
        }

        private void SetDotNetFormatters()
        {
            if (FormatYAxisLabel != null)
            {
                if (Options.Yaxis == null) { Options.Yaxis = new List<YAxis>(); }
                if (!Options.Yaxis.Any()) { Options.Yaxis.Add(new YAxis()); }

                var yAxis = Options.Yaxis.First();

                if (yAxis.Labels == null) { yAxis.Labels = new YAxisLabels(); }

                yAxis.Labels.Formatter = @"function (value, index, w) {
                                          return window.blazor_apexchart.getYAxisLabel(value, index, w);
                                         }";
            }
        }

        private void SetSeries()
        {
            Options.Series = new List<Series<TItem>>();
            var isMixed = apexSeries.Select(e => e.GetChartType()).Distinct().Count() > 1;

            if (apexSeries == null || !apexSeries.Any()) { throw new Exception($"Chart {chartId} must have at least one series"); };

            foreach (var apxSeries in apexSeries)
            {
                var series = new Series<TItem>
                {
                    Data = apxSeries.GenerateDataPoints(apxSeries.Items),
                    Name = apxSeries.Name,
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

            if (Options.Chart?.Id != null && isReady)
            {
                InvokeAsync(async () => { await JSRuntime.InvokeVoidAsync("blazor_apexchart.destroyChart", Options.Chart.Id); });
            }

            if (ObjectReference != null)
            {
                ObjectReference.Dispose();
            }
        }

        /// <summary>
        /// Callback from JavaScript to get a formatted Y-axis value
        /// </summary>
        /// <param name="value">Details from JavaScript</param>
        /// <remarks>
        /// Will execute <see cref="FormatYAxisLabel"/>
        /// </remarks>
        [JSInvokable("JSGetFormattedYAxisValue")]
        public string JSGetFormattedYAxisValue(object value)
        {
            if (value == null) { return ""; }
            if (FormatYAxisLabel == null) { return value.ToString(); }

            if (decimal.TryParse(value.ToString(), out var decimalValue))
            {
                return FormatYAxisLabel.Invoke(decimalValue);
            }

            return value.ToString();
        }

        /// <summary>
        /// Callback from JavaScript on zoom
        /// </summary>
        /// <param name="jSZoomed">Details from JavaScript</param>
        /// <remarks>
        /// Will execute <see cref="OnZoomed"/>
        /// </remarks>
        [JSInvokable("JSZoomed")]
        public void JSZoomed(JSZoomed jSZoomed)
        {
            var data = new ZoomedData<TItem>
            {
                Chart = this,
                YAxis = jSZoomed.YAxis,
                XAxis = jSZoomed.XAxis
            };

            OnZoomed.InvokeAsync(data);
        }

        /// <summary>
        /// Callback from JavaScript on selection updated
        /// </summary>
        /// <param name="jsSelection">Details from JavaScript</param>
        /// <remarks>
        /// Will execute <see cref="OnBrushScrolled"/>
        /// </remarks>
        [JSInvokable("JSBrushScrolled")]
        public void JSBrushScrolled(JSSelection jsSelection)
        {
            var selectionData = new SelectionData<TItem>
            {
                Chart = this,
                YAxis = jsSelection.YAxis,
                XAxis = jsSelection.XAxis
            };

            OnBrushScrolled.InvokeAsync(selectionData);
        }

        /// <summary>
        /// Callback from JavaScript on selection updated
        /// </summary>
        /// <param name="jsSelection">Details from JavaScript</param>
        /// <remarks>
        /// Will execute <see cref="OnSelection"/>
        /// </remarks>
        [JSInvokable("JSSelected")]
        public void JSSelected(JSSelection jsSelection)
        {
            var selectionData = new SelectionData<TItem>
            {
                Chart = this,
                YAxis = jsSelection.YAxis,
                XAxis = jsSelection.XAxis
            };

            OnSelection.InvokeAsync(selectionData);
        }

        /// <summary>
        /// Callback from JavaScript on a legend item clicked
        /// </summary>
        /// <param name="jsLegendClicked">Details from JavaScript</param>
        /// <remarks>
        /// Will execute <see cref="OnLegendClicked"/>
        /// </remarks>
        [JSInvokable("JSLegendClicked")]
        public void JSLegendClicked(JSLegendClicked jsLegendClicked)
        {
            var series = Options.Series.ElementAt(jsLegendClicked.SeriesIndex);
            var legendClicked = new LegendClicked<TItem>
            {
                Series = series,
                Collapsed = jsLegendClicked.Collapsed
            };

            //Invert if Toggle series is set to flase (default == true)
            var toggleSeries = Options?.Legend?.OnItemClick?.ToggleDataSeries;

            if (toggleSeries != false)
            {
                legendClicked.Collapsed = !legendClicked.Collapsed;
            }

            OnLegendClicked.InvokeAsync(legendClicked);
        }

        /// <summary>
        /// Callback from JavaScript on an X-axis label clicked
        /// </summary>
        /// <param name="jsXAxisLabelClick">Details from JavaScript</param>
        /// <remarks>
        /// Will execute <see cref="OnXAxisLabelClick"/>
        /// </remarks>
        [JSInvokable("JSXAxisLabelClick")]
        public void JSXAxisLabelClick(JSXAxisLabelClick jsXAxisLabelClick)
        {
            if (OnXAxisLabelClick.HasDelegate)
            {
                var data = new XAxisLabelClicked<TItem>();
                data.LabelIndex = jsXAxisLabelClick.LabelIndex;
                data.Caption = jsXAxisLabelClick.Caption;
                data.SeriesPoints = new List<SeriesDataPoint<TItem>>();

                foreach (var series in Options.Series)
                {
                    data.SeriesPoints.Add(new SeriesDataPoint<TItem>
                    {
                        Series = series,
                        DataPoint = series.Data.ElementAt(jsXAxisLabelClick.LabelIndex)
                    });
                }

                OnXAxisLabelClick.InvokeAsync(data);
            }
        }

        /// <summary>
        /// Callback from JavaScript on marker clicked
        /// </summary>
        /// <param name="selectedDataPoints">Details from JavaScript</param>
        /// <remarks>
        /// Will execute <see cref="OnMarkerClick"/>
        /// </remarks>
        [JSInvokable("JSMarkerClick")]
        public void JSMarkerClick(JSDataPointSelection selectedDataPoints)
        {
            if (OnMarkerClick.HasDelegate)
            {
                var series = Options.Series.ElementAt(selectedDataPoints.SeriesIndex);
                var dataPoint = series.Data.ElementAt(selectedDataPoints.DataPointIndex);

                var selection = new SelectedData<TItem>
                {
                    Chart = this,
                    Series = series,
                    DataPoint = dataPoint,
                    IsSelected = selectedDataPoints.SelectedDataPoints?.Any(e => e != null && e.Any(e => e != null && e.HasValue)) ?? false,
                    DataPointIndex = selectedDataPoints.DataPointIndex,
                    SeriesIndex = selectedDataPoints.SeriesIndex
                };

                OnMarkerClick.InvokeAsync(selection);
            }
        }

        /// <summary>
        /// Callback from JavaScript on data point selected
        /// </summary>
        /// <param name="selectedDataPoints">Details from JavaScript</param>
        /// <remarks>
        /// Will execute <see cref="OnDataPointSelection"/>
        /// </remarks>
        [JSInvokable("JSDataPointSelected")]
        public void JSDataPointSelected(JSDataPointSelection selectedDataPoints)
        {
            if (OnDataPointSelection.HasDelegate)
            {
                var series = Options.Series.ElementAt(selectedDataPoints.SeriesIndex);
                var dataPoint = series.Data.ElementAt(selectedDataPoints.DataPointIndex);

                var selection = new SelectedData<TItem>
                {
                    Chart = this,
                    Series = series,
                    DataPoint = dataPoint,
                    IsSelected = selectedDataPoints.SelectedDataPoints.Any(e => e != null && e.Any(e => e != null && e.HasValue)),
                    DataPointIndex = selectedDataPoints.DataPointIndex,
                    SeriesIndex = selectedDataPoints.SeriesIndex
                };

                OnDataPointSelection.InvokeAsync(selection);
            }
        }

        /// <summary>
        /// Callback from JavaScript on data point enter
        /// </summary>
        /// <param name="selectedDataPoints">Details from JavaScript</param>
        /// <remarks>
        /// Will execute <see cref="OnDataPointEnter"/>
        /// </remarks>
        [JSInvokable("JSDataPointEnter")]
        public void JSDataPointEnter(JSDataPointSelection selectedDataPoints)
        {
            if (OnDataPointEnter.HasDelegate || ApexPointTooltip != null)
            {
                var series = Options.Series.ElementAt(selectedDataPoints.SeriesIndex);
                var dataPoint = series.Data.ElementAt(selectedDataPoints.DataPointIndex);

                var hoverData = new HoverData<TItem>
                {
                    Chart = this,
                    Series = series,
                    DataPoint = dataPoint,
                    DataPointIndex = selectedDataPoints.DataPointIndex,
                    SeriesIndex = selectedDataPoints.SeriesIndex
                };

                OnDataPointEnter.InvokeAsync(hoverData);

                if (ApexPointTooltip != null)
                {
                    tooltipData = hoverData;
                    StateHasChanged();
                }
            }
        }

        /// <summary>
        /// Callback from JavaScript on data point leave
        /// </summary>
        /// <param name="selectedDataPoints">Details from JavaScript</param>
        /// <remarks>
        /// Will execute <see cref="OnDataPointLeave"/>
        /// </remarks>
        [JSInvokable("JSDataPointLeave")]
        public void JSDataPointLeave(JSDataPointSelection selectedDataPoints)
        {
            if (OnDataPointLeave.HasDelegate)
            {
                var series = Options.Series.ElementAt(selectedDataPoints.SeriesIndex);
                var dataPoint = series.Data.ElementAt(selectedDataPoints.DataPointIndex);

                var hoverData = new HoverData<TItem>
                {
                    Chart = this,
                    Series = series,
                    DataPoint = dataPoint,
                    DataPointIndex = selectedDataPoints.DataPointIndex,
                    SeriesIndex = selectedDataPoints.SeriesIndex
                };

                OnDataPointLeave.InvokeAsync(hoverData);
            }
        }
    }
}
