using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace R5A08_TP1.Models.EntityFramework
{
    [Table("brand")]
    public class Brand
    {
        [Key]
        [Column("id_brand")]
        public int IdBrand { get; set; }

        [Column("name_brand")]
        public string NameBrand { get; set; }

        [InverseProperty(nameof(Product.RelatedBrand))]
        public virtual ICollection<Product> RelatedProductsBrands { get; set; } = new List<Product>();
    }
}
