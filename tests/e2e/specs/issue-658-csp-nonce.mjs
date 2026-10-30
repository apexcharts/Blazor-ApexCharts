// Issue 658: Chart.Nonce reaches apexcharts.js, so the <style> elements the chart injects carry the
// nonce and survive a strict CSP. The page is served with a real Content-Security-Policy header whose
// style-src allows external stylesheets and the nonce, but not 'unsafe-inline'. Scripts are left
// unrestricted so the docs host and the Blazor runtime boot as usual.
import { gotoChart, assert, isIgnorableError } from '../lib/helpers.mjs';

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

  // Add the CSP header to the HTML document only.
  await page.route(`${BASE}/issues/658`, async route => {
    const response = await route.fetch();
    await route.fulfill({ response, headers: { ...response.headers(), 'content-security-policy': CSP } });
  });

  await gotoChart(page, BASE, 'issues/658', '#issue-658-chart .apexcharts-canvas svg');
  await page.locator('#issue-658-chart .apexcharts-legend-series').first().waitFor({ timeout: 15000 });

  // Show the Razor tooltip once, so its copy into the chart tooltip also runs under the policy.
  // A single mouse move can land beside the bar when the page shifts after the bar was measured
  // (a late font or the bar's entry animation), and the chart sees no further move, so measure and
  // hover again until the tooltip shows or the time runs out.
  // Checked after the nonce assertions, which explain a failure better.
  const tooltip = page.locator('#issue-658-chart .apexcharts-tooltip #issue-658-tooltip').first().filter({ hasText: /\S/ });
  let tooltipShown = false;
  for (const deadline = Date.now() + 15000; !tooltipShown && Date.now() < deadline;) {
    const bar = await page.locator('#issue-658-chart .apexcharts-bar-area').first().boundingBox();
    if (bar) {
      await page.mouse.move(0, 0);
      await page.mouse.move(bar.x + bar.width / 2, bar.y + bar.height / 2);
    }
    tooltipShown = await tooltip.waitFor({ timeout: 2000 }).then(() => true, () => false);
  }

  const state = await page.evaluate(() => {
    const root = document.querySelector('#issue-658-chart');
    const canvas = root.querySelector('.apexcharts-canvas');
    const sheet = document.getElementById('apexcharts-css');
    // A header-delivered CSP hides the nonce content attribute, so read the IDL property.
    const legendSheets = [...root.querySelectorAll('style')]
      .filter(s => s.textContent.includes('apexcharts-legend'))
      .map(s => s.nonce);
    const tooltipSource = root.querySelector('[id^="tooltip_source_"]');
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
      tooltipSourceVisible: tooltipSource ? tooltipSource.parentElement.checkVisibility() : null,
      // Violations that hit a <style> element or anything the chart component rendered.
      violations: window.__cspViolations
        .filter(v => v.target instanceof Element && (v.target.tagName === 'STYLE' || root.contains(v.target)))
        .map(v => `${v.directive}: ${v.target.outerHTML.slice(0, 120)}`),
    };
  });

  const details = JSON.stringify(state);

  assert(state.sheetNonce === NONCE, `#apexcharts-css should carry the nonce: ${details}`);
  assert(state.legendSheets.length > 0 && state.legendSheets.every(n => n === NONCE),
    `the legend <style> should carry the nonce: ${details}`);
  assert(state.focusColor.toLowerCase() === '#008ffb', `the apexcharts stylesheet was not applied: ${details}`);
  assert(state.legendDisplay === 'flex', `the legend stylesheet was not applied: ${details}`);
  assert(state.probeVisible === true, `the page rule should override [hidden] on a plain span: ${details}`);
  assert(state.tooltipSourceVisible === false, `the source of the Razor tooltip should stay hidden: ${details}`);
  assert(state.violations.length === 0, `CSP blocked styles of the chart: ${details}`);
  assert(tooltipShown, `the Razor tooltip did not show on hover: ${details}`);

  const realErrors = errors.filter(e => !isIgnorableError(e) && !/Content Security Policy/.test(e));
  assert(realErrors.length === 0, `console/page errors: ${JSON.stringify(realErrors)}`);
}
