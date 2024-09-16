using System.ComponentModel.DataAnnotations;

namespace Vitrina.UseCases.Common;

/// <summary>
/// Role dto.
/// </summary>
public class RoleDto
{
    /// <summary>
    /// Role id.
    /// </summary>ы
    public int Id { get; init; }

    /// <summary>
    /// Name.
    /// </summary>
    [Required]
    required public string Name { get; init; }
}
