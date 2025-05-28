using AutoMapper;
using MediatR;
using Vitrina.Infrastructure.Abstractions.Interfaces.Repositories;
using Vitrina.UseCases.ProjectPage.Dto;
using Vitrina.UseCases.ProjectPages.GetProjectPage;

namespace Vitrina.UseCases.ProjectPage.GetProjectPage;

/// <inheritdoc />
public class GetProjectPageByIdQueryHandler(IProjectPageRepository repository, IMapper mapper)
    : IRequestHandler<GetProjectPageByIdQuery, ProjectPageDto>
{
    /// <inheritdoc />
    public async Task<ProjectPageDto> Handle(GetProjectPageByIdQuery request, CancellationToken cancellationToken)
    {
        var page = await repository.GetByIdAsync(request.Id, cancellationToken);
        page.SortContentBlocks();
        return mapper.Map<ProjectPageDto>(page);
    }
}
