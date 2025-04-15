using Microsoft.EntityFrameworkCore;

namespace Vitrina.Domain.File;

/// <inheritdoc />
[Index(nameof(Extension), IsUnique = true)]
public class FileExtension : BaseEntity<Guid>
{
    /// <summary>
    ///     Extension name.
    /// </summary>
    required public string Extension { get; init; }
}
