using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.EntityFrameworkCore.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.Runtime.Validation;
using Abp.UI;
using Microsoft.AspNetCore.Mvc;
using Schola.DeliveryAssignments.Dto;
using Schola.Users.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Schola.DeliveryAssignments
{
    public class DeliveryAssignmentAppService : AsyncCrudAppService<DeliveryAssignments.DeliveryAssignment, DeliveryAssignmentViewDto, int, PagedDeliveryAssignmentResultRequestDto, DeliveryAssignmentFullDto, DeliveryAssignmentViewDto>, IDeliveryAssignmentAppService
    {
        private readonly IRepository<DeliveryAssignments.DeliveryAssignment, int> _asignationRepository;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repository"></param>
        public DeliveryAssignmentAppService(IRepository<DeliveryAssignments.DeliveryAssignment, int> repository)
            : base(repository)
        {
            _asignationRepository = repository;
        }

        [HttpPost]
        public async Task<PagedResultDto<DeliveryAssignmentFullViewDtoFE>> createOrUpdateAssistance(List<DeliveryAssignmentFullViewDtoFE> list)
        {
            CheckCreatePermission();
            List<ValidationResult> validationResults = new List<ValidationResult>();
            if (list.Count() > 0)
            {
                foreach (var item in list)
                {
                    var existingAssistance = _asignationRepository.FirstOrDefault(x => x.UserStudentId == item.UserStudentId && x.AsignationId == item.AsignationId);
                    if (existingAssistance != null)
                    {
                        existingAssistance.Qualification = item.Qualification;
                        existingAssistance.Comment = item.Comment;
                        existingAssistance.status = item.status;
                        await _asignationRepository.UpdateAsync(existingAssistance);
                    }
                    else
                    {
                        await _asignationRepository.InsertAsync(ObjectMapper.Map<DeliveryAssignment>(item));
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
            return new PagedResultDto<DeliveryAssignmentFullViewDtoFE>(
                    list.Count,
                    list
                );
        }
        public override async Task<PagedResultDto<DeliveryAssignmentViewDto>> GetAllAsync(PagedDeliveryAssignmentResultRequestDto input)
        {
            try
            {
                CheckGetAllPermission();

                var query = CreateFilteredQuery(input);

                var totalCount = await AsyncQueryableExecuter.CountAsync(query);

                query = ApplySorting(query, input);
                query = ApplyPaging(query, input);

                var entities = await AsyncQueryableExecuter.ToListAsync(query);

                return new PagedResultDto<DeliveryAssignmentViewDto>(
                    totalCount,
                    entities.Select(MapToEntityDto).ToList()
                );
            }
            catch(Exception ex)
            {
                throw new UserFriendlyException(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
            
        }

        protected override IQueryable<DeliveryAssignments.DeliveryAssignment> CreateFilteredQuery(PagedDeliveryAssignmentResultRequestDto input)
        {
            return Repository.GetAllIncluding(x => x.UserStudent, x=> x.Asignation)
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.UserStudent.Name.ToString().Contains(input.Keyword) || x.UserStudent.Surname.ToString().Contains(input.Keyword) || x.Qualification.ToString().Contains(input.Keyword));
        }

        protected override IQueryable<DeliveryAssignments.DeliveryAssignment> ApplySorting(IQueryable<DeliveryAssignments.DeliveryAssignment> query, PagedDeliveryAssignmentResultRequestDto input)
        {
            return query.OrderByDescending(r => r.UserStudent.Name);
        }
    }
}
