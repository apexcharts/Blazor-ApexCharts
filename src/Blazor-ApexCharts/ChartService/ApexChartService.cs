using ApexCharts.Internal;
using Microsoft.JSInterop;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
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

    private Dictionary<string, IApexChart> charts = new();
    private IJSObjectReference jSObjectReference;
    private readonly IJSRuntime jSRuntime;

    public List<IApexChart> Charts => charts.Values.ToList();

    public async Task LoadJavascriptModuleAsync(string path = null)
    {
        jSObjectReference = await JSLoader.LoadAsync(jSRuntime, path);
    }

    public async Task SetGlobalOptionsAsync(IApexChartBaseOptions options)
    {
        if (jSObjectReference == null)
        {
            await LoadJavascriptModuleAsync();
        }

        var json = ChartSerializer.SerializeOptions(options);
        await jSObjectReference.InvokeVoidAsync("blazor_apexchart.setGlobalOptions", json);
    }

    internal void RegisterChart(IApexChart apexChart)
    {
        charts.Add(apexChart.ChartId, apexChart);
    }

    internal void UnRegisterChart(IApexChart apexChart)
    {
        charts.Remove(apexChart.ChartId);
    }

}
