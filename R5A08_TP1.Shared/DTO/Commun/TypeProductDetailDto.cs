using R5A08_TP1.Shared.DTO.Products;

namespace R5A08_TP1.Shared.DTO.Commun
{
    public class TypeProductDetailDto
    {
        public int IdTypeProduct { get; set; }
        public string NameTypeProduct { get; set; }
        public IEnumerable<ProductDto> Products { get; set; }
    }
}
