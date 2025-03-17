using MediatR;
using Vitrina.UseCases.User.DTO.Profile;

namespace Vitrina.UseCases.User.GetUser.GetCuratorById;

/// <summary>
/// Query to get a curator by id.
/// </summary>
public record GetCuratorByIdQuery(int CuratorId) : IRequest<CuratorDto>;
