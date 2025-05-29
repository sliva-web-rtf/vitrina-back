using Vitrina.Infrastructure.Abstractions.Interfaces.Repositories;
using Vitrina.Infrastructure.DataAccess.Repositories;
using Vitrina.UseCases.Project.CreateProject;
using Vitrina.UseCases.User;

namespace Vitrina.Web.Infrastructure.DependencyInjection;

/// <summary>
///     Register Mediator as dependency.
/// </summary>
internal static class MediatRModule
{
    /// <summary>
    ///     Register dependencies.
    /// </summary>
    /// <param name="services">Services.</param>
    public static void Register(IServiceCollection services)
    {
        services.AddSingleton<UpdateUserDtoValidator>();
        services.AddScoped<ISpecializationRepository, SpecializationRepository>();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateProjectCommand).Assembly));
    }
}
