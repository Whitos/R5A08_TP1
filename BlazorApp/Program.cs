using BlazorApp.Services;
using BlazorApp.Shared;
using BlazorBootstrap;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace BlazorApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");
            builder.Services.AddScoped<IProductService, WebServiceProducts>();
            builder.Services.AddScoped<IBrandService, WebServiceBrand>();
            builder.Services.AddScoped<ITypeProductService, WebServiceTypeProducts>();
            builder.Services.AddBlazorBootstrap();


            await builder.Build().RunAsync();
        }
    }
}
