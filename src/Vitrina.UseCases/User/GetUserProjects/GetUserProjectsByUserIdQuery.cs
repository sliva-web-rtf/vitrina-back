using MediatR;
using Vitrina.UseCases.User.DTO.Profile;

namespace Vitrina.UseCases.User.GetUserProjects;

/// <inheritdoc />
public record GetUserProjectsByUserIdQuery(int UserId) : IRequest<ICollection<PreviewProjectDto>>;
