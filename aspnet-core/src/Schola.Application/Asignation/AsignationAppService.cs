using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.EntityFrameworkCore.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.Runtime.Validation;
using Schola.Asignation.Dto;
using Schola.Assistances;
using Schola.Users.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Schola.Asignation
{
    public class AsignationAppService : AsyncCrudAppService<Asignations.Asignation, AsignationViewDto, int, PagedAsignationResultRequestDto, AsignationFullDto, AsignationViewDto>, IAsignationAppService
    {
        private readonly IRepository<Asignations.Asignation, int> _asignationRepository;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repository"></param>
        public AsignationAppService(IRepository<Asignations.Asignation, int> repository)
            : base(repository)
        {
            _asignationRepository = repository;
        }

        

        protected override IQueryable<Asignations.Asignation> CreateFilteredQuery(PagedAsignationResultRequestDto input)
        {
            return Repository.GetAllIncluding(x => x.Course)
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.DeliveryDate.Date.ToString().Contains(input.Keyword) || x.Description.Contains(input.Keyword) || x.Title.Contains(input.Keyword))
                .WhereIf(input.CourseId > 0, x => x.CourseId == input.CourseId);
        }

        protected override IQueryable<Asignations.Asignation> ApplySorting(IQueryable<Asignations.Asignation> query, PagedAsignationResultRequestDto input)
        {
            return query.OrderByDescending(r => r.DeliveryDate.Date);
        }
    }
}
