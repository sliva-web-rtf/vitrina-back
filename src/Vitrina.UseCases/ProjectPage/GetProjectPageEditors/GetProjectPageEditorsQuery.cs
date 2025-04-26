using System.Collections;
using MediatR;
using Vitrina.Domain.Project.Page;

namespace Vitrina.UseCases.ProjectPages.GetProjectPageEditor;

public record GetProjectPageEditorsQuery(Guid PageId) : IRequest<ICollection<PageEditorDto>>;
