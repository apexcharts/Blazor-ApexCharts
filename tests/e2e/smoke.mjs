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
  // v6 feature demo pages (each renders a chart; premium ones show the trial watermark locally).
  'features/v6/renderer', 'features/v6/streaming', 'features/v6/easing', 'features/v6/bar-race',
  'features/v6/os-theme', 'features/v6/measure', 'features/v6/history', 'features/v6/perspectives',
  'features/v6/ink', 'features/v6/context-menu', 'features/v6/storyboard', 'features/v6/linked-views',
];

// The docs "view source" widget fetches each demo's .razor from the deployed GitHub Pages site; for
// demos not yet deployed that is a harmless 404 the app catches and shows as text. Ignore those so
// the smoke test only fails on real chart/interop errors.
// - the docs "view source" widget fetching each demo's .razor from the deployed site: a 404 when a demo is not yet
//   deployed, ERR_CONNECTION_REFUSED when the runner has no outbound network, or a CORS block / ERR_FAILED when the
//   runner CAN reach apexcharts.github.io (cross-origin fetch, no CORS headers). All are the same harmless fallback,
//   not a chart/interop error. The razor_source path is the only cross-origin fetch the app makes.
// - the `[Apex]` license-domain notice: the demo's app-wide key is domain-locked to apexcharts.github.io, so on any
//   other host (CI, localhost) the premium features render in trial mode and the core logs this. That is expected.
const isIgnorableError = t =>
  /Failed to load resource: the server responded with a status of 404/.test(t) ||
  /ERR_CONNECTION_REFUSED|ERR_NAME_NOT_RESOLVED|ERR_INTERNET_DISCONNECTED/.test(t) ||
  /razor_source|blocked by CORS policy|Access to fetch|net::ERR_FAILED/.test(t) ||
  /\[Apex\].*licen[sc]e|not valid for this domain/i.test(t);

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

// v6 premium features + licensing: the page must render both charts; the chart relying on the app-wide
// (domain-locked) key is watermarked in trial mode here, while the per-chart-licensed chart is not. This guards
// the whole license/watermark chain (typed premium options -> serialize -> core gating) which build+serialize can't.
pageErrors = [];
await page.goto(BASE + '/v6-premium-features', { waitUntil: 'domcontentloaded' });
await page.waitForSelector('#licensed-wrap .apexcharts-canvas svg', { timeout: 30000 });
await page.waitForTimeout(2500);
const v6 = await page.evaluate(() => ({
  trialCanvas: document.querySelectorAll('#trial-wrap .apexcharts-canvas svg').length,
  licensedCanvas: document.querySelectorAll('#licensed-wrap .apexcharts-canvas svg').length,
  trialWatermarks: document.querySelectorAll('#trial-wrap [data-apexcharts-watermark]').length,
  licensedWatermarks: document.querySelectorAll('#licensed-wrap [data-apexcharts-watermark]').length,
}));
const v6errs = realErrors();
if (v6.trialCanvas === 0 || v6.licensedCanvas === 0) failures.push(`[v6] premium page did not render (trial=${v6.trialCanvas}, licensed=${v6.licensedCanvas})`);
if (v6.trialWatermarks !== 1) failures.push(`[v6] trial (unlicensed) chart should show exactly 1 watermark, got ${v6.trialWatermarks}`);
if (v6.licensedWatermarks !== 0) failures.push(`[v6] per-chart-licensed chart should have no watermark, got ${v6.licensedWatermarks}`);
if (v6errs.length) failures.push(`[v6] console/page errors: ${JSON.stringify(v6errs)}`);
console.log(`[v6-premium-features] trialWM=${v6.trialWatermarks} licensedWM=${v6.licensedWatermarks} canvases=${v6.trialCanvas}/${v6.licensedCanvas} realErrors=${v6errs.length}`);

await browser.close();

if (failures.length) {
  console.error('\nE2E SMOKE FAILED:\n' + failures.map(f => '  - ' + f).join('\n'));
  process.exit(1);
}
console.log('\nE2E smoke passed: all chart pages rendered, violin drew density bodies, type-morph re-rendered, and v6 premium licensing gated the watermark (trial watermarked, per-chart-licensed clean).');
