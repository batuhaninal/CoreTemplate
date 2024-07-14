using Application.Models.DTOs.Writers;
using Application.Models.Enums;
using AutoMapper;
using Domain.Entities;

namespace Application.Utilities.MappingProfiles
{
    public class WriterProfile : Profile
    {
        public WriterProfile()
        {
            CreateMap<Writer, WriterInfoDto>()
                .ForMember(dest => dest.WriterId, src => src.MapFrom(x => x.Id))
                .ForMember(dest => dest.Level, src => src.MapFrom(x => (WriterLevel)x.Level));
        }
    }
}
