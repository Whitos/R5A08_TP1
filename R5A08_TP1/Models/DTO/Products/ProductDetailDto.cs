namespace R5A08_TP1.Models.DTO.Products
{
    // (pour GET /products/{id}) détail d'un produit
    public class ProductDetailDto
    {
        public int IdProduct { get; set; }
        public string? NameProduct { get; set; }
        public string? Type { get; set; }
        public string? Brand { get; set; }
        public string? Description { get; set; }
        public string? NamePhoto { get; set; }
        public string? UriPhoto { get; set; }
        public int? Stock { get; set; }
        public bool EnReappro { get; set; }
    }
}
