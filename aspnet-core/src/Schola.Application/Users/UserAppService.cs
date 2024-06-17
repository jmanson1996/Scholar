using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.IdentityFramework;
using Abp.Linq.Extensions;
using Abp.Localization;
using Abp.Runtime.Session;
using Abp.UI;
using Schola.Authorization;
using Schola.Authorization.Accounts;
using Schola.Authorization.Roles;
using Schola.Authorization.Users;
using Schola.Roles.Dto;
using Schola.Users.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Schola.UserCourses;
using Schola.Assistances;
using Schola.DeliveryAssignments;
using Abp.EntityFrameworkCore.Repositories;

namespace Schola.Users
{
    //[AbpAuthorize(PermissionNames.Pages_Users)]
    public class UserAppService : AsyncCrudAppService<User, UserDto, long, PagedUserResultRequestDto, CreateUserDto, UserDto>, IUserAppService
    {
        private readonly UserManager _userManager;
        private readonly RoleManager _roleManager;
        private readonly IRepository<Role> _roleRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IAbpSession _abpSession;
        private readonly LogInManager _logInManager;
        private readonly IRepository<UserCourse, int> _courseRepository;
        private readonly IRepository<User, long> _repository;
        private readonly IRepository<Assistance> _assistanceRepository;
        private readonly IRepository<DeliveryAssignment> _taskRepository;
        private readonly IRepository<Asignations.Asignation,int> _asignationRepository;
        public UserAppService(
            IRepository<User, long> repository,
            UserManager userManager,
            RoleManager roleManager,
            IRepository<Role> roleRepository,
            IPasswordHasher<User> passwordHasher,
            IRepository<UserCourse, int> courseRepository,
            IRepository<Assistance> assistanceRepository,
            IRepository<DeliveryAssignment> taskRepository,
            IAbpSession abpSession,
            IRepository<Asignations.Asignation, int> asignationRepository,
            LogInManager logInManager)
            : base(repository)
        {
            _userManager = userManager;
            _taskRepository = taskRepository;
            _repository = repository;
            _roleManager = roleManager;
            _assistanceRepository = assistanceRepository;
            _roleRepository = roleRepository;
            _courseRepository = courseRepository;
            _passwordHasher = passwordHasher;
            _abpSession = abpSession;
            _logInManager = logInManager;
            _asignationRepository = asignationRepository;
        }

        public override async Task<UserDto> CreateAsync(CreateUserDto input)
        {
            CheckCreatePermission();

            var user = ObjectMapper.Map<User>(input);

            user.TenantId = AbpSession.TenantId;
            user.IsEmailConfirmed = true;

            await _userManager.InitializeOptionsAsync(AbpSession.TenantId);

            CheckErrors(await _userManager.CreateAsync(user, input.Password));

            if (input.RoleNames != null)
            {
                CheckErrors(await _userManager.SetRolesAsync(user, input.RoleNames));
            }

            CurrentUnitOfWork.SaveChanges();

            return MapToEntityDto(user);
        }

        public override async Task<UserDto> UpdateAsync(UserDto input)
        {
            CheckUpdatePermission();

            var user = await _userManager.GetUserByIdAsync(input.Id);

            MapToEntity(input, user);

            CheckErrors(await _userManager.UpdateAsync(user));

            if (input.RoleNames != null)
            {
                CheckErrors(await _userManager.SetRolesAsync(user, input.RoleNames));
            }

            return await GetAsync(input);
        }

        public override async Task DeleteAsync(EntityDto<long> input)
        {
            var user = await _userManager.GetUserByIdAsync(input.Id);
            await _userManager.DeleteAsync(user);
        }

        [AbpAuthorize(PermissionNames.Pages_Users_Activation)]
        public async Task Activate(EntityDto<long> user)
        {
            await Repository.UpdateAsync(user.Id, async (entity) =>
            {
                entity.IsActive = true;
            });
        }

        [AbpAuthorize(PermissionNames.Pages_Users_Activation)]
        public async Task DeActivate(EntityDto<long> user)
        {
            await Repository.UpdateAsync(user.Id, async (entity) =>
            {
                entity.IsActive = false;
            });
        }

        public async Task<ListResultDto<RoleDto>> GetRoles()
        {
            var roles = await _roleRepository.GetAllListAsync();
            return new ListResultDto<RoleDto>(ObjectMapper.Map<List<RoleDto>>(roles));
        }

