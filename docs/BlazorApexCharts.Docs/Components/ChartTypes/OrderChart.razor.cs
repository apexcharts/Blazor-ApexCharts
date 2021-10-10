using ApexCharts;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace BlazorApexCharts.Docs.Components.ChartTypes
{
    public partial class OrderChart
    {
        //[Parameter] public List<Order> Orders { get; set; }
        [Parameter] public ChartType ChartType { get; set; }


        private ApexChartOptions<Order> options = new ApexChartOptions<Order>();
        private List<Order> orders { get; set; } = SampleData.GetOrders();

        private bool IsXYChart()
        {
            return ChartType switch
            {
                ChartType.Pie or ChartType.Donut or ChartType.Treemap or ChartType.RadialBar or ChartType.PolarArea => false,
                _ => true,
            };
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            options.Chart = new Chart
            {
                Sparkline = new ChartSparkline
                {
                    Enabled = ChartType != ChartType.Histogram //; //&& ChartType //!= ChartType.Radar
                }
            };

        }
    }
}
