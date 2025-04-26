using FluentValidation;
using FluentValidation.AspNetCore;
using Vitrina.Validators;

namespace Vitrina.Web.Infrastructure.DependencyInjection;

/// <summary>
/// Application specific dependencies.
/// </summary>
internal static class ApplicationModule
{
    /// <summary>
    /// Register dependencies.
    /// </summary>
    /// <param name="services">Services.</param>
    /// <param name="configuration">Configuration.</param>
#pragma warning disable CA1801 // Review unused parameters
    public static void Register(IServiceCollection services, IConfiguration configuration)
#pragma warning restore CA1801 // Review unused parameters
    {
        services.AddControllers()
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssembly(typeof(ContentBlockDtoValidator).Assembly);
    }
}
