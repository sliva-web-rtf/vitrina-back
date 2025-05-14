using Vitrina.UseCases.Project.CreateProject;

namespace Vitrina.Web.Infrastructure.DependencyInjection;

/// <summary>
///     Register AutoMapper dependencies.
/// </summary>
public class AutoMapperModule
{
    /// <summary>
    ///     Register dependencies.
    /// </summary>
    /// <param name="services">Services.</param>
    public static void Register(IServiceCollection services) =>
        services.AddAutoMapper(
            typeof(CreateProjectCommand).Assembly);
}
