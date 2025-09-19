using R5A08_TP1.Models.DTO;
using R5A08_TP1.Models.EntityFramework;

namespace R5A08_TP1.Models.Mapper
{
    public class ProduitMapper : IMapper<Produit, ProduitDto>
    {
        public Produit? FromDTO(ProduitDto dto)
        {
            return new Produit()
            {
                IdProduit = dto.Id,
                NomProduit = dto.Nom,
                TypeProduitAssoc = new TypeProduit() { NomTypeProduit = dto.Type },
                MarqueAssoc = new Marque() { NomMarque = dto.Marque }
            };
        }

        public ProduitDto? FromEntity(Produit entity)
        {
            return new ProduitDto()
            {
                Id = entity.IdProduit,
                Nom = entity.NomProduit,
                Type = entity.TypeProduitAssoc?.NomTypeProduit,
                Marque = entity.MarqueAssoc?.NomMarque
            };
        }
    }
}
