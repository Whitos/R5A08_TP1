using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using R5A08_TP1.Models.EntityFramework;
using R5A08_TP1.Models.Repository;

namespace R5A08_TP1.Models.DataManager
{
    public class TypeProductManager : IDataRepository<TypeProduct>
    {
        private readonly ProductsDbContext dbContext;
        
        public TypeProductManager(ProductsDbContext context)
        {
            dbContext = context;
        }

        public async Task<ActionResult<IEnumerable<TypeProduct>>> GetAllAsync()
        {
            var typeProducts = await dbContext.TypeProducts
                .Include(tp => tp.RelatedProducts)
                .ToListAsync();
            
            return new ActionResult<IEnumerable<TypeProduct>>(typeProducts);
        }

        public async Task<ActionResult<TypeProduct>> GetByIdAsync(int id)
        {
            var typeProduct = await dbContext.TypeProducts
                .Include(tp => tp.RelatedProducts)
                .FirstOrDefaultAsync(tp => tp.IdTypeProduct == id);
            
            if (typeProduct == null)
            {
                return new NotFoundResult();
            }
            
            return new ActionResult<TypeProduct>(typeProduct);
        }

        public async Task<ActionResult<TypeProduct>> GetByStringAsync(string str)
        {
            var typeProduct = await dbContext.TypeProducts
                .Include(tp => tp.RelatedProducts)
                .FirstOrDefaultAsync(tp => tp.NameTypeProduct == str);
            
            if (typeProduct == null)
            {
                return new NotFoundResult();
            }
            
            return new ActionResult<TypeProduct>(typeProduct);
        }

        public async Task AddAsync(TypeProduct entity)
        {
            await dbContext.TypeProducts.AddAsync(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(TypeProduct entityToUpdate, TypeProduct entity)
        {
            dbContext.TypeProducts.Attach(entityToUpdate);
            dbContext.Entry(entityToUpdate).CurrentValues.SetValues(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(TypeProduct entity)
        {
            dbContext.TypeProducts.Remove(entity);
            await dbContext.SaveChangesAsync();
        }

        // Méthode supplémentaire spécifique aux TypeProducts
        public async Task<ActionResult<TypeProduct>> GetByTypeProductNameAsync(string name)
        {
            return await GetByStringAsync(name);
        }
    }
}
