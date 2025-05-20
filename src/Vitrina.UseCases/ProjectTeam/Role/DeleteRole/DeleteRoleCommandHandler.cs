using MediatR;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Infrastructure.Abstractions.Interfaces;

namespace Vitrina.UseCases.ProjectTeam.Role.DeleteRole;

/// <inheritdoc />
public class DeleteRoleCommandHandler(IAppDbContext dbContext) : IRequestHandler<DeleteRoleCommand>
{
    /// <inheritdoc />
    public async Task Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await dbContext.ProjectRoles.FindAsync(request.Id, cancellationToken)
                   ?? throw new NotFoundException($"The role with {nameof(request.Id)} = {request.Id} was not found");
        dbContext.ProjectRoles.Remove(role);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
