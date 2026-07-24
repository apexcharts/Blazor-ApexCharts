using System.Collections.Generic;

namespace BlazorApexCharts.Docs.Components.UI
{
    public static class UIHelpers
    {
        public static string MergeClass(string baseClass, IReadOnlyDictionary<string, object> attributes)
        {
            if (attributes != null && attributes.TryGetValue("class", out var value) && value is string extra && !string.IsNullOrWhiteSpace(extra))
            {
                return $"{baseClass} {extra}";
            }

            return baseClass;
        }

        public static string ToColorSuffix(this TablerColor color) => color.ToString().ToLowerInvariant();

        public static string ToColorSuffix(this TablerColor? color) => color?.ToString().ToLowerInvariant();
    }
}
