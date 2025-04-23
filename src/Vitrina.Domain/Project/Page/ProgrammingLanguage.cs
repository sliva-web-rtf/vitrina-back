using System.ComponentModel.DataAnnotations;

namespace Vitrina.Domain.Project.Page;

/// <inheritdoc />
public class ProgrammingLanguage
{
    /// <summary>
    ///     Name of the programming language.
    /// </summary>
    [Key]
    required public string Name { get; init; }
}
