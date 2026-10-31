using ApexCharts.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace ApexCharts.Tests.Services;

/// <summary>
/// Issue #654, follow-up: the global options may be an instance of the consumer's own class (a subclass of
/// ApexChartBaseOptions, or an implementation of IApexChartBaseOptions), and nested options may be
/// subclasses too (of Theme, for example). Every scope must still get its own copy of them.
/// </summary>
public class Issue654CustomOptionTypesTests
{
    private sealed class MyGlobalOptions : ApexChartBaseOptions
    {
        public string? Brand { get; set; }
    }

    private sealed class MyTheme : Theme
    {
        public string? Accent { get; set; }
    }

    /// <summary>
    /// The consumer's own implementation of the interface, with no library base class.
    /// </summary>
    private sealed class MyOwnOptions : IApexChartBaseOptions
    {
        public Annotations? Annotations { get; set; }
        public ApexChartsBlazorOptions? Blazor { get; set; }
        public Chart? Chart { get; set; }
        public List<string>? Colors { get; set; }
        public DataLabels? DataLabels { get; set; }
        public bool? Debug { get; set; }
        public Fill? Fill { get; set; }
        public ForecastDataPoints? ForecastDataPoints { get; set; }
        public Grid? Grid { get; set; }
        public List<string>? Labels { get; set; }
        public Legend? Legend { get; set; }
        public Markers? Markers { get; set; }
        public NoData? NoData { get; set; }
        public PlotOptions? PlotOptions { get; set; }
        public States? States { get; set; }
        public Stroke? Stroke { get; set; }
        public Subtitle? Subtitle { get; set; }
        public Theme? Theme { get; set; }
        public Title? Title { get; set; }
        public Tooltip? Tooltip { get; set; }
        public XAxis? Xaxis { get; set; }
        public List<YAxis>? Yaxis { get; set; }
    }

    [Fact]
    public void A_subclass_of_ApexChartBaseOptions_is_not_shared_between_scopes()
    {
        using var provider = Issue654ScopedGlobalOptionsTests.BuildProvider(o => o.GlobalOptions = new MyGlobalOptions
        {
            Brand = "acme",
            Theme = new Theme { Mode = Mode.Light },
        });
        using var userA = provider.CreateScope();
        using var userB = provider.CreateScope();
        var serviceA = userA.ServiceProvider.GetRequiredService<IApexChartService>();
        var serviceB = userB.ServiceProvider.GetRequiredService<IApexChartService>();

        serviceA.GlobalOptions.Theme.Mode = Mode.Dark;

        Assert.NotSame(serviceA.GlobalOptions, serviceB.GlobalOptions);
        Assert.Equal(Mode.Light, serviceB.GlobalOptions.Theme.Mode);
        var copyB = Assert.IsType<MyGlobalOptions>(serviceB.GlobalOptions);
        Assert.Equal("acme", copyB.Brand);
        Assert.Equal(Mode.Light, provider.GetRequiredService<ApexChartsServiceOptions>().GlobalOptions.Theme.Mode);
    }

    [Fact]
    public void A_subclass_of_Theme_is_not_shared_between_scopes()
    {
        using var provider = Issue654ScopedGlobalOptionsTests.BuildProvider(o => o.GlobalOptions = new ApexChartBaseOptions
        {
            Theme = new MyTheme { Mode = Mode.Light, Accent = "#ff0000" },
        });
        using var userA = provider.CreateScope();
        using var userB = provider.CreateScope();
        var serviceA = userA.ServiceProvider.GetRequiredService<IApexChartService>();
        var serviceB = userB.ServiceProvider.GetRequiredService<IApexChartService>();

        serviceA.GlobalOptions.Theme.Mode = Mode.Dark;

        Assert.NotSame(serviceA.GlobalOptions.Theme, serviceB.GlobalOptions.Theme);
        Assert.Equal(Mode.Light, serviceB.GlobalOptions.Theme.Mode);
        Assert.Equal("#ff0000", Assert.IsType<MyTheme>(serviceB.GlobalOptions.Theme).Accent);
    }

