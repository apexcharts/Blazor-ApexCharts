using System.Collections.Concurrent;
using ApexCharts.Tests.TestHelpers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;

namespace ApexCharts.Tests.Components;

/// <summary>
/// A timer or a background feed calls <see cref="ApexChart{TItem}.AppendDataAsync"/> or
/// <see cref="ApexChart{TItem}.UpdateSeriesAsync"/> and cannot tell that the chart is disposed while
/// the call is in flight. In Blazor Server that call then fails when the browser stops answering
/// (an interop timeout, a cancelled call) or reaches JavaScript after <c>destroyChart</c>. The docs
/// streaming demo let such a failure escape its timer callback and it took the whole host down.
/// Those failures are ignored once the chart is disposed and still reach the caller while it is alive.
/// </summary>
public class CallInFlightAtDisposeTests : ApexChartsTestContext
{
    private const string AppendData = "blazor_apexchart.appendData";
    private const string UpdateSeries = "blazor_apexchart.updateSeries";

    private readonly HoldingChartModule module = new();

    public CallInFlightAtDisposeTests()
    {
        Services.AddSingleton<IJSRuntime>(new ModuleRuntime(module));
    }

    public static TheoryData<string> TeardownFailures => new() { "timeout", "cancelled", "js" };

    private static Exception Failure(string failure) => failure switch
    {
        "timeout" => new TaskCanceledException("interop call timed out"),
        "cancelled" => new OperationCanceledException("interop call cancelled"),
        "js" => new JSException("chart not found"),
        "disconnected" => new JSDisconnectedException("circuit gone"),
        _ => new InvalidOperationException("not a teardown failure"),
    };

    private IRenderedComponent<ApexChart<Sale>> RenderLineChart(Action? onRendered = null)
    {
        var cut = Render<ApexChart<Sale>>(p => p
            .Add(c => c.Options, new ApexChartOptions<Sale>())
            .Add(c => c.OnRendered, () => onRendered?.Invoke())
            .AddChildContent<ApexPointSeries<Sale>>(s => s
                .Add(x => x.Items, SampleData.Sales())
                .Add(x => x.SeriesType, SeriesType.Line)
                .Add(x => x.XValue, e => e.Month)
                .Add(x => x.YValue, e => e.Amount)));
        cut.WaitForAssertion(() => Assert.Contains(JsIdentifiers.RenderChart, module.Calls));
        return cut;
    }

    private static Sale NewPoint() => new() { Month = "Next", Amount = 42 };

    /// <summary>Starts <paramref name="call"/> on the renderer and returns its task without awaiting it.</summary>
    private static async Task<Task> StartAsync(IRenderedComponent<ApexChart<Sale>> cut, Func<ApexChart<Sale>, Task> call)
    {
        Task started = Task.CompletedTask;
        await cut.InvokeAsync(() => { started = call(cut.Instance); });
        return started;
    }

    [Theory]
    [MemberData(nameof(TeardownFailures))]
    public async Task AppendDataAsync_in_flight_when_the_chart_is_disposed_ignores_a_teardown_failure(string failure)
    {
        var cut = RenderLineChart();
        var append = module.Hold(AppendData);

        var call = await StartAsync(cut, c => c.AppendDataAsync(new[] { NewPoint() }));
        await DisposeComponentsAsync();
        append.SetException(Failure(failure));

        await call;
        Assert.True(call.IsCompletedSuccessfully);
    }

    [Theory]
    [MemberData(nameof(TeardownFailures))]
    public async Task UpdateSeriesAsync_in_flight_when_the_chart_is_disposed_ignores_a_teardown_failure(string failure)
    {
        var cut = RenderLineChart();
        var update = module.Hold(UpdateSeries);

        var call = await StartAsync(cut, c => c.UpdateSeriesAsync(true));
        await WaitUntil(() => module.Calls.Contains(UpdateSeries));
        await DisposeComponentsAsync();
        update.SetException(Failure(failure));

        await call;
        Assert.True(call.IsCompletedSuccessfully);
    }

    [Fact]
    public async Task A_failure_teardown_does_not_cause_still_reaches_the_caller_after_dispose()
    {
        var cut = RenderLineChart();
        var append = module.Hold(AppendData);

        var call = await StartAsync(cut, c => c.AppendDataAsync(new[] { NewPoint() }));
        await DisposeComponentsAsync();
        append.SetException(Failure("other"));

        await Assert.ThrowsAsync<InvalidOperationException>(() => call);
    }

    [Theory]
    [MemberData(nameof(TeardownFailures))]
    public async Task While_the_chart_is_alive_the_same_failures_reach_the_caller(string failure)
    {
        var cut = RenderLineChart();
        var append = module.Hold(AppendData);
        var update = module.Hold(UpdateSeries);

        var appendCall = await StartAsync(cut, c => c.AppendDataAsync(new[] { NewPoint() }));
        var updateCall = await StartAsync(cut, c => c.UpdateSeriesAsync(true));
        await WaitUntil(() => module.Calls.Contains(UpdateSeries));
        append.SetException(Failure(failure));
        update.SetException(Failure(failure));

        var expected = Failure(failure).GetType();
        Assert.IsType(expected, await Record.ExceptionAsync(() => appendCall));
        Assert.IsType(expected, await Record.ExceptionAsync(() => updateCall));
    }

