// Plain-JS repro for issue #557: zoom into a window that lies entirely inside
// an X range annotation and report the annotation rect geometry.
// Serve the repository root first: python3 -m http.server 5314
// Then: node tests/e2e/repros/xannotation-zoom/run.mjs [pageUrl] [libUrl]
// libUrl optionally points at another apexcharts.js UMD build to compare with.
//
// Seen with the vendored 6.5.0 and with 8.6.0: "num" (epoch ms on a datetime
// axis) keeps the fill (w = plot width) when zoomed inside the range; "iso"
// (strings with no xaxis.type, so a category axis) drops it to w=0; "isodt"
// (strings on a datetime axis) never draws the range at all.
import { chromium } from 'playwright';

const URL = process.argv[2] || 'http://localhost:5314/tests/e2e/repros/xannotation-zoom/index.html';
const LIB = process.argv[3] ? `&lib=${encodeURIComponent(process.argv[3])}` : '';

const browser = await chromium.launch();
try {
  for (const mode of ['num', 'iso', 'isodt']) {
    const page = await browser.newPage({ viewport: { width: 1000, height: 600 } });
    page.setDefaultTimeout(10000);
    page.on('pageerror', (e) => console.log(`[${mode}] pageerror: ${e.message}`));
    await page.goto(`${URL}?x=${mode}${LIB}`);
    await page.waitForFunction(() => window.ready === true);

    const state = () => page.evaluate(() => {
      const w = window.chart.w;
      return {
        axis: w.config.xaxis.type,
        minX: w.globals.minX,
        maxX: w.globals.maxX,
        rects: [...document.querySelectorAll('.apexcharts-annotation-rect')]
          .map((e) => `${e.getAttribute('fill')} x=${Number(e.getAttribute('x')).toFixed(1)} w=${Number(e.getAttribute('width')).toFixed(1)}`)
      };
    });

    console.log(`[${mode}] initial`, JSON.stringify(await state()));
    const cases = [
      ['inside 02:00-06:00', 2, 6],
      ['inside 00:30-11:30', 0.5, 11.5],
      ['crossing 10:00-14:00', 10, 14]
    ];
    for (const [label, a, b] of cases) {
      const before = await page.evaluate(() => window.chart.w.globals.minX);
      await page.evaluate(([a, b]) => window.zoomHours(a, b), [a, b]);
      await page.waitForFunction((before) => window.chart.w.globals.minX !== before, before)
        .catch(() => console.log(`[${mode}] ${label}: minX did not change`));
      console.log(`[${mode}] ${label}`, JSON.stringify(await state()));
    }
    await page.close();
  }
} finally {
  await browser.close();
}
