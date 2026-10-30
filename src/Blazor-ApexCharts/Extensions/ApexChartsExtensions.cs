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
    /// <remarks>
    /// <see cref="ApexChartsServiceOptions"/> is registered as a singleton and holds the startup defaults.
    /// <see cref="IApexChartService"/> is scoped, and each instance works on its own copy of
    /// <see cref="ApexChartsServiceOptions.GlobalOptions"/>, so on Blazor Server a change made through
    /// the chart service (a theme, a locale) stays with the circuit that made it.
    /// </remarks>
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
    /// <remarks>
    /// These are the startup defaults. Every <see cref="IApexChartService"/> instance (one per DI scope,
    /// that is one per Blazor Server circuit) starts from its own deep copy of them, so change the global
    /// options at runtime through <see cref="IApexChartService.GlobalOptions"/> or
    /// <see cref="IApexChartService.SetGlobalOptionsAsync(IApexChartBaseOptions, bool)"/>. Changing this
    /// object after startup only affects chart services created afterwards.
    /// </remarks>
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