using Abp.Application.Services;
using Schola.DeliveryAssignments.Dto;

namespace Schola.DeliveryAssignments
{
    public interface IDeliveryAssignmentAppService : IAsyncCrudAppService<DeliveryAssignmentViewDto, int, PagedDeliveryAssignmentResultRequestDto, DeliveryAssignmentFullDto, DeliveryAssignmentViewDto>
    {
    }
}
