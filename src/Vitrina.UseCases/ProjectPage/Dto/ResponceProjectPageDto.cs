using Vitrina.Domain.Project.Page;
using Vitrina.UseCases.Common.DTO;

namespace Vitrina.UseCases.ProjectPage.Dto;

public record ResponceProjectPageDto : BaseEntityDto<Guid>
{
    /// <summary>
    ///     Page status.
    /// </summary>
    required public PageReadyStatusEnum ReadyStatus { get; init; }

    /// <summary>
    ///     Page content blocks.
    /// </summary>
    required public ICollection<ContentBlockDto> ContentBlocks { get; init; } = new List<ContentBlockDto>();

    /// <summary>
    ///     Project id.
    /// </summary>
    public int? ProjectId { get; init; }
}
