using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Vitrina.Domain.Project;
using Vitrina.Domain.Project.Page;
using Vitrina.Domain.Project.Page.Content;
using Vitrina.Domain.Project.Page.Editor;
using Vitrina.Domain.Project.Teammate;
using Vitrina.Domain.User;
using Vitrina.Infrastructure.Abstractions.Interfaces;

namespace Vitrina.Infrastructure.DataAccess;

/// <summary>
///     Application unit of work.
/// </summary>
public class AppDbContext : IdentityDbContext<User, AppIdentityRole, int>, IAppDbContext, IDataProtectionKeyContext
{
    /// <summary>
    ///     Constructor.
    /// </summary>
    /// <param name="options">The options to be used by a <see cref="Microsoft.EntityFrameworkCore.DbContext" />.</param>
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Project> Projects => Set<Project>();

    public DbSet<ProjectSphere> ProjectSpheres => Set<ProjectSphere>();

    public DbSet<ProjectThematics> ProjectThematics => Set<ProjectThematics>();

    public DbSet<Team> Teams => Set<Team>();

    public DbSet<Teammate> Teammates => Set<Teammate>();

    public DbSet<ProjectRole> ProjectRoles => Set<ProjectRole>();

    public DbSet<ProjectPage> ProjectPages => Set<ProjectPage>();

    public DbSet<Specialization> Specializations => Set<Specialization>();

    public DbSet<PageEditor> PageEditors => Set<PageEditor>();

    public DbSet<ContentBlock> ContentBlocks => Set<ContentBlock>();

    public DbSet<DataProtectionKey> DataProtectionKeys { get; private set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseLazyLoadingProxies();

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
            relationship.DeleteBehavior = DeleteBehavior.Cascade;
        }
    }

    private static void ForceHavingAllStringsAsVarchars(ModelBuilder modelBuilder)
    {
        var stringColumns = modelBuilder.Model
            .GetEntityTypes()
            .SelectMany(e => e.GetProperties())
            .Where(p => p.ClrType == typeof(string));
        foreach (var mutableProperty in stringColumns)
        {
            mutableProperty.SetIsUnicode(false);
        }
    }
}
