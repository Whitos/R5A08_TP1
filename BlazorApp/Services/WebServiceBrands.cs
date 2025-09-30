using BlazorApp.Models;
using System.Net.Http.Json;

namespace BlazorApp.Services
{
    public class BrandService : IService<Brand>
    {
        private readonly HttpClient httpClient = new()
        {
            BaseAddress = new Uri("https://localhost:7234/api/")
        };

        public async Task<List<Brand>?> GetAllAsync()
        {
            return await httpClient.GetFromJsonAsync<List<Brand>?>("brands");
        }

        public async Task<Brand?> GetByIdAsync(int id)
        {
            return await httpClient.GetFromJsonAsync<Brand?>($"brands/{id}");
        }

        public async Task AddAsync(Brand brand)
        {
            await httpClient.PostAsJsonAsync("brands", brand);
        }

        public async Task UpdateAsync(Brand brand)
        {
            await httpClient.PutAsJsonAsync($"brands/{brand.IdBrand}", brand);
        }

        public async Task DeleteAsync(int id)
        {
            await httpClient.DeleteAsync($"brands/{id}");
        }

        public Task<Product?> GetByNameAsync(string name) => throw new NotImplementedException();
    }
}
