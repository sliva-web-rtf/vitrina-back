using MediatR;

namespace Vitrina.UseCases.ProjectThematics.DeleteThematics;

public record DeleteThematicsCommand(Guid Id) : IRequest;
