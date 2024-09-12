using Application.Models.DTOs.Auths;
using Application.Models.DTOs.Commons.Results;
using Application.Models.DTOs.Users;
using Application.Models.DTOs.Writers;
using AutoMapper;
using Domain.Entities;

namespace Application.Utilities.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UpdateUserDto, User>()
                .ForMember(dest => dest.Id, src => src.MapFrom(x => x.UserId));

            CreateMap<RegisterDto, User>();

            CreateMap<RegisterWriterDto, User>();

            CreateMap<User, UserInfoDto>()
                .ForMember(dest=> dest.UserId, src=> src.MapFrom(x=> x.Id));

            CreateMap<User, UserItemDto>()
                .ForMember(dest => dest.UserId, src => src.MapFrom(x => x.Id))
                .ForMember(dest => dest.FullName, src => src.MapFrom(x => string.Join(' ', x.FirstName, x.LastName)));

            CreateMap<SecuredUserDto, SearchUserDto>()
                .ForMember(dest => dest.UserId, src => src.MapFrom(x => x.Id));

            CreateMap<PaginatedListDto<SecuredUserDto>, PaginatedListDto<SearchUserDto>>();
        }
    }
}
