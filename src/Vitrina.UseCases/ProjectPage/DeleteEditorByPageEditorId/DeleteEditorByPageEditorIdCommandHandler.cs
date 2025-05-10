using AutoMapper;
using MediatR;
using Vitrina.Infrastructure.Abstractions.Interfaces.Repositories;
using Vitrina.UseCases.ProjectPages;
using Vitrina.UseCases.ProjectPages.DeleteEditorByPageEditorld;

namespace Vitrina.UseCases.ProjectPage.DeleteEditorByPageEditorId;

/// <inheritdoc />
public class DeleteEditorByPageEditorldCommandHandler(IPageEditorRepository pageEditorRepository, IMapper mapper)
    : IRequestHandler<DeleteEditorByPageEditorIdCommand, PageEditorDto>
{
    /// <inheritdoc />
    public async Task<PageEditorDto> Handle(DeleteEditorByPageEditorIdCommand request,
        CancellationToken cancellationToken)
    {
        var editor = await pageEditorRepository.DeleteAsync(request.EditorId, request.PageId, cancellationToken);
        await pageEditorRepository.SaveChangesAsync(cancellationToken);
        return mapper.Map<PageEditorDto>(editor);
    }
}
