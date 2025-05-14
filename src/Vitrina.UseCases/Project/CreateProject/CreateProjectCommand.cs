using MediatR;
using Vitrina.UseCases.Common.DTO;

namespace Vitrina.UseCases.Project.CreateProject;

/// <summary>
///     Add project command.
/// </summary>
public record CreateProjectCommand : ProjectDto, IRequest<int>
{
    /*/// <summary>
    /// Team members.
    /// </summary>
    public ICollection<TeammateDto> TeamMembers { get; set; } = new List<TeammateDto>();*/
}
