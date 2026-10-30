// Issue #660: with three or more line series on a numeric x axis, hiding one series through
// Series.Hide() left the shared tooltip listing a single series, while the legend was correct.
// The cause was in apexcharts.js (5.3.6): Tooltip Utils.isInitialSeriesSameLen compared the
// emptied data of the collapsed series with the others, so the tooltip fell back to a
// single-series tooltip. Fixed upstream in 5.4.0; this spec keeps the shipped bundle honest.
import { gotoChart, watchErrors, assert, assertNoErrors } from '../lib/helpers.mjs';

// Names of the tooltip rows that are displayed (hidden series rows stay in the DOM with display none).
const visibleTooltipRows = page => page.$$eval('#issue660-chart .apexcharts-tooltip-series-group', els =>
  els.filter(e => getComputedStyle(e).display !== 'none')
    .map(e => (e.querySelector('.apexcharts-tooltip-text-y-label')?.textContent || '').replace(/:\s*$/, '').trim()));

// Hover positions across the plot (fraction of its width) and the x value each one snaps to: the
// demo's points sit at x = 1, 2, 3 and 4.
const HOVERS = [[0.05, 1], [0.35, 2], [0.65, 3], [0.95, 4]];

// Hovers the plot at each position and returns the visible rows seen at each. The tooltip stays
// in the DOM with the class apexcharts-active from one position to the next, so after each move
// this waits for the header to show the x value of the new position: two consecutive positions
// never share it (the last of one call is x = 4, the first of the next x = 1), so the rows read
// afterwards are the ones drawn for this hover, not stale ones from the previous position.
async function hoverRows(page) {
  const box = await page.locator('#issue660-chart .apexcharts-grid').first().boundingBox();
  assert(box, 'plot grid not found');
  const seen = [];
  for (const [fx, x] of HOVERS) {
    await page.mouse.move(box.x + box.width * fx, box.y + box.height / 2);
    await page.waitForFunction(x => {
      const tip = document.querySelector('#issue660-chart .apexcharts-tooltip.apexcharts-active');
      const title = tip && tip.querySelector('.apexcharts-tooltip-title');
      return !!title && Number(title.textContent) === x;
    }, x, { timeout: 5000 }).catch(async () => {
      const title = await page.$eval('#issue660-chart .apexcharts-tooltip', e => e.querySelector('.apexcharts-tooltip-title')?.textContent).catch(() => null);
      throw new Error(`hovering at ${fx} of the plot width did not show the tooltip for x = ${x} (header ${JSON.stringify(title)})`);
    });
    seen.push(await visibleTooltipRows(page));
  }
  await page.mouse.move(0, 0);
  return seen;
}

export default async function (page, BASE) {
  const realErrors = watchErrors(page);

  await gotoChart(page, BASE, 'issues/660', '#issue660-chart .apexcharts-canvas svg');

  await page.click('#issue660-add');
  await page.locator('#issue660-status').filter({ hasText: 'Series: 2,' }).waitFor({ timeout: 10000 });
  await page.click('#issue660-add');
  await page.locator('#issue660-status').filter({ hasText: 'Series: 3,' }).waitFor({ timeout: 10000 });
  await page.waitForFunction(() => document.querySelectorAll('#issue660-chart .apexcharts-series[seriesName]').length === 3, null, { timeout: 10000 });

  const before = await hoverRows(page);
  for (const rows of before) {
    assert(rows.length === 3, `before hiding, expected 3 tooltip rows, got ${JSON.stringify(rows)}`);
  }

  await page.click('#issue660-hide-first');
  await page.locator('#issue660-status').filter({ hasText: 'hidden: Customer' }).waitFor({ timeout: 10000 });
  await page.waitForFunction(() => document.querySelectorAll('#issue660-chart .apexcharts-series.apexcharts-series-collapsed').length === 1, null, { timeout: 10000 });

  const after = await hoverRows(page);
  for (const rows of after) {
    assert(JSON.stringify(rows) === JSON.stringify(['Customer_2', 'Customer_3']),
      `after hiding "Customer", expected tooltip rows ["Customer_2","Customer_3"], got ${JSON.stringify(rows)}`);
  }

  await page.click('#issue660-show-all');
  await page.locator('#issue660-status').filter({ hasText: 'hidden: none' }).waitFor({ timeout: 10000 });
  await page.waitForFunction(() => document.querySelectorAll('#issue660-chart .apexcharts-series.apexcharts-series-collapsed').length === 0, null, { timeout: 10000 });

  const restored = await hoverRows(page);
  for (const rows of restored) {
    assert(rows.length === 3, `after showing all, expected 3 tooltip rows, got ${JSON.stringify(rows)}`);
  }

  assertNoErrors(realErrors, 'issue-660');
}
