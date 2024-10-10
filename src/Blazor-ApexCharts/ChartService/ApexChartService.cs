using ApexCharts.Internal;
using Microsoft.JSInterop;
using Microsoft.JSInterop.Implementation;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexCharts;


public class ApexChartService
{

    public ApexChartService(IJSRuntime jSRuntime)
    {
        this.jSRuntime = jSRuntime;
    }

    private Dictionary<string, IApexChartBase> charts = new();
    //private IJSObjectReference jSObjectReference;
    private readonly IJSRuntime jSRuntime;
    private IApexChartBaseOptions globalOptions = new ApexChartBaseOptions();

    public List<IApexChartBase> Charts => charts.Values.ToList();
    public IApexChartBaseOptions GlobalOptions => globalOptions;

    public async Task LoadJavascriptAsync(string path = null)
    {
         await JSLoader.LoadAsync(jSRuntime, path);
    }

    public async Task ReRenderChartsAsync()
    {

        var list = Charts.ToList().Select(e => e.RenderAsync());
        await Task.WhenAll(list);

    }

    public async Task SetGlobalOptionsAsync(IApexChartBaseOptions options)
    {
        options ??= new ApexChartBaseOptions();

        globalOptions = options;
        var jSObjectReference = await JSLoader.LoadAsync(jSRuntime, options?.Blazor?.JavascriptPath);
        var json = ChartSerializer.SerializeOptions(options);
        await jSObjectReference.InvokeVoidAsync("blazor_apexchart.setGlobalOptions", json);
    }

    internal void RegisterChart(IApexChartBase apexChart)
    {
        charts.Add(apexChart.ChartId, apexChart);
    }

    internal void UnRegisterChart(IApexChartBase apexChart)
    {
        charts.Remove(apexChart.ChartId);
    }

}
