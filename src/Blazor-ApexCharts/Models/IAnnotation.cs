using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexCharts
{
    /// <summary>
    /// Interface for annotations
    /// </summary>
    public interface IAnnotation
    {
        /// <summary>
        /// Id of the annotation
        /// </summary>
        public string Id { get; set; }

        /// <inheritdoc cref="ApexCharts.Label" />
        public Label Label { get; set; }

    }
}
