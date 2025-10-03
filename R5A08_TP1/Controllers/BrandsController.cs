using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using R5A08_TP1.Models.EntityFramework;
using R5A08_TP1.Models.Repository;
using R5A08_TP1.Shared.DTO.Commun;

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
    public async Task<ActionResult<BrandDto>> Create(BrandCreateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var entity = _mapper.Map<Brand>(dto);
        await dataRepository.AddAsync(entity);

        var createdBrandDto = _mapper.Map<BrandDto>(entity);
        return CreatedAtAction(nameof(Get), new { id = entity.IdBrand }, createdBrandDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, BrandUpdateDto dto)
    {
        var existingBrand = await dataRepository.GetByIdAsync(id);
        if (existingBrand.Value == null)
            return NotFound();

        _mapper.Map(dto, existingBrand.Value);
        await dataRepository.UpdateAsync(existingBrand.Value, existingBrand.Value);

        var updatedDto = _mapper.Map<BrandDto>(existingBrand.Value);
        return Ok(updatedDto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var brandToDelete = await dataRepository.GetByIdAsync(id);
        if (brandToDelete.Value == null)
            return NotFound();

        await dataRepository.DeleteAsync(brandToDelete.Value);
        return NoContent();
    }

}
