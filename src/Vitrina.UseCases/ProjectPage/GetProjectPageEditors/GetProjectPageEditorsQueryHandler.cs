using AutoMapper;
using MediatR;
using Vitrina.Infrastructure.Abstractions.Interfaces.Repositories;

namespace Vitrina.UseCases.ProjectPages.GetProjectPageEditor;

public class GetProjectPageEditorsQueryHandler(IProjectPageRepository repository, IMapper mapper)
    : IRequestHandler<GetProjectPageEditorsQuery, ICollection<PageEditorDto>>
{
    public async Task<ICollection<PageEditorDto>> Handle(GetProjectPageEditorsQuery request,
        CancellationToken cancellationToken)
    {
        var page = await repository.GetByIdAsync(request.PageId, cancellationToken);
        return mapper.Map<ICollection<PageEditorDto>>(page.Editors);
    }
}
