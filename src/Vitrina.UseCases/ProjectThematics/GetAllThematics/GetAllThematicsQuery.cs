using MediatR;

namespace Vitrina.UseCases.ProjectThematics.GetThematics;

public record GetAllThematicsQuery : IRequest<ICollection<ResponceThematicsDto>>;
