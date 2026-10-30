// Issue #565: RenderAsync scrolled the page up. renderChart destroys the chart and builds a new one
// in the same container; destroying empties the container and apexcharts.js resets its min-height,
// so while the new chart rendered the container was shorter, the page got shorter and the browser
// clamped the scroll position. /issues/565 puts a donut with no fixed height (its height is worked
// out while it renders) at the end of a long page, and a chart with a height of 100% at the top.
import { gotoChart, watchErrors, assert, assertNoErrors } from '../lib/helpers.mjs';

const CHART = '#issue565-chart';
const PERCENT_CARD = '#issue565-percent-card';

export default async function (page, BASE) {
  const realErrors = watchErrors(page);

  await gotoChart(page, BASE, 'issues/565', `${CHART} .apexcharts-canvas svg`);
  await page.locator('#issue565-rendered', { hasText: '1' }).waitFor({ timeout: 20000 });
  await page.locator('#issue565-percent-rendered', { hasText: '1' }).waitFor({ timeout: 20000 });

  // Count the promise rejections nobody handles, for the whole spec.
  await page.evaluate(() => {
    window.__issue565Unhandled = [];
    window.addEventListener('unhandledrejection', e => {
      window.__issue565Unhandled.push(String(e.reason && e.reason.message ? e.reason.message : e.reason));
    });
  });

  // 1. The scroll position stays put while RenderAsync rebuilds the chart.
  //
  // Chromium's scroll anchoring puts the position back on its own when the anchor node (the first
  // element in view) is above the chart, so the first round, with anchoring and the spacer in view,
  // passes without the fix: it checks that the hold does not fight anchoring. The other two fail
  // without the fix: a browser without anchoring (Safari), and a viewport so short that the anchor
  // node is inside the chart, which is destroyed.
  const viewport = page.viewportSize();
  const rounds = [
    { anchor: 'auto', height: viewport.height },
    { anchor: 'none', height: viewport.height },
    { anchor: 'auto', height: 300 },
  ];
  let renders = 1;
  for (const { anchor, height } of rounds) {
    await page.setViewportSize({ width: viewport.width, height });
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
      `[overflow-anchor: ${anchor}, viewport height ${height}] scroll position should stay at ${before.scrollY} while the chart re-renders, saw ${JSON.stringify(after)}`);

    // The new chart sets the same min-height the first one did, and the hold leaves nothing behind.
    assert(after.minHeight === before.minHeight,
      `[overflow-anchor: ${anchor}, viewport height ${height}] container min-height should be the chart's own ${before.minHeight}, got ${after.minHeight}`);
  }
  await page.setViewportSize(viewport);

  // 2. A chart with a height of 100% keeps its height over renders. apexcharts.js works that height
  // out from the parent of the chart's container, here a card sized by its content (a min-height of
  // 300px and a 30px heading). A container held at its old height would make the card taller while
  // the new chart measures it, and the chart would grow by the heading on every render.
  const percentHeight = () => page.evaluate(sel => {
    const svg = document.querySelector(sel + ' .apexcharts-canvas svg');
    return svg ? Number(svg.getAttribute('height')) : null;
  }, PERCENT_CARD);

  const percentHeights = [await percentHeight()];
  for (let render = 2; render <= 4; render++) {
    await page.evaluate(sel => {
      document.querySelector(sel + ' .apexcharts-canvas svg').setAttribute('data-issue565-old', '');
    }, PERCENT_CARD);
    await page.click('#issue565-percent-render');
    await page.waitForFunction(([sel, count]) => {
      const svg = document.querySelector(sel + ' .apexcharts-canvas svg');
      return !!svg && !svg.hasAttribute('data-issue565-old')
        && document.getElementById('issue565-percent-rendered').textContent.trim() === String(count)
        && (window.blazor_apexchart.heldContainers === undefined || window.blazor_apexchart.heldContainers.size === 0);
    }, [PERCENT_CARD, render], { timeout: 20000 });
    percentHeights.push(await percentHeight());
  }

  assert(percentHeights[0] > 0 && percentHeights.every(h => h === percentHeights[0]),
    `a chart with a height of 100% should keep its height over renders, got ${JSON.stringify(percentHeights)}`);

  // 3. A re-render that fails releases the hold and is handled as before: apexcharts.js handles its
  // own render promise, so a failed render raises no unhandled rejection. Drives the real
  // renderChart with a chart type apexcharts.js cannot draw.
  await page.evaluate(() => {
    const el = document.createElement('div');
    el.id = 'issue565-failing';
    document.body.appendChild(el);
    const options = type => JSON.stringify({
      chart: { id: 'issue565-failing', type: type, height: 200, animations: { enabled: false } },
      series: [{ name: 'a', data: [1, 2, 3] }],
      xaxis: { categories: ['a', 'b', 'c'] }
    });
    window.__issue565Options = options;
    window.blazor_apexchart.renderChart({ invokeMethodAsync: () => Promise.resolve() }, el, options('line'), {});
  });
  await page.waitForSelector('#issue565-failing .apexcharts-canvas svg .apexcharts-series', { timeout: 20000 });

  const failing = await page.evaluate(async () => {
    const lib = window.blazor_apexchart;
    const el = document.getElementById('issue565-failing');
    const unhandledBefore = window.__issue565Unhandled.length;

    lib.renderChart({ invokeMethodAsync: () => Promise.resolve() }, el, window.__issue565Options('issue565-no-such-type'), {});
    const heldWhileRendering = lib.heldContainers.has('issue565-failing');

    // The hold is released once the render has failed
    await new Promise(resolve => {
      const check = () => lib.heldContainers.has('issue565-failing') ? requestAnimationFrame(check) : resolve();
      check();
    });

    // A rejection nobody handles is reported in a task queued at the end of the microtask checkpoint
    // in which it was rejected; let those tasks run before counting.
    await new Promise(resolve => setTimeout(resolve, 0));
    await new Promise(resolve => setTimeout(resolve, 0));

    const result = {
      heldWhileRendering,
      height: el.style.getPropertyValue('height'),
      unhandled: window.__issue565Unhandled.slice(unhandledBefore),
    };
    lib.destroyChart('issue565-failing');
    el.remove();
    return result;
  });

  assert(failing.heldWhileRendering,
    `the failing re-render should hold its container (else this check proves nothing), got ${JSON.stringify(failing)}`);
  assert(failing.height === '' && failing.unhandled.length === 0,
    `a failed re-render should release its container and raise no unhandled rejection, got ${JSON.stringify(failing)}`);

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

  const unhandled = await page.evaluate(() => window.__issue565Unhandled);
  assert(unhandled.length === 0, `no promise rejection should go unhandled, got ${JSON.stringify(unhandled)}`);

  assertNoErrors(realErrors, 'issue-565');
}
