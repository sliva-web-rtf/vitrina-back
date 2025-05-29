using MediatR;
using Vitrina.Infrastructure.Abstractions.Interfaces.Repositories;

namespace Vitrina.UseCases.ProjectPage.DeleteProjectPage;

/// <inheritdoc />
public class DeleteProjectPageCommandHandler(IProjectPageRepository repository)
    : IRequestHandler<DeleteProjectPageCommand>
{
    /// <inheritdoc />
    public async Task Handle(DeleteProjectPageCommand request, CancellationToken cancellationToken)
    {
        var page = await repository.GetByIdAsync(request.Id, cancellationToken);
        page.ThrowExceptionIfNoAccessRights(request.IdAuthorizedUser);
        await repository.Delete(request.Id, cancellationToken);
        await repository.SaveChangesAsync(cancellationToken);
    }
}
