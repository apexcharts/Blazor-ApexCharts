using System.Text.Json;
using ApexCharts.Tests.TestHelpers;

namespace ApexCharts.Tests.Serialization;

/// <summary>
/// Issue #611: <see cref="AnnotationsYAxis.Width"/> was a number, but apexcharts.js reads it as a
/// CSS length string (it calls <c>width.indexOf('%')</c>), so any width made the chart throw.
/// A number must now reach the browser as a px string and a string must pass through unchanged.
/// </summary>
public class Issue611AnnotationWidthTests : ApexChartsTestContext
{
    private static JsonElement? WidthOf(AnnotationsYAxis annotation)
        => ChartJson.ToElement<Sale>(annotation).Get("width");

    [Fact]
    public void Number_width_is_sent_as_a_px_string()
    {
        var width = WidthOf(new AnnotationsYAxis { Y = 20, Width = 100 });

        Assert.Equal(JsonValueKind.String, width?.ValueKind);
        Assert.Equal("100px", width!.Value.GetString());
    }

    [Fact]
    public void Fractional_and_nullable_double_widths_use_the_invariant_culture()
    {
        var previous = Thread.CurrentThread.CurrentCulture;
        try
        {
            // A culture with a comma decimal separator must not leak into the JSON.
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("de-DE");
            double? fromNullable = 12.5;

            Assert.Equal("12.5px", WidthOf(new AnnotationsYAxis { Width = fromNullable })?.GetString());
            Assert.Equal("40px", WidthOf(new AnnotationsYAxis { Width = 40m })?.GetString());
        }
        finally
        {
            Thread.CurrentThread.CurrentCulture = previous;
        }
    }

    [Theory]
    [InlineData("50%")]
    [InlineData("200px")]
    [InlineData("100%")]
    public void String_width_is_sent_unchanged(string value)
    {
        var width = WidthOf(new AnnotationsYAxis { Y = 20, Width = value });

        Assert.Equal(value, width?.GetString());
    }

    [Fact]
    public void Unset_width_is_left_out_so_apexcharts_uses_its_default()
    {
        Assert.Null(WidthOf(new AnnotationsYAxis { Y = 20 }));

        double? none = null;
        Assert.Null(WidthOf(new AnnotationsYAxis { Y = 20, Width = none }));
    }

    [Fact]
    public void Width_in_chart_options_reaches_renderChart_as_a_string()
    {
        var options = new ApexChartOptions<Sale>
        {
            Annotations = new Annotations
            {
                Yaxis = new List<AnnotationsYAxis>
                {
                    new AnnotationsYAxis { Y = 20, Width = 100 },
                    new AnnotationsYAxis { Y = 25, Width = "50%" },
                }
            }
        };

        RenderChart<Sale>(p => p
            .Add(c => c.Options, options)
            .AddChildContent<ApexPointSeries<Sale>>(s => s
                .Add(x => x.Items, SampleData.Sales())
                .Add(x => x.SeriesType, SeriesType.Line)
                .Add(x => x.XValue, e => e.Month)
                .Add(x => x.YValue, e => e.Amount)));

        var json = RenderedOptions();

        Assert.Equal("100px", json.Get("annotations.yaxis.0.width")?.GetString());
        Assert.Equal("50%", json.Get("annotations.yaxis.1.width")?.GetString());
    }

    [Fact]
    public async Task Width_passed_to_AddYAxisAnnotationAsync_is_sent_as_a_string()
    {
        var cut = RenderChart<Sale>(p => p
            .AddChildContent<ApexPointSeries<Sale>>(s => s
                .Add(x => x.Items, SampleData.Sales())
                .Add(x => x.SeriesType, SeriesType.Line)
                .Add(x => x.XValue, e => e.Month)
                .Add(x => x.YValue, e => e.Amount)));

        await cut.InvokeAsync(() => cut.Instance.AddYAxisAnnotationAsync(new AnnotationsYAxis { Y = 20, Width = 300 }, true));

        var json = LastJsonArgument("blazor_apexchart.addYaxisAnnotation", 1);
        Assert.Equal("300px", json.Get("width")?.GetString());
    }
}
