// Issue 442: on a pie chart OnDataPointSelection and OnLegendClicked must each report their own click.
//
// Up to apexcharts.js 6.6 a legend click on a pie also selected the slice, so OnDataPointSelection ran
// for legend clicks too and SelectedData.Source had to say Legend. Since 6.7 ("feat(legend):
// pie/donut/polarArea legend click toggles the slice") a legend click hides or shows the slice the way
// it hides a series on an axis chart, and selects nothing. This spec asserts that behaviour through the
// Blazor wiring:
// - OnLegendClicked runs on every legend click, not only the first, and each click toggles its slice;
// - a legend click never reaches OnDataPointSelection;
// - a click on the slice itself is reported as DataPoint, a selection made in code as Method;
// - a selection that an OnLegendClicked handler makes in code is Method, not Legend.
import { gotoChart, watchErrors, assert, assertNoErrors } from '../lib/helpers.mjs';

// Labels of the legend items the core shows as collapsed (hidden slices).
const hiddenSlices = (page, root) => page.evaluate((root) =>
  [...document.querySelectorAll(`${root} .apexcharts-legend-series`)]
    .filter(e => e.getAttribute('data:collapsed') === 'true')
    .map(e => e.querySelector('.apexcharts-legend-text')?.textContent.trim()),
  root);

const waitForHidden = async (page, root, expected) => {
  await page.waitForFunction(([root, expected]) => {
    const hidden = [...document.querySelectorAll(`${root} .apexcharts-legend-series`)]
      .filter(e => e.getAttribute('data:collapsed') === 'true')
      .map(e => e.querySelector('.apexcharts-legend-text')?.textContent.trim());
    return JSON.stringify(hidden) === JSON.stringify(expected);
  }, [root, expected], { timeout: 10000 }).catch(async () => {
    throw new Error(`${root}: expected hidden slices ${JSON.stringify(expected)}, got ${JSON.stringify(await hiddenSlices(page, root))}`);
  });
};

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

  // Several legend clicks, including a repeat, each reach OnLegendClicked and toggle their slice.
  // Every slice is clicked twice, so all four are back when the slice and code selections run.
  const steps = [
    { index: 0, hidden: ['A'] },
    { index: 1, hidden: ['A', 'B'] },
    { index: 2, hidden: ['A', 'B', 'C'] },
    { index: 1, hidden: ['A', 'C'] },
    { index: 0, hidden: ['C'] },
    { index: 2, hidden: [] },
  ];
  for (let n = 0; n < steps.length; n++) {
    await legendItems.nth(steps[n].index).click();
    await waitForCounts(n + 1, 0);
    await waitForHidden(page, '#issue442-pie', steps[n].hidden);
  }

  // A click on the slice itself: the Blazor event wiring for a selection the user makes.
  await page.locator('#issue442-pie .apexcharts-pie-slice-3').click();
  await waitForCounts(steps.length, 1);

  // A selection made in code.
  await page.locator('#issue442-toggle').click();
  await waitForCounts(steps.length, 2);

  const log = await logItems.allInnerTexts();
  const selections = log.filter(l => l.startsWith('selection:'));
  const legends = log.filter(l => l.startsWith('legend:'));

  const expectedLegends = ['legend:A', 'legend:B', 'legend:C', 'legend:B', 'legend:A', 'legend:C'];
  assert(JSON.stringify(legends) === JSON.stringify(expectedLegends),
    `OnLegendClicked should run on every legend click: expected ${JSON.stringify(expectedLegends)}, got ${JSON.stringify(legends)}`);

  // The log is in event order, so a selection raised by any legend click would come first here.
  const expectedSelections = ['selection:D:DataPoint', 'selection:C:Method'];
  assert(JSON.stringify(selections) === JSON.stringify(expectedSelections),
    `a legend click must not select, a slice click is DataPoint and a code selection Method: expected ${JSON.stringify(expectedSelections)}, got ${JSON.stringify(selections)}`);

  // An OnLegendClicked handler that selects a different slice in code. On WebAssembly that handler runs
  // synchronously inside the legend click; the slice it selects must be reported as Method, and the
  // legend click itself (which hides A) must add no selection of its own.
  const chainLegend = page.locator('#issue442-chain .apexcharts-legend-series');
  const chainCount = page.locator('#issue442-chain-count');
  const chainLog = page.locator('#issue442-chain-log li');

  await chainLegend.nth(0).click();
  await chainCount.filter({ hasText: /^1$/ }).waitFor({ timeout: 10000 });
  await waitForHidden(page, '#issue442-chain', ['A']);

  // A plain legend click on the same chart hides B and selects nothing. The slice click after it is a
  // marker: once its selection arrives, any selection from the legend click would already be logged.
  await chainLegend.nth(1).click();
  await waitForHidden(page, '#issue442-chain', ['A', 'B']);
  await page.locator('#issue442-chain .apexcharts-pie-slice-2').click();
  await chainCount.filter({ hasText: /^2$/ }).waitFor({ timeout: 10000 });

  const chain = await chainLog.allInnerTexts();
  const expectedChain = ['D:Method', 'C:DataPoint'];
  assert(JSON.stringify(chain) === JSON.stringify(expectedChain),
    `A selection made in code by OnLegendClicked should be Method, and legend clicks should select nothing: expected ${JSON.stringify(expectedChain)}, got ${JSON.stringify(chain)}`);

  assertNoErrors(realErrors, 'issue-442');
}
