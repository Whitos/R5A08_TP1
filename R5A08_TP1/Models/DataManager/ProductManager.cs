using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using R5A08_TP1.Models.EntityFramework;
using R5A08_TP1.Models.Repository;

namespace R5A08_TP1.Models.DataManager
{
    public class ProductManager : IDataRepository<Product>
    {
        private readonly ProductsDbContext dbContext;

        public ProductManager(ProductsDbContext context)
        {
            dbContext = context;
        }

        // GET all
        public async Task<ActionResult<IEnumerable<Product>>> GetAllAsync()
        {
            var products = await dbContext.Products
                .Include(p => p.RelatedBrand)
                .Include(p => p.RelatedTypeProduct)
                .ToListAsync();
            
            return new ActionResult<IEnumerable<Product>>(products);
        }

        // GET by ID
        public async Task<ActionResult<Product>> GetByIdAsync(int id)
        {
            var product = await dbContext.Products
                .Include(p => p.RelatedBrand)
                .Include(p => p.RelatedTypeProduct)
                .FirstOrDefaultAsync(p => p.IdProduct == id);
            
            if (product == null)
            {
                return new NotFoundResult();
            }
            
            return new ActionResult<Product>(product);
        }

        // GET by string (e.g., NameProduct)
        public async Task<ActionResult<Product>> GetByStringAsync(string str)
        {
            var product = await dbContext.Products
                .Include(p => p.RelatedBrand)
                .Include(p => p.RelatedTypeProduct)
                .FirstOrDefaultAsync(p => p.NameProduct == str);
            
            if (product == null)
            {
                return new NotFoundResult();
            }
            
            return new ActionResult<Product>(product);
        }

        // POST
        public async Task AddAsync(Product entity)
        {
            await dbContext.Products.AddAsync(entity);
            await dbContext.SaveChangesAsync();
        }

        // PUT
        public async Task UpdateAsync(Product entityToUpdate, Product entity)
        {
            dbContext.Products.Attach(entityToUpdate);
            dbContext.Entry(entityToUpdate).CurrentValues.SetValues(entity);

            await dbContext.SaveChangesAsync();
        }

        // DELETE
        public async Task DeleteAsync(Product entity)
        {
            dbContext.Products.Remove(entity);
            await dbContext.SaveChangesAsync();
        }
    }
}
