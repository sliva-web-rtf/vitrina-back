using MediatR;

namespace Vitrina.UseCases.ProjectPages.CreateProjectPage;

/// <inheritdoc />
public record CreateProjectPageCommand(ProjectPageDto PageDto) : IRequest<Guid>;
