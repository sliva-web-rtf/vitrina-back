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
    required public string ProgrammingLanguage { get; init; }
}
