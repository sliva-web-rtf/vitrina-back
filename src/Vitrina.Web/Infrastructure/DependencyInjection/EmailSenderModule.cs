using Microsoft.Extensions.Options;
using Vitrina.Infrastructure.Abstractions.Interfaces;
using Vitrina.UseCases.Email;

namespace Vitrina.Web.Infrastructure.DependencyInjection;

internal class EmailSenderModule
{
    /// <summary>
    ///     Register dependencies.
    /// </summary>
    /// <param name="services">Services.</param>
    public static void Register(IServiceCollection services, IConfiguration configuration)
    {
        var emailSettings = new EmailSettings();
        configuration.Bind(EmailSettings.SectionName, emailSettings);
        services.AddSingleton(Options.Create(emailSettings));
        services.AddScoped<IEmailSender, EmailSender>();
    }
}
