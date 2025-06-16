using MediatR;

namespace Vitrina.UseCases.ProjectThematics.GetAllThematics;

public record GetAllThematicsQuery : IRequest<ICollection<ResponceThematicsDto>>;
