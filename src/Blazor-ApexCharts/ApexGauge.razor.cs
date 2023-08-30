using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;

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

        /// <inheritdoc/>
        protected override void OnInitialized()
        {
            options = Options;
        }

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
