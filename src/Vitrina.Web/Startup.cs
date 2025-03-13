﻿using System.Net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Vitrina.Domain.User;
using Vitrina.Infrastructure.DataAccess;
using Vitrina.Web.Infrastructure.DependencyInjection;
using Vitrina.Web.Infrastructure.Middlewares;
using Vitrina.Web.Infrastructure.Settings;
using Vitrina.Web.Infrastructure.Startup;
using Vitrina.Web.Infrastructure.Startup.Swagger;

namespace Vitrina.Web;

/// <summary>
/// Entry point for ASP.NET Core app.
/// </summary>
public class Startup
{
    private readonly IConfiguration configuration;

    /// <summary>
    /// Entry point for web application.
    /// </summary>
    /// <param name="configuration">Global configuration.</param>
    public Startup(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    /// <summary>
    /// Configure application services on startup.
    /// </summary>
    /// <param name="services">Services to configure.</param>
    /// <param name="environment">Application environment.</param>
    public void ConfigureServices(IServiceCollection services, IWebHostEnvironment environment)
    {
        // Swagger.
        services.AddSwaggerGen(new SwaggerGenOptionsSetup().Setup);

        // CORS.
        var frontendOrigin = (configuration["AppSettings:FrontendOrigin"] ?? string.Empty)
            .Split(';', StringSplitOptions.RemoveEmptyEntries);
        services.AddCors(new CorsOptionsSetup(
            environment.IsDevelopment(),
            frontendOrigin
        ).Setup);

        // x-forward
        var knownProxies = (configuration["AppSettings:KnownProxies"] ?? string.Empty)
            .Split(';', StringSplitOptions.RemoveEmptyEntries);
        services.Configure<ForwardedHeadersOptions>(options =>
        {
            foreach (var proxy in knownProxies)
            {
                options.KnownProxies.Add(IPAddress.Parse(proxy));
            }
        });

        // Health check.
        var databaseConnectionString = configuration.GetConnectionString("AppDatabase")
                                       ?? throw new ArgumentNullException("ConnectionStrings:AppDatabase",
                                           "Database connection string is not initialized");

        // MVC.
        services
            .AddControllers()
            .AddJsonOptions(new JsonOptionsSetup().Setup);
        services.Configure<ApiBehaviorOptions>(new ApiBehaviorOptionsSetup().Setup);

        // We need to set the application name to data protection, since the default token
        // provider uses this data to create the token. If it is not specified explicitly,
        // tokens from different instances will be incompatible.
        services.AddDataProtection().SetApplicationName("Application")
            .PersistKeysToDbContext<AppDbContext>();

        // Identity.
        services.AddIdentity<User, AppIdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();
        services.Configure<IdentityOptions>(new IdentityOptionsSetup().Setup);

        // JWT
        var jwtSecretKey = configuration["Jwt:SecretKey"] ?? throw new ArgumentNullException("Jwt:SecretKey");
        var jwtIssuer = configuration["Jwt:Issuer"] ?? throw new ArgumentNullException("Jwt:Issuer");
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(new JwtBearerOptionsSetup(
                jwtSecretKey,
                jwtIssuer).Setup
            );

        // Database.
        services.AddDbContext<AppDbContext>(
            new DbContextOptionsSetup(databaseConnectionString).Setup);
        services.AddAsyncInitializer<DatabaseInitializer>();

        // Logging.
        services.AddLogging(new LoggingOptionsSetup(configuration, environment).Setup);

        // Application settings.
        services.Configure<AppSettings>(configuration.GetSection("Application"));

        // HTTP client.
        services.AddHttpClient();

        // Other dependencies.
        AutoMapperModule.Register(services);
        ApplicationModule.Register(services, configuration);
        MediatRModule.Register(services);
        SystemModule.Register(services);
    }

    /// <summary>
    /// Configure web application.
    /// </summary>
    /// <param name="app">Application builder.</param>
    /// <param name="environment">Application environment.</param>
    public void Configure(IApplicationBuilder app, IWebHostEnvironment environment)
    {
        app.UseSwagger();
        app.UseSwaggerUI(new SwaggerUIOptionsSetup().Setup);

        // Custom middlewares.
        app.UseMiddleware<ApiExceptionMiddleware>();

        // MVC.
        app.UseRouting();

        // CORS.
        app.UseCors(CorsOptionsSetup.CorsPolicyName);
        app.UseForwardedHeaders(new ForwardedHeadersOptions { ForwardedHeaders = ForwardedHeaders.All });

        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            Infrastructure.Startup.HealthCheck.HealthCheckModule.Register(endpoints);
            endpoints.Map("/", context =>
            {
                context.Response.Redirect("/swagger");
                return Task.CompletedTask;
            });
            endpoints.MapControllers();
        });
    }
}
