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
    public partial class ApexChart<TItem> : IDisposable where TItem : class
    {
        [Inject] private IServiceProvider ServiceProvider { get; set; }
        [Inject] private IJSRuntime JSRuntime { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public RenderFragment<HoverData<TItem>> ApexPointTooltip { get; set; }
        [Parameter] public ApexChartOptions<TItem> Options { get; set; } = new ApexChartOptions<TItem>();
        [Parameter] public string Title { get; set; }
        [Parameter] public XAxisType? XAxisType { get; set; }
        [Parameter] public bool Debug { get; set; }
        [Parameter] public object Width { get; set; }
        [Parameter] public object Height { get; set; }
        [Parameter] public EventCallback<SelectedData<TItem>> OnDataPointSelection { get; set; }
        [Parameter] public EventCallback<HoverData<TItem>> OnDataPointEnter { get; set; }
        [Parameter] public EventCallback<HoverData<TItem>> OnDataPointLeave { get; set; }
        [Parameter] public EventCallback<LegendClicked<TItem>> OnLegendClicked { get; set; }
        [Parameter] public EventCallback<SelectionData<TItem>> OnSelection { get; set; }
        [Parameter] public EventCallback<SelectionData<TItem>> OnBrushScrolled { get; set; }
        [Parameter] public EventCallback<ZoomedData<TItem>> OnZoomed { get; set; }

        [Parameter] public EventCallback OnRendered { get; set; }
        [Parameter] public Func<decimal, string> FormatYAxisLabel { get; set; }

        private ChartSerializer chartSerializer = new();
        public List<IApexSeries<TItem>> Series => apexSeries;

        private DotNetObjectReference<ApexChart<TItem>> ObjectReference;
        private ElementReference ChartContainer { get; set; }
        private List<IApexSeries<TItem>> apexSeries = new();
        private bool isReady;
        private bool forceRender = true;
        private string chartId;
        private HoverData<TItem> tooltipData;

        public string ChartId => chartId;

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
                strokeWidths.Add(series.ApexSeries.Stroke?.Width ?? 4); // 
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

                if (Options.Markers.Size == null || Options.Markers.Size <= 0)
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

        [Obsolete("Please use RenderAsync(), this method will be removed in future versions")]
        public void SetRerenderChart()
        {
            forceRender = true;
        }

        public virtual async Task RenderAsync()
        {
            await RenderChartAsync();
        }

        public virtual async Task AddPointAnnotationAsync(AnnotationsPoint annotationsPoint, bool pushToMemory)
        {
            var json = Serialize(annotationsPoint);
            await JSRuntime.InvokeVoidAsync("blazor_apexchart.addPointAnnotation", Options.Chart.Id, json, pushToMemory);
        }

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
                //apxSeries
                seriesList.Add(new AppendData<TItem>
                {
                    Data = data
                }); ;
            }

            var json = Serialize(seriesList);
            await JSRuntime.InvokeVoidAsync("blazor_apexchart.appendData", Options.Chart.Id, json);
        }

        public virtual async Task ZoomXAsync(ZoomOptions zoomOptions)
        {
            if(zoomOptions == null) {  throw new ArgumentNullException(nameof(zoomOptions)); }
            await JSRuntime.InvokeVoidAsync("blazor_apexchart.zoomX", Options.Chart.Id, zoomOptions.Start, zoomOptions.End);
        }

        public virtual async Task ZoomXAsync(decimal start, decimal end)
        {
            await JSRuntime.InvokeVoidAsync("blazor_apexchart.zoomX", Options.Chart.Id, start, end);
        }

        public virtual async Task ResetSeriesAsync(bool shouldUpdateChart, bool shouldResetZoom)
        {
            await JSRuntime.InvokeVoidAsync("blazor_apexchart.resetSeries", Options.Chart.Id, shouldUpdateChart, shouldResetZoom);
        }

        public virtual async Task<string> GetDataUriAsync(DataUriOptions dataUriOptions)
        {
            var json = Serialize(dataUriOptions);
            var result = await JSRuntime.InvokeAsync<DataUriResult>("blazor_apexchart.dataUri", Options.Chart.Id, json);
            return result.ImgURI;
        }

        public virtual async Task AddXAxisAnnotationAsync(AnnotationsXAxis annotationsXAxis, bool pushToMemory)
        {
            var json = Serialize(annotationsXAxis);
            await JSRuntime.InvokeVoidAsync("blazor_apexchart.addXaxisAnnotation", Options.Chart.Id, json, pushToMemory);
        }

        public virtual async Task AddYAxisAnnotationAsync(AnnotationsYAxis annotationsYAxis, bool pushToMemory)
        {
            var json = Serialize(annotationsYAxis);
            await JSRuntime.InvokeVoidAsync("blazor_apexchart.addYaxisAnnotation", Options.Chart.Id, json, pushToMemory);
        }

        public virtual async Task ClearAnnotationsAsync()
        {
            await JSRuntime.InvokeVoidAsync("blazor_apexchart.clearAnnotations", Options.Chart.Id);
        }

        public virtual async Task RemoveAnnotationAsync(string id)
        {
            await JSRuntime.InvokeVoidAsync("blazor_apexchart.removeAnnotation", Options.Chart.Id, id);
        }

        public virtual async Task UpdateOptionsAsync(bool redrawPaths, bool animate, bool updateSyncedCharts, ZoomOptions zoom = null)
        {
            PrepareChart();
            var json = Serialize(Options);
            await JSRuntime.InvokeVoidAsync("blazor_apexchart.updateOptions", Options.Chart.Id, json, redrawPaths, animate, updateSyncedCharts, zoom);
        }

        public virtual async Task UpdateSeriesAsync(bool animate = true)
        {
            SetSeries();
            UpdateDataForNoAxisCharts();
            var jsonSeries = Serialize(Options.Series);
            await JSRuntime.InvokeVoidAsync("blazor_apexchart.updateSeries", Options.Chart.Id, jsonSeries, animate);
        }


        /// <summary>
        /// For no axis charts only provide the seriesIndex value, set dataPointIndex to null
        /// </summary>
        /// <param name="seriesIndex"></param>
        /// <param name="dataPointIndex"></param>
        /// <returns></returns>
        public virtual async Task ToggleDataPointSelectionAsync(int seriesIndex, int? dataPointIndex)
        {
            await JSRuntime.InvokeVoidAsync("blazor_apexchart.toggleDataPointSelection", Options.Chart.Id, seriesIndex, dataPointIndex);

        }

        public virtual async Task ToggleSeriesAsync(string seriesName)
        {
            await JSRuntime.InvokeVoidAsync("blazor_apexchart.toggleSeries", Options.Chart.Id, seriesName);
        }

        public virtual async Task ShowSeriesAsync(string seriesName)
        {
            await JSRuntime.InvokeVoidAsync("blazor_apexchart.showSeries", Options.Chart.Id, seriesName);
        }

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
