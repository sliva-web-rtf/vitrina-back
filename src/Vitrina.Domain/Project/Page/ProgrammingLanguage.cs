namespace Vitrina.Domain.Project.Page;

/// <inheritdoc />
public class ProgrammingLanguage : BaseEntity<Guid>
{
    /// <summary>
    ///     Name of the programming language.
    /// </summary>
    required public string Name { get; init; }
}
