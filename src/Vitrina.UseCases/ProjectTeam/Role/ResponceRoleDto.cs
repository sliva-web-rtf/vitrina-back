using System.ComponentModel.DataAnnotations;
using Vitrina.UseCases.Common.DTO;

namespace Vitrina.UseCases.ProjectTeam.Role;

/// <summary>
///     Role dto.
/// </summary>
public record ResponceRoleDto : BaseEntityDto<int>
{
    /// <summary>
    ///     Name.
    /// </summary>
    [Required]
    [StringLength(255, ErrorMessage = "The Name must be no more than 255 characters long.")]
    required public string Name { get; init; }
}
