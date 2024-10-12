using ApexCharts.Internal;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;


namespace ApexCharts;


///  <inheritdoc/>
public class ApexChartService : IApexChartService
{

    /// <summary>
    /// Initialize a new instance of the <see cref="ApexChartService"/> class
    /// </summary>
    /// <param name="jSRuntime"></param>
    /// <param name="httpClient"></param>
    /// <param name="navManager"></param>
    /// <param name="serviceOptions"></param>
    public ApexChartService(IJSRuntime jSRuntime, HttpClient httpClient, NavigationManager navManager, ApexChartsServiceOptions serviceOptions)
    {
        this.jSRuntime = jSRuntime;
        this.httpClient = httpClient;
        this.navManager = navManager;

        PopulateBuiltInLocales();
        globalOptions = serviceOptions.GlobalOptions;

        if (globalOptions != null)
        {
            globalOptionsInitialized = false;
        }
        else
        {
            globalOptions ??= new ApexChartBaseOptions();
        }
    }

    private readonly ConcurrentDictionary<string, IApexChartBase> charts = new();
    private readonly Dictionary<string, LocaleResource> locales = new();
    private readonly IJSRuntime jSRuntime;
    private IApexChartBaseOptions globalOptions;
    private HttpClient httpClient;
    private readonly NavigationManager navManager;
    private bool globalOptionsInitialized = true;


    ///  <inheritdoc/>
    public List<IApexChartBase> Charts => charts.Values.ToList();

    ///  <inheritdoc/>
    public List<LocaleResource> LocaleResources => locales.Values.ToList();

    ///  <inheritdoc/>
    public IApexChartBaseOptions GlobalOptions => globalOptions;

    private void PopulateBuiltInLocales()
    {
        var basePath = navManager.BaseUri + "_content/Blazor-ApexCharts/locales/";
        locales.Clear();
        locales.Add("fr", new LocaleResource { Name = "fr", Language = "French", Path = basePath + "fr.json" });
        locales.Add("sv", new LocaleResource { Name = "sv", Language = "Swedish", Path = basePath + "sv.json" });
    }

    ///  <inheritdoc/>
    public async Task SetLocaleAsync(LocaleResource localeResource, bool reRenderCharts)
    {
        localeResource.Locale ??= await httpClient.GetFromJsonAsync<ChartLocale>(localeResource.Path, ChartSerializer.GetOptions());
        await SetLocaleAsync(localeResource.Locale, reRenderCharts);
    }

    ///  <inheritdoc/>
    public async Task SetLocaleAsync(ChartLocale locale, bool reRenderCharts)
    {
        globalOptions.Chart ??= new();
        globalOptions.Chart.DefaultLocale = locale.Name;
        globalOptions.Chart.Locales = new List<ChartLocale> { locale };
        await SetGlobalOptionsAsync(globalOptions, reRenderCharts);
    }

    ///  <inheritdoc/>
    public async Task LoadJavascriptAsync(string path = null)
    {
        await JSLoader.LoadAsync(jSRuntime, path);
    }

    ///  <inheritdoc/>
    public async Task ReRenderChartsAsync()
    {
        await Task.WhenAll(Charts.ToList().Select(e => e.RenderAsync()));
    }
    ///  <inheritdoc/>
    public async Task SetGlobalOptionsAsync(IApexChartBaseOptions options, bool reRenderCharts)
    {
        options ??= new ApexChartBaseOptions();
        globalOptions = options;
        var jSObjectReference = await JSLoader.LoadAsync(jSRuntime, options?.Blazor?.JavascriptPath);
        var json = ChartSerializer.SerializeOptions(options);
        await jSObjectReference.InvokeVoidAsync("blazor_apexchart.setGlobalOptions", json);

        globalOptionsInitialized = true;

        if (reRenderCharts)
        {
            await ReRenderChartsAsync();
        }
    }

    ///  <inheritdoc/>
    public async Task LoadLocaleFileAsync(string name)
    {
        var result = await httpClient.GetFromJsonAsync<ChartLocale>("_content/Blazor-ApexCharts/locales/fr.json", ChartSerializer.GetOptions());
    }


    private SemaphoreSlim _semaphore = new(1, 1);
    ///  <inheritdoc/>
    public async Task GlobalOptionsInitializedAsync()
    {
        if (globalOptionsInitialized) { return; }

        await _semaphore.WaitAsync();
        try
        {
            await CheckGlobalOptionsInitializedAsync();
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            _semaphore.Release();
        }
    }

    private async Task CheckGlobalOptionsInitializedAsync()
    {
        if (!globalOptionsInitialized)
        {
            await SetGlobalOptionsAsync(globalOptions, false);
        }
    }

    /// <summary>
    /// Used to register an chart, Internal usage
    /// </summary>
    /// <param name="apexChart"></param>
    public void RegisterChart(IApexChartBase apexChart)
    {
        charts.TryAdd(apexChart.ChartId, apexChart);
    }

    /// <summary>
    /// Used to UnRegister an chart, Internal usage
    /// </summary>
    /// <param name="apexChart"></param>
    public void UnRegisterChart(IApexChartBase apexChart)
    {
        charts.TryRemove(apexChart.ChartId, out _);
    }


}
