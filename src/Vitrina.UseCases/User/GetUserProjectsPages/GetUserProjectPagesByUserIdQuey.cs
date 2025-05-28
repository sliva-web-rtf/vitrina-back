using MediatR;
using Vitrina.UseCases.ProjectPage.Dto;

namespace Vitrina.UseCases.User.GetUserProjectsPages;

/// <inheritdoc />
public record GetUserProjectPagesByUserIdQuey(int UserId) : IRequest<ICollection<ProjectPageDto>>;
