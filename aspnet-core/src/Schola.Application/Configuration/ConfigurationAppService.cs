using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using Schola.Configuration.Dto;

namespace Schola.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : ScholaAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
