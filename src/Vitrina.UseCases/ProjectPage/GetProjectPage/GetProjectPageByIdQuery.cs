using MediatR;

namespace Vitrina.UseCases.ProjectPages.GetProjectPage;

public record GetProjectPageByIdQuery(Guid Id) : IRequest<ProjectPageDto>;
