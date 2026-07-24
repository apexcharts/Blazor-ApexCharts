using Microsoft.JSInterop;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApexCharts
{
    /// <summary>
    /// Service responsible for charts global options
    /// </summary>
    public interface IApexChartService
    {
        /// <summary>
        /// Charts holds the list of rendred charts
        /// </summary>
        List<IApexChartBase> Charts { get; }

        /// <summary>
        /// List all the built in locale resources
        /// </summary>
        List<LocaleResource> LocaleResources { get; }

        /// <summary>
        /// Current global options
        /// </summary>
        IApexChartBaseOptions GlobalOptions { get; }

        /// <summary>
        /// Manually load the required javascript modules
        /// and set not initialized global options
        /// </summary>
        /// <param name="javascriptPath"></param>
        /// <returns></returns>
        Task InitalizeChartAsync(string javascriptPath = null);

        /// <summary>
        /// ReRender all charts
        /// </summary>
        /// <returns></returns>
        Task ReRenderChartsAsync();

        /// <summary>
        /// Sets the global chart options
        /// </summary>
        /// <param name="options"></param>
        /// <param name="reRenderCharts"></param>
        /// <returns></returns>
        Task SetGlobalOptionsAsync(IApexChartBaseOptions options, bool reRenderCharts);

        /// <summary>
        /// Sets the current global options
        /// </summary>
        /// <param name="reRenderCharts"></param>
        /// <returns></returns>
        Task SetGlobalOptionsAsync(bool reRenderCharts);

        /// <summary>
        /// Sets the current Locale
        /// </summary>
        /// <param name="localeResource"></param>
        /// <param name="reRenderCharts"></param>
        /// <returns></returns>
        Task SetLocaleAsync(LocaleResource localeResource, bool reRenderCharts);

        /// <summary>
        /// Sets the current locale
        /// </summary>
        /// <param name="chartLocale"></param>
        /// <param name="reRenderCharts"></param>
        /// <returns></returns>
        Task SetLocaleAsync(ChartLocale chartLocale, bool reRenderCharts);

        /// <summary>
        ///
        /// </summary>
        Task GlobalOptionsInitializedAsync();

        /// <summary>
        /// Applies a license key application-wide via <c>ApexCharts.setLicense</c>. A valid key removes the
        /// trial-mode <c>APEXCHARTS</c> watermark from the gated premium features. Can be called at runtime;
        /// a late valid key followed by a chart update clears an on-screen watermark without a full re-render.
        /// </summary>
        /// <param name="licenseKey"></param>
        Task SetLicenseAsync(string licenseKey);

        /// <summary>
        /// Applies the license key configured via <see cref="ApexChartsServiceOptions.LicenseKey"/> once,
        /// before any chart renders. Internal usage (called by the chart component on first render).
        /// </summary>
        Task InitializeLicenseAsync();

        /// <summary>
        /// Registers a shared record set with the crossfilter engine via <c>ApexCharts.crossfilter</c> (ApexCharts v6
        /// premium <c>link</c> feature). Charts that declare <see cref="ChartLink.Id"/> matching <paramref name="id"/>
        /// with a <see cref="ChartLink.Dimension"/> aggregate over these records; clicking a mark filters every linked
        /// chart. Register before the linked charts render so their first paint is already aggregated.
        /// </summary>
        /// <param name="id">Shared crossfilter group id.</param>
        /// <param name="records">The record set (one object per row); serialized to JS as-is.</param>
        Task RegisterCrossfilterAsync(string id, object records);

        /// <summary>
        /// Clears all active filters on the crossfilter group <paramref name="id"/> (<c>getCrossfilter(id).reset()</c>).
        /// </summary>
        Task ResetCrossfilterAsync(string id);

        /// <summary>
        /// Subscribes to <c>change</c> events on the crossfilter group <paramref name="id"/>. The handler's
        /// <c>[JSInvokable] OnCrossfilterChange(CrossfilterState)</c> method is invoked whenever the active filters
        /// change. Dispose the <see cref="DotNetObjectReference{T}"/> when done.
        /// </summary>
        Task SubscribeCrossfilterAsync<T>(string id, DotNetObjectReference<T> handler) where T : class;

		/// <summary>
		/// Used to register an chart, Internal usage
		/// </summary>
		void RegisterChart(IApexChartBase apexChart);

		/// <summary>
		/// Used to UnRegister an chart, Internal usage
		/// </summary>
		void UnRegisterChart(IApexChartBase apexChart);
    }
}