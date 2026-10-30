using ApexCharts.Tests.TestHelpers;

namespace ApexCharts.Tests.Serialization;

/// <summary>
/// Issue #349: <see cref="LabelPosition"/> only had Left and Right, but apexcharts.js places an
/// x-axis annotation label at top, center or bottom and a y-axis annotation label at left, center
/// or right. Each member must serialize to the lowercase word apexcharts.js compares against, and
/// the existing members must keep their numeric values.
/// </summary>
public class Issue349LabelPositionTests
{
    [Theory]
    [InlineData(LabelPosition.Top, "top")]
    [InlineData(LabelPosition.Center, "center")]
    [InlineData(LabelPosition.Bottom, "bottom")]
    public void XAxis_label_position_is_sent_as_the_word_apexcharts_reads(LabelPosition position, string expected)
    {
        var annotation = new AnnotationsXAxis { X = "Feb", Label = new Label { Text = "x", Position = position } };

        Assert.Equal(expected, ChartJson.ToElement<Sale>(annotation).Get("label.position")?.GetString());
    }

    [Theory]
    [InlineData(LabelPosition.Left, "left")]
    [InlineData(LabelPosition.Center, "center")]
    [InlineData(LabelPosition.Right, "right")]
    public void YAxis_label_position_is_sent_as_the_word_apexcharts_reads(LabelPosition position, string expected)
    {
        var annotation = new AnnotationsYAxis { Y = 20, Label = new Label { Text = "y", Position = position } };

        Assert.Equal(expected, ChartJson.ToElement<Sale>(annotation).Get("label.position")?.GetString());
    }

    [Fact]
    public void Existing_members_keep_their_values()
    {
        // Left and Right were the only members, so code compiled against an older build stored 0 and 1.
        Assert.Equal(0, (int)LabelPosition.Left);
        Assert.Equal(1, (int)LabelPosition.Right);
    }

    [Fact]
    public void Unset_position_is_left_out()
    {
        var annotation = new AnnotationsXAxis { X = "Feb", Label = new Label { Text = "x" } };

        Assert.Null(ChartJson.ToElement<Sale>(annotation).Get("label.position"));
    }
}
