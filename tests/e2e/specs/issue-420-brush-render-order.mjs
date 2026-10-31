// Issue 420: a brush chart failed with "Cannot read properties of undefined (reading 'w')" and
// stayed blank when it rendered before the chart it targets. Charts render independently, so on
// a cold page load the order varies; /issues/420 declares the brush first to force that order.
import { gotoChart, watchErrors, assert, assertNoErrors } from '../lib/helpers.mjs';

const SELECTION_MIN = Date.UTC(2024, 0, 31);
const SELECTION_MAX = Date.UTC(2024, 2, 11);

// Values set by the "Update brush" button on /issues/420
const UPDATED_COLOR = '#E51C15';
const UPDATED_VALUE = 99;
const NOTE_TEXT = 'issue420-note';

// The brush is wired when the target knows its brush source and shows the selected range.
async function waitForBrush(page, brushId, targetId) {
  await page.waitForFunction(([b, t]) => {
    const AC = window.ApexCharts;
    const brush = AC && AC.getChartByID(b);
    const target = AC && AC.getChartByID(t);
    return !!(brush && target && target.w.globals.brushSource === brush);
  }, [brushId, targetId], { timeout: 20000 });

  const range = await page.evaluate(t => {
    const g = window.ApexCharts.getChartByID(t).w.globals;
    return { min: g.minX, max: g.maxX };
  }, targetId);
  assert(range.min === SELECTION_MIN && range.max === SELECTION_MAX,
    `${targetId} should show the brush selection, got ${JSON.stringify(range)}`);
}

