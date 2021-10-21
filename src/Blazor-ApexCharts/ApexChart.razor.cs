using ApexCharts.Models;
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
        [Inject] public IJSRuntime JSRuntime { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public ApexChartOptions<TItem> Options { get; set; } = new ApexChartOptions<TItem>();
        [Parameter] public string Title { get; set; }

        [Parameter] public XAxisType? XAxisType { get; set; }
        [Parameter] public bool Debug { get; set; }
        [Parameter] public object Width { get; set; }
        [Parameter] public object Height { get; set; }
        [Parameter] public EventCallback<SelectedData<TItem>> OnDataPointSelection { get; set; }

        private DotNetObjectReference<ApexChart<TItem>> ObjectReference;
        private ElementReference ChartContainer { get; set; }
      
        private bool isReady;
        private bool forceRender = true;


        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                isReady = true;
                ObjectReference = DotNetObjectReference.Create(this);
            }

            if (isReady && forceRender)
            {
                await Render();
            }
        }

      
        protected override void OnParametersSet()
        {
            if (Options.Chart == null) { Options.Chart = new Chart(); }

            Options.Debug = Debug;
            Options.Chart.Width = Width;
            Options.Chart.Height = Height;

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

        private bool IsNoAxisChart { 
        get
            {
                return Options?.Chart?.Type == ChartType.Pie ||
               Options?.Chart?.Type == ChartType.Donut ||
               Options?.Chart?.Type == ChartType.PolarArea ||
               Options?.Chart?.Type == ChartType.RadialBar;
            }
        }
      

        //internal DataCategory DataCategory
        //{
        //    get
        //    {
        //        switch (chartType)
        //        {
        //            case ChartType.Candlestick:
        //                return DataCategory.Candle;
        //            case ChartType.RangeBar:
        //                return DataCategory.Range;
        //            case ChartType.BoxPlot:
        //                return DataCategory.BoxPlot;
        //            case ChartType.Bubble:
        //                return DataCategory.XYZ;
        //            case ChartType.Pie:
        //            case ChartType.Donut:
        //            case ChartType.RadialBar:
        //            case ChartType.PolarArea:
        //                return DataCategory.NoAxis;

        //            default:
        //                return DataCategory.Point;
        //        }
        //    }
        //}

        private void SetStroke()
        {
            if (Options?.Series == null) { return; }
            if (Options.Series.All(e => (e.Stroke == null))) { return; }

            if (Options.Stroke == null) { Options.Stroke = new Stroke(); }

            var strokeWidths = new List<int>();
            var strokeColors = new List<string>();
            var strokeDash = new List<int>();
            foreach (var series in Options.Series)
            {
                strokeWidths.Add(series.Stroke?.Width ?? 4); // 
                strokeColors.Add(series.Stroke?.Color ?? "#d3d3d3"); //Default is light gray
                strokeDash.Add(series.Stroke?.DashSpace ?? 0);
            }

            Options.Colors = strokeColors;
            Options.Stroke.Width = strokeWidths;
            Options.Stroke.Colors = strokeColors;
            Options.Stroke.DashArray = strokeDash;
        }

        private void SetDatalabels()
        {
            if (Options?.Series == null) { return; }

            if (Options.DataLabels == null) { Options.DataLabels = new DataLabels(); }
            if (Options.DataLabels.EnabledOnSeries == null) { Options.DataLabels.EnabledOnSeries = new List<double>(); }

            foreach (var series in Options.Series)
            {
                var index = Options.Series.FindIndex(e => e == series);
                if (series.ShowDataLabels)
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

            if (Options.Series.Any(e => e.ShowDataLabels))
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
                Options.SeriesNonXAxis = null;
                Options.Labels = null;
                return;
            };

            if (Options.Series == null || !Options.Series.Any()) { return; }
            var noAxisSeries = Options.Series.First();
            var data = noAxisSeries.Data.Cast<DataPoint<TItem>>().ToList();
            Options.SeriesNonXAxis = data.Select(e => e.Y).Cast<object>().ToList();
            Options.Labels = data.Select(e => e.X?.ToString()).ToList();
        }

        public void FixLineDataSelection()
        {
            if ((Options.Chart.Type == ChartType.Line || Options.Chart.Type == ChartType.Area || Options.Chart.Type == ChartType.Radar) && OnDataPointSelection.HasDelegate)
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

        public void SetForceRender()
        {
            forceRender = true;
        }

        public async Task Render()
        {
            forceRender = false;
            SetStroke();
            SetDatalabels();
            FixLineDataSelection();
            UpdateDataForNoAxisCharts();

            var serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                IgnoreNullValues = true,
            };

            serializerOptions.Converters.Add(new DataPointConverter<TItem>());
            serializerOptions.Converters.Add(new CustomJsonStringEnumConverter());
            var jsonOptions = JsonSerializer.Serialize(Options, serializerOptions);

            await JSRuntime.InvokeVoidAsync("blazor_apexchart.renderChart", ObjectReference, ChartContainer, jsonOptions);
            await OnDataPointSelection.InvokeAsync(null);
        }


        public void Dispose()
        {
            GC.SuppressFinalize(this);
            if (Options.Chart?.ChartId != null && isReady)
            {
                InvokeAsync(async () => { await JSRuntime.InvokeVoidAsync("blazor_apexchart.destroyChart", Options.Chart.ChartId); });
            }

            if (ObjectReference != null)
            {
                ObjectReference.Dispose();
            }
        }

        [JSInvokable]
        public void DataPointSelected(DataPointSelection<TItem> selectedDataPoints)
        {
            if (OnDataPointSelection.HasDelegate)
            {
                var series = Options.Series.ElementAt(selectedDataPoints.SeriesIndex);
                var dataPoint = series.Data.ElementAt(selectedDataPoints.DataPointIndex);

                var selection = new SelectedData<TItem>
                {
                    Series = series,
                    DataPoint = dataPoint
                };

                OnDataPointSelection.InvokeAsync(selection);
            }
        }
    }
}
