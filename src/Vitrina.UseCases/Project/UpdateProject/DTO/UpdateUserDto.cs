using System.ComponentModel.DataAnnotations;

namespace Vitrina.UseCases.Project.UpdateProject.DTO;

/// <summary>
/// Dto for update.
/// </summary>
public class UpdateUserDto
{
    /// <summary>
    /// User id.
    /// </summary>ы
    public int Id { get; init; }

    /// <summary>
    /// User email.
    /// </summary>
    public string? Email { get; init; }

    /// <summary>
    /// User name.
    /// </summary>
    [Required]
    required public string FirstName { get; init; }

    /// <summary>
    /// User last name.
    /// </summary>
    [Required]
    required public string LastName { get; init; }

    /// <summary>
    /// User patronymic.
    /// </summary>
    public string? Patronymic { get; init; }

    /// <summary>
    /// User avatar.
    /// </summary>
    public ICollection<byte>? Avatar { get; init; } = new List<byte>();

    /// <summary>
    /// User roles.
    /// </summary>
    public ICollection<UpdateRoleDto> Roles { get; init; } = new List<UpdateRoleDto>();
}
