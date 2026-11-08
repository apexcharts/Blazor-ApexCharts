// Issue #517: a point annotation on a horizontal rangeBar names its row with a
// category Y. The marker must sit on the row it names (Beta, Gamma), centred on
// that row's bar and level with its y-axis label, at the 04:00 x position.
//
// Everything is measured on screen (getBoundingClientRect), so the marker, the
// bar and the label are compared in one coordinate space. apexcharts.js 8.8.0
// ("fix(annotations): align horizontal bar points with the row", #5412) places the marker on the row's geometric centre, where the bar is
// drawn. Up to 8.7 it used the label's text baseline, which sits about 4.5% of
// the row height below that centre.
import { gotoChart, watchErrors, assert, assertNoErrors } from '../lib/helpers.mjs';

const ROWS = ['Alpha', 'Beta', 'Gamma'];

function measure(page) {
  return page.evaluate((rows) => {
    const root = document.querySelector('#issue517-chart');
    const centre = (el) => {
      if (!el) return null;
      const r = el.getBoundingClientRect();
      return { x: r.left + r.width / 2, y: r.top + r.height / 2 };
    };
    const marker = (cls) => centre(root.querySelector(`.apexcharts-point-annotation-marker.${cls}`));
    const labels = [...root.querySelectorAll('.apexcharts-yaxis-texts-g text')];
    const label = (name) => centre(labels.find(e => (e.querySelector('tspan')?.textContent ?? e.textContent).trim() === name));
    // One rangeBar per row, in category order (j is the data point index).
    const bar = (i) => centre(root.querySelector(`.apexcharts-rangebar-area[j="${i}"]`));
    return {
      beta: marker('issue517-beta'),
      gamma: marker('issue517-gamma'),
      labels: Object.fromEntries(rows.map(n => [n, label(n)])),
      bars: Object.fromEntries(rows.map((n, i) => [n, bar(i)])),
    };
  }, ROWS);
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

  const m = await measure(page);
  const { beta, gamma, labels, bars } = m;
  const y = (o) => (o ? Number(o.y.toFixed(2)) : null);
  console.log(`  issue 517 marker y beta=${y(beta)} gamma=${y(gamma)}; bar centres ${JSON.stringify(Object.fromEntries(ROWS.map(n => [n, y(bars[n])])))}; label centres ${JSON.stringify(Object.fromEntries(ROWS.map(n => [n, y(labels[n])])))}`);

  assert(ROWS.every(n => labels[n] && bars[n]), `row labels or bars not found: ${JSON.stringify(m)}`);
  // On the row's bar: the centre the core computes for the row.
  assert(Math.abs(beta.y - bars.Beta.y) < 1, `Beta marker y ${beta.y} should be centred on the Beta bar ${bars.Beta.y}`);
  assert(Math.abs(gamma.y - bars.Gamma.y) < 1, `Gamma marker y ${gamma.y} should be centred on the Gamma bar ${bars.Gamma.y}`);
  // Level with the row's label (the glyph box, not its baseline).
  assert(Math.abs(beta.y - labels.Beta.y) < 2, `Beta marker y ${beta.y} should be level with the Beta label ${labels.Beta.y}`);
  assert(Math.abs(gamma.y - labels.Gamma.y) < 2, `Gamma marker y ${gamma.y} should be level with the Gamma label ${labels.Gamma.y}`);
  assert(Math.abs(beta.y - bars.Alpha.y) > 10, 'Beta marker must not sit on the Alpha row');
  // Both markers use the same 04:00 timestamp, so they share an x inside the plot.
  assert(beta.x > 0 && Math.abs(beta.x - gamma.x) < 0.5, `markers should share the 04:00 x, got ${beta.x} and ${gamma.x}`);

  assertNoErrors(realErrors, 'issue 517');
}
