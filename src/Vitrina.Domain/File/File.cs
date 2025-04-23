using Microsoft.EntityFrameworkCore;

namespace Vitrina.Domain.File;

/// <inheritdoc />
[Index(nameof(Link), IsUnique = true)]
public class File : BaseEntity<Guid>
{
    /// <summary>
    ///     The link to the cloud storage.
    /// </summary>
    required public string Link { get; init; }

    /// <summary>
    ///     File Extension.
    /// </summary>
    required public FileExtension Extension { get; init; }
}
