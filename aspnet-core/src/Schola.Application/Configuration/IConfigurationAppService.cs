using System.Threading.Tasks;
using Schola.Configuration.Dto;

namespace Schola.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
