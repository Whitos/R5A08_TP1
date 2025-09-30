
namespace BlazorApp.Models
{
    public class Product
    {
        public int IdProduct { get; set; }
        public string NameProduct { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string NamePhoto { get; set; } = null!;
        public string UriPhoto { get; set; } = null!;
        public string NameTypeProduct { get; set; } = null!;
        public string NameBrand { get; set; } = null!;
    }
}
