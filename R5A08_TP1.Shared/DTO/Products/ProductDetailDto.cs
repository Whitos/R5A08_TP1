namespace R5A08_TP1.Shared.DTO.Products
{
    // (pour GET /products/{id}) détail d'un produit
    public class ProductDetailDto
    {
        public int IdProduct { get; set; }
        public string? NameProduct { get; set; }
        public string? Description { get; set; }
        public string? NamePhoto { get; set; }
        public string? UriPhoto { get; set; }


        public int IdBrand { get; set; }
        public int IdTypeProduct { get; set; }
        public int ActualStock { get; set; }
        public int MinStock { get; set; }
        public int MaxStock { get; set; }


        public bool EnReappro { get; set; }
        public string? Brand { get; set; }
        public string? Type { get; set; }
    }
}
