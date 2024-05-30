using Microsoft.AspNetCore.Mvc.Rendering;
using Vitrina.Infrastructure.Abstractions.Interfaces;
using Vitrina.Infrastructure.DataAccess;
using Vitrina.Web.Infrastructure.Web;

namespace Vitrina.Web.Infrastructure.DependencyInjection;

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
        services.AddScoped<IAppDbContext>(s => s.GetRequiredService<AppDbContext>());
    }
}
