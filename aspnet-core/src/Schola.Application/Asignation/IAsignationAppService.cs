using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Schola.Asignation.Dto;

namespace Schola.Asignation
{
    public interface IAsignationAppService : IAsyncCrudAppService<AsignationViewDto, int, PagedAsignationResultRequestDto, AsignationFullDto, AsignationViewDto>
    {
    }
}
