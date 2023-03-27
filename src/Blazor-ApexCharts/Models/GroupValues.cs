using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexCharts
{
    public class GroupValues
    {
        /// <summary>
        /// Indicates the max value of values to show, including the grouped values.
        /// </summary>
        public int? MaxValuesCount { get; set; }

        /// <summary>
        /// The precentage value that indicate if a value should be grouped.
        /// </summary>
        public decimal? PercentageThreshold { get; set; }

        public string Name { get; set; } = "Other";
        public bool Show { get; set; } = true;
    }

  

}