    [Fact]
    public void An_own_implementation_of_IApexChartBaseOptions_is_not_shared_between_scopes()
    {
        using var provider = Issue654ScopedGlobalOptionsTests.BuildProvider(o => o.GlobalOptions = new MyOwnOptions
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

        Assert.NotSame(serviceA.GlobalOptions, serviceB.GlobalOptions);
        Assert.IsType<MyOwnOptions>(serviceB.GlobalOptions);
        Assert.Equal(Mode.Light, serviceB.GlobalOptions.Theme.Mode);
        Assert.Single(serviceB.GlobalOptions.Colors);
    }
}

/// <summary>
/// Issue #654, review follow-up: a consumer's option class that also implements IDisposable or
/// IAsyncDisposable is still an option object. Only the library's own components, services and
/// disposable resources are kept by reference.
/// </summary>
public class Issue654DisposableOptionTypesTests
{
    private sealed class DisposableGlobal : ApexChartBaseOptions, IDisposable
    {
        public bool Disposed { get; private set; }
        public void Dispose() => Disposed = true;
    }

    private sealed class AsyncDisposableTheme : Theme, IAsyncDisposable
    {
        public ValueTask DisposeAsync() => ValueTask.CompletedTask;
    }

    private sealed class DisposableOwnOptions : IApexChartBaseOptions, IDisposable
    {
        public Annotations? Annotations { get; set; }
        public ApexChartsBlazorOptions? Blazor { get; set; }
        public Chart? Chart { get; set; }
        public List<string>? Colors { get; set; }
        public DataLabels? DataLabels { get; set; }
        public bool? Debug { get; set; }
        public Fill? Fill { get; set; }
        public ForecastDataPoints? ForecastDataPoints { get; set; }
        public Grid? Grid { get; set; }
        public List<string>? Labels { get; set; }
        public Legend? Legend { get; set; }
        public Markers? Markers { get; set; }
        public NoData? NoData { get; set; }
        public PlotOptions? PlotOptions { get; set; }
        public States? States { get; set; }
        public Stroke? Stroke { get; set; }
        public Subtitle? Subtitle { get; set; }
        public Theme? Theme { get; set; }
        public Title? Title { get; set; }
        public Tooltip? Tooltip { get; set; }
        public XAxis? Xaxis { get; set; }
        public List<YAxis>? Yaxis { get; set; }
        public void Dispose() { }
    }

    private sealed class UserItem
    {
        public int Value { get; set; }
    }

    private sealed class MyChart : ApexChart<UserItem> { }

    [Fact]
    public void A_disposable_subclass_of_ApexChartBaseOptions_is_not_shared_between_scopes()
    {
        using var provider = Issue654ScopedGlobalOptionsTests.BuildProvider(o => o.GlobalOptions = new DisposableGlobal
        {
            Theme = new Theme { Mode = Mode.Light },
        });
        using var userA = provider.CreateScope();
        using var userB = provider.CreateScope();
        var serviceA = userA.ServiceProvider.GetRequiredService<IApexChartService>();
        var serviceB = userB.ServiceProvider.GetRequiredService<IApexChartService>();

        serviceA.GlobalOptions.Theme.Mode = Mode.Dark;

        Assert.NotSame(serviceA.GlobalOptions, serviceB.GlobalOptions);
        Assert.IsType<DisposableGlobal>(serviceB.GlobalOptions);
        Assert.Equal(Mode.Light, serviceB.GlobalOptions.Theme.Mode);
        Assert.Equal(Mode.Light, provider.GetRequiredService<ApexChartsServiceOptions>().GlobalOptions.Theme.Mode);
    }

