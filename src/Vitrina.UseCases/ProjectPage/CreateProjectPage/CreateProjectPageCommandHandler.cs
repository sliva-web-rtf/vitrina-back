using AutoMapper;
using MediatR;
using Vitrina.Domain.Project.Page;
using Vitrina.Infrastructure.Abstractions.Interfaces.Repositories;

namespace Vitrina.UseCases.ProjectPages.CreateProjectPage;

public class CreateProjectPageCommandHandler(IMapper mapper, IProjectPageRepository repository)
    : IRequestHandler<CreateProjectPageCommand, Guid>
{
    public async Task<Guid> Handle(CreateProjectPageCommand request, CancellationToken cancellationToken)
    {
        var page = mapper.Map<Domain.Project.Page.ProjectPage>(request);
        await repository.AddAsync(page, cancellationToken);
        return page.Id;
    }
}
