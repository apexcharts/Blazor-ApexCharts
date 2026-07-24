using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace BlazorApexCharts.Docs.Shared
{
    public partial class MainNavigation : IDisposable
    {
        [Inject] public NavigationManager NavManager { get; set; }
        [Inject] public IJSRuntime JSRuntime { get; set; }

        private bool showGlobalOptions;
        private ElementReference panelRef;
        private bool focusPending;

        protected override void OnInitialized()
        {
            NavManager.LocationChanged += OnLocationChanged;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JSRuntime.InvokeVoidAsync("docsInterop.expandActiveNavParents");
            }

            if (focusPending)
            {
                focusPending = false;
                await panelRef.FocusAsync();
            }
        }

        private async void OnLocationChanged(object sender, LocationChangedEventArgs e)
        {
            await Task.Delay(100);
            await JSRuntime.InvokeVoidAsync("docsInterop.expandActiveNavParents");
        }

        public Task ShowGlobalOptions()
        {
            showGlobalOptions = true;
            focusPending = true;
            return Task.CompletedTask;
        }

        private void CloseGlobalOptions()
        {
            showGlobalOptions = false;
        }

        private void OnPanelKeyDown(KeyboardEventArgs e)
        {
            if (e.Key == "Escape")
            {
                CloseGlobalOptions();
            }
        }

        public void Dispose()
        {
            NavManager.LocationChanged -= OnLocationChanged;
        }
    }
}
