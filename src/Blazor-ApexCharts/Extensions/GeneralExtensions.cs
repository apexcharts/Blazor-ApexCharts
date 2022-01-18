using System;

namespace ApexCharts
{
    public static class GeneralExtensions
    {
        public static DateTimeOffset FirstDayOfMonth(this DateTimeOffset value)
        {
            return new DateTimeOffset(value.Year, value.Month, 1, 0, 0, 0, new TimeSpan());
        }

        public static DateTimeOffset DayOnly(this DateTimeOffset value)
        {
            return new DateTimeOffset(value.Year, value.Month, value.Day, 0, 0, 0, new TimeSpan());
        }

       
        public static long ToUnixTimeMilliseconds(this DateTime d)
        {
            DateTime epoch = DateTime.UnixEpoch;
            return (long)(d - epoch).TotalMilliseconds;
        }

    }
}
