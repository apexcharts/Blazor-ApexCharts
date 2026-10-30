// Issue 442: on a pie chart a legend click also selects the slice, so OnDataPointSelection runs for
// both a legend click and a slice click. SelectedData.Source must say which one it was, and
// OnLegendClicked must run on every legend click, not only the first.
import { gotoChart, watchErrors, assert, assertNoErrors } from '../lib/helpers.mjs';

export default async function (page, BASE) {
  const realErrors = watchErrors(page);

  await gotoChart(page, BASE, 'issues/442', '#issue442-pie .apexcharts-legend-series');

  const legendItems = page.locator('#issue442-pie .apexcharts-legend-series');
  const legendCount = page.locator('#issue442-legend-count');
  const selectionCount = page.locator('#issue442-selection-count');
  const logItems = page.locator('#issue442-log li');

  const waitForCounts = async (legend, selection) => {
    await legendCount.filter({ hasText: new RegExp(`^${legend}$`) }).waitFor({ timeout: 10000 });
    await selectionCount.filter({ hasText: new RegExp(`^${selection}$`) }).waitFor({ timeout: 10000 });
  };

  // Several legend clicks, including a second slice and a repeat, each has to reach OnLegendClicked.
  const legendOrder = [0, 1, 2, 1];
  for (let n = 0; n < legendOrder.length; n++) {
    await legendItems.nth(legendOrder[n]).click();
    await waitForCounts(n + 1, n + 1);
  }

  // A click on the slice itself.
  await page.locator('#issue442-pie .apexcharts-pie-slice-3').click();
  await waitForCounts(4, 5);

  // A selection made in code.
  await page.locator('#issue442-toggle').click();
  await waitForCounts(4, 6);

  const log = await logItems.allInnerTexts();
  const selections = log.filter(l => l.startsWith('selection:'));
  const legends = log.filter(l => l.startsWith('legend:'));

  const expectedLegends = ['legend:A', 'legend:B', 'legend:C', 'legend:B'];
  assert(JSON.stringify(legends) === JSON.stringify(expectedLegends),
    `OnLegendClicked should run on every legend click: expected ${JSON.stringify(expectedLegends)}, got ${JSON.stringify(legends)}`);

  const expectedSelections = [
    'selection:A:Legend', 'selection:B:Legend', 'selection:C:Legend', 'selection:B:Legend',
    'selection:D:DataPoint',
    'selection:C:Method',
  ];
  assert(JSON.stringify(selections) === JSON.stringify(expectedSelections),
    `SelectedData.Source should tell a legend click from a slice click: expected ${JSON.stringify(expectedSelections)}, got ${JSON.stringify(selections)}`);

  assertNoErrors(realErrors, 'issue-442');
}
