using R5A08_TP1.Shared.DTO.Products;
using System.Net.Http.Json;

namespace BlazorApp.Services
{
    public class WebServiceProducts : IProductService
    {
        private readonly HttpClient httpClient = new()
        {
            BaseAddress = new Uri("https://localhost:7234/api/")
        };
        public async Task<List<ProductDto>?> GetAllAsync()
        {
            return await httpClient.GetFromJsonAsync<List<ProductDto>?>("products");
        }

        public async Task<ProductDetailDto?> GetByIdAsync(int id)
        {
            return await httpClient.GetFromJsonAsync<ProductDetailDto?>($"products/{id}");
        }

        //public async Task<Product?> GetByNameAsync(string name)
        //{
        //    var response = await httpClient.PostAsJsonAsync("products/search", name);
        //    response.EnsureSuccessStatusCode();

        //    return await response.Content.ReadFromJsonAsync<Product>();
        //}

        public async Task AddAsync(ProductCreateDto productDto)
        {
            await httpClient.PostAsJsonAsync("products", productDto);
        }
        public async Task UpdateAsync(ProductUpdateDto updatedEntity)
        {
            await httpClient.PutAsJsonAsync($"products/{updatedEntity.IdProduct}", updatedEntity);
        }

        public async Task DeleteAsync(int id)
        {
            await httpClient.DeleteAsync($"products/{id}");
        }


    }
}
