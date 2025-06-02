using MediatR;

namespace Vitrina.UseCases.ProjectThematics.GetThematicsById;

public record GetThematicsByIdQuery(Guid ThematicsId) : IRequest<ResponceThematicsDto>;
