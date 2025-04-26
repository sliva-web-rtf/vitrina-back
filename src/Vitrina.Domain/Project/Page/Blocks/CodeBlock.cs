namespace Vitrina.Domain.Project.Page.Blocks;

public class CodeBlock : NumberedBlockBase
{
    /// <summary>
    ///     A line with a code.
    /// </summary>
    required public string Code { get; set; }

    /// <summary>
    ///     Programming language
    /// </summary>
    required public ProgrammingLanguage ProgrammingLanguage { get; init; }
}
