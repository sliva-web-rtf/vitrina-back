using System.Text.Json;
using System.Text.Json.Nodes;
using MediatR;

namespace Vitrina.UseCases.User.GetUser;

/// <summary>
/// Query for getting a user's profile by ID.
/// </summary>
public record GetUserProfileByIdQuery(int UserId) : IRequest<JsonDocument>;
