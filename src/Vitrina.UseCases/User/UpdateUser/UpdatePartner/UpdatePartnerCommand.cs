using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Vitrina.UseCases.Common;
using Vitrina.UseCases.User.DTO.Profile;

namespace Vitrina.UseCases.User.UpdateUser.UpdatePartner;

public record UpdatePartnerCommand(int PartnerId, JsonPatchDocument<PartnerDto> PatchDocument) : IRequest<PartnerDto>;
