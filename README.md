[![.NET Core](https://github.com/apexcharts/Blazor-ApexCharts/workflows/.NET%20Core/badge.svg?branch=master)](https://github.com/apexcharts/Blazor-ApexCharts/actions)
[![NuGet](https://img.shields.io/nuget/v/Blazor-ApexCharts.svg)](https://www.nuget.org/packages/Blazor-ApexCharts/)
[![Downloads](https://img.shields.io/nuget/dt/Blazor-ApexCharts.svg)](https://www.nuget.org/packages/Blazor-ApexCharts/)

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

These global options are the startup defaults. Each `IApexChartService` (one per DI scope, which on Blazor Server is one per user circuit) starts from its own deep copy of them, so a change made at runtime through `ChartService.GlobalOptions`, `SetGlobalOptionsAsync` or `SetLocaleAsync` only affects the current user. Every option object is copied, including your own subclasses of the option classes (or your own `IApexChartBaseOptions` implementation), even when they also implement `IDisposable`. Strings, delegates and objects of other types that you place in the options (for example items bound to a series) are not copied and stay shared.

To change the global options after startup, change `ChartService.GlobalOptions` and call `ChartService.SetGlobalOptionsAsync(true)`, or pass new options to `ChartService.SetGlobalOptionsAsync(options, true)`. Changing the object given to `AddApexCharts` (or the injected `ApexChartsServiceOptions.GlobalOptions`) after startup only reaches chart services created afterwards, never one that already exists. Earlier versions shared that object with every chart service, so code that relied on changing it at runtime to update every user now has to call `SetGlobalOptionsAsync` in each user's scope instead.

Registering `ApexChartsServiceOptions` and `IApexChartService` as scoped by hand, instead of calling `AddApexCharts`, still works but is no longer needed to keep users apart.

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

### Set an interactive render mode

**If your chart renders as blank space with nothing in the browser console, this is why.**

From .NET 8, Blazor Web Apps render statically by default, and a component that needs
JavaScript interop has to opt into interactivity. Set the render mode on the page or the
component:

```razor
@rendermode InteractiveServer
```

`InteractiveWebAssembly` and `InteractiveAuto` work equally well. There is no error
message for this, which is what makes it cost people an afternoon.


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


### Choosing a chart type

For most chart types, change the `SeriesType` enum on the series and nothing else. It
covers twelve values: `Area`, `Bar`, `Donut`, `Heatmap`, `Line`, `Pie`, `PolarArea`,
`Radar`, `RadialBar`, `Scatter`, `Treemap` and `RangeArea`.

Pie, donut, polar area and radial bar are single dimensional, so they normally pair
`SeriesType` with `YAggregate` instead of `YValue`, collapsing many rows into one value
per category:

```razor
<ApexChart TItem="Order" Title="Gross value by country">
    <ApexPointSeries TItem="Order"
                     Items="orders"
                     Name="Gross Value"
                     SeriesType="SeriesType.Donut"
                     XValue="@(e => e.Country)"
                     YAggregate="@(e => e.Sum(e => e.GrossValue))"
                     OrderByDescending="e => e.Y"
                     ShowDataLabels />
</ApexChart>
```

Funnel and pyramid charts are drawn from a bar series: keep `SeriesType.Bar` and set the
chart type in the options. `PlotOptions.Funnel` sets the shape of the stages.

```razor
<ApexChart TItem="Stage" Title="Hiring pipeline" Options="options">
    <ApexPointSeries TItem="Stage"
                     Items="stages"
                     Name="Candidates"
                     SeriesType="SeriesType.Bar"
                     XValue="e => e.Name"
                     YValue="e => e.Count"
                     OrderByDescending="e => e.Y" />
</ApexChart>

@code {
    private ApexChartOptions<Stage> options = new()
    {
        Chart = new Chart { Type = ChartType.Funnel }, // or ChartType.Pyramid
        PlotOptions = new PlotOptions
        {
            Funnel = new PlotOptionsFunnel { Shape = FunnelShape.Trapezoid }
        }
    };
}
```

Chart types that need a different data shape have their **own series component** rather
than a `SeriesType` value:

| Component | For |
|---|---|
| `ApexBubbleSeries` | Bubble charts (x, y and a size dimension) |
| `ApexCandleSeries` | Candlestick / OHLC |
| `ApexBoxPlotSeries` | Box and whisker |
| `ApexRangeSeries` | Range bars |
| `ApexRangeAreaSeries` | Range areas |
| `ApexViolinSeries` | Violin plots |
| `ApexGauge` | A standalone gauge component |

### Updating a chart with new data

Changing the bound data does not redraw the chart on its own. Hold a reference with
`@ref` and call `UpdateSeriesAsync` after the data changes. This is the main surprise for
anyone arriving from React or Vue, and the upside is that you control exactly when a
redraw happens and whether it animates.

```razor
<button @onclick="Reload">Reload</button>

<ApexChart TItem="Sale" Title="Revenue" @ref="chart">
    <ApexPointSeries TItem="Sale" Items="sales" Name="Revenue"
                     SeriesType="SeriesType.Line"
                     XValue="e => e.Month"
                     YValue="e => e.Revenue" />
</ApexChart>

@code {
    private List<Sale> sales = new();
    private ApexChart<Sale> chart;

    private async Task Reload()
    {
        sales = await LoadSalesAsync();
        StateHasChanged(); // hand the new list to the series before the chart reads it
        await chart.UpdateSeriesAsync(animate: true);
    }
}
```

Keep the `StateHasChanged()` call when you assign a new list. Without it the chart redraws
the previous data: the first `await` in the handler renders the page while the old list is
still bound, and the new list only reaches the series after the handler has finished.
Refilling the list the series already holds (`sales.Clear(); sales.AddRange(newSales);`)
also works, and needs no `StateHasChanged()`.

The two update methods are:

```csharp
Task UpdateSeriesAsync(bool animate = true);
Task UpdateOptionsAsync(bool redrawPaths, bool animate, bool updateSyncedCharts,
                        ZoomOptions zoom = null);
```

`UpdateOptionsAsync` has no one-argument form: its first three parameters are required.


### Chart Options
Apex Chart options are available in the `ApexChartOptions` class that can be passed to the chart. More info in Apex documentation [ApexCharts Docs](https://apexcharts.com/docs/options/).

**The chart options cannot be shared.  Each chart instance must have its own ApexChartOptions instance**

## Premium Features & Licensing
The Blazor-ApexCharts wrapper is MIT licensed. The ApexCharts.js library it bundles and loads for you has its own dual license, which applies to any app that uses this package:

- **Community License (free)** for individuals, non-profits, educators and organizations whose annual revenue, operating budget or funding is under $2 million USD.
- **Commercial License (paid)** for organizations at or above $2 million USD.

The [ApexCharts.js license](https://github.com/apexcharts/apexcharts.js/blob/main/LICENSE.md) has the full terms, including when the OEM license applies, and [apexcharts.com/pricing](https://apexcharts.com/pricing/) lists the plans.

ApexCharts 6.0 introduced a set of premium interaction modules: history (undo/redo), perspectives (shareable view state), link (crossfilter / linked views), ink (draggable annotations), measure (delta ruler), contextMenu (point actions) and storyboard (scroll-driven views). These are part of the Premium plan. Every other chart type and option works without a license key and never shows a watermark. The key only controls the watermark; the revenue threshold above applies either way.

The premium modules work fully in **trial mode**, but a chart that uses one shows an unobtrusive `APEXCHARTS` watermark until a valid license key is applied. One license key works across every Apex product (apexcharts, apexgantt, apextree, apexsankey and the rest), and it is validated offline (no network call).

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

## Ignite UI Theming
From Blazor-ApexCharts 8.0.0 (which bundles ApexCharts.js 8.8.1), charts can follow the Ignite UI theme on the page: the series palette, text, gridlines, the tooltip, the font, light or dark. It is an add-on, it is free, and it needs no license key. Turn it on in the chart service:
```razor
services.AddApexCharts(e =>
{
    e.IgniteUI = true;
});
```
The service then loads `_content/Blazor-ApexCharts/js/features/ignite-ui.js` once, before the first chart renders. For .NET MAUI use `AddApexChartsMaui(...)` with the same option. Use this option rather than a script tag: the add-on registers on the ApexCharts instance the component loads, which a tag in `index.html` would not see.

The Ignite UI tokens (`--ig-*`) are the lowest-ranked layer above the built-in defaults. Anything above them wins:

| Rank | Source |
| ---- | ------ |
| 1 | An option you set in `ApexChartOptions` |
| 2 | The chart's own CSS variables |
| 3 | An `--apx-*` family token |
| 4 | A named theme (`Theme.Name`) or palette (`Theme.Palette`) |
| 5 | The Ignite UI theme (`--ig-*`), with the add-on loaded |
| 6 | The built-in defaults |

To turn it off for one chart, set `Theme = new Theme { DesignSystem = false }`; the `--apx-*` tokens still apply. `Tokens = false` stops every token read (`--apx-*` and `--ig-*`). Without the add-on the `--ig-*` tokens are ignored, with no error.

The chart reads its theme on every render. After a change that does not render, such as swapping the Ignite UI stylesheet for its dark variant, call `RefreshTokensAsync()` on the chart.

The theme names its font but does not load it (`--ig-font-family` is usually a web font, and the Ignite UI theme sheets carry no `@font-face` for it), so include the font's stylesheet on the page, in `index.html` or `App.razor`, as the Ignite UI setup already asks. The font applies only once that stylesheet has loaded it; the chart waits for it and re-measures, so the labels are measured with it.

## Acknowledgments
Credits to [@thirstyape](https://github.com/thirstyape) for making production release possible.


[![Stargazers repo roster for @apexcharts/Blazor-ApexCharts](https://reporoster.com/stars/dark/apexcharts/Blazor-ApexCharts)](https://github.com/apexcharts/Blazor-ApexCharts/stargazers)

