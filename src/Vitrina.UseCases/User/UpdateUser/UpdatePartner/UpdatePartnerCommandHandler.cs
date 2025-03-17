using MediatR;
using Vitrina.UseCases.Common;
using Vitrina.UseCases.User.DTO.Profile;

namespace Vitrina.UseCases.User.UpdateUser.UpdatePartner;

/// <inheritdoc />
public class UpdatePartnerCommandHandler(IHandlerUserActions handler) : IRequestHandler<UpdatePartnerCommand, PartnerDto>
{
    /// <inheritdoc />
    public async Task<PartnerDto> Handle(UpdatePartnerCommand request, CancellationToken cancellationToken)
    {
        return await handler.UpdateById<PartnerDto, PartnerDto>(request.PartnerId, request.PatchDocument, cancellationToken);
    }
}
