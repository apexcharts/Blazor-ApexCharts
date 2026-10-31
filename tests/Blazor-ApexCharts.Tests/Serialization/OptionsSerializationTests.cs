using System.Text.Json;
using ApexCharts.Tests.TestHelpers;

namespace ApexCharts.Tests.Serialization;

/// <summary>
/// Baseline checks on the JSON the component sends to JavaScript. Copy this pattern for an option
/// that serializes wrongly: build the options in C#, run them through <see cref="ChartJson"/> and
/// assert on the JSON property the JavaScript side reads.
/// </summary>
public class OptionsSerializationTests
{
    [Fact]
    public void Options_use_camel_case_and_lowercase_enum_values()
    {
        var options = new ApexChartOptions<Sale>
        {
            Chart = new Chart { Id = "sales", Type = ChartType.Bar, Stacked = true },
            Xaxis = new XAxis { Type = XAxisType.Category },
            PlotOptions = new PlotOptions { Bar = new PlotOptionsBar { Horizontal = true } },
        };

        var json = ChartJson.ToElement<Sale>(options);

        Assert.Equal("sales", json.Get("chart.id")?.GetString());
        Assert.Equal("bar", json.Get("chart.type")?.GetString());
        Assert.True(json.Get("chart.stacked")?.GetBoolean());
        Assert.Equal("category", json.Get("xaxis.type")?.GetString());
        Assert.True(json.Get("plotOptions.bar.horizontal")?.GetBoolean());
    }

    [Fact]
    public void Unset_options_are_omitted_rather_than_sent_as_null()
    {
        var options = new ApexChartOptions<Sale> { Chart = new Chart { Type = ChartType.Line } };

        var json = ChartJson.ToElement<Sale>(options);

        // apexcharts.js merges the options over its defaults, so a null would override a default.
        Assert.Null(json.Get("title"));
        Assert.Null(json.Get("yaxis"));
        Assert.Null(json.Get("chart.height"));
        Assert.DoesNotContain("null", ChartJson.Serialize(options));
    }

    [Fact]
    public void Data_point_keeps_a_null_y_so_the_chart_draws_a_gap()
    {
        var point = new DataPoint<Sale> { X = "Feb", Y = null };

        var json = ChartJson.ToElement<Sale>(point);

        Assert.Equal("Feb", json.Get("x")?.GetString());
        Assert.Equal(JsonValueKind.Null, json.Get("y")?.ValueKind);
        Assert.Null(json.Get("items"));
    }

    [Fact]
    public void Global_options_serialize_without_an_item_type()
    {
        var global = new ApexChartBaseOptions { Chart = new Chart { FontFamily = "Inter" } };

        var json = ChartJson.Parse(ChartJson.SerializeGlobal(global));

        Assert.Equal("Inter", json.Get("chart.fontFamily")?.GetString());
    }
}
