using BlazorApp.Models.DTO.Commun;
using System.Net.Http.Json;

namespace BlazorApp.Services
{
    public class WebServiceTypeProducts : ITypeProductService
    {
        private readonly HttpClient httpClient = new()
        {
            BaseAddress = new Uri("https://localhost:7234/api/")
        };

        public async Task<List<TypeProductDto>?> GetAllAsync()
        {
            return await httpClient.GetFromJsonAsync<List<TypeProductDto>?>("typeproducts");
        }
    }
}
