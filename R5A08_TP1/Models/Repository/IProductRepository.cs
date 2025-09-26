using Microsoft.EntityFrameworkCore;
using R5A08_TP1.Models.EntityFramework;

namespace R5A08_TP1.Models.Repository
{
    public interface IProductRepository : IDataRepository<Product>
    {
        private readonly ProductsDbContext _context;

        public IProductRepository(ProductsDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products
                .Include(p => p.RelatedBrand)
                .Include(p => p.RelatedTypeProduct)
                .ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products
                .Include(p => p.RelatedBrand)
                .Include(p => p.RelatedTypeProduct)
                .FirstOrDefaultAsync(p => p.IdProduct == id);
        }

        public async Task<Product?> GetByStringAsync(string str)
        {
            return await _context.Products
                .Include(p => p.RelatedBrand)
                .Include(p => p.RelatedTypeProduct)
                .FirstOrDefaultAsync(p => p.NameProduct == str);
        }

        public async Task AddAsync(Product entity)
        {
            await _context.Products.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product entityToUpdate, Product entity)
        {
            entityToUpdate.NameProduct = entity.NameProduct;
            entityToUpdate.Description = entity.Description;
            entityToUpdate.IdBrand = entity.IdBrand;
            entityToUpdate.IdTypeProduct = entity.IdTypeProduct;

            _context.Products.Update(entityToUpdate);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Product entity)
        {
            _context.Products.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
