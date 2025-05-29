using MediatR;
using Vitrina.UseCases.Common.DTO;
using Vitrina.UseCases.ProjectPage.Dto;

namespace Vitrina.UseCases.ProjectPage.AddEditorByUserEmail;

public record AddEditorByUserEmailCommand(Guid PageId, EmailDto UserEmail, int? IdAuthorizedUser)
    : IRequest<PageEditorDto>;
