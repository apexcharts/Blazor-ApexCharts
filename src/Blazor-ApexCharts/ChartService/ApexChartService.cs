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
    private readonly IJSRuntime jSRuntime;

    public List<IApexChart> Charts => charts.Values.ToList();

    public async Task<IJSObjectReference> LoadJavascriptModuleAsync(string path = null)
    {
        return await JSLoader.LoadAsync(jSRuntime, path);
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
