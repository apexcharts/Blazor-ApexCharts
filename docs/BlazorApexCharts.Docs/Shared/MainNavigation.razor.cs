using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using TabBlazor;

namespace BlazorApexCharts.Docs.Shared
{
    public partial class MainNavigation
    {
        [Inject] public IOffcanvasService offCanvas { get; set; }

        public async Task ShowGlobalOptions()
        {

            var component = new RenderComponent<GlobalOptions>();
            await offCanvas.ShowAsync("Global Options", component, new OffcanvasOptions {  Position = OffcanvasPosition.End, Backdrop = false, CloseOnClickOutside = true, CloseOnEsc = true} );


        }

    }
}