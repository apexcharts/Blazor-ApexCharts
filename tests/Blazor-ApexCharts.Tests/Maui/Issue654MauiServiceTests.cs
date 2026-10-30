using Microsoft.JSInterop;

namespace ApexCharts.Tests.Maui;

/// <summary>
/// Issue #654 for the MAUI chart service: AddApexChartsMaui also registers the options as a singleton,
/// and every BlazorWebView gets its own scope, so each scope must work on its own copy.
/// </summary>
public class Issue654MauiServiceTests
{
    private sealed class MyGlobalOptions : ApexChartBaseOptions { }

    private static ServiceProvider BuildProvider(Action<ApexChartsServiceOptions> configure)
    {
        var services = new ServiceCollection();
        services.AddScoped<IJSRuntime>(_ => new BunitJSInterop { Mode = JSRuntimeMode.Loose }.JSRuntime);
        services.AddApexChartsMaui(configure);
        return services.BuildServiceProvider(new ServiceProviderOptions { ValidateScopes = true, ValidateOnBuild = true });
    }

    [Fact]
    public void Registers_the_maui_service()
    {
        using var provider = BuildProvider(_ => { });
        using var scope = provider.CreateScope();

        Assert.IsType<ApexChartServiceMaui>(scope.ServiceProvider.GetRequiredService<IApexChartService>());
    }

    [Fact]
    public void A_change_in_one_scope_is_not_seen_by_another_scope_or_the_defaults()
    {
        using var provider = BuildProvider(o => o.GlobalOptions = new ApexChartBaseOptions
        {
            Theme = new Theme { Mode = Mode.Light },
            Colors = new List<string> { "#111111" },
        });
        using var userA = provider.CreateScope();
        using var userB = provider.CreateScope();
        var serviceA = userA.ServiceProvider.GetRequiredService<IApexChartService>();
        var serviceB = userB.ServiceProvider.GetRequiredService<IApexChartService>();

        serviceA.GlobalOptions.Theme.Mode = Mode.Dark;
        serviceA.GlobalOptions.Colors.Add("#222222");

        Assert.Equal(Mode.Light, serviceB.GlobalOptions.Theme.Mode);
        Assert.Single(serviceB.GlobalOptions.Colors);
        Assert.Equal(Mode.Light, provider.GetRequiredService<ApexChartsServiceOptions>().GlobalOptions.Theme.Mode);
    }

    [Fact]
    public void A_subclass_of_ApexChartBaseOptions_is_not_shared_between_scopes()
    {
        using var provider = BuildProvider(o => o.GlobalOptions = new MyGlobalOptions { Theme = new Theme { Mode = Mode.Light } });
        using var userA = provider.CreateScope();
        using var userB = provider.CreateScope();
        var serviceA = userA.ServiceProvider.GetRequiredService<IApexChartService>();
        var serviceB = userB.ServiceProvider.GetRequiredService<IApexChartService>();

        serviceA.GlobalOptions.Theme.Mode = Mode.Dark;

        Assert.IsType<MyGlobalOptions>(serviceB.GlobalOptions);
        Assert.Equal(Mode.Light, serviceB.GlobalOptions.Theme.Mode);
    }
}
