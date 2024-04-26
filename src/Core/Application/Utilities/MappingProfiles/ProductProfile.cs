using Application.Models.DTOs.Products;
using AutoMapper;
using Domain.Entities;

namespace Application.Utilities.MappingProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<CreateProductDto, Product>();

            CreateMap<Product, ProductItemDto>()
                .ForMember(dest=> dest.ProductId, src=> src.MapFrom(x=> x.Id));

            CreateMap<Product, ProductInfoDto>()
                .ForMember(dest => dest.ProductId, src => src.MapFrom(x => x.Id));

            // Update durumlarında eski değerlerini koruyup sadece değişen değerlerin update edilmesi için bu configurasyon kullanılabilir. Kod örneği ProductService update metodunda gösterilmiştir.
            // Dikkat! Eğer sadece değişen değirlerin update olmasını isteniyorsa EntityFramework Update senaryoları incelenmelidir. EntityState = Modified durumunda tüm alanların güncellendiği bir sql query veri tabanına yollanır. Alternatif olarak DbContext.Entry() yöntemi kullanılabilir. Bu durum değişen değerler için sql query oluşturur.

            CreateMap<UpdateProductDto, Product>()
                .ForMember(dest => dest.Id, src => src.MapFrom(x => x.ProductId))
                 .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));


            // Entity State Kullanılmayan durumlarda updateddate createdDate gibi durumlar automapper tarafından handler edileibilir

            //CreateMap<UpdateProductDto, Product>()
            //    .ForMember(dest => dest.Id, src => src.MapFrom(x => x.ProductId))
            //    .ForMember(dest => dest.UpdatedDate, val => val.MapFrom(x => DateTime.UtcNow));


            //CreateMap<CreateProductDto, Product>()
            //    .ForMember(dest=> dest.CreatedDate, val=> val.MapFrom(x=> DateTime.UtcNow));


            //CreateMap<UpdateProductDto, Product>()
            //    .ForMember(dest => dest.Id, src => src.MapFrom(x => x.ProductId));
        }
    }
}
