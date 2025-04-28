namespace Vitrina.Web.Infrastructure.Startup;

/// <summary>
///     Logging setup for application.
/// </summary>
internal class LoggingOptionsSetup
{
    private readonly IConfiguration configuration;
    private readonly IWebHostEnvironment environment;

    /// <summary>
    ///     Constructor.
    /// </summary>
    /// <param name="configuration">Configuration.</param>
    /// <param name="environment">Host environment.</param>
    public LoggingOptionsSetup(IConfiguration configuration, IWebHostEnvironment environment)
    {
        this.configuration = configuration;
        this.environment = environment;
    }

    /// <summary>
    ///     Setup logging.
    /// </summary>
    /// <param name="options">Logging builder.</param>
    public void Setup(ILoggingBuilder options)
    {
        if (!environment.IsProduction())
        {
            options.AddDebug();
        }

        options.AddConfiguration(configuration);
    }
}
