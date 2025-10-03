using R5A08_TP1.Shared.DTO.Commun;

namespace BlazorApp.Services
{
    public interface ITypeProductService
    {
        Task<List<TypeProductDto>?> GetAllAsync();
        Task AddAsync(TypeProductCreateDto dto); // ajout
    }
}
