using R5A08_TP1.Models.DTO.Common;

namespace R5A08_TP1.Models.DTO.Products
{
    // (pour GET /products/{id}) détail d'un produit
    public class ProductDetailDto
    {
        public int Id { get; set; }
        public string NameProduct { get; set; } = null!;
        public string? Description { get; set; }
        public string NamePhoto { get; set; } = null!;
        public string? UriPhoto { get; set; }
        public int ActualStock { get; set; }
        public int MinStock { get; set; }
        public int MaxStock { get; set; }
        public BrandDto? Brand { get; set; }
        public TypeProductDto? TypeProduct { get; set; }
    }
}
