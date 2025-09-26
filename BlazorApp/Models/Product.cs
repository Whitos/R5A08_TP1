
namespace BlazorApp.Models
{
    public class Product
    {
        public int IdProduct { get; set; }
        public string NameProduct { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string NamePhoto { get; set; } = null!;
        public string UriPhoto { get; set; } = null!;
        public int? IdTypeProduct { get; set; } = null!;
        public int? IdBrand { get; set; } = null!;
    }
}
