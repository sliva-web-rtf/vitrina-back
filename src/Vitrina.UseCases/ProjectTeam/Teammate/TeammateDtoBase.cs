using Vitrina.UseCases.ProjectTeam.Role;

namespace Vitrina.UseCases.Project;

public abstract record TeammateDtoBase
{
    /// <summary>
    ///     User roles.
    /// </summary>
    public ICollection<ResponceRoleDto> Roles { get; init; } = new List<ResponceRoleDto>();
}
