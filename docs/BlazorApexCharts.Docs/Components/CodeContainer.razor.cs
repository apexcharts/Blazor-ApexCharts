using BlazorApexCharts.Docs.Components;
using ColorCode;
using Microsoft.AspNetCore.Components;
using System;

namespace BlazorApexCharts.Docs.Components
{
    public partial class CodeContainer : ICodeSnippet, IDisposable
    {
        [CascadingParameter] DocExamples DocExamples { get; set; }
        [Parameter] public string Title { get; set; }

        [Parameter] public RenderFragment ChildContent { get; set; }


        public Guid Id { get; set; } = Guid.NewGuid();
  
        protected override void OnInitialized()
        {
            DocExamples.AddCodeSnippet(this);

            base.OnInitialized();
        }

        public void Dispose()
        {
            DocExamples.RemoveCodeSnippet(this);
        }
    }
}