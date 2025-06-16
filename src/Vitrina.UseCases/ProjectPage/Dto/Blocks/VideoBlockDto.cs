using System.ComponentModel.DataAnnotations;

namespace Vitrina.UseCases.ProjectPage.Dto.Blocks;

public record VideoBlockDto
{
    /// <summary>
    ///     Link to the video.
    /// </summary>
    required public string VideoUrl { get; init; }

    /// <summary>
    ///     Block styles.
    /// </summary>
    [StringLength(1_000_000, ErrorMessage = "The line of styles is too big")]
    public string? Css { get; set; }
}
