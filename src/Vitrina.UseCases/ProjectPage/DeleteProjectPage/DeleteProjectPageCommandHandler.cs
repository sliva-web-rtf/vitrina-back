using MediatR;
using Vitrina.Infrastructure.Abstractions.Interfaces.Repositories;

namespace Vitrina.UseCases.ProjectPages.DeleteProjectPage;

public class DeleteProjectPageCommandHandler(IProjectPageRepository repository)
    : IRequestHandler<DeleteProjectPageCommand>
{
    public async Task Handle(DeleteProjectPageCommand request, CancellationToken cancellationToken)
    {
        await repository.Delete(request.Id, cancellationToken);
        await repository.SaveChangesAsync(cancellationToken);
    }
}
