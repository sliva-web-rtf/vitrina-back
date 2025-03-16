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
    [StringLength(255, ErrorMessage = "The Name must be no more than 255 characters long.")]
    required public string Name { get; init; }
}
