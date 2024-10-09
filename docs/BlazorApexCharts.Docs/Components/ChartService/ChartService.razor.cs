using ApexCharts;
using BlazorApexCharts.Docs.Components.Features.Export;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BlazorApexCharts.Docs.Components.ChartService
{
    public partial class ChartService
    {
        [Inject] private ApexChartService chartService { get; set; }

        private bool showCharts;

        private async Task LoadJavascript()
        {
            await chartService.LoadJavascriptModuleAsync();
            showCharts = false;
        }

        private async Task TestChart()
        {

            foreach (var chart in chartService.Charts)
            {
                var t = chart.ChartId;

                if (chart.IOptions.Theme?.Mode == null)
                {
                    chart.IOptions.Theme = new Theme { Mode = Mode.Light };
                }

                if (chart.IOptions.Theme.Mode == Mode.Light)
                {
                    chart.IOptions.Theme.Mode = Mode.Dark;
                }
                else
                {
                    chart.IOptions.Theme.Mode = Mode.Light;
                }
                await chart.UpdateOptionsAsync(true, true, true);


            }
        }


    }
}