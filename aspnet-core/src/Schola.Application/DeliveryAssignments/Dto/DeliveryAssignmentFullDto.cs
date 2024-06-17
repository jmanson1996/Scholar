using Schola.Authorization.Users;

namespace Schola.DeliveryAssignments.Dto
{
    public class DeliveryAssignmentFullDto
    {
        public long UserStudentId { get; set; }
        public int AsignationId { get; set; }
        public bool isPresent { get; set; } = false;
        public double Qualification { get; set; }
        public string Comment { get; set; }
        public bool status { get; set; }
    }
}
