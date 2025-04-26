using MediatR;

namespace Vitrina.UseCases.ProjectPages.GetProjectPageEditor;

public record GetProjectPageEditorQuery(Guid Id) : IRequest<Guid>;
