using System.Text.Json;
using MediatR;
using Vitrina.UseCases.User.DTO;

namespace Vitrina.UseCases.User.UpdateUser;

/// <summary>
/// The command to update user profile data by ID.
/// </summary>
public record UpdateUserProfileCommand(int UserId, UpdateUserDto User) : IRequest<JsonDocument>;
