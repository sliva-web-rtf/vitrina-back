using McMaster.Extensions.CommandLineUtils;

namespace Vitrina.Web;

/// <summary>
/// Entry point class.
/// </summary>
internal sealed class Program
{
    private static WebApplication? app;

    /// <summary>
    /// Entry point method.
    /// </summary>
    /// <param name="args">Program arguments.</param>
    public static async Task<int> Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var startup = new Startup(builder.Configuration);
        // For dev: builder.WebHost.UseUrls("http://localhost:5006");
        startup.ConfigureServices(builder.Services, builder.Environment);
        app = builder.Build();
        startup.Configure(app, app.Environment);

        // Command line processing.
        var commandLineApplication = new CommandLineApplication<Program>();
        using var scope = app.Services.CreateScope();
        commandLineApplication
            .Conventions
            .UseConstructorInjection(scope.ServiceProvider)
            .UseDefaultConventions();
        return await commandLineApplication.ExecuteAsync(args);
    }

    /// <summary>
    /// Command line application execution callback.
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
