using Application.Models.DTOs.Auths;
using Application.Models.DTOs.Users;
using AutoMapper;
using Domain.Entities.Identities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
