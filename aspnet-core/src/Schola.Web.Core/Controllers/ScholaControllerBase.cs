using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace Schola.Controllers
{
    public abstract class ScholaControllerBase: AbpController
    {
        protected ScholaControllerBase()
        {
            LocalizationSourceName = ScholaConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
