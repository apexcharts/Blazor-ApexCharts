![.NET Core](https://github.com/joadan/Blazor-ApexCharts/workflows/.NET%20Core/badge.svg?branch=master)



# Blazor-ApexCharts
A blazor wrapper for [ApexCharts.js](https://apexcharts.com/)
## [Demo](https://apexcharts.github.io/Blazor-ApexCharts)

---

## v1.0 Released to production!

Credits to [@thirstyape](https://github.com/thirstyape) for pushing it over the edge!

We have cleaned up the options class in order to align with the js version as much as possible.
It should only be minor changes but if you have any problems just let us know.

---


## Installation
### Nuget
[Blazor-ApexCharts](https://www.nuget.org/packages/Blazor-ApexCharts/)

```bash
dotnet add package Blazor-ApexCharts
```

## Usage

### Assets
In `_Host.cshtml` (server-side) or in `index.html` (client-side) add the following lines to the `body` tag **after** the `_framework` reference

```html
<script src="_content/Blazor-ApexCharts/js/apex-charts.min.js"></script>
<script src="_content/Blazor-ApexCharts/js/blazor-apex-charts.js"></script>
```

### Imports
Add a reference to `Blazor-ApexCharts` in your `_Imports.razor`
```csharp
@using ApexCharts;
```

### Chart Options
Apex Chart options are available in the ApexChartOptions class that can be passed to the chart. More info in Apex documentation [ApexCharts Docs](https://apexcharts.com/docs/options/).


[![Stargazers repo roster for @apexcharts/Blazor-ApexCharts](https://reporoster.com/stars/dark/apexcharts/Blazor-ApexCharts)](https://github.com/apexcharts/Blazor-ApexCharts/stargazers)
