namespace Vitrina.UseCases.Project.Dto;

/// <summary>
///     Project dto.
/// </summary>
public record CreateProjectDto : UpdateProjectDto
{
    /// <summary>
    ///     Project page id.
    /// </summary>
    required public Guid PageId { get; init; }
}
