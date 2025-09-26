using AutoMapper;
using R5A08_TP1.Models.DTO.Products;
using R5A08_TP1.Models.EntityFramework;

namespace R5A08_TP1.Models.Mapping
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            // Product -> ProductDto (GET /products)
            CreateMap<Product, ProductDto>()
                .ForMember(d => d.IdProduct, m => m.MapFrom(s => s.IdProduct))
                .ForMember(d => d.NameProduct, m => m.MapFrom(s => s.NameProduct))
                .ForMember(d => d.Brand, m => m.MapFrom(s => s.RelatedBrand.NameBrand))
                .ForMember(d => d.Type, m => m.MapFrom(s => s.RelatedTypeProduct.NameTypeProduct));

            // Product -> ProductDetailDto (GET /products/{id})
            CreateMap<Product, ProductDetailDto>()
                .ForMember(d => d.IdProduct, m => m.MapFrom(s => s.IdProduct))
                .ForMember(d => d.NameProduct, m => m.MapFrom(s => s.NameProduct))
                .ForMember(d => d.Description, m => m.MapFrom(s => s.Description))
                .ForMember(d => d.NamePhoto, m => m.MapFrom(s => s.UriPhoto))
                .ForMember(d => d.Brand, m => m.MapFrom(s => s.RelatedBrand.NameBrand))
                .ForMember(d => d.Type, m => m.MapFrom(s => s.RelatedTypeProduct.NameTypeProduct));

            // ProductCreateDto -> Product (POST)
            CreateMap<ProductCreateDto, Product>()
                .ForMember(d => d.IdBrand, m => m.MapFrom(s => s.IdBrand))
                .ForMember(d => d.IdTypeProduct, m => m.MapFrom(s => s.IdTypeProduct))
                .ForMember(d => d.IdProduct, m => m.Ignore());

            // Product -> ProductCreateDto (response POST)
            CreateMap<Product, ProductCreateDto>()
                .ForMember(d => d.IdBrand, m => m.MapFrom(s => s.IdBrand))
                .ForMember(d => d.IdTypeProduct, m => m.MapFrom(s => s.IdTypeProduct));

            // ProductUpdateDto -> Product (PUT)
            CreateMap<ProductUpdateDto, Product>()
                .ForMember(d => d.IdBrand, m => m.MapFrom(s => s.IdBrand))
                .ForMember(d => d.IdTypeProduct, m => m.MapFrom(s => s.IdTypeProduct))
                .ForMember(d => d.IdProduct, m => m.Ignore());
        }
    }
}
