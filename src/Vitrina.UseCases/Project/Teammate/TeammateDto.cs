using Vitrina.UseCases.Common.DTO;
using Vitrina.UseCases.ProjectPages.Blocks;

namespace Vitrina.UseCases.Project;

public record TeammateDto : BaseEntityDto<int>
{
    /// <summary>
    ///     User project id.
    /// </summary>
    public int ProjectId { get; set; }

    required public ShortenedUserDto User { get; init; }

    /// <summary>
    ///     User roles.
    /// </summary>
    public ICollection<RoleDto> Roles { get; init; } = new List<RoleDto>();
}
