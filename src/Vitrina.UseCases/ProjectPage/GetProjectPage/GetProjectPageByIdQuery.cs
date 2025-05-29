using MediatR;
using Vitrina.UseCases.ProjectPage.Dto;

namespace Vitrina.UseCases.ProjectPage.GetProjectPage;

public record GetProjectPageByIdQuery(Guid Id, int? IdAuthorizedUser) : IRequest<ProjectPageDto>;
