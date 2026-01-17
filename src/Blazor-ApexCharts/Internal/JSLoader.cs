using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace ApexCharts.Internal;

/// <summary>
/// Internal use
/// </summary>
public static class JSLoader
{
    /// <summary>
    /// Loads the main JS file to run blazor apexcharts
    /// </summary>
    /// <param name="jsRuntime"></param>
    /// <param name="path"></param>
	public static async Task<IJSObjectReference> LoadAsync(IJSRuntime jsRuntime, string path = null)
    {
        var javascriptPath = "./_content/Blazor-ApexCharts/js/blazor-apexcharts.js?ver=6.1.0";
        if (!string.IsNullOrWhiteSpace(path)) { javascriptPath = path; }

        // load Module ftom ES6 script
        IJSObjectReference module = await jsRuntime.InvokeAsync<IJSObjectReference>("import", javascriptPath);
        // load the  blazor_apexchart parent, currently window! to be compatyble with JS interop calls e.g blazor_apexchart.dataUri
        return await module.InvokeAsync<IJSObjectReference>("get_apexcharts");
    }
}
