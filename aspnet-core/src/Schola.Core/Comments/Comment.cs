using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Schola.Authorization.Users;
using Schola.Courses;
using System;

namespace Schola.Comments
{
    public class Comment : FullAuditedAggregateRoot<int>, ISoftDelete
    {
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public long UserCommentId { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public User User { get; set; }
    }
}
