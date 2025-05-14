using System.ComponentModel.DataAnnotations;

namespace Vitrina.UseCases.ProjectPages.Blocks;

public record TextBlockDto
{
    /// <summary>
    ///     The marking of the text.
    /// </summary>
    [StringLength(1_000_000, ErrorMessage = "The markup is too voluminous")]
    required public string Markdown { get; init; }
}
