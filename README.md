

![.NET Core](https://github.com/joadan/Blazor-ApexCharts/workflows/.NET%20Core/badge.svg?branch=master)



# Blazor-ApexCharts
A blazor wrapper for [ApexCharts.js](https://apexcharts.com/)
## [Demo](https://apexcharts.github.io/Blazor-ApexCharts)


## Installation
### NuGet
For Blazor projects running in a web browser, WinForms, or WPF.
[Blazor-ApexCharts](https://www.nuget.org/packages/Blazor-ApexCharts/)

```bash
dotnet add package Blazor-ApexCharts
```

For Blazor projects running on .NET MAUI.
[Blazor-ApexCharts-MAUI](https://www.nuget.org/packages/Blazor-ApexCharts-MAUI/)

```bash
dotnet add package Blazor-ApexCharts-MAUI
```

### ChartService
ApexChartService is an optional service that will manage global options, set locales, manage charts on the screen.
Add the chart service to the DI container by using the extension AddApexCharts(). This will add a scoped IApexChartService to the container.

```razor
services.AddApexCharts();
```
or add it with global options

```razor
services.AddApexCharts(e =>
            {
                e.GlobalOptions = new ApexChartBaseOptions
                {
                    Debug = true,
                    Theme = new Theme { Palette = PaletteType.Palette6 }
                };
            });
```

The same as above can be used for .NET MAUI, just make sure to use the method below to add the chart service.

```razor
services.AddApexChartsMaui();
```

## Usage


### Imports
Add a reference to `Blazor-ApexCharts` in your `_Imports.razor`
```razor
@using ApexCharts
```

### .NET 8
If you are on .NET 8 you need to set the rendermode to Interactive.

*Interactive Server, Interactive WebAssembly or Interactive Auto*


### Your first chart
```razor
    <ApexChart TItem="MyData"
               Title="Sample Data">

        <ApexPointSeries TItem="MyData"
                         Items="Data"
                         Name="Net Profit"
                         SeriesType="SeriesType.Bar"
                         XValue="e => e.Category"
                         YValue="e=> e.NetProfit" />

        <ApexPointSeries TItem="MyData"
                         Items="Data"
                         Name="Revenue"
                         SeriesType="SeriesType.Bar"
                         XValue="e => e.Category"
                         YValue="e=> e.Revenue" />
    </ApexChart>
    
@code {
    private List<MyData> Data { get; set; } = new();
    protected override void OnInitialized()
    {
        Data.Add(new MyData { Category = "Jan", NetProfit = 12, Revenue = 33 });
        Data.Add(new MyData { Category = "Feb", NetProfit = 43, Revenue = 42 });
        Data.Add(new MyData { Category = "Mar", NetProfit = 112, Revenue = 23 });
    }

    public class MyData
    {
        public string Category { get; set; }
        public int NetProfit { get; set; }
        public int Revenue { get; set; }
    }
}
```


### Chart Options
Apex Chart options are available in the `ApexChartOptions` class that can be passed to the chart. More info in Apex documentation [ApexCharts Docs](https://apexcharts.com/docs/options/).

**The chart options cannot be shared.  Each chart instance must have its own ApexChartOptions instance**

## Premium Features & Licensing
ApexCharts 6.0 introduced a set of premium interaction modules: history (undo/redo), perspectives (shareable view state), link (crossfilter / linked views), ink (draggable annotations), measure (delta ruler), contextMenu (point actions) and storyboard (scroll-driven views). All chart types and every other option remain free and are never gated.

The premium modules work fully in **trial mode**, but a chart that uses one shows an unobtrusive `APEXCHARTS` watermark until a valid license key is applied. The key format is shared across the ApexCharts family (apexgantt, apextree, apexsankey) and is validated offline (no network call).

Apply a key one of three ways:

**1. Application-wide via the chart service** (recommended; the key is applied once before any chart renders):
```razor
services.AddApexCharts(e =>
{
    e.LicenseKey = "APEX-your-license-key";
});
```
For .NET MAUI use `AddApexChartsMaui(...)` with the same option.

**2. From configuration / environment:**
```razor
services.AddApexCharts(e =>
{
    e.LicenseKey = builder.Configuration["APEXCHARTS_LICENSE_KEY"];
});
```

**3. Per-chart** via the chart options (works without the service, and never causes a first-render flash):
```razor
<ApexChart TItem="MyData" Options="@(new ApexChartOptions<MyData> { Chart = new Chart { License = "APEX-your-license-key" } })">
    ...
</ApexChart>
```

You can also apply or change a key at runtime through the service; a late valid key followed by a chart update clears an on-screen watermark without a full re-render:
```razor
await ChartService.SetLicenseAsync("APEX-your-license-key");
```

Without any key, `AddApexCharts()` still works; premium features simply render watermarked.

## Acknowledgments
Credits to [@thirstyape](https://github.com/thirstyape) for making production release possible.


[![Stargazers repo roster for @apexcharts/Blazor-ApexCharts](https://reporoster.com/stars/dark/apexcharts/Blazor-ApexCharts)](https://github.com/apexcharts/Blazor-ApexCharts/stargazers)

