﻿using Application.Models.DTOs.Categories;
using AutoMapper;
using Domain.Entities;

namespace Application.MappingProfiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CreateCategoryDto, Category>();

            CreateMap<UpdateCategoryDto, Category>()
                .ForMember(dest => dest.Id, src => src.MapFrom(x => x.CategoryId));

            //CreateMap<Category, CategoryItemDto>();

            CreateMap<Category, CategoryItemDto>()
                .ConstructUsing(x=> new CategoryItemDto(x.Id.ToString(), x.Title));
        }
    }
}
