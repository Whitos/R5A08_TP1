namespace BlazorApp.Models.DTO.Products
{
    public class ProductDetailDto
    {
        public int IdProduct { get; set; }
        public string NameProduct { get; set; } = null!;
        public string? Description { get; set; }
        public string NamePhoto { get; set; } = null!;
        public string? UriPhoto { get; set; }
        public int Stock { get; set; }
        public bool EnReappro { get; set; }
        public string? Brand { get; set; }
        public string? Type { get; set; }
    }
}
