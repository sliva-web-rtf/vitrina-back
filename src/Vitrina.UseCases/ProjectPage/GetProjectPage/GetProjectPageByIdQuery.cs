using MediatR;
using Vitrina.UseCases.ProjectPage.Dto;

namespace Vitrina.UseCases.ProjectPages.GetProjectPage;

public record GetProjectPageByIdQuery(Guid Id) : IRequest<ProjectPageDto>;
