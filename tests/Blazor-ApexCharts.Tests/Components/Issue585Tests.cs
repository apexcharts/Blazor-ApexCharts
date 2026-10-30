using System.Text.Json;
using ApexCharts.Tests.TestHelpers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;

namespace ApexCharts.Tests.Components;

/// <summary>
/// Issue 585: the .NET label formatters and the before zoom callbacks are called from JavaScript
/// synchronously, which only the in-process (WebAssembly) runtime supports. The component has to
/// tell the script per chart whether synchronous interop is available, and must not install those
/// callbacks (or throw) when it is not.
/// </summary>
public abstract class Issue585TestBase : ApexChartsTestContext
{
    protected IRenderedComponent<ApexChart<Sale>> RenderLineChart(
        Action<ComponentParameterCollectionBuilder<ApexChart<Sale>>> chartParameters) =>
        RenderChart<Sale>(p =>
        {
            p.Add(c => c.Options, new ApexChartOptions<Sale>());
            chartParameters(p);
            p.AddChildContent<ApexPointSeries<Sale>>(s => s
                .Add(x => x.Name, "Amount")
                .Add(x => x.Items, SampleData.Sales())
                .Add(x => x.SeriesType, SeriesType.Line)
                .Add(x => x.XValue, e => e.Month)
                .Add(x => x.YValue, e => e.Amount));
        });

    /// <summary>
    /// The events argument of the last renderChart call, serialized the way the interop layer
    /// sends it to JavaScript (camelCase).
    /// </summary>
    protected JsonElement RenderedEvents()
    {
        var call = InvocationsOf(JsIdentifiers.RenderChart).Last();
        return JsonSerializer.SerializeToElement(call.Arguments[3], call.Arguments[3]!.GetType(),
            new JsonSerializerOptions(JsonSerializerDefaults.Web));
    }

    protected static string[] Strings(JsonElement? array) =>
        array is { ValueKind: JsonValueKind.Array } a
            ? a.EnumerateArray().Select(e => e.GetString()!).ToArray()
            : Array.Empty<string>();
}

/// <summary>
/// WebAssembly: the runtime is in process, so the formatters are installed and the flag is true.
/// </summary>
public class Issue585InProcessTests : Issue585TestBase
{
    [Fact]
    public void Sync_interop_is_reported_and_the_dotnet_formatters_are_installed()
    {
        Assert.IsAssignableFrom<IJSInProcessRuntime>(Services.GetRequiredService<IJSRuntime>());

        RenderLineChart(p => p
            .Add(c => c.FormatYAxisLabel, v => "$" + v)
            .Add(c => c.FormatXAxisLabel, v => v + "!")
            .Add(c => c.OnBeforeZoom, x => x));

        var events = RenderedEvents();
        Assert.True(events.Get("syncInterop")?.GetBoolean());
        Assert.Empty(Strings(events.Get("unsupportedSyncCallbacks")));
        Assert.True(events.Get("hasBeforeZoom")?.GetBoolean());

        var options = RenderedOptions();
        Assert.Contains("getYAxisLabel", options.Get("yaxis.0.labels.formatter")?.GetRawText());
        Assert.Contains("getXAxisLabel", options.Get("xaxis.labels.formatter")?.GetRawText());
    }
}

/// <summary>
/// Blazor Server, or InteractiveAuto on its server circuit: the runtime is not in process. The chart
/// must still render, without the synchronous callbacks, and tell the script which ones it skipped.
/// </summary>
public class Issue585OutOfProcessTests : Issue585TestBase
{
    public Issue585OutOfProcessTests()
    {
        // Registered last, so the component is injected with a runtime that is not IJSInProcessRuntime,
        // like RemoteJSRuntime on a server circuit. Calls still reach bUnit's recorder.
        Services.AddSingleton<IJSRuntime>(new OutOfProcessJSRuntime(JSInterop.JSRuntime));
    }

    [Fact]
    public void Y_axis_formatter_is_not_installed_and_the_flag_is_false()
    {
        RenderLineChart(p => p.Add(c => c.FormatYAxisLabel, v => "$" + v));

        var events = RenderedEvents();
        Assert.False(events.Get("syncInterop")?.GetBoolean());
        Assert.Equal(new[] { "FormatYAxisLabel" }, Strings(events.Get("unsupportedSyncCallbacks")));

        // No formatter that would call invokeMethod: the axis keeps the chart's default labels.
        Assert.Null(RenderedOptions().Get("yaxis.0.labels.formatter"));
    }

    [Fact]
    public void X_axis_formatter_is_not_installed()
    {
        RenderLineChart(p => p.Add(c => c.FormatXAxisLabel, v => v + "!"));

        Assert.Equal(new[] { "FormatXAxisLabel" }, Strings(RenderedEvents().Get("unsupportedSyncCallbacks")));
        Assert.Null(RenderedOptions().Get("xaxis.labels.formatter"));
    }

    [Fact]
    public void Per_axis_y_formatter_is_not_installed()
    {
        // Issue 595 added FormatYAxisLabelForAxis, which is called synchronously like FormatYAxisLabel.
        RenderLineChart(p => p.Add(c => c.FormatYAxisLabelForAxis, (v, axis) => axis + ":" + v));

        Assert.Equal(new[] { "FormatYAxisLabelForAxis" }, Strings(RenderedEvents().Get("unsupportedSyncCallbacks")));
        Assert.Null(RenderedOptions().Get("yaxis.0.labels.formatter"));
    }

    [Fact]
    public void Before_zoom_callbacks_do_not_throw_and_are_not_wired()
    {
        RenderLineChart(p => p
            .Add(c => c.OnBeforeZoom, x => x)
            .Add(c => c.OnBeforeResetZoom, _ => new SelectionXAxis()));

        var events = RenderedEvents();
        Assert.False(events.Get("hasBeforeZoom")?.GetBoolean());
        Assert.False(events.Get("hasBeforeResetZoom")?.GetBoolean());
        Assert.Equal(new[] { "OnBeforeZoom", "OnBeforeResetZoom" }, Strings(events.Get("unsupportedSyncCallbacks")));
    }

    [Fact]
    public void Chart_without_sync_callbacks_reports_nothing_skipped()
    {
        RenderLineChart(_ => { });

        var events = RenderedEvents();
        Assert.False(events.Get("syncInterop")?.GetBoolean());
        Assert.Empty(Strings(events.Get("unsupportedSyncCallbacks")));
    }

    private sealed class OutOfProcessJSRuntime(IJSRuntime inner) : IJSRuntime
    {
        public ValueTask<TValue> InvokeAsync<TValue>(string identifier, object?[]? args)
            => inner.InvokeAsync<TValue>(identifier, args);

        public ValueTask<TValue> InvokeAsync<TValue>(string identifier, CancellationToken cancellationToken, object?[]? args)
            => inner.InvokeAsync<TValue>(identifier, cancellationToken, args);
    }
}
