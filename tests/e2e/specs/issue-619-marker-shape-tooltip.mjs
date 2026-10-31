// Issue #619: with Markers.Shape set to one shape, only the first series kept it. MarkerShapeSelections
// had no ValueOrListConverter, so one shape reached apexcharts.js as ["square"], which the chart reads
// as one shape per series index: every later series got an undefined shape in the plot and the tooltip
// (shape="undefined" on the tooltip marker, which older bundles drew as nothing at all).
// The page renders two charts with three series each: one shape for all, and a list of three shapes.
import { gotoChart, watchErrors, assert, assertNoErrors } from '../lib/helpers.mjs';

const colors = ['rgb(0, 143, 251)', 'rgb(0, 227, 150)', 'rgb(254, 176, 25)'];

// Shapes and colors of the tooltip markers, and the shape of the first plotted marker of each series.
const readChart = (page, sel) => page.$eval(sel, root => ({
  tooltip: [...root.querySelectorAll('.apexcharts-tooltip-series-group')]
    .filter(g => getComputedStyle(g).display !== 'none')
    .map(g => {
      const m = g.querySelector('.apexcharts-tooltip-marker');
      return { shape: m?.getAttribute('shape'), color: m?.style.color || m?.style.backgroundColor, drawn: !!m?.querySelector('svg') };
    }),
  plot: [...root.querySelectorAll('.apexcharts-series[seriesName]')]
    .map(s => s.querySelector('.apexcharts-marker')?.getAttribute('shape') ?? null),
}));

async function check(page, sel, expectedShapes) {
  await page.waitForFunction(s => document.querySelectorAll(`${s} .apexcharts-series[seriesName]`).length === 3, sel, { timeout: 10000 });

  const box = await page.locator(`${sel} .apexcharts-grid`).first().boundingBox();
  assert(box, `${sel}: plot grid not found`);
  await page.mouse.move(box.x + box.width * 0.35, box.y + box.height / 2);
  await page.locator(`${sel} .apexcharts-tooltip.apexcharts-active`).waitFor({ timeout: 5000 });

  // The rows are filled in on hover, so poll until all three are there before comparing.
  await page.waitForFunction(s => [...document.querySelectorAll(`${s} .apexcharts-tooltip-marker`)]
    .filter(m => m.getAttribute('shape')).length === 3, sel, { timeout: 5000 });

  const { tooltip, plot } = await readChart(page, sel);
  await page.mouse.move(0, 0);

  const tooltipShapes = tooltip.map(t => t.shape);
  assert(JSON.stringify(tooltipShapes) === JSON.stringify(expectedShapes),
    `${sel}: tooltip marker shapes should be ${JSON.stringify(expectedShapes)}, got ${JSON.stringify(tooltipShapes)}`);
  const tooltipColors = tooltip.map(t => t.color);
  assert(JSON.stringify(tooltipColors) === JSON.stringify(colors),
    `${sel}: tooltip marker colors should be ${JSON.stringify(colors)}, got ${JSON.stringify(tooltipColors)}`);
  assert(tooltip.every(t => t.drawn), `${sel}: every tooltip marker should draw an svg, got ${JSON.stringify(tooltip)}`);
  assert(JSON.stringify(plot) === JSON.stringify(expectedShapes),
    `${sel}: plotted marker shapes should be ${JSON.stringify(expectedShapes)}, got ${JSON.stringify(plot)}`);
}

export default async function (page, BASE) {
  const realErrors = watchErrors(page);

  await gotoChart(page, BASE, 'issues/619', '#issue619-list .apexcharts-canvas svg');

  await check(page, '#issue619-single', ['square', 'square', 'square']);
  await check(page, '#issue619-list', ['diamond', 'cross', 'triangle']);

  assertNoErrors(realErrors, 'issue-619');
}
