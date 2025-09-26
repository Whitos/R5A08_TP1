using Microsoft.AspNetCore.Mvc;
using R5A08_TP1.Models.EntityFramework;

namespace R5A08_TP1.Models.Repository
{
    public interface ITypeProductRepository : IDataRepository<TypeProduct>
    {
        Task<ActionResult<TypeProduct>> GetByTypeProductNameAsync(string name);
    }
}
