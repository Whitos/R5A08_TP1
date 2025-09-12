using Microsoft.AspNetCore.Mvc;
using R5A08_TP1.Models.EntityFramework;
using R5A08_TP1.Models.Repository;

namespace R5A08_TP1.Models.DataManager
{
    public class ProduitManager : IDataRepository<Produit>
    {
        public async Task AddAsync(Produit entity)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Produit entity)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult<IEnumerable<Produit>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult<Produit>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult<Produit>> GetByStringAsync(string str)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(Produit entityToUpdate, Produit entity)
        {
            throw new NotImplementedException();
        }
    }
}
