using ApexCharts.Internal;
using ApexCharts.Tests.TestHelpers;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;

namespace ApexCharts.Tests.Services;

/// <summary>
/// Issue #654: ApexChartsServiceOptions is a singleton, and every scoped chart service used to work on
/// the very same GlobalOptions object, so on Blazor Server one user's theme or locale change reached
/// every other circuit.
/// </summary>
public class Issue654ScopedGlobalOptionsTests
{
    private sealed class TestNavigationManager : NavigationManager
    {
        public TestNavigationManager() => Initialize("http://localhost/", "http://localhost/");
    }

    /// <summary>
    /// The same container a Blazor Server app builds, with scope validation on so that a captive
    /// dependency would throw.
    /// </summary>
    private static ServiceProvider BuildProvider(Action<ApexChartsServiceOptions>? configure = null)
    {
        var services = new ServiceCollection();
        services.AddSingleton<NavigationManager, TestNavigationManager>();
        services.AddScoped<IJSRuntime>(_ => new BunitJSInterop { Mode = JSRuntimeMode.Loose }.JSRuntime);
        services.AddApexCharts(configure);
        return services.BuildServiceProvider(new ServiceProviderOptions { ValidateScopes = true, ValidateOnBuild = true });
    }

    private static void LightTheme(ApexChartsServiceOptions options)
    {
        options.GlobalOptions = new ApexChartBaseOptions
        {
            Theme = new Theme { Mode = Mode.Light, Palette = PaletteType.Palette1 },
            Colors = new List<string> { "#111111", "#222222" },
            Blazor = new ApexChartsBlazorOptions { JavascriptPath = "/js/custom.js" },
        };
    }

    [Fact]
    public void Every_scope_starts_from_the_startup_defaults()
    {
        using var provider = BuildProvider(LightTheme);
        using var scope = provider.CreateScope();

        var options = scope.ServiceProvider.GetRequiredService<IApexChartService>().GlobalOptions;

        Assert.Equal(Mode.Light, options.Theme.Mode);
        Assert.Equal(PaletteType.Palette1, options.Theme.Palette);
        Assert.Equal(new[] { "#111111", "#222222" }, options.Colors);
        Assert.Equal("/js/custom.js", options.Blazor.JavascriptPath);
    }

    [Fact]
    public void A_change_in_one_scope_is_not_seen_by_a_scope_that_already_exists()
    {
        using var provider = BuildProvider(LightTheme);
        using var userA = provider.CreateScope();
        using var userB = provider.CreateScope();
        var serviceA = userA.ServiceProvider.GetRequiredService<IApexChartService>();
        var serviceB = userB.ServiceProvider.GetRequiredService<IApexChartService>();

        serviceA.GlobalOptions.Theme.Mode = Mode.Dark;
        serviceA.GlobalOptions.Colors.Add("#333333");

        Assert.Equal(Mode.Light, serviceB.GlobalOptions.Theme.Mode);
        Assert.Equal(2, serviceB.GlobalOptions.Colors.Count);
    }

    [Fact]
    public void A_change_in_one_scope_is_not_seen_by_a_scope_created_later()
    {
        using var provider = BuildProvider(LightTheme);
        using (var userA = provider.CreateScope())
        {
            userA.ServiceProvider.GetRequiredService<IApexChartService>().GlobalOptions.Theme.Mode = Mode.Dark;
        }

        using var userB = provider.CreateScope();
        Assert.Equal(Mode.Light, userB.ServiceProvider.GetRequiredService<IApexChartService>().GlobalOptions.Theme.Mode);
    }

    [Fact]
    public void A_change_in_one_scope_leaves_the_configured_defaults_untouched()
    {
        using var provider = BuildProvider(LightTheme);
        using var userA = provider.CreateScope();

        userA.ServiceProvider.GetRequiredService<IApexChartService>().GlobalOptions.Theme.Mode = Mode.Dark;

        var defaults = provider.GetRequiredService<ApexChartsServiceOptions>();
        Assert.Equal(Mode.Light, defaults.GlobalOptions.Theme.Mode);
    }

    [Fact]
    public async Task Setting_a_locale_in_one_scope_does_not_change_another_scope()
    {
        using var provider = BuildProvider(LightTheme);
        using var userA = provider.CreateScope();
        using var userB = provider.CreateScope();
        var serviceA = userA.ServiceProvider.GetRequiredService<IApexChartService>();
        var serviceB = userB.ServiceProvider.GetRequiredService<IApexChartService>();

        await serviceA.SetLocaleAsync(new ChartLocale { Name = "sv", Options = new LocaleOptions() }, false);

        Assert.Equal("sv", serviceA.GlobalOptions.Chart.DefaultLocale);
        Assert.Null(serviceB.GlobalOptions.Chart?.DefaultLocale);
        Assert.Null(serviceB.GlobalOptions.Chart?.Locales);
    }

