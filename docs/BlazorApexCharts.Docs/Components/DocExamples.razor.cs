using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using TabBlazor.Services;

namespace BlazorApexCharts.Docs.Components
{
    public partial class DocExamples : ComponentBase, IDisposable
    {
        [Inject] public TablerService TablerService { get; set; }
        [Inject] NavigationManager NavManager { get; set; }

        [Parameter] public string Title { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public RenderFragment Description { get; set; }

        public List<CodeSnippet> CodeSnippets = new();
        private bool navigateToFragment = true;


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

        private async void LocationChanged(object sender, LocationChangedEventArgs e)
        {
            await TryNavigateToFragment(100);
            navigateToFragment = false;
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
                await NavigateTo(codeSnippet);
            }
        }

        private bool IsWasm => RuntimeInformation.IsOSPlatform(OSPlatform.Create("BROWSER"));

        private async Task NavigateTo(CodeSnippet codeSnippet)
        {
            await TablerService.ScrollToFragment(codeSnippet.Id.ToString());
        }

        public void AddCodeSnippet(CodeSnippet codeSnippet)
        {
            CodeSnippets.Add(codeSnippet);
            StateHasChanged();
        }

        public void RemoveCodeSnippet(CodeSnippet codeSnippet)
        {
            CodeSnippets.Remove(codeSnippet);
            StateHasChanged();
        }

        public void Dispose()
        {
            NavManager.LocationChanged -= LocationChanged;
        }
    }
}

