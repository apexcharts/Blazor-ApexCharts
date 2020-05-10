![.NET Core](https://github.com/joadan/Blazor-ApexCharts/workflows/.NET%20Core/badge.svg?branch=master)

# Blazor-ApexCharts
A wrapper for ApexCharts.js.

View it in action [here](https://joadan.github.io/Blazor-ApexCharts/basic-charts)

**Please note: Not production ready.**


## Installation
### Nuget
[Blazor-ApexCharts](https://www.nuget.org/packages/Blazor-ApexCharts/)

```bash
dotnet add package Blazor-ApexCharts
```

## Usage

### Assets
In your `_Host.cshtml` (server-side) or in your `index.html` (client-side) add the following lines to the `body` tag **after** the `_framework` reference

```html
<script src="_content/Blazor-ApexCharts/js/apex-charts.min.js"></script>
<script src="_content/Blazor-ApexCharts/js/blazor-apex-chart.js"></script>
```

### Imports
Now add a reference to `Blazor-ApexCharts` in your `_Imports.razor`
```csharp
@using ApexCharts;
```