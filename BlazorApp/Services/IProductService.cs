using R5A08_TP1.Shared.DTO.Products;


namespace BlazorApp.Services
{
    public interface IProductService
    {
        Task<List<ProductDto>?> GetAllAsync();
        Task<ProductDetailDto?> GetByIdAsync(int id);
        Task AddAsync(ProductCreateDto entity);
        Task UpdateAsync(ProductUpdateDto updatedEntity);
        Task DeleteAsync(int id);
    }
}
