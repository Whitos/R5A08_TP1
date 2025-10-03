using BlazorApp.Shared;
using BlazorApp.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorBootstrap;

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
