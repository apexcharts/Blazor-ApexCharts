namespace ApexCharts;

/// <summary>
/// DI extensions
/// </summary>
public static class ApexChartsMauiExtensions
{
	/// <summary>
	/// Add Apexcharts service to the DI container for use in .NET MAUI
	/// </summary>
	/// <param name="services"></param>
	/// <param name="serviceOptions"></param>
	public static IServiceCollection AddApexChartsMaui(this IServiceCollection services, Action<ApexChartsServiceOptions>? serviceOptions = null)
	{
		serviceOptions ??= _ => { };

		var options = new ApexChartsServiceOptions();
		serviceOptions(options);
		services.AddSingleton(options);
		services.AddScoped<IApexChartService, ApexChartServiceMaui>();
		return services;
	}
}
