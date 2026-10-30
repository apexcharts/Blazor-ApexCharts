// Issue #595: FormatYAxisLabelForAxis formats each Y axis with the axis index the chart wrote
// into its formatter. The repro page labels axis 0 with "A0:" and axis 1 with "A1:".
// The second chart puts two series on axis 0 and one on axis 1, so a tooltip value has to be
// formatted with the axis its series is plotted on, not with the axis that has the series' index.
//
// The formatter needs synchronous JS to .NET interop, so on Blazor Server it does not run: there
// the spec checks that both charts render with their default labels and warn once each instead.
import {
  gotoChart, watchErrors, assert, assertNoErrors, blazorHost, recordSyncWarnings, syncWarnings
} from '../lib/helpers.mjs';

const axisLabels = (page, rel) =>
  page.locator(`#issue595-chart .apexcharts-yaxis[rel='${rel}'] .apexcharts-yaxis-label tspan`).allTextContents();

async function checkAxisLabels(page) {
  // The labels are drawn by the .NET callback, so wait until both axes show the formatted text.
  await page.waitForFunction(() => {
    const texts = rel => Array.from(document.querySelectorAll(
      `#issue595-chart .apexcharts-yaxis[rel='${rel}'] .apexcharts-yaxis-label tspan`)).map(e => e.textContent);
    const a0 = texts(0), a1 = texts(1);
    return a0.length > 0 && a1.length > 0 && a0.some(t => t.startsWith('A0:')) && a1.some(t => t.startsWith('A1:'));
  }, null, { timeout: 30000 });

  const axis0 = (await axisLabels(page, 0)).filter(t => t !== '');
  const axis1 = (await axisLabels(page, 1)).filter(t => t !== '');

  assert(axis0.every(t => t.startsWith('A0:')), `axis 0 labels should all start with A0:, got ${JSON.stringify(axis0)}`);
  assert(axis1.every(t => t.startsWith('A1:')), `axis 1 labels should all start with A1:, got ${JSON.stringify(axis1)}`);

  // Axis 0 is bound to Revenue (up to 2100) and axis 1 to Margin (up to 30), so the scales differ.
  const max = arr => Math.max(...arr.map(t => Number(t.split(':')[1])));
  assert(max(axis0) >= 2000, `axis 0 should carry the Revenue scale, got ${JSON.stringify(axis0)}`);
  assert(max(axis1) <= 100, `axis 1 should carry the Margin scale, got ${JSON.stringify(axis1)}`);
}

async function checkTooltipValues(page) {
  const chart = '#issue595-tooltip-chart';
  await page.waitForSelector(`${chart} .apexcharts-yaxis[rel="1"] .apexcharts-yaxis-label`, { timeout: 30000 });

  await page.locator(chart).scrollIntoViewIfNeeded();
  const grid = await page.locator(`${chart} .apexcharts-grid`).first().boundingBox();
  assert(grid, 'the tooltip chart has no grid to hover');

  // Hover over the second category and wait until the shared tooltip lists all three series
  // with formatted values.
  const readRows = () => page.evaluate(sel => Array.from(document.querySelectorAll(
    `${sel} .apexcharts-tooltip .apexcharts-tooltip-series-group`)).map(g => ({
      name: (g.querySelector('.apexcharts-tooltip-text-y-label')?.textContent || '').replace(/:\s*$/, '').trim(),
      value: (g.querySelector('.apexcharts-tooltip-text-y-value')?.textContent || '').trim(),
    })).filter(r => r.name !== ''), chart);

  await page.mouse.move(grid.x + grid.width * 0.375, grid.y + grid.height / 2);
  await page.waitForFunction(sel => {
    const values = Array.from(document.querySelectorAll(`${sel} .apexcharts-tooltip .apexcharts-tooltip-text-y-value`))
      .map(e => e.textContent.trim()).filter(t => t !== '');
    return values.length >= 3 && values.every(t => /^A\d:/.test(t));
  }, chart, { timeout: 15000 });

  const rows = await readRows();
  const byName = Object.fromEntries(rows.map(r => [r.name, r.value]));
  const expected = { Alpha: 'A0:', Beta: 'A0:', Gamma: 'A1:' };
  for (const [name, prefix] of Object.entries(expected)) {
    assert(typeof byName[name] === 'string' && byName[name].startsWith(prefix),
      `tooltip value of ${name} should start with ${prefix} (the axis it is plotted on), got ${JSON.stringify(rows)}`);
  }
}

