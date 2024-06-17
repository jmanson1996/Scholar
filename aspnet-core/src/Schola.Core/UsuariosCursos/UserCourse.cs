using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Schola.Authorization.Users;
using Schola.Courses;

namespace Schola.UserCourses
{
    public class UserCourse : FullAuditedAggregateRoot<int>, ISoftDelete
    {
        public long IdUser { get; set; }
        public User User { get; set; }
        public int IdCourse { get; set; }
        public Course Curso { get; set; }
    }
}
 