using System.ComponentModel.DataAnnotations;
using Vitrina.UseCases.Common.DTO;
using Vitrina.UseCases.ProjectPages.Blocks;

namespace Vitrina.UseCases.ProjectPage.Dto.Blocks;

public record VideoBlockDto : NumberedBlockBaseDto
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
