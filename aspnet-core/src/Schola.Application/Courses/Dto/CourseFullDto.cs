using Newtonsoft.Json;

namespace Schola.Courses.Dto
{
    /// <summary>
    /// course full dto
    /// </summary>
    public class CourseFullDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public int MaxStudents { get; set; }
        public int MinStudents { get; set; }
        public int UserTeacherId { get; set; }
    }
}
 