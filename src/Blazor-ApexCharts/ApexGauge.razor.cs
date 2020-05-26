using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApexCharts
{
    public partial class ApexGauge : IDisposable
    {
        [Parameter] public string Title { get; set; }
        [Parameter] public decimal Precentage { get; set; }
        [Parameter] public string Label { get; set; }
        [Parameter] public ApexChartOptions<GaugeValue> Options { get; set; } = new ApexChartOptions<GaugeValue>();

        private ApexChartOptions<GaugeValue> options;
        
        protected override void OnInitialized()
        {
            options = Options;
        }

        private List<GaugeValue> GetItems()
        {
            return new List<GaugeValue> { new GaugeValue { Label = Label, Precentage = Precentage } };
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
