using AutoMapper;
using R5A08_TP1.Models.DTO.Common;
using R5A08_TP1.Models.DTO.Products;
using R5A08_TP1.Models.EntityFramework;

namespace R5A08_TP1.Models.Mapping
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            // Product -> ProductListDto (pour GET /products)
            CreateMap<Product, ProductListDto>()
                .ForMember(d => d.Id, m => m.MapFrom(s => s.IdProduct))
                .ForMember(d => d.BrandName, m => m.MapFrom(s => s.RelatedBrand != null ? s.RelatedBrand.NameBrand : null))
                .ForMember(d => d.TypeProductName, m => m.MapFrom(s => s.RelatedTypeProduct != null ? s.RelatedTypeProduct.NameTypeProduct : null));

            // Product -> ProductDetailDto (pour GET /products/{id})
            CreateMap<Product, ProductDetailDto>()
                .ForMember(d => d.Id, m => m.MapFrom(s => s.IdProduct))
                .ForMember(d => d.Brand, m => m.MapFrom(s => s.RelatedBrand))
                .ForMember(d => d.TypeProduct, m => m.MapFrom(s => s.RelatedTypeProduct));

            // ProductCreateDto -> Product (pour POST /products)
            CreateMap<ProductCreateDto, Product>()
                .ForMember(d => d.IdBrand, m => m.MapFrom(s => s.IdBrand))
                .ForMember(d => d.IdTypeProduct, m => m.MapFrom(s => s.IdTypeProduct))
                .ForMember(d => d.IdProduct, m => m.Ignore()); // L'ID sera généré par la base de données

            // Product -> ProductCreateDto (pour la réponse POST)
            CreateMap<Product, ProductCreateDto>()
                .ForMember(d => d.IdBrand, m => m.MapFrom(s => s.IdBrand))
                .ForMember(d => d.IdTypeProduct, m => m.MapFrom(s => s.IdTypeProduct));

            // ProductUpdateDto -> Product (pour PUT /products/{id})
            CreateMap<ProductUpdateDto, Product>()
                .ForMember(d => d.IdBrand, m => m.MapFrom(s => s.IdBrand))
                .ForMember(d => d.IdTypeProduct, m => m.MapFrom(s => s.IdTypeProduct))
                .ForMember(d => d.IdProduct, m => m.Ignore()); // L'ID sera défini manuellement

            // Brand -> BrandDto
            CreateMap<Brand, BrandDto>()
                .ForMember(d => d.Id, m => m.MapFrom(s => s.IdBrand))
                .ForMember(d => d.Name, m => m.MapFrom(s => s.NameBrand));

            // TypeProduct -> TypeProductDto
            CreateMap<TypeProduct, TypeProductDto>()
                .ForMember(d => d.Id, m => m.MapFrom(s => s.IdTypeProduct))
                .ForMember(d => d.Name, m => m.MapFrom(s => s.NameTypeProduct));
        }
    }
}
