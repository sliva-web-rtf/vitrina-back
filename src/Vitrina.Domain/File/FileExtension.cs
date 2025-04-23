using System.ComponentModel.DataAnnotations;

namespace Vitrina.Domain.File;

/// <inheritdoc />
public class FileExtension
{
    /// <summary>
    ///     Extension name.
    /// </summary>
    [Key]
    required public string Extension { get; init; }
}
