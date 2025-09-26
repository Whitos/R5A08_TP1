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

        public async Task<ActionResult<IEnumerable<Product>>> GetAllAsync()
        {
            return await dbContext.Products.ToListAsync();
        }

        public async Task<ActionResult<Product?>> GetByIdAsync(int id)
        {
            return await dbContext.Products.FindAsync(id);
        }

        public async Task<ActionResult<Product?>> GetByStringAsync(string str)
        {
            return await dbContext.Products.FirstOrDefaultAsync(p => p.NameProduct == str);
        }

        public async Task AddAsync(Product entity)
        {
            await dbContext.Products.AddAsync(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product entityToUpdate, Product entity)
        {
            dbContext.Products.Attach(entityToUpdate);
            dbContext.Entry(entityToUpdate).CurrentValues.SetValues(entity);

            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Product entity)
        {
            dbContext.Products.Remove(entity);
            await dbContext.SaveChangesAsync();
        }
    }
}
