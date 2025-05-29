using Microsoft.EntityFrameworkCore;
using Vitrina.Domain.Project;
using Vitrina.Domain.Project.Page;
using Vitrina.Domain.Project.Page.Content;
using Vitrina.Domain.Project.Page.Editor;
using Vitrina.Domain.Project.Teammate;
using Vitrina.Domain.User;

namespace Vitrina.Infrastructure.Abstractions.Interfaces;

/// <summary>
///     Application abstraction for unit of work.
/// </summary>
public interface IAppDbContext : IDbContextWithSets, IDisposable
{
    /// <summary>
    ///     Project spheres.
    /// </summary>
    public DbSet<ProjectSphere> ProjectSpheres { get; }

    /// <summary>
    ///     ТProject thematics.
    /// </summary>
    public DbSet<ProjectThematics> ProjectThematics { get; }

    /// <summary>
    ///     Project teams.
    /// </summary>
    DbSet<Team> Teams { get; }

    /// <summary>
    ///     Users.
    /// </summary>
    DbSet<Teammate> Teammates { get; }

    /// <summary>
    ///     Projects set.
    /// </summary>
    DbSet<Project> Projects { get; }

    /// <summary>
    ///     Roles.
    /// </summary>
    DbSet<ProjectRole> ProjectRoles { get; }

    /// <summary>
    ///     Pages.
    /// </summary>
    DbSet<ProjectPage> ProjectPages { get; }

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
    DbSet<Specialization> Specializations { get; }

    /// <summary>
    ///     Page editors of the project.
    /// </summary>
    DbSet<PageEditor> PageEditors { get; }

    /// <summary>
    ///     Page content blocks.
    /// </summary>
    DbSet<ContentBlock> ContentBlocks { get; }
}
