using Microsoft.AspNetCore.Cors.Infrastructure;

namespace Vitrina.Web.Infrastructure.Startup;

/// <summary>
///     CORS options setup.
/// </summary>
internal class CorsOptionsSetup
{
    public const string CorsPolicyName = "AllowFrontend";
    private readonly string[] frontendOrigins;

    private readonly bool isDevelopment;

    /// <summary>
    ///     Constructor.
    /// </summary>
    /// <param name="isDevelopment">Is development mode enabled.</param>
    /// <param name="frontendOrigins">Frontend origins.</param>
    public CorsOptionsSetup(bool isDevelopment, params string[]? frontendOrigins)
    {
        this.isDevelopment = isDevelopment;
        this.frontendOrigins = frontendOrigins ?? new string[] { };
    }

    /// <summary>
    ///     Setup CORS method.
    /// </summary>
    /// <param name="options">CORS options.</param>
    public void Setup(CorsOptions options) =>
        options.AddPolicy(CorsPolicyName,
            builder =>
            {
                builder.AllowAnyOrigin();
                builder
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .SetPreflightMaxAge(TimeSpan.FromDays(1));
            });
}
