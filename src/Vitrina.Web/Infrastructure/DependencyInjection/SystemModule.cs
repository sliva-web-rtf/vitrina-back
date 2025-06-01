using Microsoft.AspNetCore.Mvc.Rendering;
using Vitrina.Infrastructure.Abstractions.Interfaces;
using Vitrina.Infrastructure.Abstractions.Interfaces.Repositories;
using Vitrina.Infrastructure.DataAccess;
using Vitrina.Infrastructure.DataAccess.Repositories;
using Vitrina.UseCases.User.Auth;
using Vitrina.Web.Infrastructure.Jwt;
using Vitrina.Web.Infrastructure.Web;

namespace Vitrina.Web.Infrastructure.DependencyInjection;

/// <summary>
///     System specific dependencies.
/// </summary>
internal static class SystemModule
{
    /// <summary>
    ///     Register dependencies.
    /// </summary>
    /// <param name="services">Services.</param>
    public static void Register(IServiceCollection services)
    {
        services.AddSingleton<IJsonHelper, SystemTextJsonHelper>();
        services.AddScoped<IAuthenticationTokenService, SystemJwtTokenService>();
        services.AddScoped<IAppDbContext>(s => s.GetRequiredService<AppDbContext>());
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ILoggedUserAccessor, LoggedUserAccessor>();
    }
}
