namespace R5A08_TP1.Models.DTO.Products
{
    // GET /products
    public class ProductDto
    {
        public int IdProduct { get; set; }
        public string? NameProduct { get; set; }
        public string? Description { get; set; }
        public string? NameTypeProduct { get; set; }
        public string? NameBrand { get; set; }
    }
}
