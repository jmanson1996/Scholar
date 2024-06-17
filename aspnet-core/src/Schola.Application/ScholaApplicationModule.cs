using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Schola.Authorization;

namespace Schola
{
    [DependsOn(
        typeof(ScholaCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class ScholaApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<ScholaAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(ScholaApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
