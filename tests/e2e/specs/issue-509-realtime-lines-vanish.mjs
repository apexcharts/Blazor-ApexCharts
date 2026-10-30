// Issue #509: a real-time line chart lost its line when events arrived quickly.
//
// The demo page /issues/509 appends 12 points through overlapping AppendDataAsync calls, three
// per second of timestamp. This spec asserts the recommended setup (DateTime X on a datetime axis)
// keeps every point and a drawn line through overlapping appends, which also shows the wrapper's
// append path is safe under concurrency.
//
// The string X chart on the same page (the reporter's setup) is asserted in two parts:
// - every appended point reaches the chart, which is the wrapper's job and a regression guard;
// - the line is NOT drawn. Two points with the same category string make the bundled
//   apexcharts.js drop the whole line (Data._generateExternalLabels dedupes the points by x, see
//   tests/e2e/repros/realtime-lines-vanish/). That assertion is a canary on the upstream bug: it
//   fails when a bundle update fixes it, and the message says to turn it into a regression guard.
import { gotoChart, watchErrors, assert, assertNoErrors } from '../lib/helpers.mjs';

const INITIAL = 3;
const BURST = 12;
const UPSTREAM_BUG = 'https://github.com/apexcharts/Blazor-ApexCharts/issues/509 (repro: tests/e2e/repros/realtime-lines-vanish/)';

export default async function (page, BASE) {
  const realErrors = watchErrors(page);

  await gotoChart(page, BASE, 'issues/509', '#issue509-datetime .apexcharts-canvas svg');
  await page.waitForSelector('#issue509-category .apexcharts-canvas svg', { timeout: 30000 });

  await page.click('#issue509-burst');
  await page.locator('#issue509-status').filter({ hasText: `done ${BURST}` }).waitFor({ timeout: 30000 });

  const read = (rootId, chartId) => page.evaluate(([rootId, chartId]) => {
    const chart = window.ApexCharts && window.ApexCharts.getChartByID(chartId);
    const path = document.querySelector(`#${rootId} path.apexcharts-line`);
    const d = path ? path.getAttribute('d') || '' : '';
    return {
      points: chart ? chart.w.config.series[0].data.length : -1,
      seriesX: chart ? chart.w.globals.seriesX[0].length : -1,
      hasLine: !!path,
      segments: (d.match(/L/g) || []).length,
      corrupt: /NaN|undefined/.test(d)
    };
  }, [rootId, chartId]);

  // Every appended point reached the chart: no append was lost or applied twice.
  await page.waitForFunction(
    (n) => {
      const c = window.ApexCharts && window.ApexCharts.getChartByID('issue509-datetime-chart');
      return c && c.w.config.series[0].data.length === n;
    },
    INITIAL + BURST, { timeout: 30000 });

  // The settled line reaches the last point: one move plus a segment to each later point.
  await page.waitForFunction(
    (n) => {
      const p = document.querySelector('#issue509-datetime path.apexcharts-line');
      return p && ((p.getAttribute('d') || '').match(/L/g) || []).length === n - 1;
    },
    INITIAL + BURST, { timeout: 30000 });

  const dt = await read('issue509-datetime', 'issue509-datetime-chart');
  assert(dt.hasLine && !dt.corrupt, `datetime chart line missing or corrupt: ${JSON.stringify(dt)}`);

  // The string X chart got the same appends at the same moments; wait for all of them too.
  await page.waitForFunction(
    (n) => {
      const c = window.ApexCharts && window.ApexCharts.getChartByID('issue509-category-chart');
      return c && c.w.config.series[0].data.length === n;
    },
    INITIAL + BURST, { timeout: 30000 }).catch(() => {});

  const cat = await read('issue509-category', 'issue509-category-chart');
  console.log(`    string X chart: ${JSON.stringify(cat)}`);
  assert(cat.points === INITIAL + BURST, `string X chart lost appended points: ${JSON.stringify(cat)}`);

  // Canary: the chart re-rendered with every point (above), and the path element of a line chart
  // exists from the first frame of its update animation, so no line here is the upstream bug, not
  // an animation still in flight.
  assert(
    !cat.hasLine,
    `apexcharts.js bug ${UPSTREAM_BUG} appears fixed: the string X chart now draws its line ` +
    `(${JSON.stringify(cat)}). Flip this assertion into a regression guard: assert the line is drawn ` +
    `through all ${INITIAL + BURST} points, like the datetime chart above.`,
  );

  assertNoErrors(realErrors, 'issue-509');
}
