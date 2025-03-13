using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace Vitrina.Domain.User;

/// <summary>
/// The command to update user profile data.
/// </summary>
public record UpdateUserCommand(JsonPatchDocument<UpdateUserDto> patchDocument, User User) : IRequest<UpdateUserCommandResult>;
