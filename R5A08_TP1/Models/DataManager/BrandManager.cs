using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using R5A08_TP1.Models.EntityFramework;
using R5A08_TP1.Models.Repository;

namespace R5A08_TP1.Models.DataManager
{
    public class BrandManager : IDataRepository<Brand>
    {
        private readonly ProductsDbContext dbContext;

        public BrandManager(ProductsDbContext context)
        {
            dbContext = context;
        }

        public async Task<ActionResult<IEnumerable<Brand>>> GetAllAsync()
        {
            var brands = await dbContext.Brands
                .Include(b => b.RelatedProductsBrands)
                .ToListAsync();
            
            return new ActionResult<IEnumerable<Brand>>(brands);
        }

        public async Task<ActionResult<Brand>> GetByIdAsync(int id)
        {
            var brand = await dbContext.Brands
                .Include(b => b.RelatedProductsBrands)
                .FirstOrDefaultAsync(b => b.IdBrand == id);
            
            if (brand == null)
            {
                return new NotFoundResult();
            }
            
            return new ActionResult<Brand>(brand);
        }

        public async Task<ActionResult<Brand>> GetByStringAsync(string str)
        {
            var brand = await dbContext.Brands
                .Include(b => b.RelatedProductsBrands)
                .FirstOrDefaultAsync(b => b.NameBrand == str);
            
            if (brand == null)
            {
                return new NotFoundResult();
            }
            
            return new ActionResult<Brand>(brand);
        }

        public async Task AddAsync(Brand entity)
        {
            await dbContext.Brands.AddAsync(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Brand entityToUpdate, Brand entity)
        {
            dbContext.Brands.Attach(entityToUpdate);
            dbContext.Entry(entityToUpdate).CurrentValues.SetValues(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Brand entity)
        {
            dbContext.Brands.Remove(entity);
            await dbContext.SaveChangesAsync();
        }

        // Méthode supplémentaire spécifique aux Brands
        public async Task<ActionResult<Brand>> GetByBrandNameAsync(string name)
        {
            return await GetByStringAsync(name);
        }
    }
}