async function checkIndexResolver(page) {
  const result = await page.evaluate(() => {
    const api = window.blazor_apexchart;
    const w = { globals: { axisCharts: true, isBarHorizontal: false, seriesYAxisReverseMap: [0, 0, 1] } };
    return {
      tickLabel: api.getYAxisIndexForValue(3, 1),
      measuring: api.getYAxisIndexForValue({ seriesIndex: 1, dataPointIndex: -1, w }, 1),
      tooltipBeta: api.getYAxisIndexForValue({ seriesIndex: 1, dataPointIndex: 2, w }, 1),
      tooltipGamma: api.getYAxisIndexForValue({ seriesIndex: 2, dataPointIndex: 2, w }, 0),
      horizontal: api.getYAxisIndexForValue({ seriesIndex: 2, dataPointIndex: 2, w: { globals: { ...w.globals, isBarHorizontal: true } } }, 0),
      unmapped: api.getYAxisIndexForValue({ seriesIndex: 5, dataPointIndex: 0, w }, 1),
    };
  });
  const expected = { tickLabel: 1, measuring: 1, tooltipBeta: 0, tooltipGamma: 1, horizontal: 0, unmapped: 1 };
  assert(JSON.stringify(result) === JSON.stringify(expected),
    `getYAxisIndexForValue: expected ${JSON.stringify(expected)}, got ${JSON.stringify(result)}`);
}

// FormatYAxisLabelForAxis follows the same error policy as FormatYAxisLabel and FormatXAxisLabel:
// an exception from the user's formatter is rethrown, every time, and is not reported as a host
// limitation; the "no synchronous interop" error of Blazor Server leaves the raw value and gives one
// warning for the chart. The formatter gets exactly the value and the axis index.
async function checkErrorReporting(page) {
  const result = await page.evaluate(() => {
    const api = window.blazor_apexchart;
    const logged = [];
    const { warn, error } = console;
    const id = 'issue595-throwing-chart';
    const calls = [];
    let failure;
    api.dotNetRefs.set(id, { invokeMethod: (...args) => { calls.push(args); throw failure; } });
    console.warn = m => logged.push(['warn', String(m)]);
    console.error = m => logged.push(['error', String(m)]);
    const call = (value, index) => {
      try {
        return { returned: api.getYAxisLabelForAxis(value, index, { config: { chart: { id } } }, 1) };
      } catch (e) {
        return { thrown: e.message };
      }
    };
    try {
      failure = new Error('System.InvalidOperationException: formatter bug');
      const userErrors = [call(7, 0), call(8, 1)];
      failure = new Error('The current dispatcher does not support synchronous calls from JS to .NET. Use invokeMethodAsync instead.');
      const noSync = [call(9, 2), call(10, 3)];
      return { userErrors, noSync, logged, calls };
    } finally {
      console.warn = warn;
      console.error = error;
      api.dotNetRefs.delete(id);
      api.destroyChart(id);
    }
  });

  assert(result.userErrors.every(r => r.thrown === 'System.InvalidOperationException: formatter bug'),
    `an exception from the formatter should be rethrown each time, got ${JSON.stringify(result.userErrors)}`);
  assert(result.noSync.every((r, i) => r.returned === 9 + i),
    `without synchronous interop the raw value should be kept, got ${JSON.stringify(result.noSync)}`);
  assert(JSON.stringify(result.calls[0]) === JSON.stringify(['JSGetFormattedYAxisValueForAxis', 7, 1]),
    `the formatter should get the value and the axis index, got ${JSON.stringify(result.calls)}`);

  const errors = result.logged.filter(([kind]) => kind === 'error');
  const warnings = result.logged.filter(([kind]) => kind === 'warn');
  assert(errors.length === 0, `a formatter exception is rethrown, not logged: ${JSON.stringify(result.logged)}`);
  assert(warnings.length === 1 && /FormatYAxisLabelForAxis/.test(warnings[0][1]) && /cannot call \.NET synchronously/.test(warnings[0][1]),
    `the synchronous interop error should give exactly one warning naming FormatYAxisLabelForAxis, got ${JSON.stringify(result.logged)}`);
}

