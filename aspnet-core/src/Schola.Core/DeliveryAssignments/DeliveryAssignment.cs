using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Schola.Asignations;
using Schola.Authorization.Users;

namespace Schola.DeliveryAssignments
{
    public class DeliveryAssignment : FullAuditedAggregateRoot<int>, ISoftDelete
    {
        public long UserStudentId { get; set; }
        public User UserStudent { get; set; }
        public int AsignationId { get; set; }
        public Asignation Asignation { get; set; }
        public bool isPresent { get; set; } = false;
        public double Qualification { get; set; }
        public string Comment { get; set; }
        public bool status { get; set; }
    }
}
