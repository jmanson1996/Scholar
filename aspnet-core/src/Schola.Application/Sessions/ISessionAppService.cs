using System.Threading.Tasks;
using Abp.Application.Services;
using Schola.Sessions.Dto;

namespace Schola.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
