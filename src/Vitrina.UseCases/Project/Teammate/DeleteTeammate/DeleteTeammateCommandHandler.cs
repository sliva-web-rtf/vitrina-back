using AutoMapper;
using MediatR;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Infrastructure.Abstractions.Interfaces;

namespace Vitrina.UseCases.Project.Teammate.DeleteTeammate;

/// <inheritdoc />
public class DeleteTeammateCommandHandler(IAppDbContext dbContext, IMapper mapper)
    : IRequestHandler<DeleteTeammateCommand>
{
    /// <inheritdoc />
    public async Task Handle(DeleteTeammateCommand request, CancellationToken cancellationToken)
    {
        var teammate = await dbContext.Teammates.FindAsync(request.Id, cancellationToken)
                       ?? throw new NotFoundException($"Teammate with the specified id = {request.Id} was not found");
        if (teammate.Project.CheckYourEditingRights(request.IdAuthorizedUser))
        {
            throw new ForbiddenException("You do not have the rights to change the data of this project.");
        }

        dbContext.Teammates.Remove(teammate);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
