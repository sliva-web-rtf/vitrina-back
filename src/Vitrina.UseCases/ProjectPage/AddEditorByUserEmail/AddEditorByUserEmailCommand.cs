using MediatR;
using Vitrina.UseCases.Common.DTO;

namespace Vitrina.UseCases.ProjectPages.AddEditorByUserEmail;

public record AddEditorByUserEmailCommand(Guid PageId, EmailDto UserEmail) : IRequest<PageEditorDto>;
