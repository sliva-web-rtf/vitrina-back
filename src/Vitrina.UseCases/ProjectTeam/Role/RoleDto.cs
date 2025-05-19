using System.ComponentModel.DataAnnotations;

namespace Vitrina.UseCases.Common.DTO;

/// <summary>
///     Role dto.
/// </summary>
public class RoleDto
{
    /// <summary>
    ///     Name.
    /// </summary>
    [Required]
    [StringLength(255, ErrorMessage = "The Name must be no more than 255 characters long.")]
    required public string Name { get; init; }
}
