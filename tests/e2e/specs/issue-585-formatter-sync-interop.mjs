// Issue 585: "The current dispatcher does not support synchronous calls from JS to .NET" thrown
// from getYAxisLabel. Under Blazor Server, and on the server circuit InteractiveAuto uses before the
// WebAssembly runtime is cached, DotNetObjectReference.invokeMethod throws. The .NET formatters and
// the before zoom callbacks used it unguarded, so the exception escaped the chart render.
//
// The checks that call the script directly simulate the server dispatcher with a dotnet ref whose
// invokeMethod throws that same error, passed to the real renderChart in the page, so they run on
// any host. The checks against the demo's own charts depend on the host: on WebAssembly the .NET
// formatters run; on Blazor Server the charts keep their default labels and warn once per callback.
import {
  gotoChart, watchErrors, assert, assertNoErrors, blazorHost, recordSyncWarnings, syncWarnings
} from '../lib/helpers.mjs';

const DISPATCHER_ERROR = 'The current dispatcher does not support synchronous calls from JS to .NET. Use invokeDotNetMethodAsync instead.';

export default async function (page, BASE) {
  const realErrors = watchErrors(page);

  // Collect the library's warnings from the first chart render on, so a false warning on the real
  // WebAssembly path is caught too.
  await recordSyncWarnings(page);

  await gotoChart(page, BASE, 'features/formatters');
  await page.waitForFunction(() => window.blazor_apexchart && window.blazor_apexchart.dotNetRefs.size > 0, null, { timeout: 30000 });

  const host = blazorHost();
  const demoWarnings = host === 'server' ? await checkServerDemo(page) : await checkWasmDemo(page);

  await checkDirectCalls(page);

  // The calls above, made against charts the spec created, did not add warnings for the demo's charts.
  const demoAfter = (await syncWarnings(page)).filter(w => !String(w.chartId).startsWith('issue585-'));
  assert(JSON.stringify(demoAfter) === JSON.stringify(demoWarnings),
    `the demo charts warned again: ${JSON.stringify(demoAfter)}`);

  assertNoErrors(realErrors, 'issue-585');
}

// 1. On WebAssembly the real path formats through .NET and none of the demo's charts warn.
async function checkWasmDemo(page) {
  // One chart on the page uses FormatYAxisLabel="$" + N0.
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
  const wasmWarnings = await syncWarnings(page);
  assert(wasmWarnings.length === 0, `the WebAssembly demo logged sync interop warnings: ${JSON.stringify(wasmWarnings)}`);
  return wasmWarnings;
}

// The .NET formatters the formatters page sets, one entry per chart: FormatYAxisLabel ("Use DotNet"),
// FormatYAxisLabel and FormatXAxisLabel ("Use DotNet for X Axis"), FormatYAxisLabelForAxis ("Use
// DotNet per Y Axis").
const SERVER_EXPECTED = [
  ['FormatXAxisLabel', 'FormatYAxisLabel'],
  ['FormatYAxisLabel'],
  ['FormatYAxisLabelForAxis'],
];

// The name of the script function behind each .NET formatter.
const FORMATTER_FUNCTIONS = {
  FormatYAxisLabel: 'getYAxisLabel',
  FormatXAxisLabel: 'getXAxisLabel',
  FormatYAxisLabelForAxis: 'getYAxisLabelForAxis',
};

// 1. On Blazor Server the .NET formatters cannot run: each chart that sets one renders with its
//    default labels and warns exactly once per callback, and calling the formatter functions for it
//    returns the raw value without trying .NET or warning again.
async function checkServerDemo(page) {
  await page.waitForFunction(n => (window.__apexSyncWarnings || []).length >= n, SERVER_EXPECTED.length, { timeout: 30000 });
  const warnings = await syncWarnings(page);

  const byChart = new Map();
  for (const w of warnings) {
    assert(w.chartId, `unexpected warning: ${w.message}`);
    byChart.set(w.chartId, [...(byChart.get(w.chartId) || []), ...w.callbacks]);
  }
  const perChart = [...byChart.values()].map(names => names.slice().sort());
  for (const names of perChart) {
    assert(new Set(names).size === names.length, `a chart warned twice for one callback: ${JSON.stringify(warnings)}`);
  }
  const sorted = list => list.map(names => names.join(',')).sort();
  assert(JSON.stringify(sorted(perChart)) === JSON.stringify(sorted(SERVER_EXPECTED)),
    `expected one warning per chart per .NET formatter (${JSON.stringify(SERVER_EXPECTED)}), got ${JSON.stringify(warnings)}`);
  assert(warnings.every(w => w.message.includes('Labels.Formatter')), `a formatter warning is not actionable: ${JSON.stringify(warnings)}`);

  const result = await page.evaluate(({ charts, functions }) => {
    const api = window.blazor_apexchart;
    const before = window.__apexSyncWarnings.length;
    const out = { labels: {}, values: [], error: null };
    try {
      for (const [id, names] of charts) {
        const w = { config: { chart: { id } } };
        for (const name of names) out.values.push(api[functions[name]](1234, 0, w, 0));
        const chart = window.ApexCharts.getChartByID(id);
        out.labels[id] = chart ? Array.from(chart.el.querySelectorAll('.apexcharts-yaxis-label')).map(e => e.textContent) : null;
      }
    } catch (e) {
      out.error = e.message;
    }
    out.newWarnings = window.__apexSyncWarnings.slice(before);
    return out;
  }, { charts: [...byChart.entries()], functions: FORMATTER_FUNCTIONS });

  assert(result.error === null, `a formatter function threw on Blazor Server: ${result.error}`);
  assert(result.values.length > 0 && result.values.every(v => v === 1234), `expected the raw value back, got ${JSON.stringify(result.values)}`);
  assert(result.newWarnings.length === 0, `calling the formatters warned again: ${JSON.stringify(result.newWarnings)}`);
  for (const [id, labels] of Object.entries(result.labels)) {
    assert(Array.isArray(labels) && labels.length > 0, `chart ${id} did not render y-axis labels: ${JSON.stringify(labels)}`);
    assert(labels.every(t => !/[$%_]/.test(t)), `chart ${id} shows .NET formatted labels on Blazor Server: ${JSON.stringify(labels)}`);
  }
  return warnings;
}

