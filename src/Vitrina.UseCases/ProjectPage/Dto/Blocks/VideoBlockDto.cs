using System.ComponentModel.DataAnnotations;
using Vitrina.UseCases.Common.DTO;

namespace Vitrina.UseCases.ProjectPage.Dto.Blocks;

public record VideoBlockDto
{
    /// <summary>
    ///     Link to the video.
    /// </summary>
    required public FileDto Video { get; init; }

    /// <summary>
    ///     Block styles.
    /// </summary>
    [StringLength(1_000_000, ErrorMessage = "The line of styles is too big")]
    public string? Css { get; set; }
}
