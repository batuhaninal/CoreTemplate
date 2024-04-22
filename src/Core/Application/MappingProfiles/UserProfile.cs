using Application.Models.DTOs.Users;
using AutoMapper;
using Domain.Entities.Identities;

namespace Application.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UpdateUserDto, User>()
                .ForMember(dest => dest.Id, src => src.MapFrom(x => x.UserId));
        }
    }
}
