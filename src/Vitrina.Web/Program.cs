using McMaster.Extensions.CommandLineUtils;
using Microsoft.EntityFrameworkCore;
using Vitrina.Infrastructure.DataAccess;
using Vitrina.Web.Infrastructure.Settings;

namespace Vitrina.Web;

/// <summary>
///     Entry point class.
/// </summary>
internal sealed class Program
{
    private static WebApplication? app;

    /// <summary>
    ///     Entry point method.
    /// </summary>
    /// <param name="args">Program arguments.</param>
    public static async Task<int> Main(string[] args)
    {
        var appOptions = new WebApplicationOptions
        {
            WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")
        };
        var builder = WebApplication.CreateBuilder(appOptions);
        var startup = new Startup(builder.Configuration);
        // For dev: builder.WebHost.UseUrls("http://localhost:5006");
        startup.ConfigureServices(builder.Services, builder.Environment);
        app = builder.Build();
        startup.Configure(app, app.Environment);
        // Command line processing.
        var commandLineApplication = new CommandLineApplication<Program>();
        using var scope = app.Services.CreateScope();

        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<AppDbContext>();
        await context.Database.MigrateAsync();
        await Seeder.ConfigureRoles(services);

        commandLineApplication
            .Conventions
            .UseConstructorInjection(services)
            .UseDefaultConventions();

        return await commandLineApplication.ExecuteAsync(args);
    }

    /// <summary>
    ///     Command line application execution callback.
    /// </summary>
    /// <returns>Exit code.</returns>
    public async Task<int> OnExecuteAsync()
    {
        if (app == null)
        {
            throw new InvalidOperationException("app is not initialized");
        }

        await app.InitAsync();
        await app.RunAsync();
        return 0;
    }
}
