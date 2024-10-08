using ApexCharts;
using Microsoft.AspNetCore.Components;

namespace BlazorApexCharts.Docs.Components.ChartService
{
    public partial class ChartService
    {
        [Inject] private ApexChartService chartService { get; set; }   
        

        private void TestChart()
        {

            foreach (var chart in chartService.Charts)
            {
                var t = chart.ChartId;
            }
        }


    }
}