        public async Task ChangeLanguage(ChangeUserLanguageDto input)
        {
            await SettingManager.ChangeSettingForUserAsync(
                AbpSession.ToUserIdentifier(),
                LocalizationSettingNames.DefaultLanguage,
                input.LanguageName
            );
        }

        protected override User MapToEntity(CreateUserDto createInput)
        {
            var user = ObjectMapper.Map<User>(createInput);
            user.SetNormalizedNames();
            return user;
        }

        protected override void MapToEntity(UserDto input, User user)
        {
            ObjectMapper.Map(input, user);
            user.SetNormalizedNames();
        }

        protected override UserDto MapToEntityDto(User user)
        {
            var roleIds = user.Roles.Select(x => x.RoleId).ToArray();

            var roles = _roleManager.Roles.Where(r => roleIds.Contains(r.Id)).Select(r => r.NormalizedName);

            var userDto = base.MapToEntityDto(user);
            userDto.RoleNames = roles.ToArray();

            return userDto;
        }

        protected override IQueryable<User> CreateFilteredQuery(PagedUserResultRequestDto input)
        {
            return Repository.GetAllIncluding(x => x.Roles)
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.UserName.Contains(input.Keyword) || x.Name.Contains(input.Keyword) || x.EmailAddress.Contains(input.Keyword))
                .WhereIf(input.IsActive.HasValue, x => x.IsActive == input.IsActive);
        }

        protected override async Task<User> GetEntityByIdAsync(long id)
        {
            var user = await Repository.GetAllIncluding(x => x.Roles).FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                throw new EntityNotFoundException(typeof(User), id);
            }

