using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace ApexCharts.Internal
{
    /// <summary>
    /// Component for .Net Tooltip
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    public partial class ApexChartTooltip<TItem> where TItem : class
    {
        /// <inheritdoc cref="ChartId"/>
        [Parameter] public string ChartId { get; set; }

        /// <inheritdoc cref="Tooltip"/>
        [Parameter] public RenderFragment<HoverData<TItem>> ApexTooltip { get; set; }

        /// <inheritdoc cref="HoverData{TItem}"/>
        [Parameter] public HoverData<TItem> Data { get; set; }

        /// <inheritdoc cref="IJSObjectReference"/>
        [Parameter] public IJSObjectReference JsApexchart { get; set; }


        /// <inheritdoc cref="OnAfterRenderAsync(bool)"/>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (JsApexchart != null)
            {
                try
                {
                    await JsApexchart.InvokeVoidAsync("blazor_apexchart.copyTooltipContent", ChartId);
                }
                // The chart is being disposed or the circuit is gone: there is no tooltip left to fill.
                catch (Exception ex) when (JSInteropErrors.IsBrowserGone(ex))
                { }
            }
        }
    }
}