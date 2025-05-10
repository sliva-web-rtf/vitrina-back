using MediatR;
using Vitrina.UseCases.User.DTO;

namespace Vitrina.UseCases.User.GetUserProjects;

/// <inheritdoc />
public record GetUserProjectsByUserIdQuery(int UserId) : IRequest<ICollection<PreviewProjectDto>>;
