using BlazorApp.Models;
using BlazorApp.Models.DTO.Commun;
using System.Net.Http.Json;

namespace BlazorApp.Services
{
    public class WebServiceBrand : IBrandService
    {
        private readonly HttpClient httpClient = new()
        {
            BaseAddress = new Uri("https://localhost:7234/api/")
        };

        public async Task<List<BrandDto>?> GetAllAsync()
        {
            return await httpClient.GetFromJsonAsync<List<BrandDto>?>("brands");
        }

    }
}
