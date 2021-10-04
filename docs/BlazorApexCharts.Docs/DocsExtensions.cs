using Microsoft.Extensions.DependencyInjection;
using TabBlazor;

namespace BlazorApexCharts.Docs
{
    public static class DocsExtensions
    {
        public static IServiceCollection AddDocs(this IServiceCollection services)
        {
            return services
               .AddTabler();
             }
    }
}
