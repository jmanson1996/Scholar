using Abp.Authorization;
using Schola.Authorization.Roles;
using Schola.Authorization.Users;

namespace Schola.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
