// Drives index.html in emulated phones and prints what it observes as JSON. Not part of run.mjs:
// it is an investigation tool for #412 and #405, not a pass/fail spec.
//
//   python3 -m http.server 5315          (from the repository root)
//   cd tests/e2e && node repros/mobile-first-tap-flicker/probe.mjs
//
// Env: PROBE_BASE (default http://localhost:5315), WEBKIT_PATH (optional WebKit executable, for a
// Playwright install whose own WebKit build is not downloaded).
//
// Emulation cannot show the iOS hover-emulation heuristic or the Android URL bar, so this measures
// the mechanisms around them: listeners and preventDefault calls, redraws per window resize, blank
// frames, duplicate SVGs, and whether one tap on a below-the-fold button produces one click.
import { chromium, webkit, devices } from 'playwright';

const BASE = process.env.PROBE_BASE || 'http://localhost:5315';
const PAGE = `${BASE}/tests/e2e/repros/mobile-first-tap-flicker/index.html`;
const URL_BAR = 56; // px the viewport grows by when Chrome on Android hides its URL bar

const snapshot = page => page.evaluate(() => {
  const p = window.__probe;
  return { updated: p.updated, svgReplaced: p.svgReplaced, maxSvgs: p.maxSvgs, blankFrames: p.blankFrames,
    clicks: p.clicks, windowResizes: p.windowResizes, buttonEvents: p.buttonEvents.slice(),
    chartMutationsDuringTap: p.chartMutationsDuringTap, preventDefault: p.preventDefault.slice() };
});

async function open(browserType, device, query) {
  const launchOpts = browserType === webkit && process.env.WEBKIT_PATH ? { executablePath: process.env.WEBKIT_PATH } : {};
  const browser = await browserType.launch(launchOpts);
  const ctx = await browser.newContext({ ...devices[device] });
  const page = await ctx.newPage();
  await page.goto(`${PAGE}?${query}`);
  await page.waitForSelector('body[data-rendered="1"]');
  // Parent-resize redraws are gated on the initial animation having ended.
  await page.waitForFunction(() => window.__chart.w.globals.animationEnded === true);
  return { browser, ctx, page };
}

// #405: listeners apexcharts.js registers (touch ones that are not passive, and any on document or
// window).
async function listeners(type) {
  const { browser, page } = await open(chromium, 'Pixel 7', `type=${type}`);
  const l = await page.evaluate(() => window.__probe.listeners);
  await browser.close();
  return { check: 'listeners', type,
    nonPassiveTouch: l.filter(e => /^touch/.test(e.type) && e.passive !== true && e.target !== 'window'),
    perBarPathListeners: l.filter(e => /apexcharts-bar-area/.test(e.target)).length };
}

// #405: Chrome on Android fires window "resize" whenever the URL bar hides or shows during a
// scroll; only the height changes. Count full redraws for that, and for a rotation.
async function resizes(type, query) {
  const { browser, page } = await open(chromium, 'Pixel 7', `type=${type}${query}`);
  const vp = page.viewportSize();
  const steps = [
    ['URL bar hides (height only)', vp.width, vp.height + URL_BAR],
    ['URL bar shows (height only)', vp.width, vp.height],
    ['rotate to landscape', vp.height, vp.width],
    ['rotate back', vp.width, vp.height]
  ];
  const rows = [];
  for (const [step, width, height] of steps) {
    const before = await snapshot(page);
    await page.setViewportSize({ width, height });
    // The redraw is debounced by 150 ms; wait until it would have landed.
    await page.waitForTimeout(800);
    const after = await snapshot(page);
    rows.push({ step, windowResizeEvents: after.windowResizes - before.windowResizes,
      fullRedraws: after.updated - before.updated, blankFrames: after.blankFrames - before.blankFrames,
      maxSvgsAtOnce: after.maxSvgs });
  }
  await browser.close();
  return { check: 'redraws per resize', type, options: query || '(defaults)', rows };
}

// #412 in Chromium: touch-drag scroll that starts on the chart (real touch events through CDP),
// then one tap on the button that was below the fold.
async function touchScrollThenTap(type) {
  const { browser, ctx, page } = await open(chromium, 'Pixel 7', `type=${type}`);
  const cdp = await ctx.newCDPSession(page);
  const box = await page.locator('#chart .apexcharts-inner').boundingBox();
  const x = box.x + box.width * 0.45, y0 = box.y + box.height * 0.7;
  const touch = (t, y) => cdp.send('Input.dispatchTouchEvent', { type: t, touchPoints: t === 'touchEnd' ? [] : [{ x, y }] });
  for (let pass = 0; pass < 8; pass++) {
    await touch('touchStart', y0);
    for (let s = 1; s <= 10; s++) await touch('touchMove', y0 - s * 40);
    await touch('touchEnd');
    if (await page.evaluate(() => document.getElementById('below').getBoundingClientRect().bottom < innerHeight * 0.55)) break;
  }
  const b = await page.locator('#below').boundingBox();
  await page.touchscreen.tap(b.x + b.width / 2, b.y + b.height / 2);
  await page.locator('#clicks').filter({ hasText: 'Clicks: 1' }).waitFor({ timeout: 3000 }).catch(() => {});
  const s = await snapshot(page);
  await browser.close();
  return { check: 'first tap after touch scroll', browser: 'chromium Pixel 7', type,
    clicksAfterOneTap: s.clicks, buttonEvents: s.buttonEvents, chartMutationsDuringTap: s.chartMutationsDuringTap,
    preventDefault: s.preventDefault };
}

// #412 in WebKit with iPhone emulation: tap a bar first (so the chart holds hover state), scroll,
// then one tap on the button.
async function webkitTapAfterChartTap(type) {
  const { browser, page } = await open(webkit, 'iPhone 13', `type=${type}`);
  const box = await page.locator('#chart .apexcharts-inner').boundingBox();
  await page.touchscreen.tap(box.x + box.width * 0.3, box.y + box.height * 0.8);
  await page.evaluate(() => document.getElementById('below').scrollIntoView({ block: 'center' }));
  const b = await page.locator('#below').boundingBox();
  await page.touchscreen.tap(b.x + b.width / 2, b.y + b.height / 2);
  await page.locator('#clicks').filter({ hasText: 'Clicks: 1' }).waitFor({ timeout: 3000 }).catch(() => {});
  const s = await snapshot(page);
  await browser.close();
  return { check: 'first tap after chart tap and scroll', browser: 'webkit iPhone 13', type,
    clicksAfterOneTap: s.clicks, buttonEvents: s.buttonEvents, chartMutationsDuringTap: s.chartMutationsDuringTap,
    preventDefault: s.preventDefault };
}

const results = [];
for (const type of ['bar', 'area']) {
  results.push(await listeners(type));
  results.push(await resizes(type, ''));
  results.push(await resizes(type, '&redrawOnWindowResize=false'));
  results.push(await resizes(type, '&vhParent=1'));
  results.push(await resizes(type, '&vhParent=1&redrawOnWindowResize=false'));
  results.push(await touchScrollThenTap(type));
  results.push(await webkitTapAfterChartTap(type));
}
console.log(JSON.stringify(results, null, 1));
