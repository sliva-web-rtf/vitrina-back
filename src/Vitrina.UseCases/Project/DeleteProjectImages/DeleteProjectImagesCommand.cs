using MediatR;

namespace Vitrina.UseCases.Project.DeleteProjectImages;

/// <summary>
/// DeleteProjectImagesCommand.
/// </summary>
public class DeleteProjectImagesCommand : IRequest
{
    /// <summary>
    /// ProjectId.
    /// </summary>
    public int ProjectId { get; init; }
}
