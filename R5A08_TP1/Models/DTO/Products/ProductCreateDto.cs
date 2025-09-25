namespace R5A08_TP1.Models.DTO.Products
{
    public class ProductCreateDto
    {
        public string NameProduct { get; set; } = null!;
        public string? Description { get; set; }
        public string NamePhoto { get; set; } = null!;
        public string? UriPhoto { get; set; }
        public int? IdBrand { get; set; }
        public int? IdTypeProduct { get; set; }
        public int ActualStock { get; set; }
        public int MinStock { get; set; }
        public int MaxStock { get; set; }
    }
}
