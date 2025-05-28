using AutoMapper;
using MediatR;
using Vitrina.Infrastructure.Abstractions.Interfaces.Repositories;
using Vitrina.UseCases.ProjectPage.Dto;
using Vitrina.UseCases.ProjectPages.GetProjectPageEditor;

namespace Vitrina.UseCases.ProjectPage.GetProjectPageEditors;

/// <inheritdoc />
public class GetProjectPageEditorsQueryHandler(IProjectPageRepository repository, IMapper mapper)
    : IRequestHandler<GetProjectPageEditorsQuery, ICollection<PageEditorDto>>
{
    /// <inheritdoc />
    public async Task<ICollection<PageEditorDto>> Handle(GetProjectPageEditorsQuery request,
        CancellationToken cancellationToken)
    {
        var page = await repository.GetByIdAsync(request.PageId, cancellationToken);
        return mapper.Map<ICollection<PageEditorDto>>(page.Editors);
    }
}
