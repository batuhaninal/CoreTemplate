using Application.Models.DTOs.Auths;
using Application.Models.DTOs.Users;
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

            CreateMap<User, UserInfoDto>()
                .ForMember(dest=> dest.UserId, src=> src.MapFrom(x=> x.Id));
        }
    }
}
