using MediatR;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Infrastructure.Abstractions.Interfaces;

namespace Vitrina.UseCases.ProjectTeam.DeleteTeam;

/// <inheritdoc />
public class DeleteTeamCommandHandler(IAppDbContext dbContext) : IRequestHandler<DeleteTeamCommand>
{
    /// <inheritdoc />
    public async Task Handle(DeleteTeamCommand request, CancellationToken cancellationToken)
    {
        var team = await dbContext.Teams.FindAsync(request.Id, cancellationToken)
                   ?? throw new NotFoundException($"The team with id = {request.Id} was not found");
        team.Project.ThrowExceptionIfNoAccessRights(request.IdAuthorizedUser);
        dbContext.Teams.Remove(team);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
