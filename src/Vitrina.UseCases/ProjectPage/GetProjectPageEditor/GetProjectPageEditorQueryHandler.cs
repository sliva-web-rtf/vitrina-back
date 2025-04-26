using MediatR;
using Vitrina.Infrastructure.Abstractions.Interfaces.Repositories;

namespace Vitrina.UseCases.ProjectPages.GetProjectPageEditor;

public class GetProjectPageEditorQueryHandler(IProjectPageRepository repository) : IRequestHandler<GetProjectPageEditorQuery, Guid>
{
    public async Task<Guid> Handle(GetProjectPageEditorQuery request, CancellationToken cancellationToken)
    {
        var page = await repository.GetByIdAsync(request.Id, cancellationToken);


    }
}
