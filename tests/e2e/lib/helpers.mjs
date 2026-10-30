// Shared helpers for e2e specs (tests/e2e/specs/*.mjs). Import what you need:
//
//   import { gotoChart, watchErrors, assert } from '../lib/helpers.mjs';

// Console noise the docs demo produces on any host that is not the deployed site. Kept in step with
// isIgnorableError in smoke.mjs: the "view source" widget fetching .razor files cross-origin (404,
// connection refused, CORS) and the license-domain notice for premium features in trial mode.
export const isIgnorableError = t =>
  /Failed to load resource: the server responded with a status of 404/.test(t) ||
  /ERR_CONNECTION_REFUSED|ERR_NAME_NOT_RESOLVED|ERR_INTERNET_DISCONNECTED/.test(t) ||
  /razor_source|blocked by CORS policy|Access to fetch|net::ERR_FAILED/.test(t) ||
  /\[Apex\].*licen[sc]e|not valid for this domain/i.test(t);

// Starts recording console errors and uncaught page errors. Returns a function that gives the
// errors seen so far, minus the ignorable ones.
export function watchErrors(page) {
  const errors = [];
  page.on('console', m => { if (m.type() === 'error') errors.push(m.text().slice(0, 300)); });
  page.on('pageerror', e => errors.push('PAGEERROR: ' + e.message.slice(0, 300)));
  return () => errors.filter(e => !isIgnorableError(e));
}

// Navigates to a demo route (no leading slash) and waits until `selector` is present. The default
// selector is the first rendered chart. Waits on the element, never on a fixed delay.
export async function gotoChart(page, BASE, route, selector = '.apexcharts-canvas svg', timeout = 30000) {
  await page.goto(`${BASE}/${route}`, { waitUntil: 'domcontentloaded' });
  await page.waitForSelector(selector, { timeout });
}

// Throws with `message` unless `condition` is truthy.
export function assert(condition, message) {
  if (!condition) throw new Error(message);
}

// Throws if any non-ignorable console or page error was recorded.
export function assertNoErrors(realErrors, label = '') {
  const errs = realErrors();
  if (errs.length) throw new Error(`${label ? `[${label}] ` : ''}console/page errors: ${JSON.stringify(errs)}`);
}

// The Blazor host the demo runs on: 'wasm' (WebAssembly, the default demo) or 'server' (Blazor
// Server). Behavior that needs synchronous JS to .NET interop, such as the .NET label formatters,
// differs between the two, so a spec that checks it asks which one it is on.
// E2E_HOST=wasm|server in the environment wins; otherwise the Blazor script the page loaded decides.
// Call after the page has navigated to the demo.
export async function blazorHost(page) {
  const forced = (process.env.E2E_HOST || '').toLowerCase();
  if (forced === 'wasm' || forced === 'server') return forced;
  if (forced !== '') throw new Error(`E2E_HOST must be "wasm" or "server", got "${process.env.E2E_HOST}"`);

  const scripts = await page.evaluate(() =>
    Array.from(document.querySelectorAll('script[src]')).map(s => s.getAttribute('src')));
  if (scripts.some(src => /blazor\.server(\.min)?\.js/.test(src))) return 'server';
  if (scripts.some(src => /blazor\.webassembly(\.min)?\.js/.test(src))) return 'wasm';
  throw new Error(`cannot tell the Blazor host from the page scripts ${JSON.stringify(scripts)}; set E2E_HOST=wasm|server`);
}

// Starts collecting the "Blazor-ApexCharts:" console warnings the library writes when a host cannot
// call .NET synchronously, from the first script on. Call before navigating. Read them with
// syncWarnings(page).
export async function recordSyncWarnings(page) {
  await page.addInitScript(() => {
    window.__apexSyncWarnings = [];
    const warn = console.warn.bind(console);
    console.warn = (...args) => {
      if (String(args[0]).startsWith('Blazor-ApexCharts:')) window.__apexSyncWarnings.push(String(args[0]));
      warn(...args);
    };
  });
}

// The warnings recorded by recordSyncWarnings, each as { chartId, callbacks, message }.
export async function syncWarnings(page) {
  const messages = await page.evaluate(() => (window.__apexSyncWarnings || []).slice());
  return messages.map(message => {
    const m = /chart "([^"]*)" skipped (.+?) because/.exec(message);
    return { chartId: m ? m[1] : null, callbacks: m ? m[2].split(', ') : [], message };
  });
}
