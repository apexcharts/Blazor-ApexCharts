using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace ApexCharts.Internal
{
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
                await JsApexchart.InvokeVoidAsync("blazor_apexchart.copyTooltipContent", ChartId);
            }
        }
    }
}