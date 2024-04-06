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
        /// This must be the full path including filename and the file apexcharts.esm.js must be in the same folder
        /// /// </summary>
        public string JavascriptPath { get; set; }
    }
}
