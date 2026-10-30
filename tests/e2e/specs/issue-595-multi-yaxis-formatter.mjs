// Issue #595: FormatYAxisLabelForAxis formats each Y axis with the axis index the chart wrote
// into its formatter. The repro page labels axis 0 with "A0:" and axis 1 with "A1:".
// The second chart puts two series on axis 0 and one on axis 1, so a tooltip value has to be
// formatted with the axis its series is plotted on, not with the axis that has the series' index.
import { gotoChart, watchErrors, assert, assertNoErrors } from '../lib/helpers.mjs';

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

// An exception from the user's formatter is reported as such, every time, and only the Blazor
// Server "no synchronous interop" error gets the once-per-page WebAssembly warning.
async function checkErrorReporting(page) {
  const result = await page.evaluate(() => {
    const api = window.blazor_apexchart;
    const logged = [];
    const { warn, error } = console;
    const id = 'issue595-throwing-chart';
    let failure;
    api.yAxisForAxisWarned = false;
    api.dotNetRefs.set(id, { invokeMethod: () => { throw failure; } });
    console.warn = m => logged.push(['warn', String(m)]);
    console.error = m => logged.push(['error', String(m)]);
    try {
      const w = { config: { chart: { id } } };
      failure = new Error('System.InvalidOperationException: formatter bug');
      const returned = api.getYAxisLabelForAxis(7, 0, w, 0);
      api.getYAxisLabelForAxis(8, 1, w, 0);
      failure = new Error('The current dispatcher does not support synchronous calls from JS to .NET. Use invokeMethodAsync instead.');
      api.getYAxisLabelForAxis(9, 2, w, 0);
      api.getYAxisLabelForAxis(10, 3, w, 0);
      return { returned, logged };
    } finally {
      console.warn = warn;
      console.error = error;
      api.dotNetRefs.delete(id);
    }
  });

  assert(result.returned === 7, `a failing formatter should leave the raw value, got ${JSON.stringify(result.returned)}`);
  const errors = result.logged.filter(([kind]) => kind === 'error');
  const warnings = result.logged.filter(([kind]) => kind === 'warn');
  assert(errors.length === 2 && errors.every(([, m]) => /FormatYAxisLabelForAxis failed/.test(m)),
    `each exception from the formatter should be logged as a failure, got ${JSON.stringify(result.logged)}`);
  assert(warnings.length === 1 && warnings.every(([, m]) => /WebAssembly/.test(m)),
    `the synchronous interop error should give exactly one WebAssembly warning, got ${JSON.stringify(result.logged)}`);
  assert(!errors.some(([, m]) => /WebAssembly/.test(m)),
    `a formatter exception must not be reported as a WebAssembly limitation, got ${JSON.stringify(result.logged)}`);
}

export default async function (page, BASE) {
  const realErrors = watchErrors(page);

  await gotoChart(page, BASE, 'issues/595', '#issue595-chart .apexcharts-yaxis[rel="1"] .apexcharts-yaxis-label');

  await checkAxisLabels(page);
  await checkTooltipValues(page);
  await checkIndexResolver(page);
  await checkErrorReporting(page);

  assertNoErrors(realErrors, 'issue-595');
}
