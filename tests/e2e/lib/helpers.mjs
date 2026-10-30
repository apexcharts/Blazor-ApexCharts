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
