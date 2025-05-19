using MediatR;
using Vitrina.UseCases.ProjectTeam.Teammate;

namespace Vitrina.UseCases.ProjectTeam.AddTeammate;

/// <summary>
///     Команда для добавления
/// </summary>
public record AddTeammateCommand(RequestTeammateDto TeammateDto, int IdAuthorizedUser, Guid TeamId)
    : IRequest<int>;
