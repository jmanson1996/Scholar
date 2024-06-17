using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Schola.Material.Dto;

namespace Schola.Material
{
    public interface IMaterialAppService : IAsyncCrudAppService<MaterialViewDto, int, PagedResultRequestDto, MaterialFullDto, MaterialViewDto>
    {
    }
}
