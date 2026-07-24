using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

namespace BlazorApexCharts.Docs.Services
{

    public interface ICodeSnippetService
    {
        public Task<string> GetCodeSnippet(string className);
    }

    public class FakeSnippetService : ICodeSnippetService
    {
        public Task<string> GetCodeSnippet(string className)
        {
            return Task.FromResult("Source code view is disabled");
        }
    }

    public class LocalSnippetService : ICodeSnippetService
    {
        public async Task<string> GetCodeSnippet(string className)
        {
            var basePath = Directory.GetParent(Assembly.GetExecutingAssembly().Location).Parent.Parent.Parent.Parent.FullName;
            const string projectName = "BlazorApexCharts.Docs.";
            var classPath = projectName + className.Substring(projectName.Length-1).Replace(".", @"\");
            var codePath = Path.Combine(basePath, $"{classPath}.razor");

            if (File.Exists(codePath))
            {
                return await Task.FromResult(File.ReadAllText(codePath));
            }
            else
            {
                return await Task.FromResult($"Unable to find code at {codePath}");
            }
        }
    }

    public class GitHubSnippetService : ICodeSnippetService
    {
        // Production fallback: the deployed docs copy every demo .razor into _content/razor_source (see the
        // BuildClientAssets target in the WASM csproj).
        const string fallbackBaseUrl = "https://apexcharts.github.io/Blazor-ApexCharts/_content/razor_source";

        private readonly IHttpClientFactory httpClientFactory;
        private readonly NavigationManager navigationManager;

        private Dictionary<string, string> cachedCode = new Dictionary<string, string>();

        public GitHubSnippetService(IHttpClientFactory httpClientFactory, NavigationManager navigationManager)
        {
            this.httpClientFactory = httpClientFactory;
            this.navigationManager = navigationManager;
        }

        public async Task<string> GetCodeSnippet(string className)
        {
            if (cachedCode.TryGetValue(className, out var cached)) { return cached; }

            var baseName = "BlazorApexCharts.Docs";
            var relative = "/_content/razor_source/" + className.Replace(baseName, "").Replace(".", "/").TrimStart('/') + ".razor";

            // Try same-origin first (works locally and on any host the docs are deployed to), then the
            // apexcharts.github.io copy, so a not-yet-deployed demo still resolves wherever the source exists.
            var sameOrigin = navigationManager.BaseUri.TrimEnd('/') + relative;
            var candidates = new[] { sameOrigin, fallbackBaseUrl + relative.Replace("/_content/razor_source", "") };

            using var httpClient = httpClientFactory.CreateClient("GitHub");
            foreach (var url in candidates)
            {
                try
                {
                    var code = await httpClient.GetStringAsync(url);
                    cachedCode[className] = code;
                    return code;
                }
                catch
                {
                    // try next candidate
                }
            }

            return "// Source for this demo isn't available on this host yet.\n// It is viewable in the deployed docs and in the GitHub repository.";
        }
    }
}
