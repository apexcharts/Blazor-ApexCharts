// The Debug option (ApexChart.Debug, ApexChartOptions.Debug) is read by blazor-apexcharts.js only.
// apexcharts.js 7.9 and later warn about a top-level option they do not know, so the wrapper keeps
// `debug` out of what it hands the core, on the first render and on updateOptions, while its own
// debug logging keeps working. The control at the end proves the core still warns about the key, so
// this spec fails if the wrapper starts passing it through again.
import { gotoChart, watchErrors, assert, assertNoErrors } from '../lib/helpers.mjs';

const UNKNOWN = /unrecognized top-level option/;

// Resolves with the first console message matching `re`, or rejects after `timeout` ms.
const nextLog = (page, re, what, timeout = 15000) => new Promise((resolve, reject) => {
  const timer = setTimeout(() => reject(new Error(what)), timeout);
  page.on('console', m => { if (re.test(m.text())) { clearTimeout(timer); resolve(m.text()); } });
});

export default async function (page, BASE) {
  const realErrors = watchErrors(page);
  const logs = [];
  page.on('console', m => logs.push({ type: m.type(), text: m.text() }));

  // The grouped stacked bar demo on this page sets Debug="true".
  const rendered = nextLog(page, /^Chart \S+ rendered$/, 'the Debug chart did not log its render, so the wrapper lost its debug flag', 30000);
  await gotoChart(page, BASE, 'bar-charts');
  const id = (await rendered).split(' ')[1];

  // updateOptions with debug on: the wrapper logs the call and the core gets no `debug` key.
  const logged = nextLog(page, /^Method:updateOptions$/, 'updateOptions on a Debug chart was not logged');
  await page.evaluate((id) => window.blazor_apexchart.updateOptions(id,
    JSON.stringify({ debug: true, chart: { id } }), false, false, false, null), id);
  await logged;

  const leaked = logs.filter(l => UNKNOWN.test(l.text)).map(l => l.text);
  assert(leaked.length === 0, `the wrapper-only debug key reached apexcharts.js: ${JSON.stringify(leaked)}`);

  // Control: the bundled core does warn when it is handed `debug` directly.
  await page.evaluate(() => {
    const el = document.createElement('div');
    document.body.append(el);
    new window.ApexCharts(el, { debug: true, chart: { type: 'line' }, series: [{ data: [1, 2] }] });
  });
  assert(logs.some(l => l.type === 'warning' && UNKNOWN.test(l.text) && /debug/.test(l.text)),
    'control failed: apexcharts.js no longer warns about an unknown top-level `debug`, so this spec proves nothing; revisit it');

  assertNoErrors(realErrors, 'debug-option-wrapper-only');
}
