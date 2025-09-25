using Microsoft.EntityFrameworkCore.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace R5A08_TP1.Models.EntityFramework
{
    [Table("product")]
    public class Product
    {
        [Key]
        [Column("id_product")]
        public int IdProduct { get; set; }

        [Column("name_product")]
        public string NameProduct { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("name_photo")]
        public string NamePhoto { get; set; }

        [Column("uri_photo")]
        public string? UriPhoto { get; set; }

        [Column("id_type_product")]
        public int? IdTypeProduct { get; set; }

        [Column("id_brand")]
        public int? IdBrand { get; set; }

        [Column("actual_stock")]
        public int ActualStock { get; set; }

        [Column("min_stock")]
        public int MinStock { get; set; }

        [Column("max_stock")]
        public int MaxStock { get; set; }

        [ForeignKey(nameof(IdTypeProduct))]
        [InverseProperty(nameof(TypeProduct.RelatedProducts))]
        public virtual TypeProduct? RelatedTypeProduct { get; set; } = null!;

        [ForeignKey(nameof(IdBrand))]
        [InverseProperty(nameof(Brand.RelatedProductsBrands))]
        public virtual Brand? RelatedBrand { get; set; } = null!;

    }
}
