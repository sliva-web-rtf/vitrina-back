using Microsoft.AspNetCore.Identity;

namespace Vitrina.Domain.User;

/// <summary>
/// Custom application identity role.
/// </summary>
public class AppIdentityRole : IdentityRole<int>
{
    public AppIdentityRole() : base() { }

    public AppIdentityRole(string roleName) : base(roleName) { }
}
