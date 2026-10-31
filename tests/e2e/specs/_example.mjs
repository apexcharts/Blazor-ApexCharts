// Template spec. The leading underscore keeps run.mjs from running it; copy it to
// issue-<number>-<slug>.mjs and edit.
//
// Contract: export a default async function (page, BASE). Throw to fail. Return to pass.
import { gotoChart, watchErrors, assert, assertNoErrors } from '../lib/helpers.mjs';

export default async function (page, BASE) {
  const realErrors = watchErrors(page);

  await gotoChart(page, BASE, 'line-charts');

  const series = await page.locator('.apexcharts-series').count();
  assert(series > 0, `expected at least one series, got ${series}`);

  // Wait on a state unique to the outcome rather than a fixed delay, for example:
  //   await page.locator('#my-events').filter({ hasText: 'Clicks: 1' }).waitFor({ timeout: 10000 });

  assertNoErrors(realErrors, 'example');
}
