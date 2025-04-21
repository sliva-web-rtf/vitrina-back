using MediatR;

namespace Vitrina.UseCases.ProjectPages.CreateProjectPage;

public class CreateProjectPageCommandHandler : IRequestHandler<CreateProjectPageCommand, Guid>
{
    public Task<Guid> Handle(CreateProjectPageCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
