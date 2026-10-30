// Issue #565: RenderAsync scrolled the page up. renderChart destroys the chart and builds a new one
// in the same container; destroying empties the container and apexcharts.js resets its min-height,
// so while the new chart rendered the container was shorter, the page got shorter and the browser
// clamped the scroll position. /issues/565 puts a donut with no fixed height (its height is worked
// out while it renders) at the end of a long page.
import { gotoChart, watchErrors, assert, assertNoErrors } from '../lib/helpers.mjs';

const CHART = '#issue565-chart';

export default async function (page, BASE) {
  const realErrors = watchErrors(page);

  await gotoChart(page, BASE, 'issues/565', `${CHART} .apexcharts-canvas svg`);
  await page.locator('#issue565-rendered', { hasText: '1' }).waitFor({ timeout: 20000 });

  // Chromium's scroll anchoring often puts the scroll position back after the clamp; a browser
  // without it (or a page where the anchor is inside the chart, which is destroyed) keeps the jump.
  // Render once with anchoring and once without.
  let renders = 1;
  for (const anchor of ['auto', 'none']) {
    await page.evaluate(a => { document.documentElement.style.overflowAnchor = a; }, anchor);

    // Scroll to the end of the page, where a shorter page has to move the scroll position. The docs
    // theme scrolls smoothly, so jump there and wait until the page is at its end.
    await page.evaluate(() => window.scrollTo({ top: document.documentElement.scrollHeight, behavior: 'instant' }));
    await page.waitForFunction(() => window.scrollY > 1000
      && window.scrollY + window.innerHeight >= document.documentElement.scrollHeight - 1, null, { timeout: 10000 });

    // Mark the chart that is there now, and record every scroll position from here on.
    const before = await page.evaluate(sel => {
      const container = document.querySelector(sel + ' .apexcharts-canvas').parentElement;
      container.querySelector('svg').setAttribute('data-issue565-old', '');
      const seen = { min: window.scrollY, max: window.scrollY };
      window.__issue565 = seen;
      const record = () => {
        seen.min = Math.min(seen.min, window.scrollY);
        seen.max = Math.max(seen.max, window.scrollY);
      };
      window.addEventListener('scroll', record, { passive: true });
      const frame = () => { record(); if (!seen.stop) requestAnimationFrame(frame); };
      requestAnimationFrame(frame);
      return {
        scrollY: window.scrollY,
        minHeight: container.style.getPropertyValue('min-height'),
      };
    }, CHART);

    await page.click('#issue565-render');
    renders++;

    // The new chart has rendered (a different svg, OnRendered raised again) and the hold is released.
    await page.waitForFunction(([sel, count]) => {
      const svg = document.querySelector(sel + ' .apexcharts-canvas svg');
      const container = svg && svg.closest('.apexcharts-canvas').parentElement;
      const held = window.blazor_apexchart.heldContainers;
      return !!svg && !svg.hasAttribute('data-issue565-old')
        && document.getElementById('issue565-rendered').textContent.trim() === String(count)
        && container.style.getPropertyValue('height') === ''
        && (held === undefined || held.size === 0);
    }, [CHART, renders], { timeout: 20000 });

    const after = await page.evaluate(sel => {
      const seen = window.__issue565;
      seen.stop = true;
      const container = document.querySelector(sel + ' .apexcharts-canvas').parentElement;
      return {
        scrollY: window.scrollY,
        min: seen.min,
        max: seen.max,
        minHeight: container.style.getPropertyValue('min-height'),
      };
    }, CHART);

    assert(after.min === before.scrollY && after.max === before.scrollY && after.scrollY === before.scrollY,
      `[overflow-anchor: ${anchor}] scroll position should stay at ${before.scrollY} while the chart re-renders, saw ${JSON.stringify(after)}`);

    // The new chart sets the same min-height the first one did, and the hold leaves nothing behind.
    assert(after.minHeight === before.minHeight,
      `[overflow-anchor: ${anchor}] container min-height should be the chart's own ${before.minHeight}, got ${after.minHeight}`);
  }

  // Disposing a chart while its container is held releases the hold.
  const released = await page.evaluate(() => {
    const lib = window.blazor_apexchart;
    const el = document.createElement('div');
    document.body.appendChild(el);
    lib.holdContainerHeight('issue565-probe', el, 120);
    const held = el.style.getPropertyValue('height');
    lib.destroyChart('issue565-probe');
    const result = { held, after: el.style.getPropertyValue('height'), size: lib.heldContainers.size };
    el.remove();
    return result;
  });
  assert(released.held === '120px' && released.after === '' && released.size === 0,
    `destroyChart should release a held container, got ${JSON.stringify(released)}`);

  assertNoErrors(realErrors, 'issue-565');
}
