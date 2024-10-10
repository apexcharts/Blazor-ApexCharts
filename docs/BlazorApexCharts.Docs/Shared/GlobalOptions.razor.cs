using ApexCharts;
using BlazorApexCharts.Docs.Components.ChartService;
using Microsoft.AspNetCore.Components;
using Microsoft.VisualBasic;
using System.Threading.Tasks;

namespace BlazorApexCharts.Docs.Shared;

public partial class GlobalOptions
{
    [Inject] public IApexChartService ChartService { get; set; }

    private IApexChartBaseOptions globalOptions;

    protected override void OnInitialized()
    {
        globalOptions = ChartService.GlobalOptions;
        globalOptions.Theme ??= new Theme { Mode = Mode.Light };
    }

    private async Task UpdateOptionsAsync()
    {
        await ChartService.SetGlobalOptionsAsync(globalOptions, true);
    }
}