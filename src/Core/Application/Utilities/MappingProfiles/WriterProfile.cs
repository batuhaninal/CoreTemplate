using Application.Models.DTOs.Auths;
using Application.Models.DTOs.Writers;
using Application.Models.Enums;
using Application.Utilities.Helpers;
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
                .ForMember(dest => dest.Level, src => src.MapFrom(x => EnumConverters.ConvertWriterLevelToString((WriterLevel)x.Level)));

            CreateMap<Writer, WriterItemDto>()
                .ForMember(dest => dest.WriterId, src => src.MapFrom(x => x.Id))
                .ForMember(dest => dest.Level, src => src.MapFrom(x => EnumConverters.ConvertWriterLevelToString((WriterLevel)x.Level)));

            CreateMap<RegisterWriterDto, RegisterDto>();

            CreateMap<RegisterWriterDto, CreateWriterDto>();

            CreateMap<RegisterWriterDto, Writer>()
                .ForMember(dest => dest.Level, src => src.MapFrom(x => (byte)WriterLevel.Newbie));

            CreateMap<CreateWriterDto, Writer>()
                .ForMember(dest=> dest.Level, src=> src.MapFrom(x=> (byte)WriterLevel.Newbie));

        }
    }
}
