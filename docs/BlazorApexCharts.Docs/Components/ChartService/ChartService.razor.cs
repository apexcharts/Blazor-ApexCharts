using ApexCharts;
using BlazorApexCharts.Docs.Components.Features.Export;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BlazorApexCharts.Docs.Components.ChartService
{
    public partial class ChartService
    {
        [Inject] private IApexChartService chartService { get; set; }

        private bool showCharts;



        private async Task ReRenderCharts()
        {
            await chartService.ReRenderChartsAsync();
        }


        private async Task ResetGlobalOptions()
        {
            await chartService.SetGlobalOptionsAsync(null, true);
        }

        private async Task SetGlobalOptions()
        {
       
            var globalOptions = new ApexChartOptions<string>
            {
                Debug = true,
                Title = new Title { Text = "Global title" },
                Theme = new Theme { Mode = Mode.Dark}
            };
           
            await chartService.SetGlobalOptionsAsync(globalOptions, true);
            
        }

        private async Task LoadJavascript()
        {
            await chartService.LoadJavascriptAsync();
        }

        private async Task ToogleTheme()
        {
            foreach (var chart in chartService.Charts)
            {
                if (chart.BaseOptions.Theme?.Mode == null)
                {
                    chart.BaseOptions.Theme = new Theme { Mode = Mode.Light };
                    chart.BaseOptions.Debug = true;
                }

                if (chart.BaseOptions.Theme.Mode == Mode.Light)
                {
                    chart.BaseOptions.Theme.Mode = Mode.Dark;
                }
                else
                {
                    chart.BaseOptions.Theme.Mode = Mode.Light;
                }
                await chart.RenderAsync();
            }
        }
    }
}