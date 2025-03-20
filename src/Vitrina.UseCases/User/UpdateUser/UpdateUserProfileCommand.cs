using System.Text.Json;
using MediatR;
using Vitrina.UseCases.User.DTO;

namespace Vitrina.UseCases.User.UpdateUser;

public record UpdateUserProfileCommand(int UserId, UpdateUserDto User) : IRequest<JsonDocument>;