async function checkDirectCalls(page) {
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
    out.warnings = window.__apexSyncWarnings.filter(m => m.includes(id));
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
    out.warnings = window.__apexSyncWarnings.filter(m => m.includes(id));
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

  const zoomWarnings = await page.evaluate((id) => window.__apexSyncWarnings.filter(m => m.includes(id) && m.includes('OnBeforeZoom')), render);
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
    const events = { syncInterop: false, unsupportedSyncCallbacks: ['FormatYAxisLabel', 'OnBeforeZoom'] };
    api.renderChart(ref, host, JSON.stringify(options), events);
    api.renderChart(ref, host, JSON.stringify(options), events); // a re-render must not warn again
    return id;
  });

  await page.locator('#issue585-flagged-host .apexcharts-yaxis-label').first().waitFor({ timeout: 15000 });
  // The chart's own label pass and a direct call both go through getYAxisLabel, which the render
  // warning already named, so neither adds a warning.
  const flaggedState = await page.evaluate((id) => ({
    direct: window.blazor_apexchart.getYAxisLabel(5, 0, { config: { chart: { id } } }),
    warnings: window.__apexSyncWarnings.filter(m => m.includes(id)),
    syncCalls: window.__issue585SyncCalls
  }), flagged);
  assert(flaggedState.direct === 5, `expected the raw value from the flagged chart, got ${JSON.stringify(flaggedState.direct)}`);
  assert(flaggedState.syncCalls === 0, `invokeMethod was called ${flaggedState.syncCalls} times although .NET reported no sync interop`);
  assert(flaggedState.warnings.length === 1, `expected exactly one warning for the flagged chart, got ${JSON.stringify(flaggedState.warnings)}`);
  assert(flaggedState.warnings[0].includes('FormatYAxisLabel') && flaggedState.warnings[0].includes('OnBeforeZoom'),
    `warning does not name the skipped callbacks: ${flaggedState.warnings[0]}`);

  // 5. Disposing a chart (destroyChart from the component, not the re-render inside renderChart)
  //    forgets its warnings: nothing about the chart is kept, and a new chart under the same id warns
  //    again.
  const pruned = await page.evaluate(() => {
    const api = window.blazor_apexchart;
    const id = 'issue585-flagged';
    const remembered = () => [...api.syncWarnings.keys()].some(k => String(k).includes(id));
    const out = { before: remembered() };
    api.destroyChart(id);
    out.afterDestroy = remembered();

    const ref = { invokeMethod() { return 'x'; }, invokeMethodAsync() { return Promise.resolve(); } };
    const options = { chart: { id, type: 'line', animations: { enabled: false } }, series: [{ name: 'a', data: [1, 2, 3] }] };
    api.renderChart(ref, document.getElementById('issue585-flagged-host'), JSON.stringify(options),
      { syncInterop: false, unsupportedSyncCallbacks: ['FormatYAxisLabel'] });
    out.warnings = window.__apexSyncWarnings.filter(m => m.includes(id)).length;

    api.destroyChart('issue585-render');
    api.destroyChart(id);
    out.leftOver = [...api.syncWarnings.keys()].filter(k => ['issue585-render', id].some(x => String(k).includes(x)));
    return out;
  });
  assert(pruned.before === true, `the flagged chart's warning was not recorded: ${JSON.stringify(pruned)}`);
  assert(pruned.afterDestroy === false, `destroyChart kept the chart's warnings: ${JSON.stringify(pruned)}`);
  assert(pruned.warnings === 2, `a new chart under a disposed chart's id should warn again, got ${pruned.warnings} warnings in all`);
  assert(pruned.leftOver.length === 0, `warnings kept after destroy: ${JSON.stringify(pruned.leftOver)}`);
}
