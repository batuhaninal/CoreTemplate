using Application.Models.DTOs.Categories;
using Application.Models.DTOs.Commons.Results;
using AutoMapper;
using Domain.Entities;

namespace Application.Utilities.MappingProfiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CreateCategoryDto, Category>();

            CreateMap<UpdateCategoryDto, Category>()
                .ForMember(dest => dest.Id, src => src.MapFrom(x => x.CategoryId))
                .ForMember(dest => dest.UpdatedDate, src => src.MapFrom(x => DateTime.UtcNow));

            //CreateMap<Category, CategoryItemDto>();

            CreateMap<Category, CategoryItemDto>()
                .ForMember(dest => dest.CategoryId, src => src.MapFrom(x => x.Id));

            CreateMap<Category, CategoryToolDto>()
                .ForMember(dest => dest.CategoryId, src => src.MapFrom(x => x.Id));

            CreateMap<Category, CategoryInfoDto>()
                .ForMember(dest=> dest.CategoryId, src=> src.MapFrom(x=> x.Id));

            CreateMap<Category, SearchCategoryDto>()
                .ForMember(dest => dest.CategoryId, src => src.MapFrom(x => x.Id));

            CreateMap<Category, ParentCategoryItemDto>()
                .ForMember(dest => dest.CategoryId, src => src.MapFrom(x => x.Id));

            CreateMap<Category, ChildrenCategoryItemDto>()
                .ForMember(dest => dest.CategoryId, src => src.MapFrom(x => x.Id));

            CreateMap<PaginatedListDto<Category>, PaginatedListDto<SearchCategoryDto>>();
            CreateMap<PaginatedListDto<Category>, PaginatedListDto<CategoryToolDto>>();
        }
    }
}
