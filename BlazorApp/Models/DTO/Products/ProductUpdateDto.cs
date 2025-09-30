namespace BlazorApp.Models.DTO.Products
{
    // Utilisé pour PUT /products/{id}
    public class ProductUpdateDto
    {
        public int IdProduct { get; set; }
        public string NameProduct { get; set; } = null!;
        public string? Description { get; set; }
        public string NamePhoto { get; set; } = null!;
        public string? UriPhoto { get; set; }
        public int IdBrand { get; set; }
        public int IdTypeProduct { get; set; }
        public int ActualStock { get; set; }
        public int MinStock { get; set; }
        public int MaxStock { get; set; }
    }
}
