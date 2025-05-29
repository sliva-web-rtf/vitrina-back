using AutoMapper;
using MediatR;
using Vitrina.Infrastructure.Abstractions.Interfaces.Repositories;
using Vitrina.UseCases.ProjectPage.Dto;

namespace Vitrina.UseCases.ProjectPage.DeleteEditorByPageEditorId;

/// <inheritdoc />
public class DeleteEditorByPageEditorIdCommandHandler(
    IPageEditorRepository editorRepository,
    IProjectPageRepository pageRepository,
    IMapper mapper)
    : IRequestHandler<DeleteEditorByPageEditorIdCommand, PageEditorDto>
{
    /// <inheritdoc />
    public async Task<PageEditorDto> Handle(DeleteEditorByPageEditorIdCommand request,
        CancellationToken cancellationToken)
    {
        var page = await pageRepository.GetByIdAsync(request.PageId, cancellationToken);
        page.ThrowExceptionIfNoAccessRights(request.IdAuthorizedUser);
        var editor = await editorRepository.DeleteAsync(request.EditorId, request.PageId, cancellationToken);
        await editorRepository.SaveChangesAsync(cancellationToken);
        return mapper.Map<PageEditorDto>(editor);
    }
}
