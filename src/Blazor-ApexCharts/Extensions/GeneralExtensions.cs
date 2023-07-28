using System;
using System.Collections.Generic;

namespace ApexCharts
{
    /// <summary>
    /// Extension methods for use in Blazor components
    /// </summary>
    public static class GeneralExtensions
    {
        /// <summary>
        /// Returns the first day of the month with the time value set to 00:00:00.000000 for the provided value
        /// </summary>
        /// <param name="value">The value to get the first day of the month for</param>
        public static DateTimeOffset FirstDayOfMonth(this DateTimeOffset value)
        {
            return new DateTimeOffset(value.Year, value.Month, 1, 0, 0, 0, new TimeSpan());
        }

        /// <summary>
        /// Returns the provided value with the time set to 00:00:00.000000
        /// </summary>
        /// <param name="value">The value to remove the time portion from</param>
        public static DateTimeOffset DayOnly(this DateTimeOffset value)
        {
            return new DateTimeOffset(value.Year, value.Month, value.Day, 0, 0, 0, new TimeSpan());
        }

        /// <summary>
        /// Converts the provided value to its Unix millisecond value
        /// </summary>
        /// <param name="d">The value to convert</param>
        public static long ToUnixTimeMilliseconds(this DateTime d)
        {
            DateTime epoch = DateTime.UnixEpoch;
            return (long)(d - epoch).TotalMilliseconds;
        }

        /// <summary>
        /// Executes the provided method on each item in the provided collection and returns the updated values
        /// </summary>
        /// <typeparam name="T">The data type of the items in the list</typeparam>
        /// <param name="items">The collection of items to update</param>
        /// <param name="updateMethod">The method to execute on each item in the list</param>
        public static IList<T> SetValue<T>(this IList<T> items, Action<T> updateMethod)
        {
            if (updateMethod == null)
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
