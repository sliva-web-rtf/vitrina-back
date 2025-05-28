namespace Vitrina.UseCases.Project.Dto;

public record ResponceProjectDto : CreateProjectDto
{
    /// <summary>
    ///     ID.
    /// </summary>
    public Guid Id { get; init; }
}
