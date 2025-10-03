using R5A08_TP1.Shared.DTO.Commun;
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
            return await httpClient.GetFromJsonAsync<List<TypeProductDto>?>("type-products");
        }

        public async Task AddAsync(TypeProductCreateDto dto)
        {
            await httpClient.PostAsJsonAsync("type-products", dto);
        }
    }
}
