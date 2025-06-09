using Vitrina.Domain.Project.Page;

namespace Vitrina.UseCases.ProjectPage.Dto;

public record UpdateProjectPageDto
{
    /// <summary>
    ///     Page status.
    /// </summary>
    required public PageReadyStatusEnum ReadyStatus { get; init; }

    /// <summary>
    ///     Page content blocks.
    /// </summary>
    required public ICollection<ContentBlockDto> ContentBlocks { get; init; } = new List<ContentBlockDto>();
}
