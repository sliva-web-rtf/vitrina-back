using MediatR;
using Vitrina.Domain.User;

namespace Vitrina.UseCases.UserProfile.GetUserById;

/// <summary>
/// Query to get a user by id.
/// </summary>
public record GetUserByIdQuery<TResultDto>(int UserId) : IRequest<TResultDto>;
