using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using R5A08_TP1.Models.EntityFramework;
using R5A08_TP1.Models.Repository;

namespace R5A08_TP1.Models.DataManager
{
    public class TypeProductManager : ITypeProductRepository
    {
        private readonly ProductsDbContext dbContext;
        public TypeProductManager(ProductsDbContext context)
        {
            dbContext = context;
        }
        public async Task<ActionResult<TypeProduct?>> GetByTypeProductNameAsync(string str)
        {
            return await dbContext.TypeProducts.FirstOrDefaultAsync(p => p.NameTypeProduct == str);
        }
    }
}
