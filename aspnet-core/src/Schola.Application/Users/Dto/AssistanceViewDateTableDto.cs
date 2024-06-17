using System;

namespace Schola.Users.Dto
{
    public class AssistanceViewDateTableDto
    {
        public DateTime Date { get; set; }
        public int CourseId { get; set; }
        public int totalAssistance { get; set; }
        public int totalMissing { get; set; }
    }
}
