using AutoMapper;
using Schola.Users.Dto;

namespace Schola.DeliveryAssignments.Dto
{
    public class DeliveryAssignmentProfileMapper : Profile
    {
        public DeliveryAssignmentProfileMapper()
        {
            CreateMap<DeliveryAssignment, DeliveryAssignmentFullDto>();
            CreateMap<DeliveryAssignmentFullDto, DeliveryAssignment>();
            CreateMap<DeliveryAssignment, DeliveryAssignmentViewDto>();
            CreateMap<DeliveryAssignmentViewDto, DeliveryAssignment>();
            CreateMap<DeliveryAssignment, DeliveryAssignmentFullViewDtoFE>().ReverseMap();
        }
    }
}
