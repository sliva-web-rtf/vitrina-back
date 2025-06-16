using MediatR;
using Microsoft.EntityFrameworkCore;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Infrastructure.Abstractions.Interfaces;

namespace Vitrina.UseCases.Project.DeleteProject;

internal class DeleteProjectCommandHandler(IAppDbContext appDbContext)
    : IRequestHandler<DeleteProjectCommand>
{
    public async Task Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await appDbContext.Projects
                          .FirstOrDefaultAsync(p => p.Id == request.ProjectId, cancellationToken)
                      ?? throw new NotFoundException($"Project with id = {request.ProjectId} not found.");
        project.ThrowExceptionIfNoAccessRights(request.IdAuthorizedUser);
        appDbContext.Projects.Remove(project);
        await appDbContext.SaveChangesAsync(cancellationToken);
    }
}
