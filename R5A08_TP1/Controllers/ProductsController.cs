using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using R5A08_TP1.Models.DataManager;
using R5A08_TP1.Models.DTO.Products;
using R5A08_TP1.Models.EntityFramework;
using R5A08_TP1.Models.Repository;

namespace R5A08_TP1.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IDataRepository<Product> dataRepository;
        private readonly IMapper _mapper;

        public ProductsController(IDataRepository<Product> dataRepository, IMapper mapper)
        {
            this.dataRepository = dataRepository;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        [ActionName("GetById")]
        public async Task<ActionResult<ProductDetailDto>> Get(int id)
        {
            var result = await dataRepository.GetByIdAsync(id);

            if (result.Value == null)
            {
                return NotFound();
            }

            var dto = _mapper.Map<ProductDetailDto>(result.Value);
            return Ok(dto); 
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll()
        {
            var products = await dataRepository.GetAllAsync();
            var dtos = _mapper.Map<IEnumerable<ProductDto>>(products.Value);
            return Ok(dtos);
        }

        [HttpPost]
        public async Task<ActionResult<ProductCreateDto>> Create(ProductCreateDto productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var entity = _mapper.Map<Product>(productDto);
            await dataRepository.AddAsync(entity);

            var createdProductDto = _mapper.Map<ProductCreateDto>(entity);
            return CreatedAtAction("GetById", new { id = entity.IdProduct }, createdProductDto);
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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] ProductUpdateDto productDto)
        {
            // Vérifier que l'ID dans l'URL correspond à l'ID supposé du produit
            var existingProductResult = await dataRepository.GetByIdAsync(id);
            
            if (existingProductResult.Value == null)
            {
                return NotFound();
            }

            var productToUpdate = _mapper.Map<Product>(productDto);
            productToUpdate.IdProduct = id; // S'assurer que l'ID est correct

            await dataRepository.UpdateAsync(existingProductResult.Value, productToUpdate);
            return Ok(_mapper.Map<ProductDetailDto>(productToUpdate));
        }
    }
}