// On Blazor Server the .NET formatter never runs: both charts render both axes with the default
// labels and default tooltip values, and each chart warns once, naming FormatYAxisLabelForAxis.
async function checkServerDefaults(page) {
  await page.waitForFunction(() => (window.__apexSyncWarnings || []).length >= 2, null, { timeout: 30000 });
  await page.waitForSelector('#issue595-tooltip-chart .apexcharts-yaxis[rel="1"] .apexcharts-yaxis-label', { timeout: 30000 });

  const charts = await page.evaluate(() => ['#issue595-chart', '#issue595-tooltip-chart'].map(sel => {
    const instance = (window.Apex._chartInstances || []).find(c => document.querySelector(sel).contains(c.chart.el));
    const labels = rel => Array.from(document.querySelectorAll(`${sel} .apexcharts-yaxis[rel='${rel}'] .apexcharts-yaxis-label`))
      .map(e => e.textContent);
    return { sel, id: instance ? instance.id : null, axis0: labels(0), axis1: labels(1) };
  }));
  for (const c of charts) {
    assert(c.id, `no chart instance in ${c.sel}`);
    assert(c.axis0.length > 0 && c.axis1.length > 0, `${c.sel} should render labels on both axes, got ${JSON.stringify(c)}`);
    assert([...c.axis0, ...c.axis1].every(t => !/^A\d:/.test(t)), `${c.sel} shows .NET formatted labels on Blazor Server: ${JSON.stringify(c)}`);
  }

  const warnings = await syncWarnings(page);
  for (const c of charts) {
    const mine = warnings.filter(w => w.chartId === c.id);
    assert(mine.length === 1 && JSON.stringify(mine[0].callbacks) === '["FormatYAxisLabelForAxis"]',
      `${c.sel} should warn exactly once, naming FormatYAxisLabelForAxis, got ${JSON.stringify(warnings)}`);
  }
  assert(warnings.length === charts.length, `unexpected warnings: ${JSON.stringify(warnings)}`);

  const chart = '#issue595-tooltip-chart';
  await page.locator(chart).scrollIntoViewIfNeeded();
  const grid = await page.locator(`${chart} .apexcharts-grid`).first().boundingBox();
  assert(grid, 'the tooltip chart has no grid to hover');
  await page.mouse.move(grid.x + grid.width * 0.375, grid.y + grid.height / 2);
  await page.waitForFunction(sel => Array.from(document.querySelectorAll(`${sel} .apexcharts-tooltip .apexcharts-tooltip-text-y-value`))
    .map(e => e.textContent.trim()).filter(t => /^\d/.test(t)).length >= 3, chart, { timeout: 15000 });
}

export default async function (page, BASE) {
  const realErrors = watchErrors(page);

  await recordSyncWarnings(page);
  await gotoChart(page, BASE, 'issues/595', '#issue595-chart .apexcharts-yaxis[rel="1"] .apexcharts-yaxis-label');

  if (await blazorHost(page) === 'server') {
    await checkServerDefaults(page);
  } else {
    await checkAxisLabels(page);
    await checkTooltipValues(page);
    assert((await syncWarnings(page)).length === 0, 'the WebAssembly demo logged sync interop warnings');
  }
  await checkIndexResolver(page);
  await checkErrorReporting(page);

  assertNoErrors(realErrors, 'issue-595');
}
