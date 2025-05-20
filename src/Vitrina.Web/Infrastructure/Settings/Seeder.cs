using Microsoft.AspNetCore.Identity;
using Vitrina.Domain.User;

namespace Vitrina.Web.Infrastructure.Settings;

public static class Seeder
{
    private static readonly string[] RoleNames = ["Curator", "Partner", "Student", "Administrator"];

    /// <summary>
    ///     Sets up user roles in the application.
    /// </summary>
    public static async Task ConfigureRoles(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var services = scope.ServiceProvider;

        var roleManager = services.GetRequiredService<RoleManager<AppIdentityRole>>();

        foreach (var roleName in RoleNames)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new AppIdentityRole(roleName));
            }
        }
    }
}
