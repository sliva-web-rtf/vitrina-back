using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Vitrina.Domain.User;
using Vitrina.UseCases.Common;
using Vitrina.UseCases.UserProfile.GetUserById;

namespace Vitrina.Web.Controllers;

/// <summary>
/// A controller for working with partners.
/// </summary>
[Authorize(Roles = "Partner")]
[Route("api/partners")]
[ApiExplorerSettings(GroupName = "partners")]
public class PartnersController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Getting partner profile data by ID.
    /// </summary>
    [Produces("application/json")]
    [HttpGet("{partnerId:int}/profile")]
    public async Task<PartnerDto> GetPartnerProfileDataById([FromRoute] int partnerId, CancellationToken cancellationToken)
    {
        var query = new GetUserByIdQuery<PartnerDto>(partnerId);
        return await mediator.Send(query, cancellationToken);
    }

    /// <summary>
    /// Edits partner profile data.
    /// </summary>
    [Produces("application/json")]
    [HttpPatch("{partnerId:int}/profile/edit")]
    public async Task<PartnerDto> EditPartnerProfileById([FromRoute] int partnerId,
        [FromBody] JsonPatchDocument<PartnerDto> patchDocument, CancellationToken cancellationToken)
    {
        var command = new UpdateUserCommand<PartnerDto, PartnerDto>(partnerId, patchDocument);
        return await mediator.Send(command, cancellationToken);
    }
}
