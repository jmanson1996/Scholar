using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Schola.Assistances.Dto;
using Schola.Users.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Schola.Assistances
{
    public interface IAssistanceAppService : IAsyncCrudAppService<AssistanceViewDto, int, PagedAssistenceResultRequestDto, AssistanceFullDto, AssistanceViewDto>
    {
        Task<PagedResultDto<AssistanceViewDateTableDto>> getViewAssistance(PagedAssistenceResultRequestDto input);
    }
}
