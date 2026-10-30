using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        internal readonly DotNetObjectReference<JSHandler<TItem>> ObjectReference;

        internal JSHandler(ApexChart<TItem> chartReference)
        {
            ObjectReference = DotNetObjectReference.Create(this);
            ChartReference = chartReference;
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            ObjectReference.Dispose();
        }

        /// <summary>
        /// Callback from JavaScript to get a formatted Y-axis value
        /// </summary>
        /// <param name="value">Details from JavaScript</param>
        /// <remarks>
        /// Will execute <see cref="ApexChart{TItem}.FormatYAxisLabel"/>
        /// </remarks>
        [JSInvokable]
        public string JSGetFormattedYAxisValue(JsonElement value)
        {
            if (value.ValueKind == JsonValueKind.Null) { return ""; }
            if (ChartReference.FormatYAxisLabel == null) { return value.ToString(); }

            if (value.ValueKind == JsonValueKind.Number && value.TryGetDecimal(out var decimalValue))
            {
                return ChartReference.FormatYAxisLabel.Invoke(decimalValue);
            }

            return value.ToString();
        }

        /// <summary>
        /// Callback from JavaScript to get a formatted value for the Y axis at <paramref name="axisIndex"/>
        /// </summary>
        /// <param name="value">Details from JavaScript</param>
        /// <param name="axisIndex">Index of the Y axis in <see cref="ApexChartBaseOptions.Yaxis"/>: the axis the label belongs to, or for a tooltip value the axis its series is plotted on</param>
        /// <remarks>
        /// Will execute <see cref="ApexChart{TItem}.FormatYAxisLabelForAxis"/>
        /// </remarks>
        [JSInvokable]
        public string JSGetFormattedYAxisValueForAxis(JsonElement value, int axisIndex)
        {
            if (ChartReference.FormatYAxisLabelForAxis == null) { return JSGetFormattedYAxisValue(value); }
            if (value.ValueKind == JsonValueKind.Null) { return ""; }

            if (value.ValueKind == JsonValueKind.Number && value.TryGetDecimal(out var decimalValue))
            {
                return ChartReference.FormatYAxisLabelForAxis.Invoke(decimalValue, axisIndex);
            }

            return value.ToString();
        }

        /// <summary>
        /// Callback from JavaScript to get a formatted X-axis value
        /// </summary>
        /// <param name="value">Details from JavaScript</param>
        /// <remarks>
        /// Will execute <see cref="ApexChart{TItem}.FormatXAxisLabel"/>
        /// </remarks>
        [JSInvokable]
        public string JSGetFormattedXAxisValue(JsonElement value)
        {
            if (value.ValueKind == JsonValueKind.Null) { return ""; }
            if (ChartReference.FormatXAxisLabel == null) { return value.ToString(); }

            if (value.ValueKind == JsonValueKind.Number && value.TryGetDecimal(out var decimalValue))
            {
                return ChartReference.FormatXAxisLabel.Invoke(decimalValue);
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
            Series<TItem> series = null;
            IDataPoint<TItem> point = null;
            if (ChartReference.IsNoAxisChart)
            {
                series = ChartReference.Options.Series?.FirstOrDefault();
                point = series?.Data?.ElementAtOrDefault(jsLegendClicked.SeriesIndex);
            }
            else
            {
                series = ChartReference.Options.Series?.ElementAtOrDefault(jsLegendClicked.SeriesIndex);
            }

            if (series == null) { return; }

            var legendClicked = new LegendClicked<TItem>
            {
                Series = series,
                Collapsed = jsLegendClicked.Collapsed,
                DataPoint = point,
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

                foreach (var series in ChartReference.Options.Series ?? [])
                {
                    data.SeriesPoints.Add(new SeriesDataPoint<TItem>
                    {
                        Series = series,
                        DataPoint = series.Data?.ElementAtOrDefault(jsXAxisLabelClick.LabelIndex)
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
                var hoverData = GetHoverData(selectedDataPoints);
                if (hoverData == null) { return; }

                var selection = new SelectedData<TItem>
                {
                    Chart = ChartReference,
                    Series = hoverData.Series,
                    DataPoint = hoverData.DataPoint,
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
                var hoverData = GetHoverData(selectedDataPoints);
                if (hoverData == null) { return; }

                var selection = new SelectedData<TItem>
                {
                    Chart = ChartReference,
                    Series = hoverData.Series,
                    DataPoint = hoverData.DataPoint,
                    IsSelected = selectedDataPoints.SelectedDataPoints?.Any(e => e != null && e.Any(e => e != null && e.HasValue)) ?? false,
                    DataPointIndex = selectedDataPoints.DataPointIndex,
                    SeriesIndex = selectedDataPoints.SeriesIndex,
                    Source = GetSelectionSource(selectedDataPoints.Source)
                };

                ChartReference.OnDataPointSelection.InvokeAsync(selection);
            }
        }

        /// <summary>
        /// Maps the source string sent by blazor-apexcharts.js to <see cref="DataPointSelectionSource"/>
        /// </summary>
        /// <param name="source">"dataPoint", "legend" or "method"</param>
        /// <returns>The matching value, or <see cref="DataPointSelectionSource.DataPoint"/> when the source is missing or unknown</returns>
        internal static DataPointSelectionSource GetSelectionSource(string source)
        {
            return source switch
            {
                "legend" => DataPointSelectionSource.Legend,
                "method" => DataPointSelectionSource.Method,
                _ => DataPointSelectionSource.DataPoint
            };
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
            if (ChartReference.OnDataPointEnter.HasDelegate)
            {
                var hoverData = GetHoverData(selectedDataPoints);
                if (hoverData == null) { return; }

                ChartReference.OnDataPointEnter.InvokeAsync(hoverData);
            }
        }

        private HoverData<TItem> GetHoverData(JSDataPointSelection selectedDataPoints)
        {
            var series = ChartReference.Options.Series?.ElementAtOrDefault(selectedDataPoints.SeriesIndex);
            var dataPoint = series?.Data?.ElementAtOrDefault(selectedDataPoints.DataPointIndex);
            if (series == null || dataPoint == null) { return null; }

            return new HoverData<TItem>
            {
                Chart = ChartReference,
                Series = series,
                DataPoint = dataPoint,
                DataPointIndex = selectedDataPoints.DataPointIndex,
                SeriesIndex = selectedDataPoints.SeriesIndex
            };
        }

        [JSInvokable]
        public void RazorTooltip(JSDataPointSelection selectedDataPoints)
        {
            if (ChartReference.ApexPointTooltip != null)
            {
                var hoverData = GetHoverData(selectedDataPoints);
                if (hoverData == null) { return; }

                ChartReference.UpdateTooltipData(hoverData);
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
                var hoverData = GetHoverData(selectedDataPoints);
                if (hoverData == null) { return; }

                ChartReference.OnDataPointLeave.InvokeAsync(hoverData);
            }
        }

        /// <summary>
        /// Callback from JavaScript on animation end
        /// </summary>
        /// <remarks>
        /// Will execute <see cref="ApexChart{TItem}.OnAnimationEnd"/>
        /// </remarks>
        [JSInvokable]
        public void JSAnimationEnd()
        {
            ChartReference.OnAnimationEnd.InvokeAsync();
        }

        /// <summary>
        /// Callback from JavaScript on custom icon click
        /// </summary>
        /// <remarks>
        /// Will execute <see cref="ToolCustomIcon.OnClick"/>
        /// </remarks>
        [JSInvokable]
        public void JSCustomIconClick(string id)
        {
            if (Guid.TryParse(id, out var iconId))
            {
                var icon = ChartReference.Options?.Chart?.Toolbar?.Tools?.CustomIcons.FirstOrDefault(e => e.Id == iconId);
                icon?.OnClick.Invoke(icon);
            }
        }


        /// <summary>
        /// Callback from JavaScript on before mount
        /// </summary>
        /// <remarks>
        /// Will execute <see cref="ApexChart{TItem}.OnBeforeMount"/>
        /// </remarks>
        [JSInvokable]
        public void JSBeforeMount()
        {
            ChartReference.OnBeforeMount.InvokeAsync();
        }

        /// <summary>
        /// Callback from JavaScript on mounted
        /// </summary>
        /// <remarks>
        /// Will execute <see cref="ApexChart{TItem}.OnMounted"/>
        /// </remarks>
        [JSInvokable]
        public void JSMounted()
        {
            ChartReference.OnMounted.InvokeAsync();
        }

        /// <summary>
        /// Callback from JavaScript when a brush chart that waited for its target charts has rendered
        /// </summary>
        /// <remarks>
        /// Will execute <see cref="ApexChart{TItem}.OnRendered"/>
        /// </remarks>
        [JSInvokable]
        public Task JSRendered()
        {
            return ChartReference.OnRendered.InvokeAsync();
        }

        /// <summary>
        /// Callback from JavaScript on updated
        /// </summary>
        /// <remarks>
        /// Will execute <see cref="ApexChart{TItem}.OnUpdated"/>
        /// </remarks>
        [JSInvokable]
        public void JSUpdated()
        {
            ChartReference.OnUpdated.InvokeAsync();
        }

        /// <summary>
        /// Callback from JavaScript on mouse move
        /// </summary>
        /// <remarks>
        /// Will execute <see cref="ApexChart{TItem}.OnMouseMove"/>
        /// </remarks>
        [JSInvokable]
        public void JSMouseMove(JSDataPointSelection selectedDataPoints)
        {
            var series = ChartReference.Options.Series?.ElementAtOrDefault(selectedDataPoints.SeriesIndex);
            var dataPoint = series?.Data?.ElementAtOrDefault(selectedDataPoints.DataPointIndex);

            ChartReference.OnMouseMove.InvokeAsync(new SelectedData<TItem>
            {
                Chart = ChartReference,
                Series = series,
                DataPoint = dataPoint,
                DataPointIndex = selectedDataPoints.DataPointIndex,
                SeriesIndex = selectedDataPoints.SeriesIndex
            });
        }

        /// <summary>
        /// Callback from JavaScript on mouse leave
        /// </summary>
        /// <remarks>
        /// Will execute <see cref="ApexChart{TItem}.OnMouseLeave"/>
        /// </remarks>
        [JSInvokable]
        public void JSMouseLeave()
        {
            ChartReference.OnMouseLeave.InvokeAsync();
        }

        /// <summary>
        /// Callback from JavaScript on click
        /// </summary>
        /// <remarks>
        /// Will execute <see cref="ApexChart{TItem}.OnClick"/>
        /// </remarks>
        [JSInvokable]
        public void JSClick(JSDataPointSelection selectedDataPoints)
        {
            var series = ChartReference.Options.Series?.ElementAtOrDefault(selectedDataPoints.SeriesIndex);
            var dataPoint = series?.Data?.ElementAtOrDefault(selectedDataPoints.DataPointIndex);

            ChartReference.OnClick.InvokeAsync(new SelectedData<TItem>
            {
                Chart = ChartReference,
                Series = series,
                DataPoint = dataPoint,
                DataPointIndex = selectedDataPoints.DataPointIndex,
                SeriesIndex = selectedDataPoints.SeriesIndex
            });
        }

        /// <summary>
        /// Callback from JavaScript on before zoom
        /// </summary>
        /// <remarks>
        /// Will execute <see cref="ApexChart{TItem}.OnBeforeZoom"/>
        /// </remarks>
        [JSInvokable]
        public SelectionXAxis JSBeforeZoom(JSSelection jsSelection)
        {
            return ChartReference.OnBeforeZoom.Invoke(jsSelection.XAxis);
        }

        /// <summary>
        /// Callback from JavaScript on before reset zoom
        /// </summary>
        /// <remarks>
        /// Will execute <see cref="ApexChart{TItem}.OnBeforeResetZoom"/>
        /// </remarks>
        [JSInvokable]
        public SelectionXAxis JSBeforeResetZoom()
        {
            return ChartReference.OnBeforeResetZoom.Invoke(new object());
        }

        /// <summary>
        /// Callback from JavaScript on scrolled
        /// </summary>
        /// <param name="jsSelection">Details from JavaScript</param>
        /// <remarks>
        /// Will execute <see cref="ApexChart{TItem}.OnScrolled"/>
        /// </remarks>
        [JSInvokable]
        public void JSScrolled(JSSelection jsSelection)
        {
            var selectionData = new SelectionData<TItem>
            {
                Chart = ChartReference,
                XAxis = jsSelection.XAxis
            };

            ChartReference.OnScrolled.InvokeAsync(selectionData);
        }

        /// <summary>
        /// Callback from JavaScript on Annotaion Label Event (Click, MouseLeave, MouseEnter)
        /// </summary>
        /// <param name="jsAnnotationEvent">Details from JavaScript</param>
        [JSInvokable]
        public void JSAnnotationLabelEvent(JSAnnotationEvent jsAnnotationEvent)
        {
            var eventData = new AnnotationEvent<TItem>
            {
                Chart = ChartReference,
                AnnotationId = jsAnnotationEvent.Id,
                Annotation = ChartReference?.Options.Annotations.GetAllAnnotations().FirstOrDefault(e => e.Id == jsAnnotationEvent.Id),
                Target = AnnotationEventTarget.Label
            };

            switch (jsAnnotationEvent.EventType)
            {
                case "Click":
                    eventData.EventType = AnnotationEventType.Click;
                    ChartReference.OnAnnotationLabelClick.InvokeAsync(eventData);
                    return;

                case "MouseLeave":
                    eventData.EventType = AnnotationEventType.MouseLeave;
                    ChartReference.OnAnnotationLabelMouseLeave.InvokeAsync(eventData);
                    return;
                case "MouseEnter":
                    eventData.EventType = AnnotationEventType.MouseEnter;
                    ChartReference.OnAnnotationLabelMouseEnter.InvokeAsync(eventData);
                    return;
            }
        }

        /// <summary>
        /// Callback from JavaScript on Annotaion Label Event (Click, MouseLeave, MouseEnter)
        /// </summary>
        /// <param name="jsAnnotationEvent">Details from JavaScript</param>
        [JSInvokable]
        public void JSAnnotationPointEvent(JSAnnotationEvent jsAnnotationEvent)
        {
            var eventData = new AnnotationEvent<TItem>
            {
                Chart = ChartReference,
                AnnotationId = jsAnnotationEvent.Id,
                Annotation = ChartReference?.Options.Annotations.GetAllAnnotations().FirstOrDefault(e => e.Id == jsAnnotationEvent.Id),
                Target = AnnotationEventTarget.Point
            };

            switch (jsAnnotationEvent.EventType)
            {
                case "Click":
                    eventData.EventType = AnnotationEventType.Click;
                    ChartReference.OnAnnotationPointClick.InvokeAsync(eventData);
                    return;

                case "MouseLeave":
                    eventData.EventType = AnnotationEventType.MouseLeave;
                    ChartReference.OnAnnotationPointMouseLeave.InvokeAsync(eventData);
                    return;
                case "MouseEnter":
                    eventData.EventType = AnnotationEventType.MouseEnter;
                    ChartReference.OnAnnotationPointMouseEnter.InvokeAsync(eventData);
                    return;
            }

        }

        /// <summary>
        /// Callback from JavaScript when an ink annotation is created.
        /// Will execute <see cref="ApexChart{TItem}.OnAnnotationCreated"/>.
        /// </summary>
        [JSInvokable]
        public void JSAnnotationCreated(InkAnnotationEventData data)
            => ChartReference.OnAnnotationCreated.InvokeAsync(data);

        /// <summary>
        /// Callback from JavaScript when an ink annotation is dragged.
        /// Will execute <see cref="ApexChart{TItem}.OnAnnotationDragged"/>.
        /// </summary>
        [JSInvokable]
        public void JSAnnotationDragged(InkAnnotationEventData data)
            => ChartReference.OnAnnotationDragged.InvokeAsync(data);

        /// <summary>
        /// Callback from JavaScript when an ink annotation is edited.
        /// Will execute <see cref="ApexChart{TItem}.OnAnnotationEdited"/>.
        /// </summary>
        [JSInvokable]
        public void JSAnnotationEdited(InkAnnotationEventData data)
            => ChartReference.OnAnnotationEdited.InvokeAsync(data);

        /// <summary>
        /// Callback from JavaScript when an ink annotation is styled.
        /// Will execute <see cref="ApexChart{TItem}.OnAnnotationStyled"/>.
        /// </summary>
        [JSInvokable]
        public void JSAnnotationStyled(InkAnnotationEventData data)
            => ChartReference.OnAnnotationStyled.InvokeAsync(data);

        /// <summary>
        /// Callback from JavaScript when an ink annotation is deleted.
        /// Will execute <see cref="ApexChart{TItem}.OnAnnotationDeleted"/>.
        /// </summary>
        [JSInvokable]
        public void JSAnnotationDeleted(InkAnnotationEventData data)
            => ChartReference.OnAnnotationDeleted.InvokeAsync(data);

        /// <summary>
        /// Callback from JavaScript when the measure ruler completes a measurement.
        /// Will execute <see cref="ApexChart{TItem}.OnMeasured"/>.
        /// </summary>
        [JSInvokable]
        public void JSMeasured(MeasuredData data)
            => ChartReference.OnMeasured.InvokeAsync(data);

        /// <summary>
        /// Callback from JavaScript when the active storyboard beat changes.
        /// Will execute <see cref="ApexChart{TItem}.OnBeatChange"/>.
        /// </summary>
        [JSInvokable]
        public void JSBeatChange(BeatChangeData data)
            => ChartReference.OnBeatChange.InvokeAsync(data);
    }
}
