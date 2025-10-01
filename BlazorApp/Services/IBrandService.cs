using BlazorApp.Models.DTO.Commun;

namespace BlazorApp.Services
{
    public interface IBrandService
    {
        Task<List<BrandDto>?> GetAllAsync();
    }
}
