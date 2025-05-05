using Microsoft.EntityFrameworkCore;
using Vitrina.Domain.Project;
using Vitrina.Domain.User;

namespace Vitrina.Infrastructure.Abstractions.Interfaces;

/// <summary>
///     Application abstraction for unit of work.
/// </summary>
public interface IAppDbContext : IDbContextWithSets, IDisposable
{
    /// <summary>
    ///     Users.
    /// </summary>
    DbSet<Teammate> Teammates { get; }

    /// <summary>
    ///     Projects set.
    /// </summary>
    DbSet<Project> Projects { get; }

    /// <summary>
    ///     Tags.
    /// </summary>
    DbSet<Tag> Tags { get; }

    /// <summary>
    ///     Roles.
    /// </summary>
    DbSet<ProjectRole> ProjectRoles { get; }

    /// <summary>
    ///     Contents.
    /// </summary>
    DbSet<Content> Contents { get; }

    /// <summary>
    ///     Users.
    /// </summary>
    DbSet<User> Users { get; }

    /// <summary>
    ///     Confirmation codes.
    /// </summary>
    DbSet<ConfirmationCode> Codes { get; }

    /// <summary>
    ///     Students' specializations.
    /// </summary>
    public DbSet<Specialization> Specializations { get; }
}
