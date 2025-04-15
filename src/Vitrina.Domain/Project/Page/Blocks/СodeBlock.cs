using Vitrina.Domain.File;

namespace Vitrina.Domain.Project.Page.Blocks;

public class СodeBlock : NumberedBlockBase
{
    /// <summary>
    ///     The file with the code
    /// </summary>
    required public CloudFile File { get; init; }

    /// <summary>
    ///     Programming language
    /// </summary>
    required public ProgrammingLanguage ProgrammingLanguage { get; init; }
}
