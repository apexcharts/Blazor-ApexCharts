// Issue 655: chart teardown in the browser. destroyChart drops the chart's .NET reference before it
// calls chart.destroy(), so when chart.destroy() throws, the half destroyed chart's formatters (which
// look the reference up in dotNetRefs) format with the raw value instead of calling a reference .NET
// is about to release.
//
// This covers the JavaScript half only. The .NET half (Dispose() releases the reference only after
// destroyChart) is pinned by Issue655DisposeTests: this demo runs on WebAssembly, where Blazor
// disposes through DisposeAsync and destroyChart runs in process, so no browser run reaches it.
import { gotoChart, watchErrors, assert, assertNoErrors } from '../lib/helpers.mjs';

export default async function (page, BASE) {
  const realErrors = watchErrors(page);

  await gotoChart(page, BASE, 'features/formatters');
  await page.waitForFunction(() => window.blazor_apexchart
    && [...window.blazor_apexchart.dotNetRefs.keys()].some(id => window.ApexCharts.getChartByID(id) !== undefined),
    null, { timeout: 30000 });

  const dropped = await page.evaluate(() => {
    const api = window.blazor_apexchart;
    const id = [...api.dotNetRefs.keys()].find(key => window.ApexCharts.getChartByID(key) !== undefined);
    const chart = window.ApexCharts.getChartByID(id);
    const destroy = chart.destroy;
    chart.destroy = () => { throw new Error('issue655 destroy failed'); };
    let threw = false;
    try {
      api.destroyChart(id);
    } catch (e) {
      threw = String(e && e.message).includes('issue655');
    } finally {
      chart.destroy = destroy;
    }
    const stillReferenced = api.dotNetRefs.has(id);
    destroy.call(chart);
    return { id, threw, stillReferenced };
  });
  assert(dropped.threw, 'the patched chart.destroy() was not called by destroyChart');
  assert(!dropped.stillReferenced, `destroyChart kept the .NET reference of chart ${dropped.id} after chart.destroy() threw`);

  assertNoErrors(realErrors, 'destroyChart with a failing chart.destroy()');
}
