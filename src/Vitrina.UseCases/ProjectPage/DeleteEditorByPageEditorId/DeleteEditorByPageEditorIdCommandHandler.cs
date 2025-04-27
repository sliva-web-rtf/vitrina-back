using MediatR;

namespace Vitrina.UseCases.ProjectPages.DeleteEditorByPageEditorld;

public class DeleteEditorByPageEditorldCommandHandler : IRequestHandler<DeleteEditorByPageEditorIdCommand, PageEditorDto>
{
    public Task<PageEditorDto> Handle(DeleteEditorByPageEditorIdCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
