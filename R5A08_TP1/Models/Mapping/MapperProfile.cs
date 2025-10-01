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
            // ---------------- PRODUCT LIST ----------------
            // Entité Product -> ProductDto
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.NameBrand,
                            cfg => cfg.MapFrom(src => src.RelatedBrand != null ? src.RelatedBrand.NameBrand : null))
                .ForMember(dest => dest.NameTypeProduct,
                            cfg => cfg.MapFrom(src => src.RelatedTypeProduct != null ? src.RelatedTypeProduct.NameTypeProduct : null));

            // Je mape pas IdProduct, NameProduct, Description car Automaper le fait tout seul


            // ---------------- PRODUCT DETAIL ----------------
            // Entité Product -> ProductDetailDto
            CreateMap<Product, ProductDetailDto>()
                .ForMember(dest => dest.EnReappro, 
                           cfg => cfg.MapFrom(src => src.ActualStock <= src.MinStock))  // calcul pour savoir si le ActualStock est <= à MinStock EnReappro = true sinon false 
                .ForMember(dest => dest.Brand, 
                           cfg => cfg.MapFrom(src => src.RelatedBrand != null ? src.RelatedBrand.NameBrand : null))
                .ForMember(dest => dest.Type, 
                           cfg => cfg.MapFrom(src => src.RelatedTypeProduct != null ? src.RelatedTypeProduct.NameTypeProduct : null));
            //  Ici aussi, tout ce qui a le même nom (IdProduct, NameProduct, ActualStock, etc.) 


            // ---------------- PRODUCT CREATION ----------------
            // ProductCreateDto (client) -> Product (POST) 
            // Conversion DTO -> Entité
            CreateMap<ProductCreateDto, Product>()
                .ForMember(dest => dest.IdProduct, cfg => cfg.Ignore()); 
                // l'API génère l'ID, on ne l’envoie pas côté client.

            // Pour renvoyer le produit créé
            CreateMap<Product, ProductCreateDto>();


            // ---------------- PRODUCT UPDATE ----------------
            // Product -> ProductUpdateDto (pré-remplir formulaire)
            CreateMap<Product, ProductUpdateDto>();

            // ProductUpdateDto -> Product (PUT)
            CreateMap<ProductUpdateDto, Product>()
                .ForMember(dest => dest.IdProduct, cfg => cfg.Ignore());
            // l’ID est pris dans l’URL, pas dans le body.


            // ---------------- BRAND & TYPE ----------------
            // Sais mapper de Brand vers BrandDto ET aussi de BrandDto vers Brand automatiquement
            CreateMap<Brand, BrandDto>().ReverseMap();
            CreateMap<TypeProduct, TypeProductDto>().ReverseMap();
        }
    }
}
