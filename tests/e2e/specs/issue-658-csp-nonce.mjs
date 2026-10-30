// Issue 658: Chart.Nonce reaches apexcharts.js, so the <style> elements the chart injects carry the
// nonce and survive a strict CSP. The page is served with a real Content-Security-Policy header whose
// style-src allows external stylesheets and the nonce, but not 'unsafe-inline'. Scripts are left
// unrestricted so the docs host and the Blazor runtime boot as usual.
import { gotoChart, assert, detectHost, isIgnorableError } from '../lib/helpers.mjs';

const NONCE = 'issue658e2enonce'; // Issue658.razor CspNonce
const CSP = `style-src 'self' https: 'nonce-${NONCE}'`;

export default async function (page, BASE) {
  // The browser logs every CSP violation as a console error. The docs host page has an inline style
  // attribute of its own (the loading spinner), so violations are checked below by the element they
  // hit, and CSP console messages are left out of the error check.
  const errors = [];
  page.on('console', m => { if (m.type() === 'error') errors.push(m.text().slice(0, 300)); });
  page.on('pageerror', e => errors.push('PAGEERROR: ' + e.message.slice(0, 300)));

  await page.addInitScript(() => {
    window.__cspViolations = [];
    document.addEventListener('securitypolicyviolation', e => {
      window.__cspViolations.push({ directive: e.effectiveDirective, target: e.target });
    });
    // A page rule that shows every span in the chart's container, as ".card span { display: inline-block }"
    // would in an app without a CSS framework. Such a rule beats the browser's [hidden] rule, so the tooltip
    // source must not rely on that alone. The docs site's CSS framework already declares
    // [hidden] { display: none !important }, so this rule needs !important to override it here. A constructed
    // stylesheet is CSSOM, which the policy does not block.
    const sheet = new CSSStyleSheet();
    sheet.replaceSync('#issue-658-chart span { display: inline-block !important; }');
    document.adoptedStyleSheets = [...document.adoptedStyleSheets, sheet];
  });

  // A document answered through route.fulfill loses the local address space Chromium recorded for it,
  // so Local Network Access treats the page as public and blocks its connections back to localhost.
  // The WASM host needs none after boot; a Blazor Server host needs its SignalR WebSocket, so grant the
  // permission there (and on a host that could be either).
  if (await detectHost(page, BASE) !== 'wasm') {
    await page.context().grantPermissions(['local-network-access']);
  }

  // Add the CSP header to the HTML document only.
  await page.route(`${BASE}/issues/658`, async route => {
    const response = await route.fetch();
    await route.fulfill({ response, headers: { ...response.headers(), 'content-security-policy': CSP } });
  });

  await gotoChart(page, BASE, 'issues/658', '#issue-658-chart .apexcharts-canvas svg');
  await page.locator('#issue-658-chart .apexcharts-legend-series').first().waitFor({ timeout: 15000 });

  // Before any hover, the live tooltip source sits in its hidden home next to the chart.
  const beforeHover = await page.evaluate(() => {
    const root = document.querySelector('#issue-658-chart');
    const home = root.querySelector('[id^="tooltip_home_"]');
    const source = root.querySelector('[id^="tooltip_source_"]');
    return {
      homeHidden: home ? home.hasAttribute('hidden') && !home.checkVisibility() : null,
      sourceInHome: !!home && !!source && source.parentElement === home,
      sourceVisible: source ? source.checkVisibility() : null,
    };
  });

  // Show the Razor tooltip on one bar and then on another, so the live source is moved into the chart
  // tooltip, and the copy apexcharts clones from it (styles set from JS included) is inserted, under
  // the policy. A single mouse move can land beside the bar when the page shifts after the bar was
  // measured (a late font or the bar's entry animation), and the chart sees no further move, so
  // measure and hover again until the tooltip shows the bar or the time runs out.
  // Checked after the nonce assertions, which explain a failure better.
  const bar = j => page.locator(`#issue-658-chart .apexcharts-series[data\\:realIndex="0"] path[j="${j}"]`);
  // Resolves to the tooltip text once it is non-empty and differs from `previous`, or null at the deadline.
  async function hoverUntil(j, previous) {
    for (const deadline = Date.now() + 15000; Date.now() < deadline;) {
      const box = await bar(j).boundingBox();
      if (box) {
        await page.mouse.move(0, 0);
        await page.mouse.move(box.x + box.width / 2, box.y + box.height / 2);
      }
      const text = await page.waitForFunction(prev => {
        const el = document.querySelector('#issue-658-chart .apexcharts-tooltip #issue-658-tooltip');
        const t = el ? el.textContent.trim() : '';
        return t !== '' && t !== prev ? t : null;
      }, previous, { timeout: 2000 }).then(h => h.jsonValue(), () => null);
      if (text !== null) return text;
    }
    return null;
  }
  const firstText = await hoverUntil(0, null);
  const secondText = firstText === null ? null : await hoverUntil(1, firstText);
  const tooltipShown = firstText !== null && secondText !== null;

  const state = await page.evaluate(() => {
    const root = document.querySelector('#issue-658-chart');
    const canvas = root.querySelector('.apexcharts-canvas');
    const sheet = document.getElementById('apexcharts-css');
    // A header-delivered CSP hides the nonce content attribute, so read the IDL property.
    const legendSheets = [...root.querySelectorAll('style')]
      .filter(s => s.textContent.includes('apexcharts-legend'))
      .map(s => s.nonce);
    const home = root.querySelector('[id^="tooltip_home_"]');
    const sources = [...document.querySelectorAll('[id^="tooltip_source_"]')];
    // The tooltip fragment anywhere on the page but in the chart tooltip.
    const offTooltip = [...document.querySelectorAll('#issue-658-tooltip')]
      .filter(e => !e.closest('.apexcharts-tooltip') && e.checkVisibility());
    // A plain <span hidden> under the same rule, to show the rule really overrides [hidden].
    const probe = document.createElement('span');
    probe.hidden = true;
    probe.textContent = 'probe';
    root.appendChild(probe);
    const probeVisible = probe.checkVisibility();
    probe.remove();
    return {
      sheetNonce: sheet ? sheet.nonce : null,
      legendSheets,
      // Declared only in the injected apexcharts stylesheet, so it is set only when that sheet applied.
      focusColor: getComputedStyle(canvas).getPropertyValue('--apexcharts-focus-color').trim(),
      legendDisplay: getComputedStyle(root.querySelector('.apexcharts-legend')).display,
      probeVisible,
      homeHidden: home ? home.hasAttribute('hidden') && !home.checkVisibility() : null,
      // While the tooltip shows, the one live source sits in it: no second copy, no duplicate id.
      sourceCount: sources.length,
      sourceInTooltip: sources.length === 1 && !!sources[0].closest('.apexcharts-tooltip'),
      visibleOffTooltip: offTooltip.length,
      // Violations that hit a <style> element or anything the chart component rendered.
      violations: window.__cspViolations
        .filter(v => v.target instanceof Element && (v.target.tagName === 'STYLE' || root.contains(v.target)))
        .map(v => `${v.directive}: ${v.target.outerHTML.slice(0, 120)}`),
    };
  });

  const details = JSON.stringify({ beforeHover, firstText, secondText, ...state });

  assert(state.sheetNonce === NONCE, `#apexcharts-css should carry the nonce: ${details}`);
  assert(state.legendSheets.length > 0 && state.legendSheets.every(n => n === NONCE),
    `the legend <style> should carry the nonce: ${details}`);
  assert(state.focusColor.toLowerCase() === '#008ffb', `the apexcharts stylesheet was not applied: ${details}`);
  assert(state.legendDisplay === 'flex', `the legend stylesheet was not applied: ${details}`);
  assert(state.probeVisible === true, `the page rule should override [hidden] on a plain span: ${details}`);
  assert(beforeHover.homeHidden === true && beforeHover.sourceInHome && beforeHover.sourceVisible === false,
    `before a hover the source of the Razor tooltip should sit hidden in its home: ${details}`);
  assert(state.homeHidden === true, `the home of the Razor tooltip source should stay hidden: ${details}`);
  assert(state.visibleOffTooltip === 0, `the Razor tooltip content should show only in the chart tooltip: ${details}`);
  assert(state.violations.length === 0, `CSP blocked styles of the chart: ${details}`);
  assert(tooltipShown, `the Razor tooltip did not show on hover of two bars: ${details}`);
  assert(state.sourceCount === 1 && state.sourceInTooltip,
    `the live Razor tooltip source should be the only one and sit in the chart tooltip: ${details}`);

  const realErrors = errors.filter(e => !isIgnorableError(e) && !/Content Security Policy/.test(e));
  assert(realErrors.length === 0, `console/page errors: ${JSON.stringify(realErrors)}`);
}
