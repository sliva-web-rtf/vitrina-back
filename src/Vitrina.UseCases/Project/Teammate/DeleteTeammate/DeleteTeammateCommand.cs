using MediatR;

namespace Vitrina.UseCases.Project.Teammate.DeleteTeammate;

/// <summary>
///     Command to delete a team member.
/// </summary>
public record DeleteTeammateCommand(int Id, int IdAuthorizedUser) : IRequest;
