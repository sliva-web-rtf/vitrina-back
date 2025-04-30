using Extensions.Hosting.AsyncInitialization;
using Microsoft.EntityFrameworkCore;
using Vitrina.Infrastructure.DataAccess;

namespace Vitrina.Web.Infrastructure.Startup;

/// <summary>
/// Contains database migration helper methods.
/// </summary>
internal sealed class DatabaseInitializer : IAsyncInitializer
{
    private readonly AppDbContext appDbContext;

    /// <summary>
    /// Database initializer. Performs migration and data seed.
    /// </summary>
    /// <param name="appDbContext">Data context.</param>
    public DatabaseInitializer(AppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    /// <inheritdoc />
    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        await appDbContext.Database.ExecuteSqlRawAsync("SELECT pg_advisory_lock(123456789)", cancellationToken);
        await appDbContext.Database.MigrateAsync(cancellationToken);
        await appDbContext.Database.ExecuteSqlRawAsync("SELECT pg_advisory_unlock(123456789)", cancellationToken);
    }
}
