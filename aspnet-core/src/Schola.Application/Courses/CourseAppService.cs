using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.UI;
using Schola.Courses.Dto;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Schola.Courses
{
    /// <summary>
    /// CourseAppService class
    /// </summary>
    public class CourseAppService : AsyncCrudAppService<Course, CourseViewDto, int, PagedResultRequestDto, CourseFullDto, CourseViewDto>, ICourseAppService
    {
        private readonly IRepository<Course, int> _courseRepository;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repository"></param>
        public CourseAppService(IRepository<Course, int> repository)
            : base(repository)
        {
            _courseRepository = repository;
        }

        public override async Task<CourseViewDto> CreateAsync(CourseFullDto input)
        {
            try
            {
                CheckCreatePermission();

                var entity = MapToEntity(input);

                await Repository.InsertAsync(entity);
                await CurrentUnitOfWork.SaveChangesAsync();

                return MapToEntityDto(entity);
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException(ex.Message);
            }
        }

        public override async Task<PagedResultDto<CourseViewDto>> GetAllAsync(PagedResultRequestDto input)
        {
            try
            {
                CheckGetAllPermission();

                var query = CreateFilteredQuery(input);

                var totalCount = await AsyncQueryableExecuter.CountAsync(query);

                query = ApplySorting(query, input);
                query = ApplyPaging(query, input);

                var entities = await AsyncQueryableExecuter.ToListAsync(query);

                return new PagedResultDto<CourseViewDto>(
                    totalCount,
                    entities.Select(MapToEntityDto).ToList()
                );
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException(ex.Message);
            }
        }

        public async Task<PagedResultDto<CourseViewDto>> GetAllByUserAsync(PagedCourseResultRequestDto input)
        {
            try
            {

                CheckGetAllPermission();

                var query = CreateFilteredQuery(input);

                var totalCount = await _courseRepository.CountAsync(x => x.UserTeacherId == input.idUser);

                query = ApplySorting(query, input);
                query = ApplyPaging(query, input);

                var entities = await AsyncQueryableExecuter.ToListAsync(query);
                entities = entities.Where(x => x.UserTeacherId == input.idUser).ToList();
                return new PagedResultDto<CourseViewDto>(
                    totalCount,
                    entities.Select(MapToEntityDto).ToList()
                );
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException(ex.Message);
            }
        }

        public override Task DeleteAsync(EntityDto<int> input)
        {
            try
            {
                CheckDeletePermission();
                return Repository.DeleteAsync(input.Id);
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException(ex.Message);
            }

        }
    }
}
