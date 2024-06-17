using Abp.Application.Services.Dto;

namespace Schola.UsersCourses.Dto
{
    public class UserCourseViewDto : EntityDto
    {
        public long IdUser { get; set; }
        public int IdCourse { get; set; }
    }
}
