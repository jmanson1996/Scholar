using Abp.Application.Services.Dto;

namespace Schola.DeliveryAssignments.Dto
{
    public class PagedDeliveryAssignmentResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}
