using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ApexCharts
{
    /// <summary>
    /// Adds a data series to the enclosing <see cref="Chart"/> component.
    /// </summary>
    public abstract class ApexBaseSeries<TItem> : ComponentBase, IDisposable where TItem : class
    {
        [CascadingParameter(Name = "Chart")] public ApexChart<TItem> Chart { get; set; }
        [Parameter] public string Name { get; set; }
        [Parameter] public Expression<Func<TItem, object>> XValue { get; set; }
        [Parameter] public bool ShowDataLabels { get; set; }
        [Parameter] public IEnumerable<TItem> Items { get; set; }
        [Parameter] public SeriesStroke Stroke { get; set; }

        internal readonly Series<TItem> series = new Series<TItem>();

        protected override void OnParametersSet()
        {
            series.Name = Name;
            series.ShowDataLabels = ShowDataLabels;
            series.Stroke = Stroke;
        }

        protected override void OnInitialized()
        {
            if (Chart.Options.Series == null) { Chart.Options.Series = new List<Series<TItem>>(); }
            Chart.Options.Series.Add(series);
        }

        internal void SetChartType(ChartType chartType)
        {
            if (Chart.Options.Chart == null) { Chart.Options.Chart = new Chart(); }

            if (Chart.Options.Chart.Type == null)
            {
                Chart.Options.Chart.Type = chartType;
            }
            else if (Chart.Options.Chart.Type != chartType)
            {
                Chart.Options.Chart.Type = null;
            }

            SetMixedChartType(chartType);

        }

        private void SetMixedChartType(ChartType chartType)
        {
            //if (Chart.Options?.Chart?.Type == null || Chart.Options?.Chart?.Type == chartType)
            //{
            //    return;
            //}

            if (chartType == ChartType.Line)
            {
                series.Type = MixedType.Line;
                return;
            }

            if (chartType == ChartType.Scatter)
            {
                series.Type = MixedType.Scatter;
                return;
            }

            if (chartType == ChartType.Area)
            {
                series.Type = MixedType.Area;
                return;
            }

            if (chartType == ChartType.Bubble)
            {
                series.Type = MixedType.Bubble;
                return;
            }

            if (chartType == ChartType.Bar)
            {
                if (Chart?.Options?.PlotOptions?.Bar?.Horizontal == true)
                {
                    series.Type = MixedType.Bar;
                }
                else
                {
                    series.Type = MixedType.Column;
                }
            }

        }


        void IDisposable.Dispose()
        {
            if (Chart.Options.Series != null && Chart.Options.Series.Contains(series))
            {
                Chart.Options.Series.Remove(series);
            }
        }
    }
}