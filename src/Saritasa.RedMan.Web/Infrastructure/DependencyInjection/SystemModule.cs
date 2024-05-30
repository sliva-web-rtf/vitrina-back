using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Saritasa.RedMan.Infrastructure.Abstractions.Interfaces;
using Saritasa.RedMan.Infrastructure.DataAccess;
using Saritasa.RedMan.UseCases.Users.AuthenticateUser;
using Saritasa.RedMan.Web.Infrastructure.Jwt;
using Saritasa.RedMan.Web.Infrastructure.Web;

namespace Saritasa.RedMan.Web.Infrastructure.DependencyInjection;

/// <summary>
/// System specific dependencies.
/// </summary>
internal static class SystemModule
{
    /// <summary>
    /// Register dependencies.
    /// </summary>
    /// <param name="services">Services.</param>
    public static void Register(IServiceCollection services)
    {
        services.AddSingleton<IJsonHelper, SystemTextJsonHelper>();
        services.AddScoped<IAuthenticationTokenService, SystemJwtTokenService>();
        services.AddScoped<IAppDbContext>(s => s.GetRequiredService<AppDbContext>());
        services.AddScoped<ILoggedUserAccessor, LoggedUserAccessor>();
    }
}
