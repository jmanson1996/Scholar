using Abp.Application.Services.Dto;

namespace Schola.DeliveryAssignments.Dto
{
    public class DeliveryAssignmentViewDto : EntityDto
    {
        public long UserStudentId { get; set; }
        public int AsignationId { get; set; }
        public bool isPresent { get; set; } = false;
        public double Qualification { get; set; }
        public string Comment { get; set; }
        public bool status { get; set; }
    }
}
