using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexCharts;

/// <summary>
/// DI extensions
/// </summary>
public static class ApexChartsExtensions
{

    /// <summary>
    /// Add Apexcharts service
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddApexCharts(this IServiceCollection services)
    {
        return services.AddScoped<IApexChartService, ApexChartService>();
    }

}
