using System;
using System.Collections.Generic;

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

        public static IList<T> SetValue<T>(this IList<T> items, Action<T> updateMethod)
        {
            if(updateMethod == null)
            {
                return items;
            }

            foreach (T item in items)
            {
                updateMethod(item);
            }
            return items;
        }
    }
}
