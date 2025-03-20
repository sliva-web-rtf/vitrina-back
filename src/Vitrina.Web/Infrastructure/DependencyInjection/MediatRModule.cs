using Vitrina.Infrastructure.Abstractions.Interfaces;
using Vitrina.UseCases.Common.Repositories;
using Vitrina.UseCases.Project.AddProject;
using Vitrina.UseCases.User;

namespace Vitrina.Web.Infrastructure.DependencyInjection;

/// <summary>
/// Register Mediator as dependency.
/// </summary>
internal static class MediatRModule
{
    /// <summary>
    /// Register dependencies.
    /// </summary>
    /// <param name="services">Services.</param>
    public static void Register(IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AddProjectCommand).Assembly));
    }
}
