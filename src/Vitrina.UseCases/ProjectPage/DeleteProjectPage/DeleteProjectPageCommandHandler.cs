using MediatR;
using Vitrina.Infrastructure.Abstractions.Interfaces.Repositories;
using Vitrina.UseCases.ProjectPages.DeleteProjectPage;

namespace Vitrina.UseCases.ProjectPage.DeleteProjectPage;

/// <inheritdoc />
public class DeleteProjectPageCommandHandler(IProjectPageRepository repository)
    : IRequestHandler<DeleteProjectPageCommand>
{
    /// <inheritdoc />
    public async Task Handle(DeleteProjectPageCommand request, CancellationToken cancellationToken)
    {
        await repository.Delete(request.Id, cancellationToken);
        await repository.SaveChangesAsync(cancellationToken);
    }
}
