namespace BlazorApexCharts.Docs
{
    public class SupportIncident
    {
        public string WeekName => $"W{WeekNumber}";
        public int WeekNumber { get; set; }
        public int LeadTime { get; set; }
        public int Severity { get; set; }
        public IncidentSource Source { get; set; }
        public string PointColor { get; set; }

    }
    public enum IncidentSource
    {
        Internal,
        Customer,
        ThirdParty,
        Integration
    }

}
