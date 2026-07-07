// Headless browser smoke + interaction test for the Blazor-ApexCharts docs demo.
//
// Why this exists: a Blazor chart wrapper can build and even serialize its options perfectly yet
// still fail in the browser, because the real contract lives in the JS interop and the core library.
// (For example: apexcharts renders a violin only from a precomputed density profile, so a series
// that passes raw values builds and serializes fine but renders an empty chart.) This test drives
// the actual WASM app in a real browser and fails CI if a chart page throws, shows the Blazor error
// UI, or renders no chart. It also checks that the chart-type morph interaction actually re-renders.
//
// Usage:
//   BASE_URL=http://localhost:5184 node smoke.mjs
//   PW_CHANNEL=chrome node smoke.mjs   # drive an installed Chrome instead of bundled chromium
let chromium;
try {
  ({ chromium } = await import('playwright'));
} catch {
  ({ chromium } = await import('playwright-core'));
}

const BASE = process.env.BASE_URL || 'http://localhost:5184';
const channel = process.env.PW_CHANNEL;

// Chart-type demo pages. Each embeds several demos and must render at least one ApexCharts canvas
// with no fatal console/page errors.
const ROUTES = [
  'violin-charts', 'boxplot-charts', 'heatmap-charts',
  'line-charts', 'scatter-charts', 'pie-charts',
];

// The docs "view source" widget fetches each demo's .razor from the deployed GitHub Pages site; for
// demos not yet deployed that is a harmless 404 the app catches and shows as text. Ignore those so
// the smoke test only fails on real chart/interop errors.
const isIgnorableError = t => /Failed to load resource: the server responded with a status of 404/.test(t);

const failures = [];
const launchOpts = channel ? { channel, headless: true } : { headless: true };
const browser = await chromium.launch(launchOpts);
const page = await browser.newPage({ viewport: { width: 1400, height: 1000 } });

let pageErrors = [];
page.on('console', m => { if (m.type() === 'error') pageErrors.push(m.text().slice(0, 200)); });
page.on('pageerror', e => pageErrors.push('PAGEERROR: ' + e.message.slice(0, 200)));
const realErrors = () => pageErrors.filter(e => !isIgnorableError(e));

// Boot so the WASM runtime is warm before asserting.
await page.goto(BASE + '/', { waitUntil: 'domcontentloaded' });
await page.waitForTimeout(4000);

async function checkRoute(route) {
  pageErrors = [];
  await page.goto(BASE + '/' + route, { waitUntil: 'domcontentloaded' });
  try { await page.waitForSelector('.apexcharts-canvas svg', { timeout: 30000 }); } catch { /* asserted below */ }
  await page.waitForTimeout(2500);

  const st = await page.evaluate(() => {
    const eu = document.querySelector('#blazor-error-ui');
    return {
      errUiShown: eu ? getComputedStyle(eu).display !== 'none' : false,
      canvases: document.querySelectorAll('.apexcharts-canvas svg').length,
      series: document.querySelectorAll('.apexcharts-series').length,
    };
  });

  const errs = realErrors();
  if (errs.length) failures.push(`[${route}] console/page errors: ${JSON.stringify(errs)}`);
  if (st.errUiShown) failures.push(`[${route}] Blazor error UI is visible`);
  if (st.canvases === 0 || st.series === 0) failures.push(`[${route}] no chart rendered (canvases=${st.canvases}, series=${st.series})`);
  console.log(`[${route}] canvases=${st.canvases} series=${st.series} errUi=${st.errUiShown} realErrors=${errs.length}`);
}

for (const r of ROUTES) await checkRoute(r);

// Extra assertion for violin: it must actually draw density bodies (the interop-only bug renders none).
pageErrors = [];
await page.goto(BASE + '/violin-charts', { waitUntil: 'domcontentloaded' });
await page.waitForSelector('.apexcharts-canvas svg', { timeout: 30000 });
await page.waitForTimeout(2500);
const violinAreas = await page.evaluate(() =>
  document.querySelectorAll('.apexcharts-violin-area, [class*="violin-area"]').length);
if (violinAreas === 0) failures.push('[violin-charts] no violin density bodies rendered');
console.log(`[violin-charts] violinAreas=${violinAreas}`);

// Interaction: the chart-type morph button must re-render pie -> bar (bars appear).
pageErrors = [];
await page.goto(BASE + '/pie-charts', { waitUntil: 'domcontentloaded' });
await page.waitForSelector('.apexcharts-canvas svg', { timeout: 30000 });
await page.waitForTimeout(2500);
const morphBtn = page.locator('button:has-text("Morph to")').first();
const before = await page.evaluate(() => document.querySelectorAll('.apexcharts-bar-series rect, .apexcharts-bar-area').length);
await morphBtn.click().catch(e => failures.push(`[typemorph] click failed: ${e.message}`));
await page.waitForTimeout(2500);
const after = await page.evaluate(() => document.querySelectorAll('.apexcharts-bar-series rect, .apexcharts-bar-area').length);
if (!(before === 0 && after > 0)) failures.push(`[typemorph] morph did not produce bars: before=${before} after=${after}`);
const morphErrs = realErrors();
if (morphErrs.length) failures.push(`[typemorph] console/page errors: ${JSON.stringify(morphErrs)}`);
console.log(`[typemorph] bars before=${before} after=${after} realErrors=${morphErrs.length}`);

await browser.close();

if (failures.length) {
  console.error('\nE2E SMOKE FAILED:\n' + failures.map(f => '  - ' + f).join('\n'));
  process.exit(1);
}
console.log('\nE2E smoke passed: all chart pages rendered, violin drew density bodies, and type-morph re-rendered.');