export default async function (page, BASE) {
  const realErrors = watchErrors(page);

  // C: a brush chart whose target id matches no chart says so in the console after a while.
  const warned = page.waitForEvent('console', {
    predicate: m => m.type() === 'warning' && m.text().includes('issue420-c-brush') && m.text().includes('issue420-c-missing'),
    timeout: 60000,
  });
  warned.catch(() => { }); // awaited below; keeps an early timeout from going unhandled

  // A: the brush chart is declared, and renders, before its target.
  await gotoChart(page, BASE, 'issues/420', '#issue420-a-target-host .apexcharts-canvas svg');
  await page.waitForSelector('#issue420-a-brush-host .apexcharts-selection-rect', { timeout: 20000 });
  await waitForBrush(page, 'issue420-a-brush', 'issue420-a-target');

  // B: the target does not exist yet. The brush chart waits for it instead of failing.
  assert(await page.locator('#issue420-b-target-host').count() === 0, 'B target should start hidden');
  await page.waitForFunction(() => window.blazor_apexchart.pendingBrushCharts.has('issue420-b-brush'), null, { timeout: 20000 });
  assertNoErrors(realErrors, 'issue-420 before target');

  // A read while it waits returns an empty result instead of failing (GetDataUriAsync).
  await page.click('#issue420-read-image');
  await page.locator('#issue420-b-image', { hasText: '1: empty' }).waitFor({ timeout: 20000 });

  // Updates sent while it waits are kept, and OnRendered has not fired for a chart that is not there.
  await page.click('#issue420-update-brush');
  await page.locator('#issue420-b-updated', { hasText: 'sent' }).waitFor({ timeout: 20000 });
  assert(await page.locator('#issue420-b-brush-host .apexcharts-canvas').count() === 0, 'B brush should not render before its target');
  assert((await page.textContent('#issue420-b-brush-rendered')).trim() === '0', 'B brush OnRendered should wait for the chart to render');

  await page.click('#issue420-show-target');
  await page.waitForSelector('#issue420-b-target-host .apexcharts-canvas svg', { timeout: 20000 });
  await page.waitForSelector('#issue420-b-brush-host .apexcharts-selection-rect', { timeout: 20000 });
  await waitForBrush(page, 'issue420-b-brush', 'issue420-b-target');

  // The brush chart renders with the options and series sent while it waited (UpdateOptionsAsync,
  // UpdateSeriesAsync), then gets the annotation (AddXAxisAnnotationAsync) and raises OnRendered.
  const state = await page.evaluate(() => {
    const w = window.ApexCharts.getChartByID('issue420-b-brush').w;
    return { color: w.config.colors && w.config.colors[0], values: w.globals.series[0] };
  });
  assert(state.color === UPDATED_COLOR, `B brush should use the color from UpdateOptionsAsync, got ${state.color}`);
  assert(state.values.length === 100 && state.values.every(v => v === UPDATED_VALUE),
    `B brush should show the series from UpdateSeriesAsync, got ${JSON.stringify(state.values.slice(0, 5))}`);
  await page.locator('#issue420-b-brush-host .apexcharts-xaxis-annotations', { hasText: NOTE_TEXT }).waitFor({ timeout: 20000 });
  await page.locator('#issue420-b-brush-rendered', { hasText: '1' }).waitFor({ timeout: 20000 });

  // Once it has rendered the same read returns the image.
  await page.click('#issue420-read-image');
  await page.locator('#issue420-b-image', { hasText: '2: image' }).waitFor({ timeout: 20000 });

  // D: a brush chart that cannot render once its target is there (its container left the page while it
  // waited) logs an error naming it instead of an uncaught rejection, drops the calls it kept and does
  // not report itself rendered. E: a call kept while waiting that fails asynchronously when it is made
  // is logged too, and the chart still reports itself rendered.
  const dLogged = page.waitForEvent('console', {
    predicate: m => m.type() === 'error' && m.text().includes('issue420-d-brush') && m.text().includes('failed to render'),
    timeout: 20000,
  });
  const eLogged = page.waitForEvent('console', {
    predicate: m => m.type() === 'error' && m.text().includes('issue420-e-brush') && m.text().includes('issue420ReplayRejects failed'),
    timeout: 20000,
  });
  dLogged.catch(() => { });
  eLogged.catch(() => { });

  await page.evaluate(() => {
    const api = window.blazor_apexchart;
    const fake = name => {
      const calls = [];
      window[name] = calls;
      return { invokeMethodAsync: method => { calls.push(method); return Promise.resolve(); } };
    };
    const brush = (id, target) => JSON.stringify({
      chart: { id, type: 'area', height: 100, brush: { enabled: true, target }, selection: { enabled: true } },
      series: [{ name: 'Value', data: [1, 2, 3] }],
    });
    const target = id => JSON.stringify({ chart: { id, type: 'line', height: 100 }, series: [{ name: 'Value', data: [1, 2, 3] }] });
    const host = id => { const el = document.createElement('div'); el.id = id; document.body.appendChild(el); return el; };

    // D: held back, then its container is removed before the target renders
    const dContainer = host('issue420-d-brush-host');
    api.renderChart(fake('issue420DBrushCalls'), dContainer, brush('issue420-d-brush', 'issue420-d-target'), {});
    api.zoomX('issue420-d-brush', 1, 2);
    dContainer.remove();
    api.renderChart(fake('issue420DTargetCalls'), host('issue420-d-target-host'), target('issue420-d-target'), {});

    // E: held back with a kept call that rejects when it is made
    api.issue420ReplayRejects = () => Promise.reject(new Error('issue420-e-replay'));
    api.renderChart(fake('issue420EBrushCalls'), host('issue420-e-brush-host'), brush('issue420-e-brush', 'issue420-e-target'), {});
    api.deferBrushChartCall('issue420-e-brush', 'issue420ReplayRejects', []);
    api.renderChart(fake('issue420ETargetCalls'), host('issue420-e-target-host'), target('issue420-e-target'), {});
  });

  await dLogged;
  await eLogged;
  await page.waitForFunction(() => window.issue420EBrushCalls.includes('JSRendered'), null, { timeout: 20000 });
  const d = await page.evaluate(() => ({
    pending: window.blazor_apexchart.pendingBrushCharts.has('issue420-d-brush'),
    chart: window.ApexCharts.getChartByID('issue420-d-brush') !== undefined,
    calls: window.issue420DBrushCalls,
  }));
  assert(!d.pending && !d.chart && d.calls.length === 0,
    `D brush should be dropped without reporting itself rendered, got ${JSON.stringify(d)}`);

  // F: a brush chart whose render fails after apexcharts.js registered it (here its mount rejects) is
  // logged and taken out of the registry, so a lookup by its id no longer finds the broken chart.
  // G: a brush chart whose render fails only after it was destroyed and rendered again: the late
  // failure belongs to the chart that is gone, so nothing is logged and the new chart stays.
  // H: as G, but the chart is destroyed and not rendered again: nothing is logged for it either.
  const fLogged = page.waitForEvent('console', {
    predicate: m => m.type() === 'error' && m.text().includes('issue420-f-brush') && m.text().includes('failed to render'),
    timeout: 20000,
  });
  fLogged.catch(() => { });

  await page.evaluate(() => {
    const api = window.blazor_apexchart;
    const AC = window.ApexCharts;
    const fake = name => {
      const calls = [];
      window[name] = calls;
      return { invokeMethodAsync: method => { calls.push(method); return Promise.resolve(); } };
    };
    const brush = (id, target) => JSON.stringify({
      chart: { id, type: 'area', height: 100, brush: { enabled: true, target }, selection: { enabled: true } },
      series: [{ name: 'Value', data: [1, 2, 3] }],
    });
    const target = id => JSON.stringify({ chart: { id, type: 'line', height: 100 }, series: [{ name: 'Value', data: [1, 2, 3] }] });
    const host = id => { const el = document.createElement('div'); el.id = id; document.body.appendChild(el); return el; };

    // The first mount of each of these charts rejects, as a failure inside apexcharts.js would
    const failOnce = new Set(['issue420-f-brush', 'issue420-g-brush', 'issue420-h-brush']);
    window.issue420Mount = AC.prototype.mount;
    AC.prototype.mount = function (graphData) {
      const id = this.w.config.chart.id;
      if (failOnce.delete(id)) {
        return Promise.reject(new Error(id + ' mount'));
      }
      return window.issue420Mount.call(this, graphData);
    };

    // F: held back, then its render fails once the target is there
    api.renderChart(fake('issue420FBrushCalls'), host('issue420-f-brush-host'), brush('issue420-f-brush', 'issue420-f-target'), {});
    api.renderChart(fake('issue420FTargetCalls'), host('issue420-f-target-host'), target('issue420-f-target'), {});

    // G: its render fails, and before that failure is reported the component renders it again
    const gHost = host('issue420-g-brush-host');
    api.renderChart(fake('issue420GBrushCalls'), gHost, brush('issue420-g-brush', 'issue420-g-target'), {});
    api.renderChart(fake('issue420GTargetCalls'), host('issue420-g-target-host'), target('issue420-g-target'), {});
    window.issue420GFailed = AC.getChartByID('issue420-g-brush');
    api.renderChart(fake('issue420GBrushCalls2'), gHost, brush('issue420-g-brush', 'issue420-g-target'), {});

    // H: its render fails, and before that failure is reported the component is disposed
    api.renderChart(fake('issue420HBrushCalls'), host('issue420-h-brush-host'), brush('issue420-h-brush', 'issue420-h-target'), {});
    api.renderChart(fake('issue420HTargetCalls'), host('issue420-h-target-host'), target('issue420-h-target'), {});
    api.destroyChart('issue420-h-brush');
  });

  await fLogged;
  // The failures are reported in the task that started these renders, long before G's second chart
  // has mounted, so its selection rectangle in the host means all of them have been handled.
  await page.waitForSelector('#issue420-g-brush-host .apexcharts-selection-rect', { state: 'attached', timeout: 20000 });
  const fg = await page.evaluate(() => {
    const AC = window.ApexCharts;
    const g = AC.getChartByID('issue420-g-brush');
    return {
      f: AC.getChartByID('issue420-f-brush') !== undefined,
      fRendered: window.issue420FBrushCalls.includes('JSRendered'),
      gReplaced: g !== undefined && g !== window.issue420GFailed && !!g.el.querySelector('.apexcharts-selection-rect'),
      h: AC.getChartByID('issue420-h-brush') !== undefined,
      hRendered: window.issue420HBrushCalls.includes('JSRendered'),
    };
  });
  assert(!fg.f && !fg.fRendered, `F brush should be unregistered without reporting itself rendered, got ${JSON.stringify(fg)}`);
  assert(fg.gReplaced, `G brush should be the chart rendered again, got ${JSON.stringify(fg)}`);
  assert(!fg.h && !fg.hRendered, `H brush should stay destroyed without reporting itself rendered, got ${JSON.stringify(fg)}`);
  assert(!realErrors().some(e => e.includes('issue420-g-brush')), 'G brush failure is stale and should not be logged');
  assert(!realErrors().some(e => e.includes('issue420-h-brush')), 'H brush failure is stale and should not be logged');

  await page.evaluate(() => {
    const api = window.blazor_apexchart;
    window.ApexCharts.prototype.mount = window.issue420Mount;
    delete window.issue420Mount;
    delete window.issue420GFailed;
    ['issue420-d-brush', 'issue420-d-target', 'issue420-e-brush', 'issue420-e-target',
      'issue420-f-brush', 'issue420-f-target', 'issue420-g-brush', 'issue420-g-target',
      'issue420-h-brush', 'issue420-h-target'].forEach(id => {
      api.destroyChart(id);
      const el = document.getElementById(id + '-host');
      if (el) { el.remove(); }
    });
    delete api.issue420ReplayRejects;
  });

  // C: the warning names the brush chart and the id it waits for, and the chart stays empty.
  await warned;
  assert(await page.locator('#issue420-c-brush-host .apexcharts-canvas').count() === 0, 'C brush should not render');

  // The errors D, E and F log on purpose are expected; anything else (an uncaught rejection included) is not.
  const expected = ['issue420-d-brush failed to render', 'issue420ReplayRejects failed', 'issue420-f-brush failed to render'];
  assertNoErrors(() => realErrors().filter(e => !expected.some(x => e.includes(x))), 'issue-420');
}
