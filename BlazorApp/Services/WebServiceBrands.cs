using R5A08_TP1.Shared.DTO.Commun;
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

        public async Task AddAsync(BrandCreateDto dto)
        {
            await httpClient.PostAsJsonAsync("brands", dto);
        }

    }
}
