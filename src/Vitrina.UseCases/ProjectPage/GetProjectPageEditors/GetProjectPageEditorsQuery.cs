using MediatR;
using Vitrina.UseCases.ProjectPage.Dto;

namespace Vitrina.UseCases.ProjectPages.GetProjectPageEditor;

public record GetProjectPageEditorsQuery(Guid PageId) : IRequest<ICollection<PageEditorDto>>;
