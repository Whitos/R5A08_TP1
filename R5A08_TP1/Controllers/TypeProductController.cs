using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using R5A08_TP1.Models.DTO.Commun;
using R5A08_TP1.Models.EntityFramework;
using R5A08_TP1.Models.Repository;

namespace R5A08_TP1.Controllers
{
    [Route("api/typeproduct")]
    [ApiController]
    public class TypeProductController : ControllerBase
    {
        private readonly IDataRepository<TypeProduct> dataRepository;
        private readonly IMapper _mapper;

        public TypeProductController(IDataRepository<TypeProduct> dataRepository, IMapper mapper)
        {
            this.dataRepository = dataRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TypeProductDto>>> GetAll()
        {
            var typeproducts = await dataRepository.GetAllAsync();
            var dtos = _mapper.Map<IEnumerable<TypeProductDto>>(typeproducts.Value);
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        [ActionName("GetById")]
        public async Task<ActionResult<TypeProductDto>> Get(int id)
        {
            var typeproduct = await dataRepository.GetByIdAsync(id);
            if (typeproduct.Value == null)
                return NotFound();

            var dto = _mapper.Map<BrandDto>(typeproduct.Value);
            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<TypeProductDto>> Create(TypeProductDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entity = _mapper.Map<TypeProduct>(dto);
            await dataRepository.AddAsync(entity);

            var createdTypeProductDto = _mapper.Map<TypeProductDto>(entity);
            return CreatedAtAction("GetById", new { id = entity.IdTypeProduct }, createdTypeProductDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TypeProductDto dto)
        {
            var existingTypeProduct = await dataRepository.GetByIdAsync(id);
            if (existingTypeProduct.Value == null)
                return NotFound();

            var entity = _mapper.Map<TypeProduct>(dto);
            entity.IdTypeProduct = id;

            await dataRepository.UpdateAsync(existingTypeProduct.Value, entity);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            ActionResult<TypeProduct> typeProductToDelete = await dataRepository.GetByIdAsync(id);
            if (typeProductToDelete.Value == null)
            {
                return NotFound();
            }
            await dataRepository.DeleteAsync(typeProductToDelete.Value);
            return NoContent();
        }
    }
}
