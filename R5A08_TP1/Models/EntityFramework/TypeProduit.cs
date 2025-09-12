using Microsoft.EntityFrameworkCore.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace R5A08_TP1.Models.EntityFramework
{
    public class TypeProduit
    {
        [Key]
        [Column("id_type_produit")]
        public int IdTypeProduit { get; set; }

        [Column("nom_type_produit")]
        public string NomTypeProduit { get; set; }

        [InverseProperty(nameof(Produit.TypeProduitAssoc))]
        public virtual ICollection<Produit> ProduitsAssoc { get; set; } = new List<Produit>();
    }
}
