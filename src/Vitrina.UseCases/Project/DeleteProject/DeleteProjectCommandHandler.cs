using MediatR;
using Microsoft.EntityFrameworkCore;
using Vitrina.Infrastructure.Abstractions.Interfaces;

namespace Vitrina.UseCases.Project.DeleteProject;

internal class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand>
{
    private readonly IAppDbContext appDbContext;

    public DeleteProjectCommandHandler(IAppDbContext appDbContext) => this.appDbContext = appDbContext;

    public async Task Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        await appDbContext.Projects
            .Where(p => p.Id == request.ProjectId)
            .ExecuteDeleteAsync(cancellationToken);
    }
}
