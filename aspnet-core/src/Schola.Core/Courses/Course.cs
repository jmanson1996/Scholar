using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Schola.Asignations;
using Schola.Assistances;
using Schola.Authorization.Users;
using Schola.Comments;
using Schola.Materiales;
using Schola.UserCourses;
using System.Collections.Generic;

namespace Schola.Courses
{
    public class Course : FullAuditedAggregateRoot<int>, ISoftDelete
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public int MaxStudents { get; set; }
        public int MinStudents { get; set; }
        public long UserTeacherId { get; set; }
        public User User { get; set; }
        public virtual ICollection<Asignation> Asignation { get; set; } = new List<Asignation>();
        public virtual ICollection<Material> Material { get; set; } = new List<Material>();
        public virtual ICollection<Comment> Comment { get; set; } = new List<Comment>();
        public virtual ICollection<Assistance> Assistance { get; set; } = new List<Assistance>();
        public virtual ICollection<UserCourse> UserCourse { get; set; } = new List<UserCourse>();
    }
}
