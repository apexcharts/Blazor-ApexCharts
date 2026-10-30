using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using ApexCharts.Internal;
using ApexCharts.Tests.TestHelpers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;

namespace ApexCharts.Tests.Components;

/// <summary>
/// Issue #655: disposing a chart after the circuit is gone (a page refresh in Blazor Server) raised
/// JSDisconnectedException on TaskScheduler.UnobservedTaskException, because Dispose() started
/// destroyChart and the module release without awaiting either.
/// </summary>
public class Issue655DisposeTests : ApexChartsTestContext
{
    private const string DestroyChart = "blazor_apexchart.destroyChart";
    private const string ModuleDisposed = "<module disposed>";

    private readonly FakeChartModule module = new();
    private readonly FakeJsRuntime runtime;

    public Issue655DisposeTests()
    {
        // The object get_apexcharts returns is the reference every chart call and the disposal go
        // through. bUnit's interop cannot hand out a custom IJSObjectReference, so the runtime is replaced.
        runtime = new FakeJsRuntime(() => Task.FromResult<IJSObjectReference>(module));
        Services.AddSingleton<IJSRuntime>(runtime);
    }

    private IRenderedComponent<ApexChart<Sale>> RenderBarChart(string? chartId = null)
    {
        var rendered = module.Calls.Count(c => c == JsIdentifiers.RenderChart);
        var cut = Render<ApexChart<Sale>>(p => p
            .Add(c => c.Options, new ApexChartOptions<Sale> { Chart = new Chart { Id = chartId } })
            .AddChildContent<ApexPointSeries<Sale>>(s => s
                .Add(x => x.Items, SampleData.Sales())
                .Add(x => x.SeriesType, SeriesType.Bar)
                .Add(x => x.XValue, e => e.Month)
                .Add(x => x.YValue, e => e.Amount)));
        cut.WaitForAssertion(() => Assert.Equal(rendered + 1, module.Calls.Count(c => c == JsIdentifiers.RenderChart)));
        return cut;
    }

    private static Func<Exception> Failure(string failure, string marker) => failure switch
    {
        "disconnected" => () => new JSDisconnectedException(marker),
        "cancelled" => () => new TaskCanceledException(marker),
        _ => () => new ObjectDisposedException(marker),
    };

