using R5A08_TP1.Shared.DTO.Commun;

namespace BlazorApp.Services
{
    public interface IBrandService
    {
        Task<List<BrandDto>?> GetAllAsync();
        Task AddAsync(BrandCreateDto dto); // ajout
    }
}
