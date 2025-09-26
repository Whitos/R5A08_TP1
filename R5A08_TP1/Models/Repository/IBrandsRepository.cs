using Microsoft.AspNetCore.Mvc;
using R5A08_TP1.Models.EntityFramework;

namespace R5A08_TP1.Models.Repository
{
    public interface IBrandsRepository : IDataRepository<Brand>
    {
        Task<ActionResult<Brand>> GetByBrandNameAsync(string name);
    }
}
