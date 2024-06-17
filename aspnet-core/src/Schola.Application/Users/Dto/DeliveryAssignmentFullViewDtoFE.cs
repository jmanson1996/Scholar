using Abp.Application.Services.Dto;

namespace Schola.Users.Dto
{
    public class DeliveryAssignmentFullViewDtoFE 
    {
        public string fullName { get; set; }
        public long? UserStudentId { get; set; }
        public int AsignationId { get; set; }
        public double Qualification { get; set; }
        public string Comment { get; set; }
        public bool status { get; set; } = false;
    }
}
