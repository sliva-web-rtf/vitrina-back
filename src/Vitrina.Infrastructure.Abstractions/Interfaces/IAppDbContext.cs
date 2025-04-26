using Microsoft.EntityFrameworkCore;
using Vitrina.Domain.File;
using Vitrina.Domain.Project;
using Vitrina.Domain.Project.Page;
using Vitrina.Domain.Project.Page.BasicContentUnits;
using Vitrina.Domain.Project.Page.Blocks;
using Vitrina.Domain.Project.Teammate;
using Vitrina.Domain.User;
using File = Vitrina.Domain.File.File;

namespace Vitrina.Infrastructure.Abstractions.Interfaces;

/// <summary>
/// Application abstraction for unit of work.
/// </summary>
public interface IAppDbContext : IDbContextWithSets, IDisposable
{
    /// <summary>
    /// Users.
    /// </summary>
    DbSet<Teammate> Teammates { get; }

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
    DbSet<ProjectRole> ProjectRoles { get; }

    /// <summary>
    /// Pages.
    /// </summary>
    DbSet<ProjectPage> ProjectPages { get; }

    /// <summary>
    /// Users.
    /// </summary>
    DbSet<User> Users { get; }

    /// <summary>
    /// Confirmation codes.
    /// </summary>
    DbSet<ConfirmationCode> Codes { get; }

    /// <summary>
    /// Students' specializations.
    /// </summary>
    DbSet<Specialization> Specializations { get; }

    /// <summary>
    /// Page editors of the project.
    /// </summary>
    DbSet<PageEditor> PageEditors { get; }

    /// <summary>
    /// Service files.
    /// </summary>
    DbSet<File> Files { get; }

    /// <summary>
    /// Available file extension.
    /// </summary>
    DbSet<FileExtension> FileExtensions { get; }

    DbSet<ImageUnit> ImageUnits { get; }

    DbSet<TextUnit> TextUnits { get; }

    DbSet<UnitWithImageAndText> UnitsWithImageAndText { get; }

    DbSet<BlockWithTextsAndImages> BlocksWithTextsAndPictures { get; }

    DbSet<ImageCarouselBlock> ImageCarouselBlocks { get; }

    DbSet<CodeBlock> CodeBlocks { get; }

    DbSet<CommandBlock> CommandBlocks { get; }

    DbSet<HorizontalDividerBlock> HorizontalDividerBlocks { get; }

    DbSet<BlockWithTextAndImage> BlocksWithTextAndImage { get; }

    DbSet<ImageBlock> ImageBlocks { get; }

    DbSet<TextBlock> TextBlocks { get; }

    DbSet<VideoBlock> VideoBlocks { get; }
}
