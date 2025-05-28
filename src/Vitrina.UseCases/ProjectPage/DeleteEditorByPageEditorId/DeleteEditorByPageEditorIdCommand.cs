using MediatR;
using Vitrina.UseCases.ProjectPage.Dto;

namespace Vitrina.UseCases.ProjectPage.DeleteEditorByPageEditorId;

/// <inheritdoc />
public record DeleteEditorByPageEditorIdCommand(Guid PageId, Guid EditorId) : IRequest<PageEditorDto>;
