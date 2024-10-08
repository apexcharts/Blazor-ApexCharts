using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexCharts;


public class ApexChartService
{

    private Dictionary<string, IApexChart> charts = new();

    public List<IApexChart> Charts => charts.Values.ToList();

    internal void RegisterChart(IApexChart apexChart)
    {
        charts.Add(apexChart.ChartId, apexChart);
    }

    internal void UnRegisterChart(IApexChart apexChart)
    {
        charts.Remove(apexChart.ChartId);
    }

}
