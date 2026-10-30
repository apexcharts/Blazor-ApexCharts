// Issue #611: a y-axis annotation with a numeric Width made apexcharts.js throw
// (it calls width.indexOf('%')). The wrapper now sends a number as a px string.
// The page draws four lines: Width = 100, "200px", "50%" and unset (full plot).
import { gotoChart, watchErrors, assert, assertNoErrors } from '../lib/helpers.mjs';

export default async function (page, BASE) {
  const realErrors = watchErrors(page);
  await gotoChart(page, BASE, 'issues/611', '#issue611-chart svg.apexcharts-svg');

  // All four lines exist only once the annotations rendered without throwing.
  const ids = ['issue611-number', 'issue611-px', 'issue611-percent', 'issue611-default'];
  await page.waitForFunction((ids) => ids.every(id =>
    document.querySelector(`#issue611-chart .apexcharts-yaxis-annotations line.${id}`)), ids, { timeout: 15000 })
    .catch(async () => {
      throw new Error(`y-axis annotation lines did not render; errors: ${JSON.stringify(realErrors())}`);
    });

  const x2 = await page.evaluate((ids) => Object.fromEntries(ids.map(id => {
    const l = document.querySelector(`#issue611-chart .apexcharts-yaxis-annotations line.${id}`);
    return [id, Number(l.getAttribute('x2')) - Number(l.getAttribute('x1'))];
  })), ids);
  console.log(`  issue 611 line lengths: ${JSON.stringify(x2)}`);

  assert(Math.abs(x2['issue611-number'] - 100) < 0.5, `Width = 100 should draw a 100px line, got ${x2['issue611-number']}`);
  assert(Math.abs(x2['issue611-px'] - 200) < 0.5, `Width = "200px" should draw a 200px line, got ${x2['issue611-px']}`);
  const full = x2['issue611-default'];
  assert(full > 300, `unset width should cover the plot, got ${full}`);
  assert(Math.abs(x2['issue611-percent'] - full / 2) < 1, `Width = "50%" should be half of ${full}, got ${x2['issue611-percent']}`);

  assertNoErrors(realErrors, 'issue 611');
}
