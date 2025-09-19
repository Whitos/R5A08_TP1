using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using R5A08_TP1.Models.EntityFramework;
using R5A08_TP1.Models.Repository;

namespace R5A08_TP1.Models.DataManager
{
    public class ProduitManager : IDataRepository<Produit>
    {
        private readonly ProduitsDbContext dbContext;

        public ProduitManager(ProduitsDbContext context)
        {
            dbContext = context;
        }

        public async Task<ActionResult<IEnumerable<Produit>>> GetAllAsync()
        {
            return await dbContext.Produits.ToListAsync();
        }

        public async Task<ActionResult<Produit?>> GetByIdAsync(int id)
        {
            return await dbContext.Produits.FindAsync(id);
        }

        public async Task<ActionResult<Produit?>> GetByStringAsync(string str)
        {
            throw new NotImplementedException();
        }

        public async Task AddAsync(Produit entity)
        {
            await dbContext.Produits.AddAsync(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Produit entityToUpdate, Produit entity)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Produit entity)
        {
            dbContext.Produits.Remove(entity);
            await dbContext.SaveChangesAsync();
        }
    }
}
