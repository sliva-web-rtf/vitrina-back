namespace Vitrina.UseCases.ProjectPage.Dto;

public class CreateProjectPageDto
{
    /// <summary>
    ///     Page content blocks.
    /// </summary>
    public ICollection<ContentBlockDto> ContentBlocks { get; init; } = new List<ContentBlockDto>();
}
