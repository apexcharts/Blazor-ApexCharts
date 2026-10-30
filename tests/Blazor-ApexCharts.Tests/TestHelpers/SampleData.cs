namespace ApexCharts.Tests.TestHelpers;

/// <summary>
/// A minimal item type for charts under test. <see cref="ApexChart{TItem}"/> requires a reference type.
/// </summary>
public sealed class Sale
{
    /// <summary>The category on the X axis.</summary>
    public string Month { get; set; } = "";

    /// <summary>The value on the Y axis.</summary>
    public decimal Amount { get; set; }
}

/// <summary>
/// Shared fixtures so tests do not each invent their own data.
/// </summary>
public static class SampleData
{
    /// <summary>Three months of sales, in X order.</summary>
    public static List<Sale> Sales() =>
    [
        new() { Month = "Jan", Amount = 10 },
        new() { Month = "Feb", Amount = 20 },
        new() { Month = "Mar", Amount = 30 },
    ];
}
