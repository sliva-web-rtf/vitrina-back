using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Saritasa.RedMan.Domain.Store;
using Saritasa.RedMan.Domain.Users;
using Saritasa.RedMan.Infrastructure.Abstractions.Interfaces;

namespace Saritasa.RedMan.Infrastructure.DataAccess;

/// <summary>
/// Application unit of work.
/// </summary>
public class AppDbContext : IdentityDbContext<User, AppIdentityRole, int>, IAppDbContext, IDataProtectionKeyContext
{
    /// <summary>
    /// Products set.
    /// </summary>
    public DbSet<Product> Products { get; private set; }

    /// <inheritdoc/>
    public DbSet<DataProtectionKey> DataProtectionKeys { get; private set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="options">The options to be used by a <see cref="Microsoft.EntityFrameworkCore.DbContext" />.</param>
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        RestrictCascadeDelete(modelBuilder);
        ForceHavingAllStringsAsVarchars(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

    private static void RestrictCascadeDelete(ModelBuilder modelBuilder)
    {
        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }

    private static void ForceHavingAllStringsAsVarchars(ModelBuilder modelBuilder)
    {
        var stringColumns = modelBuilder.Model
            .GetEntityTypes()
            .SelectMany(e => e.GetProperties())
            .Where(p => p.ClrType == typeof(string));
        foreach (IMutableProperty mutableProperty in stringColumns)
        {
            mutableProperty.SetIsUnicode(false);
        }
    }
}
