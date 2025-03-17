using MediatR;
using Vitrina.UseCases.User.DTO.Profile;

namespace Vitrina.UseCases.User.GetUser.GetPartnerById;

/// <inheritdoc />
public class GetPartnerByIdQueryHandler(IHandlerUserActions handler)
    : IRequestHandler<GetPartnerByIdQuery, PartnerDto>
{
    /// <inheritdoc />
    public async Task<PartnerDto> Handle(GetPartnerByIdQuery request, CancellationToken cancellationToken)
    {
        return await handler.GetUserById<PartnerDto>(request.UserId, cancellationToken);
    }
}
