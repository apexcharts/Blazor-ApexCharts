// #644: an @onclick handler on a button inside an ApexPointTooltip must fire. The tooltip used to show
// an innerHTML copy of the rendered fragment, which carries none of Blazor's event handlers. The fix
// moves the live element into the chart tooltip; apexcharts inserts a clone of it on every hover, which
// the live element replaces at once. Besides clicking, this spec checks that the page never holds two
// copies of the tooltip ids at any moment, and that disposing the chart leaves nothing behind.
import { gotoChart, watchErrors, assert, assertNoErrors } from '../lib/helpers.mjs';

async function hoverAndClick(page, pointIndex, category, expectedClicks) {
  // Series 0 ("Net Profit"), the bar at pointIndex.
  const bar = page.locator(`#issue644 .apexcharts-series[data\\:realIndex="0"] path[j="${pointIndex}"]`);
  await bar.hover();

  // The tooltip shows the hovered category once the .NET render has landed. Polled without a locator,
  // so a duplicate copy fails on the count below instead of on a strict-mode violation.
  await page.waitForFunction(text =>
    [...document.querySelectorAll('#issue644 .apexcharts-tooltip #issue644-tooltip-x')].some(e => e.textContent.includes(text)),
  `Category ${category}`, { timeout: 10000 });
  const buttons = await page.evaluate(() => document.querySelectorAll('#issue644-button').length);
  assert(buttons === 1, `expected one tooltip button in the page while hovering ${category}, found ${buttons}`);

  await page.locator('#issue644 .apexcharts-tooltip #issue644-button').click();

  await page.locator('#issue644-message', { hasText: `Clicked ${category}` }).waitFor({ timeout: 10000 });
  await page.locator('#issue644-clicks', { hasText: String(expectedClicks) }).waitFor({ timeout: 10000 });
}

// Sweeps the pointer across every bar of both series in a few quick moves, with no wait for .NET between.
async function sweep(page) {
  const box = await page.locator('#issue644 .apexcharts-grid').first().boundingBox();
  assert(box, 'plot grid not found');
  const y = box.y + box.height - 5;
  for (let pass = 0; pass < 3; pass++) {
    await page.mouse.move(box.x + 2, y);
    await page.mouse.move(box.x + box.width - 2, y, { steps: 25 });
    await page.mouse.move(box.x + 2, y, { steps: 25 });
  }
}

export default async function (page, BASE) {
  const realErrors = watchErrors(page);

  // After every DOM insertion into a chart tooltip, count the tooltip ids in the whole document, so a
  // duplicate that exists only between two DOM calls of one hover is caught too.
  await page.addInitScript(() => {
    const max = window.__issue644MaxIds = { source: 0, target: 0, button: 0, checks: 0 };
    const check = node => {
      const el = node.nodeType === 1 ? node : node.parentElement;
      if (!el || !el.closest('#issue644 .apexcharts-tooltip')) return;
      max.checks++;
      max.source = Math.max(max.source, document.querySelectorAll('[id^="tooltip_source_"]').length);
      max.target = Math.max(max.target, document.querySelectorAll('[id^="tooltip_target_"]').length);
      max.button = Math.max(max.button, document.querySelectorAll('#issue644-button').length);
    };
    for (const [proto, name] of [[Node.prototype, 'appendChild'], [Node.prototype, 'insertBefore'],
      [Element.prototype, 'replaceChildren'], [Element.prototype, 'append']]) {
      const original = proto[name];
      proto[name] = function (...args) {
        const result = original.apply(this, args);
        check(this);
        return result;
      };
    }
  });

  await gotoChart(page, BASE, 'issues/644', '#issue644 .apexcharts-canvas svg');
  await page.locator('#issue644 .apexcharts-bar-area').first().waitFor({ timeout: 30000 });

  const chartId = await page.locator('#issue644 [id^="tooltip_home_"]')
    .evaluate(e => e.id.slice('tooltip_home_'.length));

  await hoverAndClick(page, 2, 'Mar', 1);
  // A second point: the tooltip keeps updating after the first click, and the handler sees the new point.
  await hoverAndClick(page, 0, 'Jan', 2);
  // The same point again, after the pointer left it.
  await page.mouse.move(0, 0);
  await hoverAndClick(page, 0, 'Jan', 3);

  // Fast moves across all points queue many .NET renders; the tooltip must settle on the last point
  // with the live, clickable element.
  await sweep(page);
  await hoverAndClick(page, 1, 'Feb', 4);

  // Destroying and re-creating the chart throws the tooltip element away with the live content in it.
  await page.locator('#issue644-rerender').click();
  await page.locator('#issue644-redraws', { hasText: 'Redraws: 1' }).waitFor({ timeout: 10000 });
  await hoverAndClick(page, 1, 'Feb', 5);

  // An apexcharts-side redraw through updateOptions.
  await page.locator('#issue644-update').click();
  await page.locator('#issue644-redraws', { hasText: 'Redraws: 2' }).waitFor({ timeout: 10000 });
  await hoverAndClick(page, 2, 'Mar', 6);

  // While the tooltip shows, the live element is the only copy: the hidden home is empty and the page
  // holds one element per id.
  const shown = await page.evaluate(id => ({
    sources: document.querySelectorAll(`[id="tooltip_source_${id}"]`).length,
    targets: document.querySelectorAll(`[id="tooltip_target_${id}"]`).length,
    buttons: document.querySelectorAll('#issue644-button').length,
    sourceInTooltip: !!document.getElementById(`tooltip_source_${id}`)?.closest('#issue644 .apexcharts-tooltip'),
    homeChildren: document.getElementById(`tooltip_home_${id}`)?.childElementCount,
    max: window.__issue644MaxIds,
  }), chartId);
  const details = JSON.stringify(shown);
  assert(shown.sources === 1 && shown.targets === 1 && shown.buttons === 1,
    `expected exactly one tooltip source, target and button in the page: ${details}`);
  assert(shown.sourceInTooltip && shown.homeChildren === 0, `the live source should sit in the chart tooltip: ${details}`);
  assert(shown.max.checks > 0, `no insertion into the chart tooltip was observed: ${details}`);
  assert(shown.max.source <= 1 && shown.max.target <= 1 && shown.max.button <= 1,
    `the page held two copies of a tooltip id at some point: ${details}`);

  // Disposing the chart (navigating away) must not keep the detached live element referenced.
  await page.evaluate(() => window.Blazor.navigateTo('issues/658'));
  await page.waitForFunction(id =>
    document.getElementById(`tooltip_home_${id}`) === null &&
    document.getElementById(`tooltip_source_${id}`) === null &&
    !window.blazor_apexchart.tooltipSources.has(id) &&
    !window.blazor_apexchart.dotNetRefs.has(id), chartId, { timeout: 10000 })
    .catch(async () => {
      const left = await page.evaluate(id => ({
        home: !!document.getElementById(`tooltip_home_${id}`),
        source: !!document.getElementById(`tooltip_source_${id}`),
        mapped: window.blazor_apexchart.tooltipSources.has(id),
        dotNetRef: window.blazor_apexchart.dotNetRefs.has(id),
      }), chartId);
      throw new Error(`the disposed chart left its tooltip behind: ${JSON.stringify(left)}`);
    });

  assertNoErrors(realErrors, 'issue-644');
}
