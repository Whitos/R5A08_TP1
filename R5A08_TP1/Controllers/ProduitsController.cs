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

        [HttpGet("id")]
        [Route("[action]/{id}")]
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

        public async Task<ActionResult<IEnumerable<Produit>>> GetAllProduits()
        {
            return await dataRepository.GetAllAsync();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutUtilisateur(int id, Produit produit)
        {
            if (id != produit.IdProduit)
            {
                return BadRequest();
            }
            var produitToUpdate = await dataRepository.GetByIdAsync(id);
            if (produitToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await dataRepository.UpdateAsync(produitToUpdate.Value, produit);
                return NoContent();
            }
        }

    }
}