    [Fact]
    public void Scopes_without_configured_global_options_do_not_share_an_instance()
    {
        using var provider = BuildProvider();
        using var userA = provider.CreateScope();
        using var userB = provider.CreateScope();
        var serviceA = userA.ServiceProvider.GetRequiredService<IApexChartService>();
        var serviceB = userB.ServiceProvider.GetRequiredService<IApexChartService>();

        serviceA.GlobalOptions.Theme = new Theme { Mode = Mode.Dark };

        Assert.Null(serviceB.GlobalOptions.Theme);
    }

    [Fact]
    public void Service_options_stay_a_singleton_that_a_singleton_can_depend_on()
    {
        var services = new ServiceCollection();
        services.AddApexCharts(LightTheme);
        services.AddSingleton<SingletonConsumer>();
        using var provider = services.BuildServiceProvider(new ServiceProviderOptions { ValidateScopes = true });

        var consumer = provider.GetRequiredService<SingletonConsumer>();

        Assert.Same(provider.GetRequiredService<ApexChartsServiceOptions>(), consumer.Options);
        Assert.Equal(Mode.Light, consumer.Options.GlobalOptions.Theme.Mode);
    }

    private sealed class SingletonConsumer(ApexChartsServiceOptions options)
    {
        public ApexChartsServiceOptions Options { get; } = options;
    }
}

/// <summary>
/// The startup defaults still reach apexcharts.js through setGlobalOptions before the first chart renders.
/// </summary>
public class Issue654GlobalOptionsRenderTests : ApexChartsTestContext
{
    public Issue654GlobalOptionsRenderTests() : base(o => o.GlobalOptions = new ApexChartBaseOptions
    {
        Theme = new Theme { Mode = Mode.Dark, Palette = PaletteType.Palette3 },
    })
    { }

    [Fact]
    public void Configured_global_options_are_sent_before_the_chart_renders()
    {
        RenderChart<Sale>(p => p
            .Add(c => c.Options, new ApexChartOptions<Sale>())
            .AddChildContent<ApexPointSeries<Sale>>(s => s
                .Add(x => x.Items, SampleData.Sales())
                .Add(x => x.SeriesType, SeriesType.Bar)
                .Add(x => x.XValue, e => e.Month)
                .Add(x => x.YValue, e => e.Amount)));

        var global = LastJsonArgument(JsIdentifiers.SetGlobalOptions, 0);
        Assert.Equal("dark", global.Get("theme.mode")?.GetString());
        Assert.Equal("palette3", global.Get("theme.palette")?.GetString());

        var calls = JSInterop.Invocations.Select(i => i.Identifier).ToList();
        Assert.True(calls.IndexOf(JsIdentifiers.SetGlobalOptions) < calls.IndexOf(JsIdentifiers.RenderChart));
    }
}

public class OptionsCopierTests
{
    private sealed class UserItem
    {
        public int Value { get; set; }
    }

    [Fact]
    public void Copies_nested_options_lists_dictionaries_and_ignored_properties()
    {
        var source = new ApexChartBaseOptions
        {
            Debug = true,
            Blazor = new ApexChartsBlazorOptions { JavascriptPath = "/a.js" },
            Chart = new Chart { Events = new Dictionary<string, object> { ["click"] = "f" } },
            Legend = new Legend { Labels = new LegendLabels { Colors = new Color("#aaaaaa", "#bbbbbb") } },
            Yaxis = new List<YAxis> { new YAxis { Min = 1 } },
        };

        var copy = OptionsCopier.DeepCopy(source);

        Assert.NotSame(source, copy);
        Assert.True(copy.Debug);
        Assert.NotSame(source.Blazor, copy.Blazor);
        Assert.Equal("/a.js", copy.Blazor.JavascriptPath);
        Assert.NotSame(source.Chart.Events, copy.Chart.Events);
        Assert.Equal("f", copy.Chart.Events["click"]);
        Assert.NotSame(source.Yaxis[0], copy.Yaxis[0]);
        Assert.Equal(1, copy.Yaxis[0].Min);

        // Color keeps its values in a private list, which has to be copied too.
        copy.Legend.Labels.Colors.Add("#cccccc");
        Assert.Equal(2, source.Legend.Labels.Colors.Count);
        Assert.Equal(3, copy.Legend.Labels.Colors.Count);
    }

    [Fact]
    public void Keeps_shared_references_shared_and_objects_of_other_assemblies_as_they_are()
    {
        var theme = new Theme { Mode = Mode.Dark };
        var item = new UserItem { Value = 1 };
        var source = new ApexChartOptions<UserItem>
        {
            Theme = theme,
            Chart = new Chart { Events = new Dictionary<string, object> { ["a"] = theme, ["b"] = item } },
        };

        var copy = OptionsCopier.DeepCopy(source);

        Assert.NotSame(theme, copy.Theme);
        Assert.Same(copy.Theme, copy.Chart.Events["a"]);
        Assert.Same(item, copy.Chart.Events["b"]);
    }

    [Fact]
    public void Returns_null_for_null()
    {
        Assert.Null(OptionsCopier.DeepCopy<IApexChartBaseOptions>(null!));
    }
}
