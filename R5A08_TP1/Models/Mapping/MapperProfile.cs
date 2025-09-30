using AutoMapper;
using R5A08_TP1.Models.DTO.Commun;
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
                .ForMember(d => d.Description, m => m.MapFrom(s => s.Description))
                .ForMember(d => d.NameBrand, m => m.MapFrom(s => s.RelatedBrand != null ? s.RelatedBrand.NameBrand : null))
                .ForMember(d => d.NameTypeProduct, m => m.MapFrom(s => s.RelatedTypeProduct != null ? s.RelatedTypeProduct.NameTypeProduct : null));

            // Product -> ProductDetailDto (GET /products/{id})
            CreateMap<Product, ProductDetailDto>()
                .ForMember(d => d.IdProduct, m => m.MapFrom(s => s.IdProduct))
                .ForMember(d => d.NameProduct, m => m.MapFrom(s => s.NameProduct))
                .ForMember(d => d.Description, m => m.MapFrom(s => s.Description))
                .ForMember(d => d.NamePhoto, m => m.MapFrom(s => s.NamePhoto))
                .ForMember(d => d.UriPhoto, m => m.MapFrom(s => s.UriPhoto))
                .ForMember(d => d.Stock, m => m.MapFrom(s => s.ActualStock))
                .ForMember(d => d.EnReappro, m => m.MapFrom(s => s.ActualStock <= s.MinStock))
                .ForMember(d => d.Brand, m => m.MapFrom(s => s.RelatedBrand != null ? s.RelatedBrand.NameBrand : null))
                .ForMember(d => d.Type, m => m.MapFrom(s => s.RelatedTypeProduct != null ? s.RelatedTypeProduct.NameTypeProduct : null));

            // ProductCreateDto -> Product (POST)
            CreateMap<ProductCreateDto, Product>()
                .ForMember(d => d.IdBrand, m => m.MapFrom(s => s.IdBrand))
                .ForMember(d => d.IdTypeProduct, m => m.MapFrom(s => s.IdTypeProduct))
                .ForMember(d => d.IdProduct, m => m.Ignore());

            // Product -> ProductUpdateDto (pré-remplir le formulaire d’édition)
            CreateMap<Product, ProductUpdateDto>();

            // ProductUpdateDto -> Product (PUT)
            CreateMap<ProductUpdateDto, Product>()
                .ForMember(d => d.IdProduct, m => m.Ignore()); // ID géré par l’URL


            CreateMap<Brand, BrandDto>().ReverseMap();
            CreateMap<TypeProduct, TypeProductDto>().ReverseMap();
        }
    }
}
