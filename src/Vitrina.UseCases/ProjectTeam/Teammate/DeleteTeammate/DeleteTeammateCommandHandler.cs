using AutoMapper;
using MediatR;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Infrastructure.Abstractions.Interfaces;
using Vitrina.UseCases.Project.Teammate.DeleteTeammate;

namespace Vitrina.UseCases.ProjectTeam.Teammate.DeleteTeammate;

/// <inheritdoc />
public class DeleteTeammateCommandHandler(IAppDbContext dbContext, IMapper mapper)
    : IRequestHandler<DeleteTeammateCommand>
{
    /// <inheritdoc />
    public async Task Handle(DeleteTeammateCommand request, CancellationToken cancellationToken)
    {
        var teammate = await dbContext.Teammates.FindAsync(request.Id, cancellationToken)
                       ?? throw new NotFoundException($"Teammate with the specified id = {request.Id} was not found");
        teammate.Team.Project.ThrowExceptionIfNoAccessRights(request.IdAuthorizedUser);
        dbContext.Teammates.Remove(teammate);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
