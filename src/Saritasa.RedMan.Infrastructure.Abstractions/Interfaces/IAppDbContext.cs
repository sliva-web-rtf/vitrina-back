using Microsoft.EntityFrameworkCore;
using Saritasa.RedMan.Domain.Project;

namespace Saritasa.RedMan.Infrastructure.Abstractions.Interfaces;

/// <summary>
/// Application abstraction for unit of work.
/// </summary>
public interface IAppDbContext : IDbContextWithSets, IDisposable
{
    /// <summary>
    /// Users.
    /// </summary>
    DbSet<User> Users { get; }

    /// <summary>
    /// Projects set.
    /// </summary>
    DbSet<Project> Projects { get; }

    /// <summary>
    /// Tags.
    /// </summary>
    DbSet<Tag> Tags { get; }

    /// <summary>
    /// Roles.
    /// </summary>
    DbSet<Role> Roles { get; }

    /// <summary>
    /// Contents.
    /// </summary>
    DbSet<Content> Contents { get; }
}
