using Vitrina.UseCases.ProjectTeam.Role;

namespace Vitrina.UseCases.ProjectTeam.Teammate;

public abstract record TeammateDtoBase
{
    /// <summary>
    ///     User roles.
    /// </summary>
    public ICollection<ResponceRoleDto> Roles { get; init; } = new List<ResponceRoleDto>();
}
