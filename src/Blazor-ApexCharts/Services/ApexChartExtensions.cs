using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexCharts
{
    public static class ApexChartExtensions
    {
        public static IServiceCollection AddApexCharts(this IServiceCollection services)
        {
            return services.AddSingleton<ApexChartService>();
        }

        public static IServiceCollection AddApexCharts(this IServiceCollection services, ApexChartBaseOptions options)
        {
            var chartServcie = new ApexChartService(options);
            return services.AddSingleton<ApexChartService>(chartServcie);
        }

    }
}
