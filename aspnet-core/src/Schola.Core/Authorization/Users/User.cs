using System;
using System.Collections.Generic;
using Abp.Authorization.Users;
using Abp.Extensions;
using Schola.Asignations;
using Schola.Assistances;
using Schola.Comments;
using Schola.Courses;
using Schola.DeliveryAssignments;
using Schola.UserCourses;

namespace Schola.Authorization.Users
{
    public class User : AbpUser<User>
    {
        public const string DefaultPassword = "123qwe";

        public static string CreateRandomPassword()
        {
            return Guid.NewGuid().ToString("N").Truncate(16);
        }

        public static User CreateTenantAdminUser(int tenantId, string emailAddress)
        {
            var user = new User
            {
                TenantId = tenantId,
                UserName = AdminUserName,
                Name = AdminUserName,
                Surname = AdminUserName,
                EmailAddress = emailAddress,
                Roles = new List<UserRole>()
            };

            user.SetNormalizedNames();

            return user;
        }
        public virtual ICollection<Course> Course { get; set; } = new List<Course>();
        public virtual ICollection<Asignation> Asignation { get; set; } = new List<Asignation>();
        public virtual ICollection<Comment> Comment { get; set; } = new List<Comment>();
        public virtual ICollection<Assistance> Assistance { get; set; } = new List<Assistance>();
        public virtual ICollection<UserCourse> UserCourse { get; set; } = new List<UserCourse>();
        public virtual ICollection<DeliveryAssignment> DeliveryAssignment { get; set; } = new List<DeliveryAssignment>();
    }
}
