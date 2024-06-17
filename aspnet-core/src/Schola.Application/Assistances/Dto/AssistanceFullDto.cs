using Schola.Authorization.Users;
using Schola.Courses;
using System;

namespace Schola.Assistances.Dto
{
    public class AssistanceFullDto
    {
        public DateTime Date { get; set; }
        public long UserStudentId { get; set; }
        public int CourseId { get; set; }
        public bool isPresent { get; set; } = false;
    }
}
