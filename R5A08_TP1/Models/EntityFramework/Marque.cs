using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace R5A08_TP1.Models.EntityFramework
{
    public class Marque
    {
        [Key]
        [Column("id_marque")]
        public int IdMarque { get; set; }

        [Column("nom_marque")]
        public string NomMarque { get; set; }

        [InverseProperty(nameof(Produit.MarqueAssoc))]
        public virtual ICollection<Produit> ProduitsAssocMarques { get; set; } = new List<Produit>();
    }
}
