using MediatR;
using Vitrina.Domain.Project;
using Vitrina.Domain.User;

namespace Vitrina.UseCases.UserProfile.GetUserById;

public class GetUserByIdQuery : IRequest<User>
{
    /// <summary>
    /// User id.
    /// </summary>
    public int UserId { get; init; }
}
