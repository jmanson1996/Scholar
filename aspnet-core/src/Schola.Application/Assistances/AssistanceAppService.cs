using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.EntityFrameworkCore.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.Runtime.Validation;
using Microsoft.AspNetCore.Mvc;
using Schola.Assistances.Dto;
using Schola.Users.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace Schola.Assistances
{
    public class AssistanceAppService : AsyncCrudAppService<Assistance, AssistanceViewDto, int, PagedAssistenceResultRequestDto, AssistanceFullDto, AssistanceViewDto>, IAssistanceAppService
    {
        private readonly IRepository<Assistance, int> _courseRepository;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repository"></param>
        public AssistanceAppService(IRepository<Assistance, int> repository)
            : base(repository)
        {
            _courseRepository = repository;
        }

        public async Task<PagedResultDto<AssistanceFullViewDtoFE>> createAssistance(List<AssistanceFullViewDtoFE> list)
        {
            CheckCreatePermission();
            List<ValidationResult> validationResults = new List<ValidationResult>();
            if (list.Count() > 0)
            {
                var today = DateTime.Today;
                var existingAssistances = await _courseRepository.GetAllListAsync(a => a.Date.Date == today && a.CourseId == list[0].CourseId);
                if (existingAssistances.Any())
                {
                    validationResults.Add(new ValidationResult("Attendance has already been passed for today."));
                }
                await _courseRepository.InsertRangeAsync(ObjectMapper.Map<List<Assistance>>(list));
            }
            else
            {
                validationResults.Add(new ValidationResult("No Students yet, add them and try again.."));
            }
            
            if (validationResults.Count() > 0)
            {
                throw new AbpValidationException(null, validationResults);
            }
            return new PagedResultDto<AssistanceFullViewDtoFE>(
                    list.Count,
                    list
                );
        }

        [HttpPut]
        public async Task<PagedResultDto<AssistanceFullViewDtoFE>> updateAssistance(List<AssistanceFullViewDtoFE> list)
        {
            CheckCreatePermission();
            List<ValidationResult> validationResults = new List<ValidationResult>();
            if (list.Count() > 0)
            {
                foreach (var item in list)
                {
                    var existingAssistance = await _courseRepository.FirstOrDefaultAsync(x => x.Date.Date == item.Date.Value.Date 
                                                                                           && x.UserStudentId == item.UserStudentId
                                                                                           && x.CourseId == item.CourseId);
                    existingAssistance.isPresent = item.isPresent;
                    if (existingAssistance != null)
                    {
                        await _courseRepository.UpdateAsync(ObjectMapper.Map<Assistance>(existingAssistance));
                    }
                }
            }
            else
            {
                validationResults.Add(new ValidationResult("No Students yet, add them and try again.."));
            }

            if (validationResults.Count() > 0)
            {
                throw new AbpValidationException(null, validationResults);
            }
            return new PagedResultDto<AssistanceFullViewDtoFE>(
                    list.Count,
                    list
                );
        }

        public async Task<PagedResultDto<AssistanceViewDateTableDto>> getViewAssistance(PagedAssistenceResultRequestDto input)
        {
            CheckGetAllPermission();
            List<AssistanceViewDateTableDto> response = new List<AssistanceViewDateTableDto>();
            var query = CreateFilteredQuery(input);
            var totalCount = await AsyncQueryableExecuter.CountAsync(query);
            query = ApplySorting(query, input);
            query = ApplyPaging(query, input);

            var entities = await AsyncQueryableExecuter.ToListAsync(query);
            //entities = entities.Where(x => x.CourseId == idCourse).ToList();

            var tableDate = entities.Select(x => x.Date.Date).Distinct();

            foreach (var item in tableDate)
            {
                AssistanceViewDateTableDto entity = new AssistanceViewDateTableDto();
                entity.Date = item.Date;
                entity.CourseId = input.CourseId;
                entity.totalAssistance = entities.Where(x => x.isPresent == true && x.Date.Date == item.Date).Count();
                entity.totalMissing = entities.Where(x => x.isPresent == false && x.Date.Date == item.Date).Count();
                response.Add(entity);
            }

            return new PagedResultDto<AssistanceViewDateTableDto>(
                totalCount,
                response
            );
        }

        protected override IQueryable<Assistance> CreateFilteredQuery(PagedAssistenceResultRequestDto input)
        {
            return Repository.GetAllIncluding(x => x.Course)
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.Date.Date.ToString().Contains(input.Keyword))
                .WhereIf(input.CourseId > 0, x => x.CourseId == input.CourseId);
        }

        protected override IQueryable<Assistance> ApplySorting(IQueryable<Assistance> query, PagedAssistenceResultRequestDto input)
        {
            return query.OrderByDescending(r => r.Date.Date);
        }
    }
}
