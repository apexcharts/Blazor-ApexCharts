using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexCharts;

/// <summary>
/// Locale resources built in Apexcharts
/// </summary>
public class LocaleResource
{
    /// <summary>
    /// Name of the resource
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// English Name of the resource
    /// </summary>
    public string Language { get; set; }

    /// <summary>
    /// The Chart Locale
    /// Is Lazy loaded
    /// </summary>
    public ChartLocale Locale { get; set; }


    internal string GetFileName()
    {

        return $@"locales\{Name}.json"; 

    }

}





