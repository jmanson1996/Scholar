using AutoMapper;
using Schola.Users.Dto;
using System;

namespace Schola.Assistances.Dto
{
    public class AssistanceProfileMapper : Profile
    {
        public AssistanceProfileMapper()
        {
            CreateMap<Assistance, AssistanceViewDto>().ReverseMap();
            CreateMap<Assistance, AssistanceFullDto>().ReverseMap();
            CreateMap<AssistanceFullViewDtoFE, Assistance>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateTime.Now))
                .ReverseMap();
        }
    }
}
