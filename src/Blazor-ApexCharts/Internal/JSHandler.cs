using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace ApexCharts.Internal
{
    /// <summary>
    /// Provides internal-only <see cref="JSInvokableAttribute"/> callbacks
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    internal sealed class JSHandler<TItem> : IDisposable where TItem : class
    {
        private readonly ApexChart<TItem> ChartReference;
        private readonly IJSRuntime JSRuntime;
        private readonly DotNetObjectReference<JSHandler<TItem>> ObjectReference;
        private readonly ElementReference ChartContainer;

        internal JSHandler(ApexChart<TItem> chartReference, ElementReference chartContainer, IJSRuntime jSRuntime)
        {
            ObjectReference = DotNetObjectReference.Create(this);
            ChartContainer = chartContainer;
            ChartReference = chartReference;
            JSRuntime = jSRuntime;
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            ObjectReference.Dispose();
        }
        
        /// <summary>
        /// Invokes the JS renderChart method
        /// </summary>
        public async Task RenderChartAsync()
        {
            await JSRuntime.InvokeVoidAsync("blazor_apexchart.renderChart", ObjectReference, ChartContainer, JsonSerializer.Serialize(ChartReference.Options, ChartSerializer.GetOptions<TItem>()), new JSEvents()
            {
                HasDataPointSelection = ChartReference.OnDataPointSelection.HasDelegate,
                HasDataPointEnter = ChartReference.OnDataPointEnter.HasDelegate || ChartReference.ApexPointTooltip != null,
                HasDataPointLeave = ChartReference.OnDataPointLeave.HasDelegate,
                HasLegendClick = ChartReference.OnLegendClicked.HasDelegate,
                HasMarkerClick = ChartReference.OnMarkerClick.HasDelegate,
                HasXAxisLabelClick = ChartReference.OnXAxisLabelClick.HasDelegate,
                HasSelection = ChartReference.OnSelection.HasDelegate,
                HasBrushScrolled = ChartReference.OnBrushScrolled.HasDelegate,
                HasZoomed = ChartReference.OnZoomed.HasDelegate
            });
        }

        /// <summary>
        /// Callback from JavaScript to get a formatted Y-axis value
        /// </summary>
        /// <param name="value">Details from JavaScript</param>
        /// <remarks>
        /// Will execute <see cref="ApexChart{TItem}.FormatYAxisLabel"/>
        /// </remarks>
        [JSInvokable]
        public string JSGetFormattedYAxisValue(object value)
        {
            if (value == null) { return ""; }
            if (ChartReference.FormatYAxisLabel == null) { return value.ToString(); }

            if (decimal.TryParse(value.ToString(), out var decimalValue))
            {
                return ChartReference.FormatYAxisLabel.Invoke(decimalValue);
            }

            return value.ToString();
        }

        /// <summary>
        /// Callback from JavaScript on zoom
        /// </summary>
        /// <param name="jSZoomed">Details from JavaScript</param>
        /// <remarks>
        /// Will execute <see cref="ApexChart{TItem}.OnZoomed"/>
        /// </remarks>
        [JSInvokable]
        public void JSZoomed(JSZoomed jSZoomed)
        {
            if (ChartReference.OnZoomed.HasDelegate)
            {
                ChartReference.OnZoomed.InvokeAsync(new ZoomedData<TItem>
                {
                    Chart = ChartReference,
                    YAxis = jSZoomed.YAxis,
                    XAxis = jSZoomed.XAxis
                });
            }
        }

        /// <summary>
        /// Callback from JavaScript on selection updated
        /// </summary>
        /// <param name="jsSelection">Details from JavaScript</param>
        /// <remarks>
        /// Will execute <see cref="ApexChart{TItem}.OnBrushScrolled"/>
        /// </remarks>
        [JSInvokable]
        public void JSBrushScrolled(JSSelection jsSelection)
        {
            if (ChartReference.OnBrushScrolled.HasDelegate)
            {
                ChartReference.OnBrushScrolled.InvokeAsync(new SelectionData<TItem>
                {
                    Chart = ChartReference,
                    YAxis = jsSelection.YAxis,
                    XAxis = jsSelection.XAxis
                });
            }
        }

        /// <summary>
        /// Callback from JavaScript on selection updated
        /// </summary>
        /// <param name="jsSelection">Details from JavaScript</param>
        /// <remarks>
        /// Will execute <see cref="ApexChart{TItem}.OnSelection"/>
        /// </remarks>
        [JSInvokable]
        public void JSSelected(JSSelection jsSelection)
        {
            if (ChartReference.OnSelection.HasDelegate)
            {
                ChartReference.OnSelection.InvokeAsync(new SelectionData<TItem>
                {
                    Chart = ChartReference,
                    YAxis = jsSelection.YAxis,
                    XAxis = jsSelection.XAxis
                });
            }
        }

        /// <summary>
        /// Callback from JavaScript on a legend item clicked
        /// </summary>
        /// <param name="jsLegendClicked">Details from JavaScript</param>
        /// <remarks>
        /// Will execute <see cref="ApexChart{TItem}.OnLegendClicked"/>
        /// </remarks>
        [JSInvokable]
        public void JSLegendClicked(JSLegendClicked jsLegendClicked)
        {
            var series = ChartReference.Options.Series.ElementAt(jsLegendClicked.SeriesIndex);
            var legendClicked = new LegendClicked<TItem>
            {
                Series = series,
                Collapsed = jsLegendClicked.Collapsed
            };

            //Invert if Toggle series is set to flase (default == true)
            var toggleSeries = ChartReference.Options?.Legend?.OnItemClick?.ToggleDataSeries;

            if (toggleSeries != false)
            {
                legendClicked.Collapsed = !legendClicked.Collapsed;
            }

            ChartReference.OnLegendClicked.InvokeAsync(legendClicked);
        }

        /// <summary>
        /// Callback from JavaScript on an X-axis label clicked
        /// </summary>
        /// <param name="jsXAxisLabelClick">Details from JavaScript</param>
        /// <remarks>
        /// Will execute <see cref="ApexChart{TItem}.OnXAxisLabelClick"/>
        /// </remarks>
        [JSInvokable]
        public void JSXAxisLabelClick(JSXAxisLabelClick jsXAxisLabelClick)
        {
            if (ChartReference.OnXAxisLabelClick.HasDelegate)
            {
                var data = new XAxisLabelClicked<TItem>()
                {
                    LabelIndex = jsXAxisLabelClick.LabelIndex,
                    Caption = jsXAxisLabelClick.Caption,
                    SeriesPoints = new List<SeriesDataPoint<TItem>>()
                };

                foreach (var series in ChartReference.Options.Series)
                {
                    data.SeriesPoints.Add(new SeriesDataPoint<TItem>
                    {
                        Series = series,
                        DataPoint = series.Data.ElementAt(jsXAxisLabelClick.LabelIndex)
                    });
                }

                ChartReference.OnXAxisLabelClick.InvokeAsync(data);
            }
        }

        /// <summary>
        /// Callback from JavaScript on marker clicked
        /// </summary>
        /// <param name="selectedDataPoints">Details from JavaScript</param>
        /// <remarks>
        /// Will execute <see cref="ApexChart{TItem}.OnMarkerClick"/>
        /// </remarks>
        [JSInvokable]
        public void JSMarkerClick(JSDataPointSelection selectedDataPoints)
        {
            if (ChartReference.OnMarkerClick.HasDelegate)
            {
                var series = ChartReference.Options.Series.ElementAt(selectedDataPoints.SeriesIndex);
                var dataPoint = series.Data.ElementAt(selectedDataPoints.DataPointIndex);

                var selection = new SelectedData<TItem>
                {
                    Chart = ChartReference,
                    Series = series,
                    DataPoint = dataPoint,
                    IsSelected = selectedDataPoints.SelectedDataPoints?.Any(e => e != null && e.Any(e => e != null && e.HasValue)) ?? false,
                    DataPointIndex = selectedDataPoints.DataPointIndex,
                    SeriesIndex = selectedDataPoints.SeriesIndex
                };

                ChartReference.OnMarkerClick.InvokeAsync(selection);
            }
        }

        /// <summary>
        /// Callback from JavaScript on data point selected
        /// </summary>
        /// <param name="selectedDataPoints">Details from JavaScript</param>
        /// <remarks>
        /// Will execute <see cref="ApexChart{TItem}.OnDataPointSelection"/>
        /// </remarks>
        [JSInvokable]
        public void JSDataPointSelected(JSDataPointSelection selectedDataPoints)
        {
            if (ChartReference.OnDataPointSelection.HasDelegate)
            {
                var series = ChartReference.Options.Series.ElementAt(selectedDataPoints.SeriesIndex);
                var dataPoint = series.Data.ElementAt(selectedDataPoints.DataPointIndex);

                var selection = new SelectedData<TItem>
                {
                    Chart = ChartReference,
                    Series = series,
                    DataPoint = dataPoint,
                    IsSelected = selectedDataPoints.SelectedDataPoints.Any(e => e != null && e.Any(e => e != null && e.HasValue)),
                    DataPointIndex = selectedDataPoints.DataPointIndex,
                    SeriesIndex = selectedDataPoints.SeriesIndex
                };

                ChartReference.OnDataPointSelection.InvokeAsync(selection);
            }
        }

        /// <summary>
        /// Callback from JavaScript on data point enter
        /// </summary>
        /// <param name="selectedDataPoints">Details from JavaScript</param>
        /// <remarks>
        /// Will execute <see cref="ApexChart{TItem}.OnDataPointEnter"/>
        /// </remarks>
        [JSInvokable]
        public void JSDataPointEnter(JSDataPointSelection selectedDataPoints)
        {
            if (ChartReference.OnDataPointEnter.HasDelegate || ChartReference.ApexPointTooltip != null)
            {
                var series = ChartReference.Options.Series.ElementAt(selectedDataPoints.SeriesIndex);
                var dataPoint = series.Data.ElementAt(selectedDataPoints.DataPointIndex);

                var hoverData = new HoverData<TItem>
                {
                    Chart = ChartReference,
                    Series = series,
                    DataPoint = dataPoint,
                    DataPointIndex = selectedDataPoints.DataPointIndex,
                    SeriesIndex = selectedDataPoints.SeriesIndex
                };

                ChartReference.OnDataPointEnter.InvokeAsync(hoverData);

                if (ChartReference.ApexPointTooltip != null)
                {
                    ChartReference.UpdateTooltipData(hoverData);
                }
            }
        }

        /// <summary>
        /// Callback from JavaScript on data point leave
        /// </summary>
        /// <param name="selectedDataPoints">Details from JavaScript</param>
        /// <remarks>
        /// Will execute <see cref="ApexChart{TItem}.OnDataPointLeave"/>
        /// </remarks>
        [JSInvokable]
        public void JSDataPointLeave(JSDataPointSelection selectedDataPoints)
        {
            if (ChartReference.OnDataPointLeave.HasDelegate)
            {
                var series = ChartReference.Options.Series.ElementAt(selectedDataPoints.SeriesIndex);
                var dataPoint = series.Data.ElementAt(selectedDataPoints.DataPointIndex);

                var hoverData = new HoverData<TItem>
                {
                    Chart = ChartReference,
                    Series = series,
                    DataPoint = dataPoint,
                    DataPointIndex = selectedDataPoints.DataPointIndex,
                    SeriesIndex = selectedDataPoints.SeriesIndex
                };

                ChartReference.OnDataPointLeave.InvokeAsync(hoverData);
            }
        }
    }
}
