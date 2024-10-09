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


        private async Task SetGlobalOptions()
        {
            var globalOptions = new ApexChartOptions<string>
            {
                Title = new Title { Text = "Global title"}
            };

            await chartService.SetGlobalOptionsAsync(globalOptions);
            
        }

        private async Task LoadJavascript()
        {
            await chartService.LoadJavascriptModuleAsync();
          
        }

        private async Task TestChart()
        {

            foreach (var chart in chartService.Charts)
            {
                var t = chart.ChartId;

                if (chart.BaseOptions.Theme?.Mode == null)
                {
                    chart.BaseOptions.Theme = new Theme { Mode = Mode.Light };
                }

                if (chart.BaseOptions.Theme.Mode == Mode.Light)
                {
                    chart.BaseOptions.Theme.Mode = Mode.Dark;
                }
                else
                {
                    chart.BaseOptions.Theme.Mode = Mode.Light;
                }
                await chart.UpdateOptionsAsync(true, true, true);


            }
        }


    }
}