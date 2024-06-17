using AutoMapper;
using Schola.UserCourses;

namespace Schola.UsersCourses.Dto
{
    public class UserCourseProfileMapper : Profile
    {
        public UserCourseProfileMapper()
        {
            CreateMap<UserCourse, UserCourseViewDto>().ReverseMap();
            CreateMap<UserCourse, UserCourseFullDto>().ReverseMap();
        }
    }
}
