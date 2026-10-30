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
        private bool disposed;

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

        // An event handler, so async void: whatever it throws goes to the circuit's error handler.
        // A page closed during the delay must not, as expanding the menu no longer matters then.
        private async void OnLocationChanged(object sender, LocationChangedEventArgs e)
        {
            try
            {
                await Task.Delay(100);
                if (disposed) { return; }
                await JSRuntime.InvokeVoidAsync("docsInterop.expandActiveNavParents");
            }
            catch (JSDisconnectedException)
            {
                // The page was closed.
            }
            catch (TaskCanceledException)
            {
                // The browser did not answer in time (a dropped connection in Blazor Server).
            }
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
            disposed = true;
            NavManager.LocationChanged -= OnLocationChanged;
        }
    }
}
