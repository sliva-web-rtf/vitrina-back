using System.Text.Json;
using MediatR;
using Vitrina.Domain.User;

namespace Vitrina.UseCases.User;

public record GetUserByIdQuery(int Id, RoleOnPlatformEnum RoleOnPlatform) : IRequest<JsonElement>;
