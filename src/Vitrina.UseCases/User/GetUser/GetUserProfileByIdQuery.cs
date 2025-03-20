using System.Text.Json;
using System.Text.Json.Nodes;
using MediatR;

namespace Vitrina.UseCases.User.GetUser;

public record GetUserProfileByIdQuery(int UserId) : IRequest<JsonDocument>;
