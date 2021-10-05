using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApexCharts
{
    public static class GeneralExtensions
    {
        public static DateTimeOffset FirstDayOfMonth(this DateTimeOffset value)
        {
            return new DateTimeOffset(value.Year, value.Month, 1, 0, 0, 0, new TimeSpan());
        }
    }
}
