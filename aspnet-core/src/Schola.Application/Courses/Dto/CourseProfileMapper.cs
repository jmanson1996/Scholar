using AutoMapper;

namespace Schola.Courses.Dto
{
    /// <summary>
    /// CourseProfileMapper class
    /// </summary>
    public class CourseProfileMapper : Profile
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public CourseProfileMapper()
        {
            CreateMap<Course, CourseViewDto>().ReverseMap();
            CreateMap<Course, CourseFullDto>().ReverseMap();
        }
    }
}
