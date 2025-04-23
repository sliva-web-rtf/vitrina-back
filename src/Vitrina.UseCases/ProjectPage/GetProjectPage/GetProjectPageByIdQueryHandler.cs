using AutoMapper;
using MediatR;
using Vitrina.Infrastructure.Abstractions.Interfaces.Repositories;

namespace Vitrina.UseCases.ProjectPages.GetProjectPage;

public class GetProjectPageByIdQueryHandler(IProjectPageRepository repository, IMapper mapper)
    : IRequestHandler<GetProjectPageByIdQuery, ProjectPageDto>
{
    public async Task<ProjectPageDto> Handle(GetProjectPageByIdQuery request, CancellationToken cancellationToken)
    {
        var page = await repository.GetByIdAsync(request.Id, cancellationToken);
        return mapper.Map<ProjectPageDto>(page);
    }
}
