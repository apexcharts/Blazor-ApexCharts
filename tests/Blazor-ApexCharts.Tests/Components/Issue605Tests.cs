using ApexCharts.Tests.TestHelpers;

namespace ApexCharts.Tests.Components;

/// <summary>
/// Issue #605: a palette set together with a theme mode was lost after UpdateOptionsAsync.
/// The palette is replaced inside apexcharts.js (Theme.updateThemeOptions and
/// Theme._applyModeDefaults), not by the wrapper. These tests pin the wrapper's side of the
/// contract: the mode and the palette reach both renderChart and updateOptions unchanged, and an
/// explicit Colors list (the workaround) is sent alongside them.
/// </summary>
public class Issue605Tests : ApexChartsTestContext
{
    private IRenderedComponent<ApexChart<Sale>> RenderThemedChart(ApexChartOptions<Sale> options) =>
        RenderChart<Sale>(p => p
            .Add(c => c.Options, options)
            .AddChildContent<ApexPointSeries<Sale>>(s => s
                .Add(x => x.Name, "Amount")
                .Add(x => x.Items, SampleData.Sales())
                .Add(x => x.SeriesType, SeriesType.Bar)
                .Add(x => x.XValue, e => e.Month)
                .Add(x => x.YValue, e => e.Amount)));

    [Theory]
    [InlineData(Mode.Dark, PaletteType.Palette1, "dark", "palette1")]
    [InlineData(Mode.Dark, PaletteType.Palette2, "dark", "palette2")]
    [InlineData(Mode.Light, PaletteType.Palette4, "light", "palette4")]
    public async Task Update_options_sends_the_mode_and_the_palette_unchanged(
        Mode mode, PaletteType palette, string expectedMode, string expectedPalette)
    {
        var cut = RenderThemedChart(new ApexChartOptions<Sale>
        {
            Theme = new Theme { Mode = mode, Palette = palette }
        });

        var rendered = RenderedOptions();
        Assert.Equal(expectedMode, rendered.Get("theme.mode")?.GetString());
        Assert.Equal(expectedPalette, rendered.Get("theme.palette")?.GetString());

        await cut.InvokeAsync(() => cut.Instance.UpdateOptionsAsync(false, false, false));

        var updated = LastJsonArgument(JsIdentifiers.UpdateOptions, 1);
        Assert.Equal(expectedMode, updated.Get("theme.mode")?.GetString());
        Assert.Equal(expectedPalette, updated.Get("theme.palette")?.GetString());
        // The wrapper adds no mode defaults of its own on top of what the caller set.
        Assert.Null(updated.Get("chart.foreColor"));
        Assert.Null(updated.Get("colors"));
    }

    [Fact]
    public async Task Update_options_sends_explicit_colors_with_the_theme()
    {
        var colors = new List<string> { "#008FFB", "#00A86F", "#CA8501", "#FF4560", "#846DD5" };
        var cut = RenderThemedChart(new ApexChartOptions<Sale>
        {
            Theme = new Theme { Mode = Mode.Dark, Palette = PaletteType.Palette1 },
            Colors = colors
        });

        await cut.InvokeAsync(() => cut.Instance.UpdateOptionsAsync(false, false, false));

        var updated = LastJsonArgument(JsIdentifiers.UpdateOptions, 1);
        Assert.Equal("dark", updated.Get("theme.mode")?.GetString());
        Assert.Equal(colors, updated.Get("colors")!.Value.EnumerateArray().Select(c => c.GetString()).ToList());
    }
}
