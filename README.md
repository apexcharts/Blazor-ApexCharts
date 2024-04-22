

![.NET Core](https://github.com/joadan/Blazor-ApexCharts/workflows/.NET%20Core/badge.svg?branch=master)



# Blazor-ApexCharts
A blazor wrapper for [ApexCharts.js](https://apexcharts.com/)
## [Demo](https://apexcharts.github.io/Blazor-ApexCharts)


## v2.0 Released to production!
It's no longer necessary to manually include javascript files. 
Please **REMOVE** any references to:  

```html
<script src="_content/Blazor-ApexCharts/js/apex-charts.min.js"></script>
<script src="_content/Blazor-ApexCharts/js/blazor-apex-charts.js"></script>
```
---

As of version 2.0 javascript interop on WASM is running synchronously for better performance. 


## Installation
### NuGet
[Blazor-ApexCharts](https://www.nuget.org/packages/Blazor-ApexCharts/)

```bash
dotnet add package Blazor-ApexCharts
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

## Acknowledgments
Credits to [@thirstyape](https://github.com/thirstyape) for making production release possible.


[![Stargazers repo roster for @apexcharts/Blazor-ApexCharts](https://reporoster.com/stars/dark/apexcharts/Blazor-ApexCharts)](https://github.com/apexcharts/Blazor-ApexCharts/stargazers)

