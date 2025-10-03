using R5A08_TP1.Shared.DTO.Products;

namespace R5A08_TP1.Shared.DTO.Commun
{
    public class BrandDetailDto
    {
        public int IdBrand { get; set; }
        public string NameBrand { get; set; }
        public IEnumerable<ProductDto> Products { get; set; }
    }
}
