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
In `_Host.cshtml` (server-side) or in `index.html` (client-side) add the following lines to the `body` tag **after** the `_framework` reference

```html
<script src="_content/Blazor-ApexCharts/js/apex-charts.min.js"></script>
<script src="_content/Blazor-ApexCharts/js/blazor-apex-chart.js"></script>
```

### Imports
Add a reference to `Blazor-ApexCharts` in your `_Imports.razor`
```csharp
@using ApexCharts;
```
### Basic Pie Chart
```html
 <ApexChart TItem="Order" Title="Orders Net Value By Type"  ChartType="ChartType.Pie">
            <ApexSeries TItem="Order"
                        Items="Orders"
                        Name="Order Value"
                        XValue="@(e => e.OrderType)"
                        YAggregate="@(e => e.Sum(e => e.NetValue))"
                        ShowDataLabels="true" />
        </ApexChart>
```

![Image](BasicPieChart.png)


**Order Class**
```csharp
    public class Order
    {
        public Guid OrderId { get; set; } = Guid.NewGuid();
        public string CustomerName { get; set; }
        public string Country { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public OrderType OrderType { get; set; }
        public decimal GrossValue { get; set; }
        public decimal NetValue { get =>  GrossValue * (1 - (DiscountPrecentage / 100)) ; }
        public decimal DiscountPrecentage { get; set; }
    }

   public enum OrderType
    {
        Web, Contract, Mail, Phone
    }
```