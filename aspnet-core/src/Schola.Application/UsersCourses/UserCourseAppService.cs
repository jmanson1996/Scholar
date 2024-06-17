using Abp;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Localization;
using Abp.Runtime.Validation;
using Abp.UI;
using Microsoft.AspNetCore.Http.HttpResults;
using Schola.Authorization.Users;
using Schola.UserCourses;
using Schola.Users.Dto;
using Schola.UsersCourses.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Schola.UsersCourses
{
    public class UserCourseAppService : AsyncCrudAppService<UserCourse, UserCourseViewDto, int, PagedResultRequestDto, UserCourseFullDto, UserCourseViewDto>, IUserCourseAppService
    {
        private readonly IRepository<UserCourse, int> _courseRepository;
        private readonly IRepository<User, long> _userRepository;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repository"></param>
        public UserCourseAppService(IRepository<UserCourse, int> repository, IRepository<User, long> userRepository)
            : base(repository)
        {
            _courseRepository = repository;
            _userRepository = userRepository;
            _userRepository = userRepository;
            LocalizationSourceName = ScholaConsts.LocalizationSourceName;
        }

        public override async Task<UserCourseViewDto> CreateAsync(UserCourseFullDto input)
        {
            //try
            //{
                List<ValidationResult> validationResults = new List<ValidationResult>();

                var existingUserCourse = await Repository.FirstOrDefaultAsync(x => x.IdUser == input.IdUser && x.IdCourse == input.IdCourse);
                if (existingUserCourse != null)
                {
                    validationResults.Add(new ValidationResult(L("StudentAlreadyExists")));
                }
                if (validationResults.Count() > 0)
                {
                    throw new AbpValidationException(null, validationResults);
                }
                CheckCreatePermission();

                var entity = MapToEntity(input);

                await Repository.InsertAsync(entity);
                await CurrentUnitOfWork.SaveChangesAsync();

                return MapToEntityDto(entity);
            //}
            //catch (Exception ex)
            //{
            //    throw new UserFriendlyException(ex.Message);
            //}
        }

        public async Task<UserCourseViewDto> DeleteCourseStudentByPk(int courseId, int idUser)
        {
            var existingUserCourse = await Repository.FirstOrDefaultAsync(x => x.IdUser == idUser && x.IdCourse == courseId);
            if (existingUserCourse == null)
            {
                throw new EntityNotFoundException($"Not Found!");
            }
            await Repository.DeleteAsync(existingUserCourse);
            return MapToEntityDto(existingUserCourse);
        }
    }
}
