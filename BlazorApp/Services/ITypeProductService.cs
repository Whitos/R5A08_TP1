using BlazorApp.Models.DTO.Commun;

namespace BlazorApp.Services
{
    public interface ITypeProductService
    {
        Task<List<TypeProductDto>?> GetAllAsync();
    }
}