    [Fact]
    public void An_async_disposable_subclass_of_Theme_is_not_shared_between_scopes()
    {
        using var provider = Issue654ScopedGlobalOptionsTests.BuildProvider(o => o.GlobalOptions = new ApexChartBaseOptions
        {
            Theme = new AsyncDisposableTheme { Mode = Mode.Light },
        });
        using var userA = provider.CreateScope();
        using var userB = provider.CreateScope();
        var serviceA = userA.ServiceProvider.GetRequiredService<IApexChartService>();
        var serviceB = userB.ServiceProvider.GetRequiredService<IApexChartService>();

        serviceA.GlobalOptions.Theme.Mode = Mode.Dark;

        Assert.NotSame(serviceA.GlobalOptions.Theme, serviceB.GlobalOptions.Theme);
        Assert.IsType<AsyncDisposableTheme>(serviceB.GlobalOptions.Theme);
        Assert.Equal(Mode.Light, serviceB.GlobalOptions.Theme.Mode);
    }

    [Fact]
    public void A_disposable_own_implementation_of_IApexChartBaseOptions_is_not_shared_between_scopes()
    {
        using var provider = Issue654ScopedGlobalOptionsTests.BuildProvider(o => o.GlobalOptions = new DisposableOwnOptions
        {
            Theme = new Theme { Mode = Mode.Light },
        });
        using var userA = provider.CreateScope();
        using var userB = provider.CreateScope();
        var serviceA = userA.ServiceProvider.GetRequiredService<IApexChartService>();
        var serviceB = userB.ServiceProvider.GetRequiredService<IApexChartService>();

        serviceA.GlobalOptions.Theme.Mode = Mode.Dark;

        Assert.NotSame(serviceA.GlobalOptions, serviceB.GlobalOptions);
        Assert.Equal(Mode.Light, serviceB.GlobalOptions.Theme.Mode);
    }

    [Fact]
    public void Library_components_and_their_subclasses_are_kept_by_reference()
    {
        var chart = new ApexChart<UserItem>();
        var myChart = new MyChart();
        var source = new Chart { Events = new Dictionary<string, object> { ["chart"] = chart, ["myChart"] = myChart } };

        var copy = OptionsCopier.DeepCopy(source);

        Assert.NotSame(source.Events, copy.Events);
        Assert.Same(chart, copy.Events["chart"]);
        Assert.Same(myChart, copy.Events["myChart"]);
    }
}

public class OptionsCopierCollectionTests
{
    /// <summary>
    /// OptionsCopier is an implementation detail and not public API. The MAUI package, which ships from the
    /// same release, reaches it through InternalsVisibleTo under its default assembly name.
    /// </summary>
    [Fact]
    public void OptionsCopier_is_internal_and_visible_to_the_MAUI_package()
    {
        Assert.False(typeof(OptionsCopier).IsPublic);
        var friends = typeof(OptionsCopier).Assembly
            .GetCustomAttributes(typeof(System.Runtime.CompilerServices.InternalsVisibleToAttribute), false)
            .Cast<System.Runtime.CompilerServices.InternalsVisibleToAttribute>()
            .Select(a => a.AssemblyName);
        Assert.Contains("Blazor-ApexCharts-MAUI", friends);
    }

    [Fact]
    public void A_dictionary_keeps_its_comparer()
    {
        var source = new Chart { Events = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase) { ["click"] = "f" } };

        var copy = OptionsCopier.DeepCopy(source);

        Assert.NotSame(source.Events, copy.Events);
        Assert.Same(StringComparer.OrdinalIgnoreCase, copy.Events.Comparer);
        Assert.Equal("f", copy.Events["CLICK"]);
    }

    [Fact]
    public void Arrays_are_copied_and_their_option_elements_too()
    {
        var theme = new Theme { Mode = Mode.Light };
        var numbers = new[] { 1, 2, 3 };
        var source = new Chart { Events = new Dictionary<string, object> { ["themes"] = new[] { theme }, ["numbers"] = numbers } };

        var copy = OptionsCopier.DeepCopy(source);

        var themes = Assert.IsType<Theme[]>(copy.Events["themes"]);
        Assert.NotSame(theme, themes[0]);
        Assert.Equal(Mode.Light, themes[0].Mode);
        var numbersCopy = Assert.IsType<int[]>(copy.Events["numbers"]);
        Assert.NotSame(numbers, numbersCopy);
        Assert.Equal(numbers, numbersCopy);
    }
}
