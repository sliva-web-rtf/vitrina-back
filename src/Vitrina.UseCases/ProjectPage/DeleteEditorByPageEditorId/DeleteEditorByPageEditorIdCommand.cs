using MediatR;

namespace Vitrina.UseCases.ProjectPages.DeleteEditorByPageEditorld;

public record DeleteEditorByPageEditorIdCommand(Guid PageId, Guid EditorId) : IRequest<PageEditorDto>;
