namespace Vitrina.UseCases.Tests.Infrastructure;

using Microsoft.EntityFrameworkCore;
using Vitrina.Domain.Project;
using Vitrina.Domain.User;
using Vitrina.Infrastructure.Abstractions.Interfaces;

/// <summary>
/// Test database context.
/// </summary>
internal class TestDbContext : DbContext, IAppDbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase(databaseName: $"TestDbContext_{Guid.NewGuid()}");
    }

    public DbSet<Teammate> Teammates { get; set; } = null!;

    public DbSet<Domain.Project.Project> Projects { get; set; } = null!;

    public DbSet<Tag> Tags { get; set; } = null!;

    public DbSet<ProjectRole> ProjectRoles { get; set; } = null!;

    public DbSet<Content> Contents { get; set; } = null!;

    public DbSet<User> Users { get; set; } = null!;

    public DbSet<ConfirmationCode> Codes { get; set; } = null!;
}
