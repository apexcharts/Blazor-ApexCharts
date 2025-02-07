namespace ApexCharts;

/// <summary>
/// Locale resources built in Apexcharts
/// </summary>
public class LocaleResource
{
    /// <summary>
    /// Name of the resource
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// English Name of the resource
    /// </summary>
    public string Language { get; set; }

    /// <summary>
    /// The Chart Locale
    /// Is Lazy loaded
    /// </summary>
    public ChartLocale Locale { get; set; }

    /// <summary>
    /// Returns the expected file name for the locale.
    /// </summary>
	public string GetFileName() => $@"locales\{Name}.json";
}
