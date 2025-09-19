using Microsoft.EntityFrameworkCore.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace R5A08_TP1.Models.EntityFramework
{
    public class Produit
    {
        [Key]
        [Column("id_produit")]
        public int IdProduit { get; set; }

        [Column("nom_produit")]
        public string NomProduit { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("nom_photo")]
        public string NomPhoto { get; set; }

        [Column("uri_photo")]
        public string? UriPhoto { get; set; }

        [Column("id_type_produit")]
        public int? IdTypeProduit { get; set; }

        [Column("id_marque")]
        public int? IdMarque { get; set; }

        [Column("stock_reel")]
        public int StockReel { get; set; }

        [Column("stock_min")]
        public int StockMin { get; set; }

        [Column("stock_max")]
        public int StockMax { get; set; }

        [ForeignKey(nameof(IdTypeProduit))]
        [InverseProperty(nameof(TypeProduit.ProduitsAssoc))]
        public virtual TypeProduit? TypeProduitAssoc { get; set; } = null!;

        [ForeignKey(nameof(IdMarque))]
        [InverseProperty(nameof(Marque.ProduitsAssocMarques))]
        public virtual Marque? MarqueAssoc { get; set; } = null!;

    }
}
