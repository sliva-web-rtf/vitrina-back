using System.ComponentModel.DataAnnotations;

namespace Vitrina.UseCases.ProjectTeam.Role;

public record RequestRoleDto
{
    [Required]
    [StringLength(255, ErrorMessage = "The Name must be no more than 255 characters long.")]
    required public string Name { get; init; }
}
