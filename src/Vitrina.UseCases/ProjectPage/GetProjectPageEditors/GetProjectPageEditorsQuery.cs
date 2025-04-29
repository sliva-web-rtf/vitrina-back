using MediatR;

namespace Vitrina.UseCases.ProjectPages.GetProjectPageEditor;

public record GetProjectPageEditorsQuery(Guid PageId) : IRequest<ICollection<PageEditorDto>>;
