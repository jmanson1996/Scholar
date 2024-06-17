using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Schola.Users.Dto;
using Schola.UsersCourses.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Schola.UsersCourses
{
    public interface IUserCourseAppService : IAsyncCrudAppService<UserCourseViewDto, int, PagedResultRequestDto, UserCourseFullDto, UserCourseViewDto>
    {
        Task<UserCourseViewDto> DeleteCourseStudentByPk(int courseId, int idUser);
    }
}
