using System.Collections;
using MediatR;
using Vitrina.UseCases.ProjectPages;

namespace Vitrina.UseCases.User.GetUserProjectsPages;

/// <inheritdoc />
public record GetUserProjectPagesByUserIdQuey(int UserId) : IRequest<ICollection<ProjectPageDto>>;
