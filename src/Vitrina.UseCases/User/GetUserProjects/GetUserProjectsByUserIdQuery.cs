using MediatR;
using Vitrina.UseCases.Project.Dto;

namespace Vitrina.UseCases.User.GetUserProjects;

/// <inheritdoc />
public record GetUserProjectsByUserIdQuery(int UserId) : IRequest<ICollection<ResponceProjectDto>>;
