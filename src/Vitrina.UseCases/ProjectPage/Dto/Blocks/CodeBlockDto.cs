using System.ComponentModel.DataAnnotations;

namespace Vitrina.UseCases.ProjectPage.Dto.Blocks;

public record CodeBlockDto
{
    /// <summary>
    ///     The file with the code
    /// </summary>
    [StringLength(1_000_000, ErrorMessage = "Too voluminous code")]
    required public string? Code { get; init; }

    /// <summary>
    ///     Programming language
    /// </summary>
    [StringLength(255, ErrorMessage = $"Too long {nameof(ProgrammingLanguage)}")]
    required public string ProgrammingLanguage { get; init; }
}
