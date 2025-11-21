using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApexCharts
{
    /// <summary>
    /// Component to create a single-value <see cref="SeriesType.RadialBar"/> chart in Blazor
    /// </summary>
    public partial class ApexGauge : IDisposable
    {
        /// <inheritdoc cref="Title.Text"/>
        [Parameter] public string Title { get; set; }

        /// <inheritdoc cref="GaugeValue.Percentage"/>
        [Parameter] public decimal Percentage { get; set; }

        /// <inheritdoc cref="GaugeValue.Label"/>
        [Parameter] public string Label { get; set; }

        /// <summary>
        /// The options to customize the radial bar chart with
        /// </summary>
        [Parameter] public ApexChartOptions<GaugeValue> Options { get; set; } = new ApexChartOptions<GaugeValue>();

        private ApexChartOptions<GaugeValue> options;

        ApexChart<GaugeValue> GaugeReference;

        /// <inheritdoc/>
        protected override void OnInitialized()
        {
            options = Options;
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
        public Task UpdateValueAsync(bool animate = true) =>
               GaugeReference?.UpdateSeriesAsync(animate);

        private List<GaugeValue> GetItems()
        {
            return new List<GaugeValue> { new GaugeValue { Label = Label, Percentage = Percentage } };
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
