using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace BlazorApexCharts.Docs.Components
{
    public partial class DocExamples : ComponentBase, IDisposable
    {
        [Inject] public IJSRuntime JSRuntime { get; set; }
        [Inject] NavigationManager NavManager { get; set; }

        [Parameter] public string Title { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public RenderFragment Description { get; set; }

        public List<ICodeSnippet> CodeSnippets = new();
        private bool navigateToFragment = true;
        private bool disposed;


        protected override void OnInitialized()
        {
            NavManager.LocationChanged += LocationChanged;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!firstRender && navigateToFragment)
            {
                var delay = 200;
                if (IsWasm)
                {
                    delay = CodeSnippets.Count * 400;
                }
                await TryNavigateToFragment(delay);
                navigateToFragment = false;
            }

        }

        // An event handler, so async void: whatever it throws goes to the circuit's error handler.
        // A page closed during the delay must not, as scrolling to the example no longer matters then.
        private async void LocationChanged(object sender, LocationChangedEventArgs e)
        {
            try
            {
                await TryNavigateToFragment(100);
                navigateToFragment = false;
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

        private async Task TryNavigateToFragment(int delayMilliseconds)
        {
            var uri = NavManager.ToAbsoluteUri(NavManager.Uri);

            if (uri.Fragment.Length == 0)
                return;

            var fragment = WebUtility.UrlDecode(uri.Fragment.Substring(1));
            var codeSnippet = CodeSnippets.FirstOrDefault(e => e.Title?.ToLower() == fragment.ToLower());
            if (codeSnippet != null)
            {
                await Task.Delay(delayMilliseconds);
                if (disposed) { return; }
                await NavigateTo(codeSnippet);
            }
        }

        private bool IsWasm => RuntimeInformation.IsOSPlatform(OSPlatform.Create("BROWSER"));

        private async Task NavigateTo(ICodeSnippet codeSnippet)
        {
            var url = NavManager.Uri;

            var pos = url.IndexOf('#');
            if (pos > 0)
            {
                url = url.Substring(0, pos); 
            }

            url = url + "#" + codeSnippet.Title.ToLower();

            NavManager.NavigateTo(url, false);
            await JSRuntime.InvokeVoidAsync("docsInterop.scrollToFragment", codeSnippet.Id.ToString());
        }

        public void AddCodeSnippet(ICodeSnippet codeSnippet)
        {
            CodeSnippets.Add(codeSnippet);
            StateHasChanged();
        }

        public void RemoveCodeSnippet(ICodeSnippet codeSnippet)
        {
            CodeSnippets.Remove(codeSnippet);
            StateHasChanged();
        }

        public void Dispose()
        {
            disposed = true;
            NavManager.LocationChanged -= LocationChanged;
        }
    }
}

