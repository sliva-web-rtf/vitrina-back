using MediatR;
using Vitrina.UseCases.Project.UploadImages.Dto;

namespace Vitrina.UseCases.Project.UploadImages;

/// <summary>
/// Upload images command.
/// </summary>
public class UploadImagesCommand : IRequest
{
    /// <summary>
    /// Project id.
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Files.
    /// </summary>
    public ICollection<FileDto> Files { get; init; } = new List<FileDto>();
}
