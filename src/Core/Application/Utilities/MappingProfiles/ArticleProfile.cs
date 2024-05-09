using Application.Models.DTOs.Articles;
using AutoMapper;
using Domain.Entities;

namespace Application.Utilities.MappingProfiles
{
    public class ArticleProfile : Profile
    {
        public ArticleProfile()
        {
            CreateMap<CreateArticleDto, Article>();

            CreateMap<Article, ArticleItemDto>()
                .ForMember(dest=> dest.ArticleId, src=> src.MapFrom(x=> x.Id));

            CreateMap<Article, ArticleInfoDto>()
                .ForMember(dest => dest.ArticleId, src => src.MapFrom(x => x.Id));

            // Update durumlarında eski değerlerini koruyup sadece değişen değerlerin update edilmesi için bu configurasyon kullanılabilir. Kod örneği ProductService update metodunda gösterilmiştir.
            // Dikkat! Eğer sadece değişen değirlerin update olmasını isteniyorsa EntityFramework Update senaryoları incelenmelidir. EntityState = Modified durumunda tüm alanların güncellendiği bir sql query veri tabanına yollanır. Alternatif olarak DbContext.Entry() yöntemi kullanılabilir. Bu durum değişen değerler için sql query oluşturur.

            CreateMap<UpdateArticleDto, Article>()
                .ForMember(dest => dest.Id, src => src.MapFrom(x => x.ArticleId))
                 .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));


            // Entity State Kullanılmayan durumlarda updateddate createdDate gibi durumlar automapper tarafından handler edileibilir

            //CreateMap<UpdateArticleDto, Article>()
            //    .ForMember(dest => dest.Id, src => src.MapFrom(x => x.ArticleId))
            //    .ForMember(dest => dest.UpdatedDate, val => val.MapFrom(x => DateTime.UtcNow));


            //CreateMap<CreateArticleDto, Article>()
            //    .ForMember(dest=> dest.CreatedDate, val=> val.MapFrom(x=> DateTime.UtcNow));


            //CreateMap<UpdateArticleDto, Article>()
            //    .ForMember(dest => dest.Id, src => src.MapFrom(x => x.ArticleId));
        }
    }
}
