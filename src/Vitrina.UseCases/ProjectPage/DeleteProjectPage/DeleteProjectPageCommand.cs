using MediatR;

namespace Vitrina.UseCases.ProjectPage.DeleteProjectPage;

/// <inheritdoc />
public record DeleteProjectPageCommand(Guid Id, int? IdAuthorizedUser) : IRequest;
