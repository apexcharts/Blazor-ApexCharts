using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexCharts
{
    public class ApexChartService
    {
        private ApexChartBaseOptions defaultOptions;

     
        public ApexChartService(ApexChartBaseOptions defaultOptions)
        {
            this.defaultOptions = defaultOptions;
        }

        public  ApexChartOptions<T> GetOptions<T>() where T : ApexChartBaseOptions
        {
            return new ApexChartOptions<T>(defaultOptions);
        }

        //public ApexChartOptions<T> GetDefaultOptions(T type)
        //{

        //}


    }
}