    /// <summary>
    /// Runs <paramref name="renderThenDispose"/> and fails if a task it left behind reports an exception
    /// carrying <paramref name="marker"/> on <see cref="TaskScheduler.UnobservedTaskException"/>.
    /// </summary>
    private async Task AssertNothingUnobservedAsync(string marker, Func<Task> renderThenDispose)
    {
        var unobserved = new ConcurrentBag<Exception>();
        void OnUnobserved(object? sender, UnobservedTaskExceptionEventArgs e)
        {
            if (e.Exception.Flatten().InnerExceptions.Any(x => x.Message.Contains(marker)))
            {
                unobserved.Add(e.Exception);
            }
        }

        TaskScheduler.UnobservedTaskException += OnUnobserved;
        try
        {
            await renderThenDispose();

            // Faulted tasks nobody awaited report their exception when they are finalized.
            for (var i = 0; i < 3; i++)
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }

            Assert.Empty(unobserved);
            Assert.Contains(ModuleDisposed, module.Calls);
        }
        finally
        {
            TaskScheduler.UnobservedTaskException -= OnUnobserved;
        }
    }

    [Fact]
    public void Chart_implements_IAsyncDisposable_so_Blazor_awaits_the_teardown()
    {
        var cut = RenderBarChart();

        Assert.IsAssignableFrom<IAsyncDisposable>(cut.Instance);
    }

    [Fact]
    public async Task DisposeAsync_after_the_circuit_is_gone_does_not_throw()
    {
        var cut = RenderBarChart();
        module.Throw = () => new JSDisconnectedException("circuit gone");

        var disposable = Assert.IsAssignableFrom<IAsyncDisposable>(cut.Instance);
        await cut.InvokeAsync(() => disposable.DisposeAsync().AsTask());

        // Both steps were still attempted, in order, and a second dispose is harmless.
        Assert.Equal(new[] { DestroyChart, ModuleDisposed }, module.Calls.Where(c => c is DestroyChart or ModuleDisposed));
        await cut.InvokeAsync(() => disposable.DisposeAsync().AsTask());
        Assert.Single(module.Calls, ModuleDisposed);
    }

    [Theory]
    [InlineData("disconnected")]
    [InlineData("cancelled")]
    [InlineData("disposed")]
    public async Task Disposing_after_the_circuit_is_gone_leaves_no_unobserved_task_exception(string failure)
    {
        var marker = $"issue-655-{failure}-{Guid.NewGuid():N}";
        await AssertNothingUnobservedAsync(marker, () => RenderThenDisposeAsync(failure, marker));
    }

    // Kept out of line so no local of the test method keeps a task of the disposal alive.
    [MethodImpl(MethodImplOptions.NoInlining)]
    private async Task RenderThenDisposeAsync(string failure, string marker)
    {
        RenderBarChart();
        module.Throw = Failure(failure, marker);

        // What the renderer does when the page is refreshed.
        await DisposeComponentsAsync();
    }

    [Fact]
    public async Task The_module_is_released_only_after_destroyChart_completes()
    {
        RenderBarChart();
        var destroy = new TaskCompletionSource();
        module.Pending[DestroyChart] = destroy.Task;

        var disposing = DisposeComponentsAsync();

        Assert.Contains(DestroyChart, module.Calls);
        Assert.DoesNotContain(ModuleDisposed, module.Calls);

        destroy.SetResult();
        await disposing;

        Assert.Equal(new[] { DestroyChart, ModuleDisposed }, module.Calls.Where(c => c is DestroyChart or ModuleDisposed));
    }

    // A cancelled call leaves a cancelled task, which never reaches UnobservedTaskException, so only
    // the two failures that fault a task are worth checking here.
    [Theory]
    [InlineData("disconnected")]
    [InlineData("disposed")]
    public async Task Calling_Dispose_directly_releases_the_chart_and_leaves_no_unobserved_task_exception(string failure)
    {
        var marker = $"issue-655-direct-{failure}-{Guid.NewGuid():N}";
        await AssertNothingUnobservedAsync(marker, () => RenderThenDisposeDirectlyAsync(failure, marker));

        Assert.Equal(new[] { DestroyChart, ModuleDisposed }, module.Calls.Where(c => c is DestroyChart or ModuleDisposed));
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private async Task RenderThenDisposeDirectlyAsync(string failure, string marker)
    {
        var cut = RenderBarChart();
        module.Throw = Failure(failure, marker);

        await cut.InvokeAsync(() => cut.Instance.Dispose());

        // Dispose() releases the browser side in the background: wait until that has finished.
        await WaitUntil(() => module.Calls.Contains(ModuleDisposed));
    }

    [Fact]
    public async Task A_Dispose_override_in_a_subclass_still_runs()
    {
        var cut = Render<TrackingChart>(p => p
            .Add(c => c.Options, new ApexChartOptions<Sale>())
            .AddChildContent<ApexPointSeries<Sale>>(s => s
                .Add(x => x.Items, SampleData.Sales())
                .Add(x => x.SeriesType, SeriesType.Bar)
                .Add(x => x.XValue, e => e.Month)
                .Add(x => x.YValue, e => e.Amount)));
        cut.WaitForAssertion(() => Assert.Contains(JsIdentifiers.RenderChart, module.Calls));
        var chart = cut.Instance;

        await DisposeComponentsAsync();

        Assert.Equal(1, chart.DisposeCalls);
        Assert.Contains(ModuleDisposed, module.Calls);
    }

    [Fact]
    public async Task A_chart_disposed_while_the_module_loads_releases_it_and_never_renders()
    {
        var late = new FakeChartModule();
        var load = new TaskCompletionSource<IJSObjectReference>();
        runtime.GetApexCharts = () => load.Task;

        Render<ApexChart<Sale>>(p => p
            .Add(c => c.Options, new ApexChartOptions<Sale>())
            .AddChildContent<ApexPointSeries<Sale>>(s => s
                .Add(x => x.Items, SampleData.Sales())
                .Add(x => x.SeriesType, SeriesType.Bar)
                .Add(x => x.XValue, e => e.Month)
                .Add(x => x.YValue, e => e.Amount)));

        await DisposeComponentsAsync();
        load.SetResult(late);

        await WaitUntil(() => late.Calls.Contains(ModuleDisposed));
        Assert.DoesNotContain(JsIdentifiers.RenderChart, late.Calls);
    }

    [Fact]
    public async Task A_render_that_resumes_after_the_chart_is_disposed_does_not_raise_OnRendered()
    {
        var rendered = 0;
        var cut = Render<ApexChart<Sale>>(p => p
            .Add(c => c.Options, new ApexChartOptions<Sale>())
            .Add(c => c.OnRendered, () => rendered++)
            .AddChildContent<ApexPointSeries<Sale>>(s => s
                .Add(x => x.Items, SampleData.Sales())
                .Add(x => x.SeriesType, SeriesType.Bar)
                .Add(x => x.XValue, e => e.Month)
                .Add(x => x.YValue, e => e.Amount)));
        cut.WaitForAssertion(() => Assert.Equal(1, rendered));

        // RenderAsync (also what SetGlobalOptionsAsync and SetLocaleAsync re-render with) yields before it
        // draws. The page navigates away in that gap, so the chart and its series are disposed first.
        Task render = Task.CompletedTask;
        Task disposing = Task.CompletedTask;
        await cut.InvokeAsync(() =>
        {
            render = cut.Instance.RenderAsync();
            disposing = DisposeComponentsAsync();
        });
        await disposing;
        await render;

        Assert.Equal(1, rendered);
    }

    [Fact]
    public async Task A_chart_with_a_fixed_id_rendered_while_the_old_one_disposes_stays_registered()
    {
        // An @if that hides a chart with a fixed id and shows it again before the browser answers destroyChart.
        var service = Services.GetRequiredService<IApexChartService>();
        RenderBarChart("fixed");
        var destroy = new TaskCompletionSource();
        module.Pending[DestroyChart] = destroy.Task;

        var disposing = DisposeComponentsAsync();
        var replacement = RenderBarChart("fixed").Instance;
        Assert.Contains(replacement, service.Charts);

        destroy.SetResult();
        await disposing;

        Assert.Contains(replacement, service.Charts);
    }

    [Fact]
    public async Task Disposing_a_chart_unregisters_it_once()
    {
        var counting = new CountingChartService();
        Services.AddScoped<IApexChartService>(sp => counting.Wrap(ActivatorUtilities.CreateInstance<ApexChartService>(sp)));
        RenderBarChart();

        await DisposeComponentsAsync();

        Assert.Equal(1, counting.UnRegisterCalls);
    }

    [Fact]
    public async Task Disposing_a_chart_leaves_another_chart_with_the_same_id_registered()
    {
        var service = Services.GetRequiredService<IApexChartService>();
        var first = RenderBarChart("shared").Instance;
        var second = RenderBarChart("shared");

        await second.InvokeAsync(() => second.Instance.Dispose());

        Assert.Contains(first, service.Charts);
    }

    /// <summary>The reference renderChart handed to the browser, which the chart's formatters and events call back through.</summary>
    private DotNetObjectReference<JSHandler<Sale>> RenderedReference() =>
        Assert.IsType<DotNetObjectReference<JSHandler<Sale>>>(module.LastArgs[JsIdentifiers.RenderChart]![0]);

    private static bool IsReleased<T>(DotNetObjectReference<T> reference) where T : class
    {
        try
        {
            _ = reference.Value;
            return false;
        }
        catch (ObjectDisposedException)
        {
            return true;
        }
    }

    [Fact]
    public async Task Calling_Dispose_directly_keeps_the_dotnet_reference_until_destroyChart_has_run()
    {
        // On Blazor Server and BlazorWebView hosts destroyChart is a round trip, and until it lands the
        // chart's event handlers still call .NET through this reference. Released first, each of those
        // calls fails in the browser with a rejected promise.
        var cut = RenderBarChart();
        var reference = RenderedReference();
        var destroy = new TaskCompletionSource();
        module.Pending[DestroyChart] = destroy.Task;

        await cut.InvokeAsync(() => cut.Instance.Dispose());
        // A second call while the first is still destroying the chart must not release it either.
        await cut.InvokeAsync(() => cut.Instance.Dispose());

        Assert.Contains(DestroyChart, module.Calls);
        Assert.False(IsReleased(reference), "The reference was released before destroyChart completed.");

        destroy.SetResult();
        await WaitUntil(() => IsReleased(reference));
        Assert.Single(module.Calls, DestroyChart);
    }

    // "disconnected" is swallowed inside the teardown; "unexpected" escapes it, into the catch that keeps
    // the background release from leaving an unobserved task exception.
    [Theory]
    [InlineData("disconnected")]
    [InlineData("unexpected")]
    public async Task Calling_Dispose_directly_releases_the_dotnet_reference_when_destroyChart_fails(string failure)
    {
        var cut = RenderBarChart();
        var reference = RenderedReference();
        module.Throw = failure == "unexpected"
            ? () => new InvalidOperationException("unexpected interop failure")
            : () => new JSDisconnectedException("circuit gone");

        await cut.InvokeAsync(() => cut.Instance.Dispose());

        await WaitUntil(() => IsReleased(reference));
        // The module reference is released too, after destroyChart.
        Assert.Equal(new[] { DestroyChart, ModuleDisposed }, module.Calls.Where(c => c is DestroyChart or ModuleDisposed));
    }

    [Fact]
    public async Task DisposeAsync_with_an_unexpected_destroyChart_failure_still_releases_both_references()
    {
        var cut = RenderBarChart();
        var reference = RenderedReference();
        module.Throw = () => new InvalidOperationException("unexpected interop failure");

        var disposable = Assert.IsAssignableFrom<IAsyncDisposable>(cut.Instance);
        // Not a teardown failure, so it still reaches the caller.
        await Assert.ThrowsAsync<InvalidOperationException>(() => cut.InvokeAsync(() => disposable.DisposeAsync().AsTask()));

        Assert.True(IsReleased(reference));
        Assert.Contains(ModuleDisposed, module.Calls);
    }

    [Fact]
    public async Task Calling_Dispose_while_DisposeAsync_awaits_destroyChart_keeps_the_dotnet_reference()
    {
        // A parent that holds the chart through @ref and disposes it while Blazor is already disposing it.
        var cut = RenderBarChart();
        var chart = cut.Instance;
        var reference = RenderedReference();
        var destroy = new TaskCompletionSource();
        module.Pending[DestroyChart] = destroy.Task;

        var disposing = DisposeComponentsAsync();
        await cut.InvokeAsync(() => chart.Dispose());

        Assert.False(IsReleased(reference), "Dispose() released the reference before destroyChart completed.");

        destroy.SetResult();
        await disposing;

        Assert.True(IsReleased(reference));
        Assert.Single(module.Calls, DestroyChart);
        Assert.Single(module.Calls, ModuleDisposed);
    }

    [Fact]
    public async Task DisposeAsync_keeps_the_dotnet_reference_until_destroyChart_has_run()
    {
        RenderBarChart();
        var reference = RenderedReference();
        var destroy = new TaskCompletionSource();
        module.Pending[DestroyChart] = destroy.Task;

        var disposing = DisposeComponentsAsync();
        Assert.False(IsReleased(reference), "The reference was released before destroyChart completed.");

        destroy.SetResult();
        await disposing;

        Assert.True(IsReleased(reference));
    }

    [Fact]
    public async Task A_DisposeAsyncCore_override_in_a_subclass_runs_once_with_the_base_teardown()
    {
        var cut = Render<AsyncTrackingChart>(p => p
            .Add(c => c.Options, new ApexChartOptions<Sale>())
            .AddChildContent<ApexPointSeries<Sale>>(s => s
                .Add(x => x.Items, SampleData.Sales())
                .Add(x => x.SeriesType, SeriesType.Bar)
                .Add(x => x.XValue, e => e.Month)
                .Add(x => x.YValue, e => e.Amount)));
        cut.WaitForAssertion(() => Assert.Contains(JsIdentifiers.RenderChart, module.Calls));
        var chart = cut.Instance;
        chart.ModuleReleased = () => module.Calls.Contains(ModuleDisposed);

        await DisposeComponentsAsync();

        Assert.Equal(1, chart.DisposeAsyncCoreCalls);
        Assert.True(chart.SawModuleReleased, "The override ran before the base teardown had finished.");
        Assert.Equal(new[] { DestroyChart, ModuleDisposed }, module.Calls.Where(c => c is DestroyChart or ModuleDisposed));
    }

    [Fact]
    public async Task A_JavaScript_error_outside_teardown_still_reaches_the_caller()
    {
        // Only teardown ignores a JSException: on a live chart it reports a real failure.
        var cut = RenderBarChart();
        module.Throw = () => new JSException("chart error");

        await Assert.ThrowsAsync<JSException>(() => cut.InvokeAsync(() => cut.Instance.ClearAnnotationsAsync()));
        await Assert.ThrowsAsync<JSException>(() => cut.InvokeAsync(() => cut.Instance.GetSvgStringAsync()));
    }

    [Theory]
    [InlineData("disconnected")]
    [InlineData("disposed")]
    public async Task A_call_on_a_chart_whose_browser_is_gone_does_not_throw(string failure)
    {
        var cut = RenderBarChart();
        module.Throw = Failure(failure, "browser gone");

        await cut.InvokeAsync(() => cut.Instance.ClearAnnotationsAsync());
        Assert.Null(await cut.InvokeAsync(() => cut.Instance.GetSvgStringAsync()));
    }

    private static async Task WaitUntil(Func<bool> condition)
    {
        var deadline = DateTime.UtcNow.AddSeconds(5);
        while (!condition())
        {
            Assert.True(DateTime.UtcNow < deadline, "Timed out waiting for the condition.");
            await Task.Delay(10);
        }
    }

    /// <summary>A subclass that overrides <see cref="ApexChart{TItem}.Dispose"/>, as consumers do.</summary>
    public class TrackingChart : ApexChart<Sale>
    {
        public int DisposeCalls { get; private set; }

        public override void Dispose()
        {
            DisposeCalls++;
            base.Dispose();
        }
    }

    /// <summary>
    /// A subclass that adds asynchronous cleanup through the standard pattern: it overrides
    /// <see cref="ApexChart{TItem}.DisposeAsyncCore"/> and calls the base implementation first.
    /// </summary>
    public class AsyncTrackingChart : ApexChart<Sale>
    {
        public int DisposeAsyncCoreCalls { get; private set; }
        public bool SawModuleReleased { get; private set; }

        /// <summary>Reports whether the test's module reference has been released.</summary>
        public Func<bool> ModuleReleased { get; set; } = () => false;

        protected override async ValueTask DisposeAsyncCore()
        {
            DisposeAsyncCoreCalls++;
            await base.DisposeAsyncCore();
            SawModuleReleased = ModuleReleased();
        }
    }

    /// <summary>Forwards to the real service and counts <see cref="IApexChartService.UnRegisterChart"/> calls.</summary>
    private sealed class CountingChartService
    {
        private int unRegisterCalls;
        public int UnRegisterCalls => unRegisterCalls;

        public IApexChartService Wrap(IApexChartService inner) => new Decorator(inner, this);

        private sealed class Decorator(IApexChartService inner, CountingChartService counter) : IApexChartService
        {
            public List<IApexChartBase> Charts => inner.Charts;
            public List<LocaleResource> LocaleResources => inner.LocaleResources;
            public IApexChartBaseOptions GlobalOptions => inner.GlobalOptions;
            public Task InitalizeChartAsync(string? javascriptPath = null) => inner.InitalizeChartAsync(javascriptPath!);
            public Task ReRenderChartsAsync() => inner.ReRenderChartsAsync();
            public Task SetGlobalOptionsAsync(IApexChartBaseOptions options, bool reRenderCharts) => inner.SetGlobalOptionsAsync(options, reRenderCharts);
            public Task SetGlobalOptionsAsync(bool reRenderCharts) => inner.SetGlobalOptionsAsync(reRenderCharts);
            public Task SetLocaleAsync(LocaleResource localeResource, bool reRenderCharts) => inner.SetLocaleAsync(localeResource, reRenderCharts);
            public Task SetLocaleAsync(ChartLocale chartLocale, bool reRenderCharts) => inner.SetLocaleAsync(chartLocale, reRenderCharts);
            public Task GlobalOptionsInitializedAsync() => inner.GlobalOptionsInitializedAsync();
            public Task SetLicenseAsync(string licenseKey) => inner.SetLicenseAsync(licenseKey);
            public Task InitializeLicenseAsync() => inner.InitializeLicenseAsync();
            public Task RegisterCrossfilterAsync(string id, object records) => inner.RegisterCrossfilterAsync(id, records);
            public Task ResetCrossfilterAsync(string id) => inner.ResetCrossfilterAsync(id);
            public Task SubscribeCrossfilterAsync<T>(string id, DotNetObjectReference<T> handler) where T : class => inner.SubscribeCrossfilterAsync(id, handler);
            public void RegisterChart(IApexChartBase apexChart) => inner.RegisterChart(apexChart);

            public void UnRegisterChart(IApexChartBase apexChart)
            {
                Interlocked.Increment(ref counter.unRegisterCalls);
                inner.UnRegisterChart(apexChart);
            }
        }
    }

    /// <summary>
    /// Answers the module import with an object whose <c>get_apexcharts</c> returns what the test chose.
    /// Every other runtime call returns a default value.
    /// </summary>
    private sealed class FakeJsRuntime(Func<Task<IJSObjectReference>> getApexCharts) : IJSRuntime
    {
        public Func<Task<IJSObjectReference>> GetApexCharts { get; set; } = getApexCharts;

        public ValueTask<TValue> InvokeAsync<TValue>(string identifier, object?[]? args) =>
            InvokeAsync<TValue>(identifier, CancellationToken.None, args);

        public ValueTask<TValue> InvokeAsync<TValue>(string identifier, CancellationToken cancellationToken, object?[]? args) =>
            identifier == JsIdentifiers.Import
                ? ValueTask.FromResult((TValue)(object)new ImportedModule(this))
                : ValueTask.FromResult(default(TValue)!);

        private sealed class ImportedModule(FakeJsRuntime runtime) : IJSObjectReference
        {
            public ValueTask<TValue> InvokeAsync<TValue>(string identifier, object?[]? args) =>
                InvokeAsync<TValue>(identifier, CancellationToken.None, args);

            public async ValueTask<TValue> InvokeAsync<TValue>(string identifier, CancellationToken cancellationToken, object?[]? args) =>
                identifier == JsIdentifiers.GetApexCharts
                    ? (TValue)await runtime.GetApexCharts()
                    : default!;

            public ValueTask DisposeAsync() => ValueTask.CompletedTask;
        }
    }

    /// <summary>
    /// Stands in for the <c>blazor_apexchart</c> object reference. It records every call and the
    /// disposal in order, can throw what a lost circuit throws, and can hold a call open.
    /// </summary>
    private sealed class FakeChartModule : IJSObjectReference
    {
        public ConcurrentQueue<string> Calls { get; } = new();
        public Func<Exception>? Throw { get; set; }
        public ConcurrentDictionary<string, Task> Pending { get; } = new();
        public ConcurrentDictionary<string, object?[]?> LastArgs { get; } = new();

        public ValueTask<TValue> InvokeAsync<TValue>(string identifier, object?[]? args) =>
            InvokeAsync<TValue>(identifier, CancellationToken.None, args);

        public ValueTask<TValue> InvokeAsync<TValue>(string identifier, CancellationToken cancellationToken, object?[]? args)
        {
            Calls.Enqueue(identifier);
            LastArgs[identifier] = args;
            if (Throw != null) { return ValueTask.FromException<TValue>(Throw()); }
            if (Pending.TryGetValue(identifier, out var pending))
            {
                return new ValueTask<TValue>(pending.ContinueWith(_ => default(TValue)!, TaskScheduler.Default));
            }
            return ValueTask.FromResult(default(TValue)!);
        }

        public ValueTask DisposeAsync()
        {
            Calls.Enqueue(ModuleDisposed);
            return Throw != null ? ValueTask.FromException(Throw()) : ValueTask.CompletedTask;
        }
    }
}

/// <summary>
/// Issue #655: every call site that ignores interop failures classifies them through
/// <see cref="JSInteropErrors"/>. A gone browser is ignorable anywhere; a JSException, a cancelled call
/// or a timeout only while the chart is torn down.
/// </summary>
public class Issue655InteropErrorTests
{
    public static TheoryData<Exception, bool, bool> Failures => new()
    {
        // exception, IsBrowserGone, IsExpectedDuringTeardown
        { new JSDisconnectedException("circuit gone"), true, true },
        { new ObjectDisposedException("reference"), true, true },
        { new JSException("chart element gone"), false, true },
        { new OperationCanceledException(), false, true },
        { new TaskCanceledException("interop timeout"), false, true },
        { new InvalidOperationException("prerendering"), false, false },
        { new ArgumentException("bad argument"), false, false },
        { new NullReferenceException(), false, false },
    };

    [Theory]
    [MemberData(nameof(Failures))]
    public void Classifies_interop_failures(Exception failure, bool browserGone, bool expectedDuringTeardown)
    {
        Assert.Equal(browserGone, JSInteropErrors.IsBrowserGone(failure));
        Assert.Equal(expectedDuringTeardown, JSInteropErrors.IsExpectedDuringTeardown(failure));
    }
}