            return user;
        }

        protected override IQueryable<User> ApplySorting(IQueryable<User> query, PagedUserResultRequestDto input)
        {
            return query.OrderBy(r => r.UserName);
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }

        public async Task<bool> ChangePassword(ChangePasswordDto input)
        {
            await _userManager.InitializeOptionsAsync(AbpSession.TenantId);

            var user = await _userManager.FindByIdAsync(AbpSession.GetUserId().ToString());
            if (user == null)
            {
                throw new Exception("There is no current user!");
            }
            
            if (await _userManager.CheckPasswordAsync(user, input.CurrentPassword))
            {
                CheckErrors(await _userManager.ChangePasswordAsync(user, input.NewPassword));
            }
            else
            {
                CheckErrors(IdentityResult.Failed(new IdentityError
                {
                    Description = "Incorrect password."
                }));
            }

            return true;
        }

        public async Task<bool> ResetPassword(ResetPasswordDto input)
        {
            if (_abpSession.UserId == null)
            {
                throw new UserFriendlyException("Please log in before attempting to reset password.");
            }
            
            var currentUser = await _userManager.GetUserByIdAsync(_abpSession.GetUserId());
            var loginAsync = await _logInManager.LoginAsync(currentUser.UserName, input.AdminPassword, shouldLockout: false);
            if (loginAsync.Result != AbpLoginResultType.Success)
            {
                throw new UserFriendlyException("Your 'Admin Password' did not match the one on record.  Please try again.");
            }
            
            if (currentUser.IsDeleted || !currentUser.IsActive)
            {
                return false;
            }
            
            var roles = await _userManager.GetRolesAsync(currentUser);
            if (!roles.Contains(StaticRoleNames.Tenants.Admin))
            {
                throw new UserFriendlyException("Only administrators may reset passwords.");
            }

            var user = await _userManager.GetUserByIdAsync(input.UserId);
            if (user != null)
            {
                user.Password = _passwordHasher.HashPassword(user, input.NewPassword);
                await CurrentUnitOfWork.SaveChangesAsync();
            }

            return true;
        }

        public async Task<List<UserDto>> GetUsersRoles(int idRole)
        {
            try
            {
                var result = await Repository.GetAllIncluding(x => x.Roles)
                    .Where(x => x.Roles.Any(r => r.RoleId == idRole) && x.IsDeleted == false)
                    .ToListAsync();
                return result.Select(MapToEntityDto).ToList();
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException(ex.Message);
            }
        }

        public async Task<PagedResultDto<UserDto>> getStudentCourse(PagedUserResultRequestDto input, int idCourse)
        {
            //CheckGetAllPermission();

            var query = CreateFilteredQuery(input);

            var totalCount = await AsyncQueryableExecuter.CountAsync(query);

            query = ApplySorting(query, input);
            query = ApplyPaging(query, input);

            var entities = await AsyncQueryableExecuter.ToListAsync(query);
            var studentCourse = _courseRepository.GetAllList(x => x.IdCourse == idCourse);
            entities = entities.Where(x => studentCourse.Select(x => x.IdUser).Contains(x.Id)).ToList();

            return new PagedResultDto<UserDto>(
                totalCount,
                entities.Select(MapToEntityDto).ToList()
            );
        }

        public async Task<PagedResultDto<AssistanceFullViewDtoFE>> getStudentCourseNotFilter(int idCourse)
        {
            try
            {
                var response = new List<AssistanceFullViewDtoFE>();
                CheckGetAllPermission();
                var entities = await _repository.GetAllListAsync();
                var studentCourse = _courseRepository.GetAllList(x => x.IdCourse == idCourse);
                entities = entities.Where(x => studentCourse.Select(x => x.IdUser).Contains(x.Id)).ToList();
                var total = entities.Count();
                foreach (var entity in entities)
                {
                    var entitity = new AssistanceFullViewDtoFE
                    {
                        Id = entity.Id,
                        fullName = entity.Name + " " + entity.Surname,
                        UserStudentId = entity.Id,
                        CourseId = idCourse,
                        Date = null,
                    };
                    response.Add(entitity);
                }
                return new PagedResultDto<AssistanceFullViewDtoFE>(
                    total,
                    response
                );
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }

        public async Task<PagedResultDto<DeliveryAssignmentFullViewDtoFE>> getStudentPresentTask(int idCourse, int idTask)
        {
            try
            {
                var response = new List<DeliveryAssignmentFullViewDtoFE>();
                CheckGetAllPermission();
                var entities = await _repository.GetAllListAsync();
                var studentCourse = _courseRepository.GetAllList(x => x.IdCourse == idCourse);
                entities = entities.Where(x => studentCourse.Select(x => x.IdUser).Contains(x.Id)).ToList();
                var total = entities.Count();
                foreach (var entity in entities)
                {
                    var nota = await _taskRepository.FirstOrDefaultAsync(x => x.UserStudentId == entity.Id && x.AsignationId == idTask);
                    if (nota != null)
                    {
                        var entitity = new DeliveryAssignmentFullViewDtoFE
                        {
                            fullName = entity.Name + " " + entity.Surname,
                            UserStudentId = entity.Id,
                            AsignationId = idTask,
                            Qualification = nota.Qualification,
                            Comment = nota.Comment,
                            status = nota.status
                        };
                        response.Add(entitity);
                    }
                    else
                    {
                        var entitity = new DeliveryAssignmentFullViewDtoFE
                        {
                            fullName = entity.Name + " " + entity.Surname,
                            UserStudentId = entity.Id,
                            AsignationId = idTask,
                            Qualification = 1,
                            Comment = "",
                            status = false
                        };
                        response.Add(entitity);
                    }

                }
                return new PagedResultDto<DeliveryAssignmentFullViewDtoFE>(
                    total,
                    response
                );
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }

        public async Task deleteStudentPresentTask(int idTask)
        {
            try
            {
                var response = new List<DeliveryAssignmentFullViewDtoFE>();
                CheckGetAllPermission();
                var task = await _asignationRepository.FirstOrDefaultAsync(x => x.Id == idTask);
                var delivery = await _taskRepository.GetAllListAsync(x => x.AsignationId == idTask);
                await _asignationRepository.DeleteAsync(task);
                foreach (var item in delivery)
                {
                    await _taskRepository.DeleteAsync(item);
                }
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }

        public async Task<PagedResultDto<AssistanceFullViewDtoFE>> GetStudentCourseAssistence(int idCourse, DateTime date)
        {
            try
            {
                CheckGetAllPermission();
                var response = new List<AssistanceFullViewDtoFE>();
                var assistances = await _assistanceRepository.GetAllIncludingAsync(x => x.User);

                var listAssistances = await assistances
                    .Where(x => x.CourseId == idCourse && x.Date.Date == date.Date)
                    .ToListAsync();

                foreach (var item in listAssistances)
                {
                    var entity = new AssistanceFullViewDtoFE
                    {
                        Id = item.Id,
                        fullName = item.User.Name + " " + item.User.Surname,
                        UserStudentId = item.User.Id,
                        CourseId = item.CourseId,
                        Date = item.Date,
                        isPresent = item.isPresent
                    };
                    response.Add(entity);
                }

                var total = response.Count;
                return new PagedResultDto<AssistanceFullViewDtoFE>(
                    total,
                    response
                );
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }


    }
}

