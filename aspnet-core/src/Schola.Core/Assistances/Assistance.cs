using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Schola.Authorization.Users;
using Schola.Courses;
using System;

namespace Schola.Assistances
{
    public class Assistance : FullAuditedAggregateRoot<int>, ISoftDelete
    {
        public DateTime Date { get; set; }
        public long UserStudentId { get; set; }
        public int CourseId { get; set; }
        public bool isPresent { get; set; } = false;
        public Course Course { get; set; }
        public User User { get; set; }
    }
}
