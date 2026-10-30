// Issue 420: a brush chart failed with "Cannot read properties of undefined (reading 'w')" and
// stayed blank when it rendered before the chart it targets. Charts render independently, so on
// a cold page load the order varies; /issues/420 declares the brush first to force that order.
import { gotoChart, watchErrors, assert, assertNoErrors } from '../lib/helpers.mjs';

const SELECTION_MIN = Date.UTC(2024, 0, 31);
const SELECTION_MAX = Date.UTC(2024, 2, 11);

// The brush is wired when the target knows its brush source and shows the selected range.
async function waitForBrush(page, brushId, targetId) {
  await page.waitForFunction(([b, t]) => {
    const AC = window.ApexCharts;
    const brush = AC && AC.getChartByID(b);
    const target = AC && AC.getChartByID(t);
    return !!(brush && target && target.w.globals.brushSource === brush);
  }, [brushId, targetId], { timeout: 20000 });

  const range = await page.evaluate(t => {
    const g = window.ApexCharts.getChartByID(t).w.globals;
    return { min: g.minX, max: g.maxX };
  }, targetId);
  assert(range.min === SELECTION_MIN && range.max === SELECTION_MAX,
    `${targetId} should show the brush selection, got ${JSON.stringify(range)}`);
}

export default async function (page, BASE) {
  const realErrors = watchErrors(page);

  // A: the brush chart is declared, and renders, before its target.
  await gotoChart(page, BASE, 'issues/420', '#issue420-a-target-host .apexcharts-canvas svg');
  await page.waitForSelector('#issue420-a-brush-host .apexcharts-selection-rect', { timeout: 20000 });
  await waitForBrush(page, 'issue420-a-brush', 'issue420-a-target');

  // B: the target does not exist yet. The brush chart waits for it instead of failing.
  assert(await page.locator('#issue420-b-target-host').count() === 0, 'B target should start hidden');
  assertNoErrors(realErrors, 'issue-420 before target');

  await page.click('#issue420-show-target');
  await page.waitForSelector('#issue420-b-target-host .apexcharts-canvas svg', { timeout: 20000 });
  await page.waitForSelector('#issue420-b-brush-host .apexcharts-selection-rect', { timeout: 20000 });
  await waitForBrush(page, 'issue420-b-brush', 'issue420-b-target');

  assertNoErrors(realErrors, 'issue-420');
}
