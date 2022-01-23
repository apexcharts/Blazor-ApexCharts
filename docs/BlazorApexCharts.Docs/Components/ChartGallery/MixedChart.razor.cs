using ApexCharts;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace BlazorApexCharts.Docs.Components.ChartGallery
{
    public partial class MixedChart
    {
       
        private ApexChartOptions<Order> options = new ApexChartOptions<Order>();
        private List<Order> orders { get; set; } = SampleData.GetOrders();

     
        protected override void OnInitialized()
        {
            base.OnInitialized();

            options.Chart = new Chart
            {
                Sparkline = new ChartSparkline { Enabled = true }
            };

        }
    }
}
