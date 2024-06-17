using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Schola.Authorization.Users;
using Schola.Courses;
using Schola.DeliveryAssignments;
using System;
using System.Collections.Generic;

namespace Schola.Asignations
{
    public class Asignation : FullAuditedAggregateRoot<int>, ISoftDelete
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public long? UserStudentId { get; set; }
        public User User { get; set; }
        public virtual ICollection<DeliveryAssignment> DeliveryAssignment { get; set; } = new List<DeliveryAssignment>();
    }
}
