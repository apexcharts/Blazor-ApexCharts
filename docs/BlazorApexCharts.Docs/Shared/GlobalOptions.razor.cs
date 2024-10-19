using ApexCharts;
using BlazorApexCharts.Docs.Components.ChartService;
using Microsoft.AspNetCore.Components;
using Microsoft.VisualBasic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApexCharts.Docs.Shared;

public partial class GlobalOptions
{
    [Inject] public IApexChartService ChartService { get; set; }

    private IApexChartBaseOptions globalOptions;
    private LocaleResource selectedLocale;

    protected override void OnInitialized()
    {
        globalOptions = ChartService.GlobalOptions;
        globalOptions.Theme ??= new Theme { Mode = Mode.Light };

        selectedLocale = ChartService.LocaleResources.FirstOrDefault(e => e.Name == globalOptions.Chart?.DefaultLocale);
    }

    private async Task SetLocaleAsync()
    {
        await ChartService.SetLocaleAsync(selectedLocale, true);
    }

    private async Task SetDebugAsync(bool debug)
    {
        globalOptions.Debug = debug;
        await UpdateOptionsAsync();
    }

    private async Task UpdateOptionsAsync()
    {
        await ChartService.SetGlobalOptionsAsync(globalOptions, true);
    }
}