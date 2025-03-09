using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Vitrina.Domain.Project;
using Vitrina.Domain.User;
using Vitrina.Infrastructure.Abstractions.Interfaces;

namespace Vitrina.Infrastructure.DataAccess;

/// <summary>
/// Application unit of work.
/// </summary>
public class AppDbContext : IdentityDbContext<User, AppIdentityRole, int>, IAppDbContext, IDataProtectionKeyContext
{
    public DbSet<Project> Projects => Set<Project>();

    /// <inheritdoc/>
    public DbSet<DataProtectionKey> DataProtectionKeys { get; private set; }

    /// <inheritdoc/>
    public DbSet<Teammate> Teammates => Set<Teammate>();

    /// <inheritdoc/>
    public DbSet<Tag> Tags => Set<Tag>();

    /// <inheritdoc/>
    public DbSet<ProjectRole> ProjectRoles => Set<ProjectRole>();

    /// <inheritdoc/>
    public DbSet<Content> Contents => Set<Content>();

    /// <inheritdoc/>
    public DbSet<ConfirmationCode> Codes => Set<ConfirmationCode>();

    public DbSet<Specialization> Specializations => Set<Specialization>();

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

        modelBuilder.Entity<Specialization>()
            .HasIndex(e => e.Name)
            .IsUnique();

        RestrictCascadeDelete(modelBuilder);
        ForceHavingAllStringsAsVarchars(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

    private static void RestrictCascadeDelete(ModelBuilder modelBuilder)
    {
        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Cascade;
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
