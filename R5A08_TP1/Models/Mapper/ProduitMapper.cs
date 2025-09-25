using R5A08_TP1.Models.DTO;
using R5A08_TP1.Models.EntityFramework;

namespace R5A08_TP1.Models.Mapper
{
    public class ProduitMapper : IMapper<Product, ProduitDto>
    {
        public Product? FromDTO(ProduitDto dto)
        {
            return new Product()
            {
                IdProduit = dto.Id,
                NameProduct = dto.Nom,
                RelatedTypeProduct = new TypeProduct() { NameTypeProduct = dto.Type },
                RelatedBrand = new Brand() { NameBrand = dto.Marque }
            };
        }

        public ProduitDto? FromEntity(Product entity)
        {
            return new ProduitDto()
            {
                Id = entity.IdProduit,
                Nom = entity.NameProduct,
                Type = entity.RelatedTypeProduct?.NameTypeProduct,
                Marque = entity.RelatedBrand?.NameBrand
            };
        }
    }
}
