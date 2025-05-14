using MediatR;
using Vitrina.UseCases.Common.DTO;

namespace Vitrina.UseCases.User.GetUserProjects;

/// <inheritdoc />
public record GetUserProjectsByUserIdQuery(int UserId) : IRequest<ICollection<ProjectDto>>;
