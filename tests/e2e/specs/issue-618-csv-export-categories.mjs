// Issue #618: CSV export of a line chart with numeric X values (1..1600) and TickAmount = 40.
//
// The default chart (no XAxisType) used to export the axis tick labels in place of the X values:
// Exports.exportToCSV read the ticks once a category axis had been turned numeric (repro in
// tests/e2e/repros/csv-export-categories/). apexcharts.js 8.7.0 fixed that ("fix(exports): write each
// point's own x in the CSV, not the axis ticks"). Both charts are now asserted to export every X
// value in order: the numeric one as the documented setup, the default one as a regression guard.
import fs from 'node:fs/promises';
import { gotoChart, watchErrors, assert, assertNoErrors } from '../lib/helpers.mjs';

const ISSUE = 'https://github.com/apexcharts/Blazor-ApexCharts/issues/618 (repro: tests/e2e/repros/csv-export-categories/)';

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
  assert(
    fallbackWrong.length === 0,
    `default: ${fallbackWrong.length} rows have the wrong category, first ${JSON.stringify(fallbackWrong.slice(0, 3))}; ` +
    `the CSV must carry each point's X value, not the axis tick labels (${ISSUE})`,
  );

  assertNoErrors(realErrors, 'issue-618');
}
