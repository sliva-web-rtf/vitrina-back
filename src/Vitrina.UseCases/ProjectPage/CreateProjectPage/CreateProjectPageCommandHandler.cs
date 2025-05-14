using AutoMapper;
using MediatR;
using Vitrina.Infrastructure.Abstractions.Interfaces.Repositories;
using Vitrina.UseCases.ProjectPage.CreateProjectPage;

namespace Vitrina.UseCases.ProjectPages.CreateProjectPage;

public class CreateProjectPageCommandHandler(IMapper mapper, IProjectPageRepository repository)
    : IRequestHandler<CreateProjectPageCommand, Guid>
{
    public async Task<Guid> Handle(CreateProjectPageCommand request, CancellationToken cancellationToken)
    {
        var page = mapper.Map<Domain.Project.Page.ProjectPage>(request);
        page.NumberCustomBlocks();
        await repository.AddAsync(page, cancellationToken);
        await repository.SaveChangesAsync(cancellationToken);
        return page.Id;
    }
}
