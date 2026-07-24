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
    /// Add Apexcharts service to the DI container
    /// </summary>
    /// <param name="services"></param>
    /// <param name="serviceOptions"></param>
    /// <returns></returns>
    public static IServiceCollection AddApexCharts(this IServiceCollection services, Action<ApexChartsServiceOptions> serviceOptions = null)
    {
        serviceOptions ??= _ => { };

        var options = new ApexChartsServiceOptions();
        serviceOptions(options);
        services.AddSingleton(options);
        services.AddScoped<IApexChartService, ApexChartService>();
        services.AddHttpClient<ApexChartService>();
        return services;
    }

}

/// <summary>
/// Set Options for the ApexChartsService
/// </summary>
public class ApexChartsServiceOptions
{

    /// <summary>
    /// Set the global options
    /// </summary>
    public IApexChartBaseOptions GlobalOptions { get; set; }

    /// <summary>
    /// Application-wide license key that unlocks the gated premium features (history, perspectives,
    /// link/crossfilter, ink, measure, contextMenu, storyboard). When set, it is applied once (before
    /// any chart renders) via <c>ApexCharts.setLicense</c>. Without a valid key those features still
    /// work in trial mode but the chart shows an <c>APEXCHARTS</c> watermark. The key format is shared
    /// across the ApexCharts family (apexgantt, apextree, apexsankey) and is validated offline.
    /// A per-chart key can also be set via <see cref="Chart.License"/>.
    /// </summary>
    public string LicenseKey { get; set; }
}