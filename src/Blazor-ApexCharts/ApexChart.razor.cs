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
        [Parameter] public ApexChartOptions Options { get; set; } = new ApexChartOptions();
        [Parameter] public string Title { get; set; }
        [Parameter] public ChartType Type { get; set; } = ChartType.Bar;
        [Parameter] public object Width { get; set; }
        [Parameter] public object Height { get; set; }
        [Parameter] public EventCallback<SelectedData<TItem>> OnDataPointSelection { get; set; }


        private DotNetObjectReference<ApexChart<TItem>> ObjectReference;
        //internal bool ReRender { get; set; } = true;
        private ElementReference ChartContainer { get; set; }

        private bool isReady;

        
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {

            if (firstRender)
            {
                isReady = true;
                ObjectReference = DotNetObjectReference.Create(this);
                await UpdateChart();
            }

           
        }
        protected override async Task OnParametersSetAsync()
        {
            if (Options.Chart == null) { Options.Chart = new Chart(); }

            if (Options.Chart.Type.ToString() != Type.ToString() ||
                Options.Chart.Width.ToString() != Width.ToString() ||
                Options.Chart.Height.ToString() != Height.ToString() ||
                Options.Title?.Text != Title)
            {
                Options.ForceRender = true;
            }

            //if (Options.Chart.Type.ToString() != Type.ToString())
            //{
            //    ReRender = true;
            //}

            Options.Chart.Type = Type;
            Options.Chart.Width = Width;
            Options.Chart.Height = Height;

            if (string.IsNullOrEmpty(Title))
            {
                Options.Title = null;
            }
            else
            {
                if (Options.Title == null) { Options.Title = new Title(); }
                Options.Title.Text = Title;
            }
             
        
            if (isReady && Options.ForceRender)
            {
                await UpdateChart();
            }
        }

        private void SetDatalabels()
        {
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

            Options.SeriesNonXAxis = noAxisSeries.Data.Select(e => e.Y).ToList();
            Options.Labels = noAxisSeries.Data.Select(e => e.X.ToString()).ToList();

        }
        private async Task UpdateChart()
        {
            SetDatalabels();
            UpdateDataForNoAxisCharts();

            var serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                IgnoreNullValues = true,
            };
            serializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));

            var jsonOptions = JsonSerializer.Serialize<ApexChartOptions>(Options, serializerOptions);
            await JSRuntime.InvokeVoidAsync("blazor_apexchart.renderChart", ObjectReference, ChartContainer, jsonOptions);
            await OnDataPointSelection.InvokeAsync(null);
            Options.ForceRender = false;
        }


        public void Dispose()
        {
            GC.SuppressFinalize(this);
            if (Options.Chart?.ChartId != null)
            {
                InvokeAsync(async () => { await JSRuntime.InvokeVoidAsync("blazor_apexchart.destroyChart", Options.Chart.ChartId); });
                
            }

            if (ObjectReference != null)
            {
                ObjectReference.Dispose();
            }
        }

        [JSInvokable]
        public void DataPointSelected(DataPointSelection selectedDataPoints)
        {
            var series = Options.Series.ElementAt(selectedDataPoints.SeriesIndex);
            var dataPoint = series.Data.ElementAt(selectedDataPoints.DataPointIndex);

            var selection = new SelectedData<TItem>();
            selection.Series = series;
            selection.X = dataPoint.X;
            selection.Y = dataPoint.Y;
            selection.Items = dataPoint.Items?.Cast<TItem>().ToList();

            if (OnDataPointSelection.HasDelegate)
            {
                OnDataPointSelection.InvokeAsync(selection);
            }

        }


    }
}
