![.NET Core](https://github.com/joadan/Blazor-ApexCharts/workflows/.NET%20Core/badge.svg?branch=master)

# Blazor-ApexCharts
A blazor wrapper for [ApexCharts.js](https://apexcharts.com/)
## [Demo](https://apexcharts.github.io/Blazor-ApexCharts)


## .NET 6 or higher is required as of v0.9.0-beta

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
Apex Chart options is available in the ApexChartOptions class that can be passed to the chart. More info in Apex documentation [ApexCharts Docs](https://apexcharts.com/docs/options/).



[![Stargazers repo roster for @apexcharts/Blazor-ApexCharts](https://reporoster.com/stars/dark/apexcharts/Blazor-ApexCharts)](https://github.com/apexcharts/Blazor-ApexCharts/stargazers)
