using Abp.Application.Services.Dto;

namespace Schola.Courses.Dto
{
    public class PagedCourseResultRequestDto : PagedResultRequestDto
    {
        public long idUser { get; set; }
    }
}
