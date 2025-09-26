using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using R5A08_TP1.Models.EntityFramework;
using R5A08_TP1.Models.Repository;

namespace R5A08_TP1.Models.DataManager
{
    public class BrandManager : IBrandsRepository
    {
        private readonly ProductsDbContext dbContext;

        public BrandManager(ProductsDbContext context)
        {
            dbContext = context;
        }

        public async Task<ActionResult<Brand?>> GetByBrandNameAsync(string str)
        {
            return await dbContext.Brands.FirstOrDefaultAsync(p => p.NameBrand == str);
        }

    }
}
