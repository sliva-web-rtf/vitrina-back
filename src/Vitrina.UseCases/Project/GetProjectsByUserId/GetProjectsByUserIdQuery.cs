using MediatR;
using Vitrina.UseCases.Common;

namespace Vitrina.UseCases.Project.GetProjectsByUserId;

public record GetProjectsByUserIdQuery(int UserId) : IRequest<IEnumerable<ProjectDto>>;
