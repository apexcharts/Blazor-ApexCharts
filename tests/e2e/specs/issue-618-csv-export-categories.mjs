// Issue #618: CSV export of a line chart with numeric X values (1..1600) and TickAmount = 40.
//
// The wrong categories in the default chart come from apexcharts.js (Exports.exportToCSV reads the
// axis tick labels when a category axis has been turned numeric), so this spec does not fail on
// them. It asserts the documented workaround, XAxisType.Numeric, exports every X value, and reports
// whether the vendored bundle still has the upstream bug.
import fs from 'node:fs/promises';
import { gotoChart, watchErrors, assert, assertNoErrors } from '../lib/helpers.mjs';

async function downloadCsv(page, containerId) {
  const container = page.locator(`#${containerId}`);
  await container.locator('.apexcharts-menu-icon').click();
  const item = container.locator('.apexcharts-menu-item.exportCSV');
  await item.waitFor({ state: 'visible', timeout: 10000 });
  const [download] = await Promise.all([page.waitForEvent('download', { timeout: 15000 }), item.click()]);
  const text = await fs.readFile(await download.path(), 'utf8');
  return text.replace(/^﻿/, '').split('\n');
}

// Rows whose category is not the 1-based row number.
const wrongRows = lines =>
  lines.slice(1).filter((line, i) => Number(line.split(',')[0]) !== i + 1);

export default async function (page, BASE) {
  const realErrors = watchErrors(page);

  await gotoChart(page, BASE, 'issues/618', '#chart-numeric .apexcharts-canvas svg');
  await page.waitForSelector('#chart-default .apexcharts-canvas svg', { timeout: 30000 });

  const numeric = await downloadCsv(page, 'chart-numeric');
  assert(numeric[0].startsWith('XValue'), `numeric: unexpected header ${JSON.stringify(numeric[0])}`);
  assert(numeric.length - 1 === 1600, `numeric: expected 1600 data rows, got ${numeric.length - 1}`);
  const numericWrong = wrongRows(numeric);
  assert(
    numericWrong.length === 0,
    `numeric: ${numericWrong.length} rows have the wrong category, first ${JSON.stringify(numericWrong.slice(0, 3))}`,
  );

  const fallback = await downloadCsv(page, 'chart-default');
  assert(fallback.length - 1 === 1600, `default: expected 1600 data rows, got ${fallback.length - 1}`);
  const fallbackWrong = wrongRows(fallback);
  console.log(
    fallbackWrong.length
      ? `   issue-618: default axis still exports ${fallbackWrong.length} tick labels as categories (apexcharts.js bug, see #618)`
      : '   issue-618: default axis exports every X value (apexcharts.js fix is in the vendored bundle)',
  );

  assertNoErrors(realErrors, 'issue-618');
}
