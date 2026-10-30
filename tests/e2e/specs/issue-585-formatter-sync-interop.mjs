// Issue 585: "The current dispatcher does not support synchronous calls from JS to .NET" thrown
// from getYAxisLabel. Under Blazor Server, and on the server circuit InteractiveAuto uses before the
// WebAssembly runtime is cached, DotNetObjectReference.invokeMethod throws. The .NET formatters and
// the before zoom callbacks used it unguarded, so the exception escaped the chart render.
//
// The demo runs on WebAssembly, so the server dispatcher is simulated with a dotnet ref whose
// invokeMethod throws that same error, passed to the real renderChart in the page.
import { gotoChart, watchErrors, assert, assertNoErrors } from '../lib/helpers.mjs';

const DISPATCHER_ERROR = 'The current dispatcher does not support synchronous calls from JS to .NET. Use invokeDotNetMethodAsync instead.';

export default async function (page, BASE) {
  const realErrors = watchErrors(page);

  // Collect the library's warnings from the first chart render on, so a false warning on the real
  // WebAssembly path is caught too.
  await page.addInitScript(() => {
    window.__issue585Warnings = [];
    const warn = console.warn.bind(console);
    console.warn = (...args) => {
      if (String(args[0]).startsWith('Blazor-ApexCharts:')) window.__issue585Warnings.push(String(args[0]));
      warn(...args);
    };
  });

  await gotoChart(page, BASE, 'features/formatters');
  await page.waitForFunction(() => window.blazor_apexchart && window.blazor_apexchart.dotNetRefs.size > 0, null, { timeout: 30000 });

  // 1. The real WebAssembly path still formats through .NET: one chart on the page uses
  //    FormatYAxisLabel="$" + N0.
  const formatted = await page.evaluate(() => {
    const api = window.blazor_apexchart;
    return [...api.dotNetRefs.keys()].map(id => api.getYAxisLabel(1234, 0, { config: { chart: { id } } }));
  });
  assert(formatted.includes('$1,234'), `expected a .NET formatted "$1,234" label, got ${JSON.stringify(formatted)}`);

  // The chart calls the formatters with undefined (tooltips on series of unequal length, missing
  // range starts). The value must still reach .NET as one argument (sent as null), which
  // JSGetFormattedYAxisValue / JSGetFormattedXAxisValue turn into "".
  const empty = await page.evaluate(() => {
    const api = window.blazor_apexchart;
    return [...api.dotNetRefs.keys()].map(id => {
      const w = { config: { chart: { id } } };
      return [api.getYAxisLabel(undefined, 0, w), api.getXAxisLabel(undefined, 0, w)];
    });
  });
  assert(empty.length > 0 && empty.every(([y, x]) => y === '' && x === ''),
    `expected "" for an undefined value on WebAssembly, got ${JSON.stringify(empty)}`);

  // WebAssembly can call .NET synchronously, so none of the real charts may warn.
  const wasmWarnings = await page.evaluate(() => window.__issue585Warnings.slice());
  assert(wasmWarnings.length === 0, `the WebAssembly demo logged sync interop warnings: ${JSON.stringify(wasmWarnings)}`);

  // 2. getYAxisLabel / getXAxisLabel never throw when invokeMethod throws, return the raw value,
  //    and warn once per chart rather than once per label.
  const direct = await page.evaluate((message) => {
    const api = window.blazor_apexchart;
    const id = 'issue585-direct';
    api.dotNetRefs.set(id, { invokeMethod() { throw new Error(message); }, invokeMethodAsync() { return Promise.resolve(); } });
    const w = { config: { chart: { id } } };
    const out = { values: [], error: null };
    try {
      out.values.push(api.getYAxisLabel(42, 0, w));
      out.values.push(api.getYAxisLabel(43, 1, w));
      out.values.push(api.getXAxisLabel(7, undefined, w));
    } catch (e) {
      out.error = e.message;
    }
    api.dotNetRefs.delete(id);
    out.warnings = window.__issue585Warnings.filter(m => m.includes(id));
    return out;
  }, DISPATCHER_ERROR);
  assert(direct.error === null, `getYAxisLabel threw: ${direct.error}`);
  assert(JSON.stringify(direct.values) === '[42,43,7]', `expected the raw values back, got ${JSON.stringify(direct.values)}`);
  assert(direct.warnings.length === 2, `expected one warning for FormatYAxisLabel and one for FormatXAxisLabel, got ${JSON.stringify(direct.warnings)}`);
  assert(direct.warnings[0].includes('FormatYAxisLabel') && direct.warnings[0].includes('Labels.Formatter'),
    `warning is not actionable: ${direct.warnings[0]}`);
  assert(direct.warnings[0].includes('cannot call .NET synchronously') && direct.warnings[0].includes('MAUI'),
    `warning does not name the hosts it applies to: ${direct.warnings[0]}`);

  // 2b. On a host that can call .NET synchronously, an exception from the user's callback is not
  //     swallowed: it surfaces as before and does not produce the "cannot call .NET synchronously"
  //     warning. A formatter always passes exactly one argument, even for an undefined value.
  const userError = await page.evaluate(() => {
    const api = window.blazor_apexchart;
    const id = 'issue585-user-error';
    const argCounts = [];
    api.dotNetRefs.set(id, {
      invokeMethod(...args) {
        argCounts.push(args.length);
        if (args[1] === 'boom') throw new Error('callback failed');
        return 'ok';
      },
      invokeMethodAsync() { return Promise.resolve(); }
    });
    const w = { config: { chart: { id } } };
    const out = { error: null, argCounts };
    out.undefinedValue = api.getYAxisLabel(undefined, 0, w);
    try {
      api.getYAxisLabel('boom', 0, w);
    } catch (e) {
      out.error = e.message;
    }
    api.dotNetRefs.delete(id);
    out.warnings = window.__issue585Warnings.filter(m => m.includes(id));
    return out;
  });
  assert(userError.undefinedValue === 'ok', `undefined value did not reach .NET: ${JSON.stringify(userError)}`);
  assert(JSON.stringify(userError.argCounts) === '[2,2]', `invokeMethod got the wrong argument count: ${JSON.stringify(userError.argCounts)}`);
  assert(userError.error === 'callback failed', `an exception from the user's callback was swallowed: ${JSON.stringify(userError)}`);
  assert(userError.warnings.length === 0, `a callback exception produced a sync interop warning: ${JSON.stringify(userError.warnings)}`);

  // 3. A whole chart render with a .NET y-axis formatter and before zoom handlers completes when
  //    the dispatcher cannot call .NET synchronously: labels are drawn and zoomX does not throw.
  const render = await page.evaluate((message) => {
    const api = window.blazor_apexchart;
    const id = 'issue585-render';
    const host = document.createElement('div');
    host.id = 'issue585-host';
    host.style.width = '600px';
    document.body.appendChild(host);

    const ref = { invokeMethod() { throw new Error(message); }, invokeMethodAsync() { return Promise.resolve(); } };
    const options = {
      chart: { id, type: 'line', animations: { enabled: false }, zoom: { enabled: true } },
      series: [{ name: 'a', data: [{ x: 1, y: 10 }, { x: 2, y: 20 }, { x: 3, y: 30 }, { x: 4, y: 25 }] }],
      xaxis: { type: 'numeric' },
      yaxis: [{ labels: { formatter: { '@eval': 'function (value, index, w) { return window.blazor_apexchart.getYAxisLabel(value, index, w); }' } } }]
    };
    api.renderChart(ref, host, JSON.stringify(options), { syncInterop: true, hasBeforeZoom: true, hasBeforeResetZoom: true });
    return id;
  }, DISPATCHER_ERROR);

  await page.locator('#issue585-host .apexcharts-yaxis-label').first().waitFor({ timeout: 15000 });

  const zoom = await page.evaluate((id) => {
    try {
      window.ApexCharts.getChartByID(id).zoomX(1.5, 3.5);
      return null;
    } catch (e) {
      return e.message;
    }
  }, render);
  assert(zoom === null, `zoomX threw through beforeZoom: ${zoom}`);

  const zoomWarnings = await page.evaluate((id) => window.__issue585Warnings.filter(m => m.includes(id) && m.includes('OnBeforeZoom')), render);
  assert(zoomWarnings.length === 1, `expected one OnBeforeZoom warning, got ${JSON.stringify(zoomWarnings)}`);
  assert(!zoomWarnings[0].includes('Labels.Formatter'), `the zoom warning points at axis label formatters: ${zoomWarnings[0]}`);

  // 4. When .NET reports no sync interop, the script does not even try invokeMethod and warns once,
  //    naming the callbacks .NET skipped.
  const flagged = await page.evaluate(() => {
    const api = window.blazor_apexchart;
    const id = 'issue585-flagged';
    const host = document.createElement('div');
    host.id = 'issue585-flagged-host';
    host.style.width = '600px';
    document.body.appendChild(host);

    window.__issue585SyncCalls = 0;
    const ref = { invokeMethod() { window.__issue585SyncCalls++; return 'x'; }, invokeMethodAsync() { return Promise.resolve(); } };
    const options = {
      chart: { id, type: 'line', animations: { enabled: false } },
      series: [{ name: 'a', data: [10, 20, 30] }],
      yaxis: [{ labels: { formatter: { '@eval': 'function (value, index, w) { return window.blazor_apexchart.getYAxisLabel(value, index, w); }' } } }]
    };
    const events = { syncInterop: false, unsupportedSyncCallbacks: ['FormatYAxisLabel'] };
    api.renderChart(ref, host, JSON.stringify(options), events);
    api.renderChart(ref, host, JSON.stringify(options), events); // a re-render must not warn again
    return id;
  });

  await page.locator('#issue585-flagged-host .apexcharts-yaxis-label').first().waitFor({ timeout: 15000 });
  const flaggedState = await page.evaluate((id) => ({
    warnings: window.__issue585Warnings.filter(m => m.includes(id)),
    syncCalls: window.__issue585SyncCalls
  }), flagged);
  assert(flaggedState.syncCalls === 0, `invokeMethod was called ${flaggedState.syncCalls} times although .NET reported no sync interop`);
  assert(flaggedState.warnings.length === 1, `expected exactly one warning for the flagged chart, got ${JSON.stringify(flaggedState.warnings)}`);
  assert(flaggedState.warnings[0].includes('FormatYAxisLabel'), `warning does not name the skipped callback: ${flaggedState.warnings[0]}`);

  await page.evaluate(() => {
    window.blazor_apexchart.destroyChart('issue585-render');
    window.blazor_apexchart.destroyChart('issue585-flagged');
  });

  assertNoErrors(realErrors, 'issue-585');
}
