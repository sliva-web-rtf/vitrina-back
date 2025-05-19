using MediatR;

namespace Vitrina.UseCases.Project.Teammate.CreateTeammate;

/// <summary>
///     Command creation of a team member.
/// </summary>
public record CreateTeammateCommand(TeammateDto TeammateDto, int IdAuthorizedUser) : IRequest<int>;
