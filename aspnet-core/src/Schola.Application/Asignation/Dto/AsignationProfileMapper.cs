using AutoMapper;
using Schola.Users.Dto;

namespace Schola.Asignation.Dto
{
    public class AsignationProfileMapper : Profile
    {
         public AsignationProfileMapper()
         {
            CreateMap<Asignations.Asignation, AsignationViewDto>().ReverseMap();
            CreateMap<Asignations.Asignation, AsignationFullDto>().ReverseMap();
         }
    }   
}
