// The linked MAUI sources (see the csproj) rely on the implicit usings of a MAUI project.
global using Microsoft.Extensions.DependencyInjection;

namespace ApexCharts;

/// <summary>
/// Stands in for Microsoft.Maui.Storage.FileSystem, which ApexChartServiceMaui uses to read locale files
/// from the app package. The tests never load a locale file.
/// </summary>
internal sealed class FileSystem
{
    public static FileSystem Current { get; } = new();

    public Task<Stream> OpenAppPackageFileAsync(string filename) =>
        throw new NotSupportedException($"The MAUI file system is not available in tests ({filename}).");
}
