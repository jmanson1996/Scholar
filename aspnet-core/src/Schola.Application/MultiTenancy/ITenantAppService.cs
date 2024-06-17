using Abp.Application.Services;
using Schola.MultiTenancy.Dto;

namespace Schola.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

