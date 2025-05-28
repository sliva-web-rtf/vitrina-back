using AutoMapper;
using MediatR;
using Vitrina.Infrastructure.Abstractions.Interfaces.Repositories;

namespace Vitrina.UseCases.ProjectPage.CreateProjectPage;

/// <inheritdoc />
public class CreateProjectPageCommandHandler(IMapper mapper, IProjectPageRepository repository)
    : IRequestHandler<CreateProjectPageCommand, Guid>
{
    /// <inheritdoc />
    public async Task<Guid> Handle(CreateProjectPageCommand request, CancellationToken cancellationToken)
    {
        var page = mapper.Map<Domain.Project.Page.ProjectPage>(request);
        page.NumberCustomBlocks();
        await repository.AddAsync(page, cancellationToken);
        await repository.SaveChangesAsync(cancellationToken);
        return page.Id;
    }
}
