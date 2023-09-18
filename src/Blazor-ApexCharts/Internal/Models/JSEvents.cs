namespace ApexCharts.Internal
{
    internal record class JSEvents
    {
        public bool HasAnimationEnd { get; init; }
        public bool HasBeforeMount { get; init; }
        public bool HasMounted { get; init; }
        public bool HasUpdated { get; init; }
        public bool HasMouseMove { get; init; }
        public bool HasMouseLeave { get; init; }
        public bool HasClick { get; init; }
        public bool HasDataPointSelection { get; init; }
        public bool HasDataPointEnter { get; init; }
        public bool HasDataPointLeave { get; init; }
        public bool HasLegendClick { get; init; }
        public bool HasMarkerClick { get; init; }
        public bool HasXAxisLabelClick { get; init; }
        public bool HasSelection { get; init; }
        public bool HasScrolled { get; init; }
        public bool HasBrushScrolled { get; init; }
        public bool HasBeforeZoom { get; init; }
        public bool HasBeforeResetZoom { get; init; }
        public bool HasZoomed { get; init; }
    }
}
