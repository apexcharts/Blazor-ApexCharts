using ApexCharts.Internal;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
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
    /// <param name="httpClientFacory"></param>
    /// <param name="navManager"></param>
    /// <param name="serviceOptions"></param>
    public ApexChartService(IJSRuntime jSRuntime, IHttpClientFactory httpClientFacory, NavigationManager navManager, ApexChartsServiceOptions serviceOptions)
    {
        this.jSRuntime = jSRuntime;
        this.httpClient = httpClientFacory.CreateClient();
       
        httpClient.BaseAddress = new Uri(navManager.BaseUri + "_content/Blazor-ApexCharts/");
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
    
    private bool globalOptionsInitialized = true;


    ///  <inheritdoc/>
    public List<IApexChartBase> Charts => charts.Values.ToList();

    ///  <inheritdoc/>
    public List<LocaleResource> LocaleResources => locales.Values.ToList();

    ///  <inheritdoc/>
    public IApexChartBaseOptions GlobalOptions => globalOptions;

    private void PopulateBuiltInLocales()
    {
        locales.Clear();
        locales.Add("ar", new LocaleResource { Name = "ar", Language = "Arabic" });
        locales.Add("be-cyrl", new LocaleResource { Name = "be-cyrl", Language = "Belarusian (Cyrillic)" });
        locales.Add("be-latn", new LocaleResource { Name = "be-latn", Language = "Belarusian (Latin)" });
        locales.Add("ca", new LocaleResource { Name = "ca", Language = "Catalan" });
        locales.Add("cs", new LocaleResource { Name = "cs", Language = "Czech" });
        locales.Add("da", new LocaleResource { Name = "da", Language = "Danish" });
        locales.Add("de", new LocaleResource { Name = "de", Language = "German" });
        locales.Add("el", new LocaleResource { Name = "el", Language = "Greek" });
        locales.Add("en", new LocaleResource { Name = "en", Language = "English" });
        locales.Add("es", new LocaleResource { Name = "es", Language = "Spanish" });
        locales.Add("et", new LocaleResource { Name = "et", Language = "Estonian" });
        locales.Add("fa", new LocaleResource { Name = "fa", Language = "Persian" });
        locales.Add("fi", new LocaleResource { Name = "fi", Language = "Finnish" });
        locales.Add("fr", new LocaleResource { Name = "fr", Language = "French" });
        locales.Add("he", new LocaleResource { Name = "he", Language = "Hebrew" });
        locales.Add("hi", new LocaleResource { Name = "hi", Language = "Hindi" });
        locales.Add("hr", new LocaleResource { Name = "hr", Language = "Croatian" });
        locales.Add("hu", new LocaleResource { Name = "hu", Language = "Hungarian" });
        locales.Add("hy", new LocaleResource { Name = "hy", Language = "Armenian" });
        locales.Add("id", new LocaleResource { Name = "id", Language = "Indonesian" });
        locales.Add("it", new LocaleResource { Name = "it", Language = "Italian" });
        locales.Add("ja", new LocaleResource { Name = "ja", Language = "Japanese" });
        locales.Add("ka", new LocaleResource { Name = "ka", Language = "Georgian" });
        locales.Add("ko", new LocaleResource { Name = "ko", Language = "Korean" });
        locales.Add("lt", new LocaleResource { Name = "lt", Language = "Lithuanian" });
        locales.Add("lv", new LocaleResource { Name = "lv", Language = "Latvian" });
        locales.Add("ms", new LocaleResource { Name = "ms", Language = "Malay" });
        locales.Add("nb", new LocaleResource { Name = "nb", Language = "Norwegian Bokmål" });
        locales.Add("nl", new LocaleResource { Name = "nl", Language = "Dutch" });
        locales.Add("pl", new LocaleResource { Name = "pl", Language = "Polish" });
        locales.Add("pt-br", new LocaleResource { Name = "pt-br", Language = "Portuguese (Brazil)" });
        locales.Add("pt", new LocaleResource { Name = "pt", Language = "Portuguese" });
        locales.Add("sr", new LocaleResource { Name = "sr", Language = "Serbian" });
        locales.Add("ru", new LocaleResource { Name = "ru", Language = "Russian" });
        locales.Add("sv", new LocaleResource { Name = "sv", Language = "Swedish" });
        locales.Add("sk", new LocaleResource { Name = "sk", Language = "Slovak" });
        locales.Add("sl", new LocaleResource { Name = "sl", Language = "Slovenian" });
        locales.Add("sq", new LocaleResource { Name = "sq", Language = "Albanian" });
        locales.Add("th", new LocaleResource { Name = "th", Language = "Thai" });
        locales.Add("tr", new LocaleResource { Name = "tr", Language = "Turkish" });
        locales.Add("uk", new LocaleResource { Name = "uk", Language = "Ukrainian" });
        locales.Add("vi", new LocaleResource { Name = "vi", Language = "Vietnamese" });
        locales.Add("zh-cn", new LocaleResource { Name = "zh-cn", Language = "Chinese (China)" });
        locales.Add("zh-tw", new LocaleResource { Name = "zh-tw", Language = "Chinese (Taiwan)" });
    }

    ///  <inheritdoc/>
    public async Task SetLocaleAsync(LocaleResource localeResource, bool reRenderCharts)
    {
        localeResource.Locale ??= await httpClient.GetFromJsonAsync<ChartLocale>(localeResource.GetFileName(), ChartSerializer.GetOptions());
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
    public async Task InitalizeChartAsync(string javascriptPath = null)
    {
        await JSLoader.LoadAsync(jSRuntime, javascriptPath);
        await GlobalOptionsInitializedAsync();
    }

    ///  <inheritdoc/>
    public async Task ReRenderChartsAsync()
    {
        await Task.WhenAll(Charts.ToList().Select(e => e.RenderAsync()));
    }


    ///  <inheritdoc/>
    public async Task SetGlobalOptionsAsync(bool reRenderCharts)
    {
        var jSObjectReference = await JSLoader.LoadAsync(jSRuntime, globalOptions?.Blazor?.JavascriptPath);
        var json = ChartSerializer.SerializeOptions(globalOptions);
        await jSObjectReference.InvokeVoidAsync("blazor_apexchart.setGlobalOptions", json);

        globalOptionsInitialized = true;

        if (reRenderCharts)
        {
            await ReRenderChartsAsync();
        }
    }

    ///  <inheritdoc/>
    public async Task SetGlobalOptionsAsync(IApexChartBaseOptions options, bool reRenderCharts)
    {
        options ??= new ApexChartBaseOptions();
        globalOptions = options;
        await SetGlobalOptionsAsync(reRenderCharts);
    }

    private readonly SemaphoreSlim _semaphore = new(1, 1);
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
