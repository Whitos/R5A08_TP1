namespace R5A08_TP1.Shared.DTO.Products
{
    public class ProductUpdateDto
    {
        public int IdProduct { get; set; }
        public string NameProduct { get; set; } = null!;
        public string? Description { get; set; }
        public string NamePhoto { get; set; } = null!;
        public string? UriPhoto { get; set; }
        public int IdBrand { get; set; }            // int FK
        public int IdTypeProduct { get; set; }      // int FK
        public int ActualStock { get; set; }
        public int MinStock { get; set; }
        public int MaxStock { get; set; }
    }
}
