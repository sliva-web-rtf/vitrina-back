using Vitrina.UseCases.Common.DTO;

namespace Vitrina.UseCases.Project;

public abstract record TeammateDtoBase
{
    /// <summary>
    ///     User roles.
    /// </summary>
    public ICollection<RoleDto> Roles { get; init; } = new List<RoleDto>();
}
