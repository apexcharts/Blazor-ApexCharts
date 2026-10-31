// Drives index.html: hovers and clicks the July point (dataPointIndex 6) where all
// four series are 15, then prints what the tooltip and the events reported.
// Not part of run.mjs. From the repository root: python3 -m http.server 5309, then
// from tests/e2e: node repros/intersecting-points-click/probe.mjs http://localhost:5309
// Observed with the vendored bundle: four markers share one cx/cy, the element under the
// cursor is the marker of seriesIndex 3 (Series 4), the tooltip lists "Series 4: 15" only, and dataPointSelection,
// click and markerClick all report seriesIndex 3.
import { chromium } from 'playwright';

const BASE = process.argv[2] || 'http://localhost:5309';
const browser = await chromium.launch();
const page = await browser.newPage({ viewport: { width: 1200, height: 800 } });
page.on('pageerror', (e) => console.log('pageerror', e.message));
await page.goto(`${BASE}/tests/e2e/repros/intersecting-points-click/index.html`);
await page.waitForFunction(() => window.__ready === true);

const markers = await page.$$eval('.apexcharts-marker[rel="6"]', (els) =>
  els.map((el) => ({
    series: el.closest('.apexcharts-series').getAttribute('data:realIndex'),
    cx: el.getAttribute('cx'),
    cy: el.getAttribute('cy')
  }))
);
console.log('markers at dataPointIndex 6:', JSON.stringify(markers));

const box = await page.locator('.apexcharts-marker[rel="6"]').first().boundingBox();
const x = box.x + box.width / 2;
const y = box.y + box.height / 2;
const top = await page.evaluate(([px, py]) => {
  const el = document.elementFromPoint(px, py);
  const s = el && el.closest('.apexcharts-series');
  return { tag: el && el.tagName, cls: el && el.getAttribute('class'), series: s && s.getAttribute('data:realIndex') };
}, [x, y]);
console.log('element under the cursor:', JSON.stringify(top));

await page.mouse.move(x, y);
await page.locator('.apexcharts-tooltip.apexcharts-active').waitFor({ timeout: 5000 });
const tooltip = await page.$$eval('.apexcharts-tooltip-series-group', (els) =>
  els.filter((g) => getComputedStyle(g).display !== 'none').map((g) => g.textContent.trim())
);
console.log('visible tooltip rows:', JSON.stringify(tooltip));

await page.mouse.down();
await page.mouse.up();
await page.waitForFunction(() => window.__events.length >= 3);
console.log('events:', JSON.stringify(await page.evaluate(() => window.__events)));
await browser.close();
