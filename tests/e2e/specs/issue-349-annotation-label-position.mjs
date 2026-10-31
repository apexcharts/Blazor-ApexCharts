// Issue #349: LabelPosition gained Top, Center and Bottom. An x-axis annotation
// label sits at the top, center or bottom of the plot; a y-axis annotation label
// at the left, center or right.
import { gotoChart, watchErrors, assert, assertNoErrors } from '../lib/helpers.mjs';

export default async function (page, BASE) {
  const realErrors = watchErrors(page);
  await gotoChart(page, BASE, 'issues/349', '#issue349-chart svg.apexcharts-svg');

  const ids = ['issue349-top', 'issue349-center', 'issue349-bottom', 'issue349-left', 'issue349-ycenter', 'issue349-right'];
  await page.waitForFunction((ids) => ids.every(id =>
    document.querySelector(`#issue349-chart text.${id}`)), ids, { timeout: 15000 })
    .catch(async () => {
      throw new Error(`annotation labels did not render; errors: ${JSON.stringify(realErrors())}`);
    });

  const pos = await page.evaluate((ids) => Object.fromEntries(ids.map(id => {
    const t = document.querySelector(`#issue349-chart text.${id}`);
    return [id, { x: Number(t.getAttribute('x')), y: Number(t.getAttribute('y')) }];
  })), ids);
  console.log(`  issue 349 label positions: ${JSON.stringify(pos)}`);

  const top = pos['issue349-top'].y, center = pos['issue349-center'].y, bottom = pos['issue349-bottom'].y;
  assert(top < center && center < bottom, `x-axis labels should run top < center < bottom, got ${top}, ${center}, ${bottom}`);

  const left = pos['issue349-left'].x, ycenter = pos['issue349-ycenter'].x, right = pos['issue349-right'].x;
  assert(left < 1, `y-axis Left label should start at the left edge, got ${left}`);
  assert(Math.abs(ycenter - right / 2) < 1, `y-axis Center label should be at half of ${right}, got ${ycenter}`);

  assertNoErrors(realErrors, 'issue 349');
}
