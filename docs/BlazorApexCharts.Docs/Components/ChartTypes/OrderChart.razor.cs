using ApexCharts;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace BlazorApexCharts.Docs.Components.ChartTypes
{
    public partial class OrderChart
    {
        [Parameter] public PointType PointType { get; set; }

        private ApexChartOptions<Order> options = new ApexChartOptions<Order>();
        private List<Order> orders { get; set; } = SampleData.GetOrders();

        private bool IsXYChart()
        {
            return PointType switch
            {
                PointType.Pie or PointType.Donut or PointType.Treemap or PointType.RadialBar or PointType.PolarArea => false,
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
                    Enabled = PointType != PointType.Histogram 
                }
            };

        }
    }
}
