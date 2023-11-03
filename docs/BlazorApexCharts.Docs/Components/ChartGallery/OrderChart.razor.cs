using ApexCharts;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace BlazorApexCharts.Docs.Components.ChartGallery
{
    public partial class OrderChart
    {
        [Parameter] public SeriesType PointType { get; set; }

        private ApexChartOptions<Order> options = new ApexChartOptions<Order>();
        private List<Order> orders;

        private bool IsXYChart()
        {
            return PointType switch
            {
                SeriesType.Pie or SeriesType.Donut or SeriesType.Treemap or SeriesType.RadialBar or SeriesType.RangeArea or SeriesType.PolarArea => false,
                _ => true,
            };
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            orders =  SampleData.GetOrders();

         
                options.Chart = new Chart
                {
                    Sparkline = new ChartSparkline
                    {
                        Enabled = true
                    }
                };
           
         

        }
    }
}
