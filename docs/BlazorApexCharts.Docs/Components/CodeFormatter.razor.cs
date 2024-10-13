using BlazorApexCharts.Docs.Components;
using ColorCode;
using Microsoft.AspNetCore.Components;
using System;

namespace BlazorApexCharts.Docs.Components
{
    public partial class CodeFormatter 
    {
        [Parameter] public string Code { get; set; }
       
        private string html;

        protected override void OnInitialized()
        {
            var formatter = new HtmlClassFormatter();
            html = formatter.GetHtmlString(Code, Languages.CSharp);
            base.OnInitialized();
        }

    }
}