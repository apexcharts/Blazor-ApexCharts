using ApexCharts.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ApexCharts
{
    public partial class ApexChart<TItem> : IDisposable where TItem : class
    {
        [Inject] public IJSRuntime JSRuntime { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public ApexChartOptions<TItem> Options { get; set; } = new ApexChartOptions<TItem>();
        [Parameter] public string Title { get; set; }
        [Parameter] public ChartType ChartType { get; set; } = ChartType.Bar;
        [Parameter] public XaxisType? XAxisType { get; set; }
        [Parameter] public object Width { get; set; }
        [Parameter] public object Height { get; set; }
        [Parameter] public EventCallback<SelectedData<TItem>> OnDataPointSelection { get; set; }

        private DotNetObjectReference<ApexChart<TItem>> ObjectReference;
        private ElementReference ChartContainer { get; set; }

        private bool isReady;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                isReady = true;
                ObjectReference = DotNetObjectReference.Create(this);
            }

            if (isReady && Options.ForceRender)
            {
                await UpdateChart();
            }

        }
        protected override void OnParametersSet()
        {
            if (Options.Chart == null) { Options.Chart = new Chart(); }

            if (Options.Chart.Type != ChartType ||
            Options.Chart.Width?.ToString() != Width?.ToString() ||
            Options.Chart.Height?.ToString() != Height?.ToString() ||
            Options.Xaxis?.Type != XAxisType ||
            Options.Title?.Text != Title)
            {
                Options.ForceRender = true;
            }

            Options.Chart.Type = ChartType;
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
            if (Options.Chart.Type != ChartType.Pie &&
                Options.Chart.Type != ChartType.Donut &&
                Options.Chart.Type != ChartType.RadialBar)
            {
                Options.SeriesNonXAxis = null;
                Options.Labels = null;
                return;
            };

            if (Options.Series == null || !Options.Series.Any()) { return; }

            var noAxisSeries = Options.Series.First();

            var data = noAxisSeries.Data.Cast<DataPoint<TItem>>().ToList();
            Options.SeriesNonXAxis = data.Select(e => e.Y).ToList();
            Options.Labels = data.Select(e => e.X.ToString()).ToList();
        }

        public void FixLineDataSelection()
        {
            if ((Options.Chart.Type == ChartType.Line || Options.Chart.Type == ChartType.Area) && OnDataPointSelection.HasDelegate)
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

        private async Task UpdateChart()
        {
            Options.ForceRender = false;
            SetDatalabels();
            FixLineDataSelection();
            UpdateDataForNoAxisCharts();

            var serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                IgnoreNullValues = true,
            };
            serializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
            serializerOptions.Converters.Add(new DataPointConverter<TItem>());

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
