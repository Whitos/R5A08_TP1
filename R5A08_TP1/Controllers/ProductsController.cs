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
    [Route("api/products")]
    [ApiController]
    public class ProductsController : Controller
    {
        private readonly IDataRepository<Product> dataRepository;

        public ProductsController(IDataRepository<Product> dataRepo)
        {
            dataRepository = dataRepo;
        }

        [HttpGet("{id}")]
        [ActionName("GetById")]

        public async Task<ActionResult<Product>> Get(int id)
        {
            var result = await dataRepository.GetByIdAsync(id);

            if (result.Value == null)
            {
                return NotFound();
            }

            return result;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll()
        {
            return await dataRepository.GetAllAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Product>> Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await dataRepository.AddAsync(product);
            return CreatedAtAction("GetById", new { id = product.IdProduct }, product);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            ActionResult<Product> productToDelete = await dataRepository.GetByIdAsync(id);
            if (productToDelete.Value == null)
            {
                return NotFound();
            }
            await dataRepository.DeleteAsync(productToDelete.Value);
            return NoContent();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] Product product)
        {
            if (id != product.IdProduct)
            {
                return BadRequest();
            }

            ActionResult<Product?> productToUpdate = await dataRepository.GetByIdAsync(id);

            if (productToUpdate.Value == null)
            {
                return NotFound();
            }

            await dataRepository.UpdateAsync(productToUpdate.Value, product);
            return NoContent();
        }
    }
}
