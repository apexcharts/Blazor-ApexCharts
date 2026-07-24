using ApexCharts;
using BlazorApexCharts.Docs.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BlazorApexCharts.Docs.Wasm
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddHttpClient("GitHub", client => client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("Blazor-ApexCharts", "1")));
            builder.Services.AddScoped<ICodeSnippetService, GitHubSnippetService>();

            // Domain-locked license for the public demo hosted at https://apexcharts.github.io/ (removes the
            // trial watermark on the v6 premium features there). The key is valid only on that host, so local
            // runs and any other domain render in trial mode (watermarked), which is expected.
            builder.Services.AddApexCharts(options =>
            {
                options.LicenseKey = "APEX-eyJpc3N1ZURhdGUiOiIyMDI2LTA3LTAxIiwiZXhwaXJ5RGF0ZSI6IjIwNTAtMDEtMDEiLCJwbGFuIjoiZW50ZXJwcmlzZSIsImRvbWFpbnMiOlsiYXBleGNoYXJ0cy5naXRodWIuaW8iXX0=";
            });

          

            await builder.Build().RunAsync();
        }
    }
}
