using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApexCharts.Docs.Data
{
    public class SupportIncident
    {
        public string WeekNumber { get; set; }
        public int Severity { get; set; }
        public IncidentSource Source { get; set; }

    }
    public enum IncidentSource
    {
        Internal,
        Customer,
        ThridParty,
        Integration
    }

}
