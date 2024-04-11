using AutoMapper;
using ET.Domain.Entities;
using ET.Web.Models;

namespace ET.Web.Core
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<SedeOlimpica, SedeOlimpicaModel>().ReverseMap();
            CreateMap<ComplejoDeportivo, ComplejoDeportivoModel>()
            .ForMember(dest => dest.SedeOlimpicaModel, opt => opt.MapFrom(src => src.SedeOlimpica))
            .ReverseMap();
        }
    }
}
