using Vitrina.UseCases.Common.DTO;

namespace Vitrina.UseCases.Project;

public record TeammateDto
{
    /// <summary>
    ///     User first name.
    /// </summary>
    public string? FirstName { get; init; }

    /// <summary>
    ///     User last name.
    /// </summary>
    public string? LastName { get; init; }

    /// <summary>
    ///     User patronymic.
    /// </summary>
    public string? Patronymic { get; init; }

    /// <summary>
    ///     User email.
    /// </summary>
    public string? Email { get; init; }

    /// <summary>
    ///     User roles.
    /// </summary>
    public ICollection<RoleDto> Roles { get; init; } = new List<RoleDto>();
}
