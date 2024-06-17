using System.Threading.Tasks;
using Schola.Models.TokenAuth;
using Schola.Web.Controllers;
using Shouldly;
using Xunit;

namespace Schola.Web.Tests.Controllers
{
    public class HomeController_Tests: ScholaWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            await AuthenticateAsync(null, new AuthenticateModel
            {
                UserNameOrEmailAddress = "admin",
                Password = "123qwe"
            });

            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}