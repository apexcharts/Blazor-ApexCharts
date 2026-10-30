using System.Text.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;

namespace ApexCharts.Tests.TestHelpers;

/// <summary>
/// Interop identifiers the component calls, so tests do not repeat string literals.
/// </summary>
public static class JsIdentifiers
{
    /// <summary>The dynamic ES module import of blazor-apexcharts.js.</summary>
    public const string Import = "import";

    /// <summary>Module export that returns the <c>blazor_apexchart</c> object every chart call goes through.</summary>
    public const string GetApexCharts = "get_apexcharts";

    /// <summary>Creates the chart. Arguments: dotnet ref, container element, options JSON, events.</summary>
    public const string RenderChart = "blazor_apexchart.renderChart";

    /// <summary>Updates options. Arguments: chart id, options JSON, redrawPaths, animate, updateSyncedCharts, zoom.</summary>
    public const string UpdateOptions = "blazor_apexchart.updateOptions";

    /// <summary>Updates series. Arguments: chart id, series JSON, animate.</summary>
    public const string UpdateSeries = "blazor_apexchart.updateSeries";

    /// <summary>Destroys the chart. Arguments: chart id.</summary>
    public const string DestroyChart = "blazor_apexchart.destroyChart";

    /// <summary>Applies global options. Arguments: options JSON.</summary>
    public const string SetGlobalOptions = "blazor_apexchart.setGlobalOptions";
}

/// <summary>
/// Base class for bUnit tests of the chart components. JS interop runs in loose mode, so every call
/// the component makes is recorded and answered with a default value (an <c>IJSObjectReference</c>
/// for the module import), and the library services are registered through <c>AddApexCharts</c>.
/// </summary>
/// <remarks>
/// Derive a test class from it and call <see cref="RenderChart{TItem}"/>:
/// <code>
/// public class MyTests : ApexChartsTestContext
/// {
///     [Fact]
///     public void Renders()
///     {
///         var cut = RenderChart&lt;Sale&gt;(p =&gt; p
///             .Add(c =&gt; c.Options, new ApexChartOptions&lt;Sale&gt;())
///             .AddChildContent&lt;ApexPointSeries&lt;Sale&gt;&gt;(s =&gt; s
///                 .Add(x =&gt; x.Items, SampleData.Sales())
///                 .Add(x =&gt; x.XValue, e =&gt; e.Month)
///                 .Add(x =&gt; x.YValue, e =&gt; e.Amount)));
///         var options = RenderedOptions();
///         Assert.Equal("bar", options.Get("chart.type")?.GetString());
///     }
/// }
/// </code>
/// </remarks>
public abstract class ApexChartsTestContext : BunitContext
{
    /// <summary>
    /// Creates the context with loose JS interop and the ApexCharts services registered.
    /// </summary>
    /// <param name="configureService">Optional service options, for example global options or a license key.</param>
    protected ApexChartsTestContext(Action<ApexChartsServiceOptions>? configureService = null)
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        Services.AddApexCharts(configureService);
    }

    /// <summary>
    /// Renders an <see cref="ApexChart{TItem}"/> and waits until the component has called
    /// <c>renderChart</c> (the call happens after an async yield, so it is not there synchronously).
    /// </summary>
    protected IRenderedComponent<ApexChart<TItem>> RenderChart<TItem>(
        Action<ComponentParameterCollectionBuilder<ApexChart<TItem>>> parameters) where TItem : class
    {
        var cut = Render(parameters);
        cut.WaitForAssertion(() => Assert.NotEmpty(InvocationsOf(JsIdentifiers.RenderChart)));
        return cut;
    }

    /// <summary>
    /// Every recorded interop call with the given identifier, in call order. Calls made on the
    /// imported module object are included.
    /// </summary>
    protected IReadOnlyList<JSRuntimeInvocation> InvocationsOf(string identifier)
        => JSInterop.Invocations.Where(i => i.Identifier == identifier).ToList();

    /// <summary>
    /// The options JSON passed to the most recent <c>renderChart</c> call, parsed.
    /// </summary>
    protected JsonElement RenderedOptions()
    {
        var call = InvocationsOf(JsIdentifiers.RenderChart).LastOrDefault();
        Assert.True(call.Identifier is not null, "renderChart was never invoked");
        return ChartJson.Parse((string)call.Arguments[2]!);
    }

    /// <summary>
    /// The JSON string argument at <paramref name="argumentIndex"/> of the most recent call to
    /// <paramref name="identifier"/>, parsed.
    /// </summary>
    protected JsonElement LastJsonArgument(string identifier, int argumentIndex)
    {
        var call = InvocationsOf(identifier).LastOrDefault();
        Assert.True(call.Identifier is not null, $"{identifier} was never invoked");
        return ChartJson.Parse((string)call.Arguments[argumentIndex]!);
    }
}
