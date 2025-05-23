﻿using McMaster.Extensions.CommandLineUtils;

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
        var appOptions = new WebApplicationOptions
        {
            WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")
        };
        TryToCreateDirectoriesForStoringFiles(appOptions.WebRootPath);
        var builder = WebApplication.CreateBuilder(appOptions);
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

    private static void TryToCreateDirectoriesForStoringFiles(string webRootPath)
    {
        if (!Directory.Exists(webRootPath))
        {
            throw new DirectoryNotFoundException($"The directory \"{webRootPath}\" does not exist.");
        }

        CreateDirectory(Path.Combine(webRootPath, "Avatars"));
        CreateDirectory(Path.Combine(webRootPath, "Preview"));
    }

    private static void CreateDirectory(string directoryPath)
    {
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }
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
