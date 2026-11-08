using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexCharts
{
    /// <summary>
    /// Options for the blazor component. 
    /// </summary>
    public class ApexChartsBlazorOptions
    {
        /// <summary>
        /// Alternative File path to the Blazor javascript file, blazor-apexcharts.js
        /// This must be the full path including filename. apexcharts.esm.js and apexcharts.full.min.js must be in the same folder, and features/ignite-ui.js below it when IgniteUI is on
        /// /// </summary>
        public string JavascriptPath { get; set; }
    }
}
