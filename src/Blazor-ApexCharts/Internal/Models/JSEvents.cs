namespace ApexCharts.Internal
{
    internal record class JSEvents
    {
        public bool HasDataPointSelection { get; init; }
        public bool HasDataPointEnter { get; init; }
        public bool HasDataPointLeave { get; init; }
        public bool HasLegendClick { get; init; }
        public bool HasMarkerClick { get; init; }
        public bool HasXAxisLabelClick { get; init; }
        public bool HasSelection { get; init; }
        public bool HasBrushScrolled { get; init; }
        public bool HasZoomed { get; init; }
    }
}
