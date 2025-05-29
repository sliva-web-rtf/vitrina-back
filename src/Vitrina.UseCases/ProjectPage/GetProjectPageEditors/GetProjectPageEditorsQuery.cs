using MediatR;
using Vitrina.UseCases.ProjectPage.Dto;

namespace Vitrina.UseCases.ProjectPage.GetProjectPageEditors;

/// <inheritdoc />
public record GetProjectPageEditorsQuery(Guid PageId, int? IdAuthorizedUser) : IRequest<ICollection<PageEditorDto>>;
