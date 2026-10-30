// Issue #557: an X range annotation (x to x2) must keep its fill after zooming
// into a window that lies entirely inside the range.
//
// The page has two charts. The datetime one (Unix milliseconds on a datetime
// axis) is the supported setup and is asserted here. The category one (DateTime
// values with no axis type) still loses the fill because of the label lookup in
// apexcharts.js; it is only reported, so this spec starts failing for no reason
// other than a regression in the supported setup.
import { gotoChart, watchErrors, assert, assertNoErrors } from '../lib/helpers.mjs';

// Width of the annotation rect as a share of the plot area width.
function rectShare(page, containerId) {
  return page.evaluate((id) => {
    const root = document.getElementById(id);
    const rect = root && root.querySelector('rect.apexcharts-annotation-rect.issue557-range');
    const grid = root && root.querySelector('clipPath[id^="gridRectMask"] rect');
    if (!rect || !grid) return null;
    return Number(rect.getAttribute('width')) / Number(grid.getAttribute('width'));
  }, containerId);
}

export default async function (page, BASE) {
  const realErrors = watchErrors(page);

  await gotoChart(page, BASE, 'issues/557', '#issue557-datetime svg.apexcharts-svg');
  await page.locator('#issue557-category svg.apexcharts-svg').waitFor({ timeout: 30000 });

  // Before zooming, 00:00 to 12:00 is about half of the day.
  await page.waitForFunction(() => {
    const r = document.querySelector('#issue557-datetime rect.apexcharts-annotation-rect.issue557-range');
    return r && Number(r.getAttribute('width')) > 0;
  }, null, { timeout: 15000 });
  const before = await rectShare(page, 'issue557-datetime');
  assert(before > 0.4 && before < 0.6, `expected the range to cover about half the day before zooming, got ${before}`);

  // Zoom to 02:00 to 06:00, entirely inside the range. The fill must span the whole plot.
  await page.click('#issue557-zoom-datetime');
  await page.waitForFunction(() => {
    const root = document.getElementById('issue557-datetime');
    const r = root.querySelector('rect.apexcharts-annotation-rect.issue557-range');
    const g = root.querySelector('clipPath[id^="gridRectMask"] rect');
    return r && g && Number(r.getAttribute('width')) / Number(g.getAttribute('width')) > 0.9;
  }, null, { timeout: 15000 }).catch(async () => {
    throw new Error(`datetime chart: annotation fill did not span the zoomed plot, share=${await rectShare(page, 'issue557-datetime')}`);
  });

  // Report, without asserting, the category chart that the issue used.
  await page.click('#issue557-zoom-category');
  await page.waitForFunction(() => {
    const t = document.querySelector('#issue557-category .apexcharts-xaxis-texts-g text');
    return t && t.textContent.includes('T02:00:00');
  }, null, { timeout: 15000 }).catch(() => {});
  console.log(`  issue 557 category axis (upstream apexcharts.js): fill share after zoom = ${await rectShare(page, 'issue557-category')}`);

  assertNoErrors(realErrors, 'issue 557');
}
