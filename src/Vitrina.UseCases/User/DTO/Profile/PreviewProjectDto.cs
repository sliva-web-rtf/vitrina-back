using Vitrina.Domain.Project;

namespace Vitrina.UseCases.User.DTO.Profile;

public record PreviewProjectDto
{
    /// <summary>
    ///     Project id.
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    ///     Name.
    /// </summary>
    required public string Name { get; init; }

    /// <summary>
    ///     Path to preview image.
    /// </summary>
    public string? PreviewImagePath { get; init; }

    /// <summary>
    ///     Project type.
    /// </summary>
    public string? Type { get; init; }

    /// <summary>
    ///     Project completion status.
    /// </summary>
    public CompletionStatusEnum CompletionStatus { get; init; }

    /// <summary>
    ///     Type of initiative.
    /// </summary>
    public TypeInitiativeEnum TypeInitiative { get; init; }

    /// <summary>
    ///     A short description of the information that will be displayed on the project card.
    /// </summary>
    public string? TextPreview { get; init; }

    /// <summary>
    ///     Project page ID.
    /// </summary>
    public Guid PageId { get; init; }
}
