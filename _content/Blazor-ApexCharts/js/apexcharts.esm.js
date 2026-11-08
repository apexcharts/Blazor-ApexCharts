// Loads the self-contained ApexCharts.js full build (apexcharts.full.min.js, copied unchanged
// from the npm package) and re-exports the global it sets.
import './apexcharts.full.min.js?ver=8.8.1';
export default globalThis.ApexCharts;
