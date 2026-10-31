// Issue #517: a point annotation on a horizontal rangeBar names its row with a
// category Y. The marker must sit on the row it names (Beta, Gamma), level with
// that row's y-axis label, at the 04:00 x position.
import { gotoChart, watchErrors, assert, assertNoErrors } from '../lib/helpers.mjs';

function centreOf(page, cls) {
  return page.evaluate((cls) => {
    const m = document.querySelector(`#issue517-chart .apexcharts-point-annotation-marker.${cls}`);
    if (!m) return null;
    const b = m.getBBox();
    return { x: b.x + b.width / 2, y: b.y + b.height / 2 };
  }, cls);
}

function rowLabelY(page, name) {
  return page.evaluate((name) => {
    const t = [...document.querySelectorAll('#issue517-chart .apexcharts-yaxis-texts-g text')]
      .find(e => (e.querySelector('tspan')?.textContent ?? e.textContent).trim() === name);
    return t ? Number(t.getAttribute('y')) : null;
  }, name);
}

export default async function (page, BASE) {
  const realErrors = watchErrors(page);
  await gotoChart(page, BASE, 'issues/517', '#issue517-chart svg.apexcharts-svg');

  await page.waitForFunction(() =>
    document.querySelector('#issue517-chart .apexcharts-point-annotation-marker.issue517-beta') &&
    document.querySelector('#issue517-chart .apexcharts-point-annotation-marker.issue517-gamma'),
    null, { timeout: 15000 }).catch(async () => {
      throw new Error(`point annotation markers did not render; errors: ${JSON.stringify(realErrors())}`);
    });

  const beta = await centreOf(page, 'issue517-beta');
  const gamma = await centreOf(page, 'issue517-gamma');
  const rows = { Alpha: await rowLabelY(page, 'Alpha'), Beta: await rowLabelY(page, 'Beta'), Gamma: await rowLabelY(page, 'Gamma') };
  console.log(`  issue 517 markers beta=${JSON.stringify(beta)} gamma=${JSON.stringify(gamma)} rows=${JSON.stringify(rows)}`);

  assert(rows.Alpha !== null && rows.Beta !== null && rows.Gamma !== null, `row labels not found: ${JSON.stringify(rows)}`);
  assert(Math.abs(beta.y - rows.Beta) < 2, `Beta marker y ${beta.y} should match the Beta row ${rows.Beta}`);
  assert(Math.abs(gamma.y - rows.Gamma) < 2, `Gamma marker y ${gamma.y} should match the Gamma row ${rows.Gamma}`);
  assert(Math.abs(beta.y - rows.Alpha) > 10, 'Beta marker must not sit on the Alpha row');
  // Both markers use the same 04:00 timestamp, so they share an x inside the plot.
  assert(beta.x > 0 && Math.abs(beta.x - gamma.x) < 0.5, `markers should share the 04:00 x, got ${beta.x} and ${gamma.x}`);

  assertNoErrors(realErrors, 'issue 517');
}
