![.NET Core](https://github.com/joadan/Blazor-ApexCharts/workflows/.NET%20Core/badge.svg?branch=master)

# Blazor-ApexCharts
A blazor wrapper for [ApexCharts.js](https://apexcharts.com/)
## [Demo](https://apexcharts.github.io/Blazor-ApexCharts)

**Please note: Development in progress, expect breaking changes.**

## Breaking Changes v0.6.0-alpha
Version v0.6.0 introduces support for all Apex chart types, this comes with a number of breaking changes
- Chart Type is no longer set on chart level
- ApexSeries has been been replaced with ApexPointSeries
- ApexPointSeries take SeriesType to set chart type
- Chart is no longer automatically rerenderd, please use the method SetRerenderChart to flag the chart to be rerendered

Please see the samples for more details.


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
### Basic  Chart
```html
 <ApexChart TItem="Order" Title="Orders Net Value By Type"  ChartType="ChartType.Pie">
            <ApexSeries TItem="Order"
                        Items="SampleData.GetOrders()"
                        Name="Order Value"
                        XValue="@(e => e.OrderType)"
                        YAggregate="@(e => e.Sum(e => e.NetValue))"
                        ShowDataLabels="true" />
 </ApexChart>
```

<img src="BasicPieChart.png" width="500">

### Chart Options
Apex Chart options is available in the ApexChartOptions class that can be passed to the chart. More info in Apex documentation [ApexCharts Docs](https://apexcharts.com/docs/options/). See sample below

### Chart Events

### Chart Render
The chart will automatically re-render if data changes. It's possible to turn this of by setting ManualRender to true on the chart and then use the chart method Render() to render the chart. See the sample [render chart](https://apexcharts.github.io/Blazor-ApexCharts/render-chart)

### Advanced  Chart
```html
 @page "/datetime-chart"

<h3>DateTime Chart</h3>

<div class="row">
    <div class="col-md-12 col-lg-6">
        <ApexChart TItem="Order" Title="Orders Value"
                   OnDataPointSelection="DataPointSelected"
                   ChartType="ChartType.Bar"
                   XAxisType="XaxisType.Datetime"
                   Options="options">

            <ApexSeries TItem="Order"
                        Items="SampleData.GetOrders()"
                        Name="Net Value"
                        XValue="@(e => e.OrderDate.FirstDayOfMonth())"
                        YAggregate="@(e => e.Sum(e => e.NetValue))"
                        OrderBy="e=>e.X" />

            <ApexSeries TItem="Order"
                        Items="SampleData.GetOrders()"
                        Name="Gross Value"
                        XValue="@(e => e.OrderDate.FirstDayOfMonth())"
                        YAggregate="@(e => e.Sum(e => e.GrossValue))"
                        OrderBy="e=>e.X" />
        </ApexChart>
    </div>
</div>

<SelectedData Data="selectedData" />
```
```csharp
@code {
    private ApexChartOptions<Order> options = new ApexCharts.ApexChartOptions<Order>();
    private SelectedData<Order> selectedData;

    protected override void OnInitialized()
    {
        options.Tooltip = new Tooltip { X = new TooltipX { Format = @"MMMM \ yyyy" } };
        options.Subtitle = new Subtitle { OffsetY = 15, Text = "DateTime sample with options" };
        options.Xaxis = new XAxis
        {
            Title = new XaxisTitle
            {
                OffsetY = 5,
                Text = "Month",
                Style = new XAxistTitleStyle { FontSize = "14px", Color = "lightgrey" }
            }
        };
        options.Yaxis = new List<YAxis>();
        options.Yaxis.Add(new YAxis
        {
            Labels = new YAxisLabels { Rotate = -45, Style = new YAxisLabelStyle { FontSize = "10px" } },
            Title = new YaxisTitle { Text = "Value", Style = new YAxisTitleStyle { FontSize = "14px", Color = "lightgrey" } }
        });

        options.Annotations = new Annotations { Yaxis = new List<AnnotationsYAxis>() };
        options.Annotations.Yaxis.Add(new AnnotationsYAxis
        {
            Y = 50000,
            BorderWidth = 2,
            StrokeDashArray = 5,
            BorderColor = "red",
            Label = new AnnotationsYAxisLabel { Text = "Monthly Target" }
        });
    }

    private void DataPointSelected(SelectedData<Order> selected)
    {
        selectedData = selected;
    }
}
```


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

**Order Data**
```csharp
  public static class SampleData
  {
        public static List<Order> GetOrders()
        {
            var orders = new List<Order>();
            orders.Add(new Order { CustomerName = "Odio Corporation", Country = "Sweden", OrderDate = DateTimeOffset.Now.AddDays(-12), GrossValue = 34531, DiscountPrecentage = 21, OrderType = OrderType.Contract });
            orders.Add(new Order { CustomerName = "Odio Corporation", Country = "Sweden", OrderDate = DateTimeOffset.Now.AddDays(-100), GrossValue = 2800, DiscountPrecentage = 12, OrderType = OrderType.Mail });
            orders.Add(new Order { CustomerName = "Odio Corporation", Country = "Sweden", OrderDate = DateTimeOffset.Now.AddDays(-128), GrossValue = 12532, DiscountPrecentage = 24, OrderType = OrderType.Contract });
            orders.Add(new Order { CustomerName = "Odio Corporation", Country = "Sweden", OrderDate = DateTimeOffset.Now.AddDays(-232), GrossValue = 1400, DiscountPrecentage = 12, OrderType = OrderType.Mail });
            orders.Add(new Order { CustomerName = "Odio Corporation", Country = "Sweden", OrderDate = DateTimeOffset.Now.AddDays(-321), GrossValue = 22000, DiscountPrecentage = 10, OrderType = OrderType.Contract });
            orders.Add(new Order { CustomerName = "Odio Corporation", Country = "Sweden", OrderDate = DateTimeOffset.Now.AddDays(-400), GrossValue = 3000, DiscountPrecentage = 17, OrderType = OrderType.Web });

            orders.Add(new Order { CustomerName = "Nascetur AB", Country = "Sweden", OrderDate = DateTimeOffset.Now.AddDays(-17), GrossValue = 2134, DiscountPrecentage = 10, OrderType = OrderType.Phone });
            orders.Add(new Order { CustomerName = "Nascetur AB", Country = "Sweden", OrderDate = DateTimeOffset.Now.AddDays(-27), GrossValue = 11345, DiscountPrecentage = 12, OrderType = OrderType.Phone });
            orders.Add(new Order { CustomerName = "Nascetur AB", Country = "Sweden", OrderDate = DateTimeOffset.Now.AddDays(-124), GrossValue = 122345, DiscountPrecentage = 32, OrderType = OrderType.Mail });
            orders.Add(new Order { CustomerName = "Nascetur AB", Country = "Sweden", OrderDate = DateTimeOffset.Now.AddDays(-299), GrossValue = 1235, DiscountPrecentage = 12, OrderType = OrderType.Mail });
            orders.Add(new Order { CustomerName = "Nascetur AB", Country = "Sweden", OrderDate = DateTimeOffset.Now.AddDays(-372), GrossValue = 44000, DiscountPrecentage = 11, OrderType = OrderType.Phone });
            orders.Add(new Order { CustomerName = "Nascetur AB", Country = "Sweden", OrderDate = DateTimeOffset.Now.AddDays(-410), GrossValue = 17000, DiscountPrecentage = 5, OrderType = OrderType.Phone });

            orders.Add(new Order { CustomerName = "Justo Eu Institute", Country = "Spain", OrderDate = DateTimeOffset.Now.AddDays(-13), GrossValue = 2800, DiscountPrecentage = 12, OrderType = OrderType.Mail });
            orders.Add(new Order { CustomerName = "Justo Eu Institute", Country = "Spain", OrderDate = DateTimeOffset.Now.AddDays(-45), GrossValue = 12532, DiscountPrecentage = 24, OrderType = OrderType.Web });
            orders.Add(new Order { CustomerName = "Justo Eu Institute", Country = "Spain", OrderDate = DateTimeOffset.Now.AddDays(-60), GrossValue = 1400, DiscountPrecentage = 12, OrderType = OrderType.Mail });
            orders.Add(new Order { CustomerName = "Justo Eu Institute", Country = "Spain", OrderDate = DateTimeOffset.Now.AddDays(-150), GrossValue = 22000, DiscountPrecentage = 10, OrderType = OrderType.Web });
            orders.Add(new Order { CustomerName = "Justo Eu Institute", Country = "Spain", OrderDate = DateTimeOffset.Now.AddDays(-200), GrossValue = 3000, DiscountPrecentage = 17, OrderType = OrderType.Web });

            orders.Add(new Order { CustomerName = "Ani Vent", Country = "France", OrderDate = DateTimeOffset.Now.AddDays(-17), GrossValue = 2134, DiscountPrecentage = 10, OrderType = OrderType.Phone });
            orders.Add(new Order { CustomerName = "Ani Vent", Country = "France", OrderDate = DateTimeOffset.Now.AddDays(-27), GrossValue = 11345, DiscountPrecentage = 12, OrderType = OrderType.Phone });
            orders.Add(new Order { CustomerName = "Ani Vent", Country = "France", OrderDate = DateTimeOffset.Now.AddDays(-124), GrossValue = 122345, DiscountPrecentage = 32, OrderType = OrderType.Mail });
            
            orders.Add(new Order { CustomerName = "Cali Inc", Country = "France", OrderDate = DateTimeOffset.Now.AddDays(-10), GrossValue = 77000, DiscountPrecentage = 17, OrderType = OrderType.Web });
            orders.Add(new Order { CustomerName = "Cali Inc", Country = "France", OrderDate = DateTimeOffset.Now.AddDays(-110), GrossValue = 120000, DiscountPrecentage = 23, OrderType = OrderType.Web });
            orders.Add(new Order { CustomerName = "Cali Inc", Country = "France", OrderDate = DateTimeOffset.Now.AddDays(-243), GrossValue = 44000, DiscountPrecentage = 8, OrderType = OrderType.Web });

            return orders;
        }
    }
```
<!--stackedit_data:
eyJoaXN0b3J5IjpbMTYwNjQ5MjEwMiw5Nzc1ODg3NTcsMTg0Mj
QxMDIwXX0=
-->
