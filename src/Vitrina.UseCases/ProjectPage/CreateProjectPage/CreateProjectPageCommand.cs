using MediatR;
using Vitrina.Domain.Project.Page;
using Vitrina.UseCases.ProjectPage.Dto.Blocks;
using Vitrina.UseCases.ProjectPages;
using Vitrina.UseCases.ProjectPages.Blocks;

namespace Vitrina.UseCases.ProjectPage.CreateProjectPage;

/// <inheritdoc />
public record CreateProjectPageCommand : IRequest<Guid>
{
    /// <summary>
    ///     Page status.
    /// </summary>
    public PageReadyStatusEnum ReadyStatus { get; init; }

    /// <summary>
    /// Page content blocks.
    /// </summary>
    public ICollection<ContentBlockDto> ContentBlocks { get; init; } = new List<ContentBlockDto>();

    /// <summary>
    ///     Project id.
    /// </summary>
    public int ProjectId { get; init; }

    /// <summary>
    ///     User ID who created the project page.
    /// </summary>
    public int CreatorId { get; init; }
}
