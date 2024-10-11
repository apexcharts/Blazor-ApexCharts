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
    /// <param name="serviceOptions"></param>
    /// <returns></returns>
    public static IServiceCollection AddApexCharts(this IServiceCollection services, Action<ApexChartsServiceOptions> serviceOptions = null)
    {

        if (serviceOptions is null)
        {
            serviceOptions = _ => { };
        }

        var options = new ApexChartsServiceOptions();
        serviceOptions(options);

        services.AddSingleton(options);
        services.AddScoped<IApexChartService, ApexChartService>();

        services.AddHttpClient<ApexChartService>();

        return services;

    }

}

public class ApexChartsServiceOptions
{
    public IApexChartBaseOptions GlobalOptions { get; set; }
}