using Microsoft.Extensions.Options;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexCharts.Internal;

internal static class JSLoader
{
    internal static async Task<IJSObjectReference> LoadAsync(IJSRuntime jsRuntime, string path = null)
    {
        var javascriptPath = "./_content/Blazor-ApexCharts/js/blazor-apexcharts.js?ver=5.1";
        if (!string.IsNullOrWhiteSpace(path)) { javascriptPath = path; }

        // load Module ftom ES6 script
        IJSObjectReference module = await jsRuntime.InvokeAsync<IJSObjectReference>("import", javascriptPath);
        // load the  blazor_apexchart parent, currently window! to be compatyble with JS interop calls e.g blazor_apexchart.dataUri                                                                                                    
        return await module.InvokeAsync<IJSObjectReference>("get_apexcharts");
    }
}
