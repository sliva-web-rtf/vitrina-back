namespace Vitrina.Domain.Project.Page.Blocks;

public class CodeBlock : NumberedBlockBase
{
    /// <summary>
    ///     The file with the code
    /// </summary>
    required public File.File File { get; init; }

    /// <summary>
    ///     Programming language
    /// </summary>
    required public ProgrammingLanguage ProgrammingLanguage { get; init; }
}
