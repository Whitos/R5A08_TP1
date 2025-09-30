using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using R5A08_TP1.Models.DataManager;
using R5A08_TP1.Models.DTO.Commun;
using R5A08_TP1.Models.EntityFramework;
using R5A08_TP1.Models.Repository;

namespace R5A08_TP1.Controllers
{
    [Route("api/brands")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IDataRepository<Brand> dataRepository;
        private readonly IMapper _mapper;

        public BrandsController(IDataRepository<Brand> dataRepository, IMapper mapper)
        {
            this.dataRepository = dataRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetAll()
        {
            var brands = await dataRepository.GetAllAsync();
            var dtos = _mapper.Map<IEnumerable<BrandDto>>(brands.Value);
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        [ActionName("GetById")]
        public async Task<ActionResult<BrandDto>> Get(int id)
        {
            var brand = await dataRepository.GetByIdAsync(id);
            if (brand.Value == null)
                return NotFound();

            var dto = _mapper.Map<BrandDto>(brand.Value);
            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<BrandDto>> Create(BrandDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entity = _mapper.Map<Brand>(dto);
            await dataRepository.AddAsync(entity);

            var createdBrandDto = _mapper.Map<BrandDto>(entity);
            return CreatedAtAction("GetById", new { id = entity.IdBrand }, createdBrandDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, BrandDto dto)
        {
            var existingBrand = await dataRepository.GetByIdAsync(id);
            if (existingBrand.Value == null)
                return NotFound();

            var entity = _mapper.Map<Brand>(dto);
            entity.IdBrand = id;

            await dataRepository.UpdateAsync(existingBrand.Value, entity);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            ActionResult<Brand> brandToDelete = await dataRepository.GetByIdAsync(id);
            if (brandToDelete.Value == null)
            {
                return NotFound();
            }
            await dataRepository.DeleteAsync(brandToDelete.Value);
            return NoContent();
        }
    }
}
