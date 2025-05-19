using MediatR;

namespace Vitrina.UseCases.Project.GetProjectTeamMembers;

/// <summary>
///     Command to get project team members.
/// </summary>
public record GetProjectTeamMembersQuery(int ProjectId) : IRequest<ICollection<TeammateDto>>;
