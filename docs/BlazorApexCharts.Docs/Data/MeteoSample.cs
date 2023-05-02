using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApexCharts.Docs.Data
{
    public class MeteoSample
    {
        public string Month { get; set; }
        public decimal CurrentTemperature { get; set; }
        public decimal LowestTemperature { get; set; }
        public decimal HighestTemperature { get; set; }
    }
}
