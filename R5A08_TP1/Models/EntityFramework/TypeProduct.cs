using Microsoft.EntityFrameworkCore.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace R5A08_TP1.Models.EntityFramework
{
    [Table("type_product")]
    public class TypeProduct
    {
        [Key]
        [Column("id_type_product")]
        public int IdTypeProduct { get; set; }

        [Column("name_type_product")]
        public string NameTypeProduct { get; set; }

        [InverseProperty(nameof(Product.RelatedTypeProduct))]
        public virtual ICollection<Product> RelatedProducts { get; set; } = new List<Product>();
    }
}
