using System.Text.Json;
using MediatR;
using Vitrina.Domain.User;

namespace Vitrina.UseCases.User.UpdateUser;

/// <summary>
///     The command to update user profile data by ID.
/// </summary>
public record UpdateUserCommand(int Id, string SerializedDto, RoleOnPlatformEnum RoleOnPlatform)
    : IRequest<JsonElement>;
