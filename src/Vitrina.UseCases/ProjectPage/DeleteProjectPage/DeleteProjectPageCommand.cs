using MediatR;

namespace Vitrina.UseCases.ProjectPages.DeleteProjectPage;

public record DeleteProjectPageCommand(Guid Id) : IRequest;
