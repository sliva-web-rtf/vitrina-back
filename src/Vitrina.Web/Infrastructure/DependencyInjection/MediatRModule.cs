using Vitrina.UseCases.Project.AddProject;

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
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AddProjectCommand).Assembly));
    }
}
