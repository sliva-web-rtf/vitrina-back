using MediatR;

namespace Vitrina.UseCases.ProjectThematics.CreateThematics;

public record CreateThematicsCommand(RequestThematicsDto ThematicsDto) : IRequest<Guid>;
