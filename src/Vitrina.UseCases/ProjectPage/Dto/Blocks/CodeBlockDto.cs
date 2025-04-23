using System.ComponentModel.DataAnnotations;
using Vitrina.UseCases.Common.DTO;

namespace Vitrina.UseCases.ProjectPages.Blocks;

public record CodeBlockDto : NumberedBlockBaseDto
{
    /// <summary>
    ///     The file with the code
    /// </summary>
    required public FileDto File { get; init; }

    /// <summary>
    ///     Programming language
    /// </summary>
    [StringLength(255, ErrorMessage = $"Too long {nameof(ProgrammingLanguage)}")]
    required public string ProgrammingLanguage { get; init; }
}