    [Fact]
    public async Task While_the_chart_is_alive_a_disconnected_circuit_is_still_ignored()
    {
        var cut = RenderLineChart();
        var append = module.Hold(AppendData);

        var call = await StartAsync(cut, c => c.AppendDataAsync(new[] { NewPoint() }));
        append.SetException(Failure("disconnected"));

        await call;
        Assert.True(call.IsCompletedSuccessfully);
    }

    [Fact]
    public async Task A_call_made_after_dispose_never_reaches_the_browser()
    {
        var cut = RenderLineChart();
        var chart = cut.Instance;
        await DisposeComponentsAsync();

        await cut.InvokeAsync(() => chart.AppendDataAsync(new[] { NewPoint() }));
        await cut.InvokeAsync(() => chart.UpdateSeriesAsync(true));

        Assert.DoesNotContain(AppendData, module.Calls);
        Assert.DoesNotContain(UpdateSeries, module.Calls);
    }

    [Theory]
    [MemberData(nameof(TeardownFailures))]
    public async Task A_render_that_fails_after_dispose_is_ignored_and_raises_no_OnRendered(string failure)
    {
        var rendered = 0;
        var cut = RenderLineChart(() => rendered++);
        cut.WaitForAssertion(() => Assert.Equal(1, rendered));
        var render = module.Hold(JsIdentifiers.RenderChart);
        var renderCalls = module.Calls.Count(c => c == JsIdentifiers.RenderChart);

        var call = await StartAsync(cut, c => c.RenderAsync());
        await WaitUntil(() => module.Calls.Count(c => c == JsIdentifiers.RenderChart) > renderCalls);
        await DisposeComponentsAsync();
        render.SetException(Failure(failure));

        await call;
        Assert.Equal(1, rendered);
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

    /// <summary>
    /// Answers the module import with an object whose <c>get_apexcharts</c> returns the chart module.
    /// </summary>
    private sealed class ModuleRuntime(IJSObjectReference chartModule) : IJSRuntime
    {
        public ValueTask<TValue> InvokeAsync<TValue>(string identifier, object?[]? args) =>
            InvokeAsync<TValue>(identifier, CancellationToken.None, args);

        public ValueTask<TValue> InvokeAsync<TValue>(string identifier, CancellationToken cancellationToken, object?[]? args) =>
            identifier == JsIdentifiers.Import
                ? ValueTask.FromResult((TValue)(object)new ImportedModule(chartModule))
                : ValueTask.FromResult(default(TValue)!);

        private sealed class ImportedModule(IJSObjectReference chartModule) : IJSObjectReference
        {
            public ValueTask<TValue> InvokeAsync<TValue>(string identifier, object?[]? args) =>
                InvokeAsync<TValue>(identifier, CancellationToken.None, args);

            public ValueTask<TValue> InvokeAsync<TValue>(string identifier, CancellationToken cancellationToken, object?[]? args) =>
                ValueTask.FromResult(identifier == JsIdentifiers.GetApexCharts ? (TValue)chartModule : default!);

            public ValueTask DisposeAsync() => ValueTask.CompletedTask;
        }
    }

    /// <summary>
    /// Stands in for the <c>blazor_apexchart</c> object reference. It records every call, and a call
    /// the test holds stays open until the test completes or fails it.
    /// </summary>
    private sealed class HoldingChartModule : IJSObjectReference
    {
        private readonly ConcurrentDictionary<string, TaskCompletionSource> held = new();

        public ConcurrentQueue<string> Calls { get; } = new();

        /// <summary>Holds the next calls to <paramref name="identifier"/> open until the returned source completes.</summary>
        public TaskCompletionSource Hold(string identifier)
        {
            var source = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
            held[identifier] = source;
            return source;
        }

        public ValueTask<TValue> InvokeAsync<TValue>(string identifier, object?[]? args) =>
            InvokeAsync<TValue>(identifier, CancellationToken.None, args);

        public ValueTask<TValue> InvokeAsync<TValue>(string identifier, CancellationToken cancellationToken, object?[]? args)
        {
            Calls.Enqueue(identifier);
            if (held.TryGetValue(identifier, out var source))
            {
                return new ValueTask<TValue>(Wait<TValue>(source.Task));
            }
            return ValueTask.FromResult(default(TValue)!);
        }

        private static async Task<TValue> Wait<TValue>(Task held)
        {
            await held;
            return default!;
        }

        public ValueTask DisposeAsync() => ValueTask.CompletedTask;
    }
}
