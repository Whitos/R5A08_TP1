namespace R5A08_TP1.Models.DTO.Products
{
    // (pour GET /products) liste des produits
    public class ProductListDto
    {
        public int Id { get; set; }
        public string NameProduct { get; set; } = null!;
        public string? Description { get; set; }
        public string NamePhoto { get; set; } = null!;
        public string? UriPhoto { get; set; }
        public int ActualStock { get; set; }
        public int MinStock { get; set; }
        public int MaxStock { get; set; }
        public string? BrandName { get; set; }
        public string? TypeProductName { get; set; }
    }
}
