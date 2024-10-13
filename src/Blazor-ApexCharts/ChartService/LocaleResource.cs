using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexCharts;


public class LocaleResource
{
    public string Name { get; set; }

    public string Language { get; set; }

    public ChartLocale Locale { get; set; }


    internal string GetFileName()
    {

        return $@"locales\{Name}.json"; 

    }

}





