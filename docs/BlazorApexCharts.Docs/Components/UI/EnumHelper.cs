using System;
using System.Collections.Generic;
using System.Linq;

namespace BlazorApexCharts.Docs.Components.UI
{
    public static class EnumHelper
    {
        public static List<TEnum> GetList<TEnum>() where TEnum : struct, Enum
            => Enum.GetValues<TEnum>().ToList();

        public static List<TEnum?> GetNullableList<TEnum>() where TEnum : struct, Enum
            => Enum.GetValues<TEnum>().Select(v => (TEnum?)v).ToList();
    }
}
