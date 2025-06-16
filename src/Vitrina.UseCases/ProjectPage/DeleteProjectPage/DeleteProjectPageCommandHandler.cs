using MediatR;
using Saritasa.Tools.Domain.Exceptions;
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
        if (page.ProjectId != null)
        {
            throw new DomainException("The project page cannot be deleted while the associated project exists.");
        }

        await repository.Delete(request.Id, cancellationToken);
        await repository.SaveChangesAsync(cancellationToken);
    }
}
