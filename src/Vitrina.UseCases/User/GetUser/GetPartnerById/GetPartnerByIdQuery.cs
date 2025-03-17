using MediatR;
using Vitrina.UseCases.User.DTO.Profile;

namespace Vitrina.UseCases.User.GetUser.GetPartnerById;

/// <summary>
/// Query to get a partner by id.
/// </summary>
public record GetPartnerByIdQuery(int UserId) : IRequest<PartnerDto>;
