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

// The Blazor hosting model the suite runs against: 'wasm' (docs/BlazorApexCharts.Docs.Wasm, the
// default) or 'server' (docs/BlazorApexCharts.Docs.Server). Chosen with the HOST environment
// variable, HOST=server or HOST=wasm, case-insensitive. Any other value, a machine name some shells
// and CI images export as HOST for instance, is not a hosting model and means the default. run.mjs
// checks the page it warms up against this value and stops the run when they disagree.
export function blazorHost() {
  return String(process.env.HOST || '').trim().toLowerCase() === 'server' ? 'server' : 'wasm';
}

// True when the suite runs against the Blazor Server docs host.
export const isServerHost = () => blazorHost() === 'server';

// Reads which hosting model the page actually loaded, from the Blazor boot script: 'server',
// 'wasm', or null when the page has no Blazor script (or has the unified blazor.web.js, which can
// be either). Call it after navigating.
export async function detectBlazorHost(page) {
  return page.evaluate(() => {
    const srcs = Array.from(document.scripts, s => s.src || '');
    if (srcs.some(s => /\/blazor\.server(\.min)?\.js(\?|$)/.test(s))) return 'server';
    if (srcs.some(s => /\/blazor\.webassembly(\.min)?\.js(\?|$)/.test(s))) return 'wasm';
    return null;
  });
}

// Thrown by skipOnHost. run.mjs reports a spec that throws it as SKIP, not FAIL, and lists it in
// the summary, so a skip is never silent.
export class SpecSkipped extends Error {
  constructor(host, reason) {
    super(`skipped on the ${host} host: ${reason}`);
    this.name = 'SpecSkipped';
    this.host = host;
    this.reason = reason;
  }
}

// Ends the calling spec as skipped when the suite runs on `host` ('wasm' or 'server'). For specs
// that are inherently tied to one hosting model; `reason` says why, and run.mjs prints it.
//
//   skipOnHost('server', 'measures WASM startup, which a Server circuit does not have');
export function skipOnHost(host, reason) {
  if (host !== 'wasm' && host !== 'server') throw new Error(`skipOnHost: unknown host ${JSON.stringify(host)}`);
  if (!reason) throw new Error('skipOnHost: a reason is required');
  if (blazorHost() === host) throw new SpecSkipped(host, reason);
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
