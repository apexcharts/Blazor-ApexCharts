// Issue #565: RenderAsync scrolled the page up. renderChart destroys the chart and builds a new one
// in the same container; destroying empties the container and apexcharts.js resets its min-height,
// so while the new chart rendered the container was shorter, the page got shorter and the browser
// clamped the scroll position. /issues/565 puts a chart with a fixed height in pixels and, last, a
// donut with no fixed height (its height is worked out while it renders) at the end of a long page.
// At the top are a chart with a height of 100%, one whose responsive entry makes its height 100%,
// and one with no height of its own, which renders at its automatic height until the spec sets a
// global height of 100%, and later a global responsive entry of 100% (setGlobalOptions).
import { gotoChart, watchErrors, assert, assertNoErrors } from '../lib/helpers.mjs';

const CHART = '#issue565-chart';
const PERCENT_CARD = '#issue565-percent-card';
const FIXED_CHART = '#issue565-fixed-chart';

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
  const renders = { [CHART]: 1, [FIXED_CHART]: 1 };
  await page.locator('#issue565-fixed-rendered', { hasText: '1' }).waitFor({ timeout: 20000 });

  // Sets the viewport height and, when that changes the size, waits for the redraws it starts.
  // apexcharts.js answers a window resize by redrawing every chart 150ms later (_windowResize), and
  // a chart can redraw again when the redraw resizes its container's parent. While the 100% charts
  // redraw the page is briefly shorter, which moves a scroll position at the page end, so a round
  // has to start after them. Each chart is marked before the resize; the wait ends once every
  // chart that redraws on a window resize has done so (resized again), finished (animationEnded),
  // and has no redraw queued, for two frames in a row, as a parent resize is reported after the
  // frame's animation callbacks.
  const setViewportHeight = async height => {
    const current = page.viewportSize();
    if (current.width === viewport.width && current.height === height) {
      return;
    }

    await page.evaluate(() => {
      for (const { chart } of window.Apex._chartInstances || []) {
        chart.w.globals.resized = false;
        if (!chart.__issue565Watched) {
          chart.__issue565Watched = true;
          const windowResize = chart._windowResize;
          chart._windowResize = function () {
            this.__issue565Queued = true;
            return windowResize.apply(this, arguments);
          };
          const update = chart.update;
          chart.update = function () {
            this.__issue565Queued = false;
            return update.apply(this, arguments);
          };
        }
      }
    });

    await page.setViewportSize({ width: viewport.width, height });

    // Checked once a frame
    await page.evaluate(() => { window.__issue565Quiet = 0; });
    await page.waitForFunction(() => {
      const settled = (window.Apex._chartInstances || []).every(({ chart }) => {
        const g = chart.w.globals;
        return !chart.w.config.chart.redrawOnWindowResize
          || (g.resized && g.animationEnded && !chart.__issue565Queued);
      });
      window.__issue565Quiet = settled ? window.__issue565Quiet + 1 : 0;
      return window.__issue565Quiet >= 2;
    }, null, { polling: 'raf', timeout: 20000 });
  };

  // Renders the chart in `sel` again with the page scrolled to its end, and checks the scroll
  // position never moves and the container is left with the chart's own min-height.
  const scrollRound = async (sel, button, counter, anchor, height, label) => {
    await setViewportHeight(height);
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
    }, sel);

    // A click through the DOM: page.click would first scroll a button out of view into view.
    await page.evaluate(b => document.querySelector(b).click(), button);
    renders[sel]++;

    // The new chart has rendered (a different svg, OnRendered raised again) and the hold is released.
    await page.waitForFunction(([sel, counter, count]) => {
      const svg = document.querySelector(sel + ' .apexcharts-canvas svg');
      const container = svg && svg.closest('.apexcharts-canvas').parentElement;
      const held = window.blazor_apexchart.heldContainers;
      return !!svg && !svg.hasAttribute('data-issue565-old')
        && document.querySelector(counter).textContent.trim() === String(count)
        && container.style.getPropertyValue('height') === ''
        && (held === undefined || held.size === 0);
    }, [sel, counter, renders[sel]], { timeout: 20000 });

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
    }, sel);

    const tag = `[${label}, overflow-anchor: ${anchor}, viewport height ${height}]`;
    assert(after.min === before.scrollY && after.max === before.scrollY && after.scrollY === before.scrollY,
      `${tag} scroll position should stay at ${before.scrollY} while the chart re-renders, saw ${JSON.stringify(after)}`);

    // The new chart sets the same min-height the first one did, and the hold leaves nothing behind.
    assert(after.minHeight === before.minHeight,
      `${tag} container min-height should be the chart's own ${before.minHeight}, got ${after.minHeight}`);
  };

  const rounds = [
    { anchor: 'auto', height: viewport.height },
    { anchor: 'none', height: viewport.height },
    { anchor: 'auto', height: 300 },
  ];
  for (const { anchor, height } of rounds) {
    await scrollRound(CHART, '#issue565-render', '#issue565-rendered', anchor, height, 'donut');
  }

  // A chart whose height is in pixels, with a responsive entry in pixels too, is still held: the
  // checks for a percentage height below must not turn the hold off for it.
  await scrollRound(FIXED_CHART, '#issue565-fixed-render', '#issue565-fixed-rendered', 'none', viewport.height, 'fixed height');
  await setViewportHeight(viewport.height);

  // 2. A chart with a height of 100% keeps its height over renders. apexcharts.js works that height
  // out from the parent of the chart's container, here a card sized by its content (a min-height of
  // 300px and a 30px heading). A container held at its old height would make the card taller while
  // the new chart measures it, and the chart would grow by the heading on every render.
  // Renders the chart in `card` again four times (RenderAsync) and returns the height of each svg,
  // led by the height of the chart there before them unless `skipFirst`.
  const svgHeight = card => page.evaluate(sel => {
    const svg = document.querySelector(sel + ' .apexcharts-canvas svg');
    return svg ? Number(svg.getAttribute('height')) : null;
  }, card);

  const heightsOverRenders = async (card, button, counter, skipFirst) => {
    let count = Number(await page.locator(counter).textContent());
    const heights = skipFirst ? [] : [await svgHeight(card)];
    for (let render = 1; render <= 4; render++) {
      await page.evaluate(sel => {
        document.querySelector(sel + ' .apexcharts-canvas svg').setAttribute('data-issue565-old', '');
      }, card);
      await page.click(button);
      count++;
      await page.waitForFunction(([sel, counter, count]) => {
        const svg = document.querySelector(sel + ' .apexcharts-canvas svg');
        return !!svg && !svg.hasAttribute('data-issue565-old')
          && document.querySelector(counter).textContent.trim() === String(count)
          && (window.blazor_apexchart.heldContainers === undefined || window.blazor_apexchart.heldContainers.size === 0);
      }, [card, counter, count], { timeout: 20000 });
      heights.push(await svgHeight(card));
    }
    return heights;
  };

  const percentHeights = await heightsOverRenders(PERCENT_CARD, '#issue565-percent-render', '#issue565-percent-rendered', false);
  assert(percentHeights[0] > 0 && percentHeights.every(h => h === percentHeights[0]),
    `a chart with a height of 100% should keep its height over renders, got ${JSON.stringify(percentHeights)}`);

  // The same 100% reached through a responsive entry that overrides the chart's own height of 350.
  await page.locator('#issue565-responsive-rendered', { hasText: '1' }).waitFor({ timeout: 20000 });
  const responsiveHeights = await heightsOverRenders('#issue565-responsive-card', '#issue565-responsive-render', '#issue565-responsive-rendered', false);
  assert(responsiveHeights[0] > 0 && responsiveHeights[0] !== 350 && responsiveHeights.every(h => h === responsiveHeights[0]),
    `a chart with a responsive height of 100% should keep its height over renders, got ${JSON.stringify(responsiveHeights)}`);

  // The same 100% reached through the global options, for a chart with no height of its own. The
  // chart rendered before the global height was set, so only the renders after it count. The
  // global options are put back afterwards, as the rest of the page was rendered with them.
  await page.locator('#issue565-global-rendered', { hasText: '1' }).waitFor({ timeout: 20000 });
  const autoHeight = await svgHeight('#issue565-global-card');
  await page.evaluate(() => {
    window.__issue565Apex = window.Apex;
    window.blazor_apexchart.setGlobalOptions(JSON.stringify({ chart: { height: '100%' } }));
  });
  try {
    const globalHeights = await heightsOverRenders('#issue565-global-card', '#issue565-global-render', '#issue565-global-rendered', true);
    assert(globalHeights[0] > 0 && globalHeights[0] !== autoHeight && globalHeights.every(h => h === globalHeights[0]),
      `a chart with a global height of 100% should keep its height over renders, got ${JSON.stringify(globalHeights)}`);

    // A chart with a height of its own in pixels is still held while a global height of 100% is set.
    await scrollRound(FIXED_CHART, '#issue565-fixed-render', '#issue565-fixed-rendered', 'none', viewport.height, 'fixed height, global height of 100%');
    await setViewportHeight(viewport.height);

    // A global responsive entry of 100% reaches the chart with no height and no responsive entries
    // of its own, so it keeps its height over renders too.
    await page.evaluate(() => {
      window.blazor_apexchart.setGlobalOptions(JSON.stringify({
        responsive: [{ breakpoint: 5000, options: { chart: { height: '100%' } } }]
      }));
    });
    const globalResponsiveHeights = await heightsOverRenders('#issue565-global-card', '#issue565-global-render', '#issue565-global-rendered', false);
    assert(globalResponsiveHeights[0] > 0 && globalResponsiveHeights[0] !== autoHeight
      && globalResponsiveHeights.every(h => h === globalResponsiveHeights[0]),
      `a chart with a global responsive height of 100% should keep its height over renders, got ${JSON.stringify(globalResponsiveHeights)}`);

    // The fixed chart has a responsive list of its own, which apexcharts.js puts in place of the
    // global one, so it keeps its 320px and its container is still held.
    await scrollRound(FIXED_CHART, '#issue565-fixed-render', '#issue565-fixed-rendered', 'none', viewport.height, 'fixed height, global responsive height of 100%');
    const fixedHeight = await svgHeight(FIXED_CHART);
    assert(fixedHeight === 320,
      `the fixed chart should keep the height of its own responsive entry under a global one of 100%, got ${fixedHeight}`);
  } finally {
    await page.evaluate(() => {
      window.__issue565Apex._chartInstances = window.Apex._chartInstances;
      window.Apex = window.__issue565Apex;
    });
  }

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
