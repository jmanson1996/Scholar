using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Schola.EntityFrameworkCore;
using Schola.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace Schola.Web.Tests
{
    [DependsOn(
        typeof(ScholaWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class ScholaWebTestModule : AbpModule
    {
        public ScholaWebTestModule(ScholaEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ScholaWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(ScholaWebMvcModule).Assembly);
        }
    }
}