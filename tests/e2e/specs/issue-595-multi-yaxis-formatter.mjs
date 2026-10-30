// Issue #595: FormatYAxisLabelForAxis formats each Y axis with the axis index the chart wrote
// into its formatter. The repro page labels axis 0 with "A0:" and axis 1 with "A1:".
import { gotoChart, watchErrors, assert, assertNoErrors } from '../lib/helpers.mjs';

const axisLabels = (page, rel) =>
  page.locator(`#issue595-chart .apexcharts-yaxis[rel='${rel}'] .apexcharts-yaxis-label tspan`).allTextContents();

export default async function (page, BASE) {
  const realErrors = watchErrors(page);

  await gotoChart(page, BASE, 'issues/595', '#issue595-chart .apexcharts-yaxis[rel="1"] .apexcharts-yaxis-label');

  // The labels are drawn by the .NET callback, so wait until both axes show the formatted text.
  await page.waitForFunction(() => {
    const texts = rel => Array.from(document.querySelectorAll(
      `#issue595-chart .apexcharts-yaxis[rel='${rel}'] .apexcharts-yaxis-label tspan`)).map(e => e.textContent);
    const a0 = texts(0), a1 = texts(1);
    return a0.length > 0 && a1.length > 0 && a0.some(t => t.startsWith('A0:')) && a1.some(t => t.startsWith('A1:'));
  }, null, { timeout: 30000 });

  const axis0 = (await axisLabels(page, 0)).filter(t => t !== '');
  const axis1 = (await axisLabels(page, 1)).filter(t => t !== '');

  assert(axis0.every(t => t.startsWith('A0:')), `axis 0 labels should all start with A0:, got ${JSON.stringify(axis0)}`);
  assert(axis1.every(t => t.startsWith('A1:')), `axis 1 labels should all start with A1:, got ${JSON.stringify(axis1)}`);

  // Axis 0 is bound to Revenue (up to 2100) and axis 1 to Margin (up to 30), so the scales differ.
  const max = arr => Math.max(...arr.map(t => Number(t.split(':')[1])));
  assert(max(axis0) >= 2000, `axis 0 should carry the Revenue scale, got ${JSON.stringify(axis0)}`);
  assert(max(axis1) <= 100, `axis 1 should carry the Margin scale, got ${JSON.stringify(axis1)}`);

  assertNoErrors(realErrors, 'issue-595');
}
