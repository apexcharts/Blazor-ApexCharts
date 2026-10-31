// Issue 442: on a pie chart a legend click also selects the slice, so OnDataPointSelection runs for
// both a legend click and a slice click. SelectedData.Source must say which one it was, and
// OnLegendClicked must run on every legend click, not only the first. A selection that an
// OnLegendClicked handler makes in code must not be mistaken for the legend selection.
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

  // An OnLegendClicked handler that selects a different slice in code. On WebAssembly that handler runs
  // synchronously inside the legend click, before the chart selects the clicked slice, so only the
  // clicked slice may be reported as Legend; the slice selected in code is Method.
  const chainLegend = page.locator('#issue442-chain .apexcharts-legend-series');
  const chainCount = page.locator('#issue442-chain-count');
  const chainLog = page.locator('#issue442-chain-log li');

  await chainLegend.nth(0).click();
  await chainCount.filter({ hasText: /^2$/ }).waitFor({ timeout: 10000 });

  // The order of the two selections differs between hosting models, so compare them sorted.
  const chainAfterA = (await chainLog.allInnerTexts()).slice().sort();
  const expectedChainA = ['A:Legend', 'D:Method'];
  assert(JSON.stringify(chainAfterA) === JSON.stringify(expectedChainA),
    `A selection made in code by OnLegendClicked should be Method, the clicked slice Legend: expected ${JSON.stringify(expectedChainA)}, got ${JSON.stringify(chainAfterA)}`);

  // A plain legend click on the same chart is still a legend selection.
  await chainLegend.nth(1).click();
  await chainCount.filter({ hasText: /^3$/ }).waitFor({ timeout: 10000 });
  const chainLast = (await chainLog.allInnerTexts()).at(-1);
  assert(chainLast === 'B:Legend', `A legend click on B should report B:Legend, got ${chainLast}`);

  assertNoErrors(realErrors, 'issue-442');
}
