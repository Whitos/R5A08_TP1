using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using R5A08_TP1.Models.DataManager;
using R5A08_TP1.Models.EntityFramework;
using R5A08_TP1.Models.Repository;

namespace R5A08_TP1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProduitsController : Controller
    {
        private readonly IDataRepository<Produit> dataRepository;

        public ProduitsController(IDataRepository<Produit> dataRepo)
        {
            dataRepository = dataRepo;
        }

        [HttpGet("{id}")]
        [ActionName("GetById")]

        public async Task<ActionResult<Produit>> Get(int id)
        {
            var result = await dataRepository.GetByIdAsync(id);

            if (result.Value == null)
            {
                return NotFound();
            }

            return result;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produit>>> GetAll()
        {
            return await dataRepository.GetAllAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Produit>> Create(Produit produit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await dataRepository.AddAsync(produit);
            return CreatedAtAction("GetById", new { id = produit.IdProduit }, produit);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            ActionResult<Produit> produitToDelete = await dataRepository.GetByIdAsync(id);
            if (produitToDelete.Value == null)
            {
                return NotFound();
            }
            await dataRepository.DeleteAsync(produitToDelete.Value);
            return NoContent();
        }
    }
}
