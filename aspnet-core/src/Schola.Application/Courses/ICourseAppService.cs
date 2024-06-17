using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Schola.Courses.Dto;
using System.Threading.Tasks;

namespace Schola.Courses
{
    /// <summary>
    /// Interface for the CourseAppService
    /// </summary>
    public interface ICourseAppService : IAsyncCrudAppService<CourseViewDto, int, PagedResultRequestDto, CourseFullDto, CourseViewDto>
    {
        Task<PagedResultDto<CourseViewDto>> GetAllByUserAsync(PagedCourseResultRequestDto input);
    }
}
