using System;
using Microsoft.JSInterop;

namespace ApexCharts.Internal;

/// <summary>
/// Classifies JavaScript interop failures, so every call site that ignores the ones a closed page
/// causes agrees on what they are.
/// </summary>
internal static class JSInteropErrors
{
    /// <summary>
    /// True when the browser side is gone: the circuit is disconnected (a closed or refreshed page in
    /// Blazor Server) or the JavaScript object reference was already released.
    /// </summary>
    /// <remarks>
    /// Safe to ignore on any call, since there is no page left to update. A
    /// <see cref="JSException"/> is not included: outside teardown it reports a real error in the
    /// chart's JavaScript and must reach the caller.
    /// </remarks>
    /// <param name="ex">The exception an interop call threw.</param>
    public static bool IsBrowserGone(Exception ex) =>
        ex is JSDisconnectedException || ex is ObjectDisposedException;

    /// <summary>
    /// True for every failure expected while a chart is torn down: the browser is gone
    /// (<see cref="IsBrowserGone"/>), the call was cancelled or timed out, or the JavaScript side
    /// threw because the chart or its element no longer exists.
    /// </summary>
    /// <remarks>
    /// Use it only on teardown calls (destroying the chart, releasing the module), where nothing can be
    /// done about a failure and the caller is going away. Other calls filter with <see cref="IsBrowserGone"/>.
    /// </remarks>
    /// <param name="ex">The exception an interop call threw.</param>
    public static bool IsExpectedDuringTeardown(Exception ex) =>
        IsBrowserGone(ex) || ex is OperationCanceledException || ex is JSException;
}
