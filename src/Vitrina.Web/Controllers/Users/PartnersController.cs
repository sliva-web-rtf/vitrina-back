using System.Text.Json;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vitrina.UseCases.User.DTO;
using Vitrina.UseCases.User.DTO.Profile;
using Vitrina.UseCases.User.GetUser;
using Vitrina.UseCases.User.UpdateUser;

namespace Vitrina.Web.Controllers.Users;

/// <summary>
/// A controller for working with partners.
/// </summary>
[Authorize(Roles = "Partner")]
[Route("api/partners/{id:int}/profile")]
[ApiExplorerSettings(GroupName = "partners")]
public class PartnersController(IMediator mediator, IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Getting partner profile data by ID.
    /// </summary>
    [Produces("application/json")]
    [HttpGet("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetPartnerProfileDataById([FromRoute] int id, CancellationToken cancellationToken)
    {
        var query = new GetUserProfileByIdQuery(id);
        var result = await mediator.Send(query, cancellationToken);
        return Ok(result.RootElement);
    }

    /// <summary>
    /// Edits partner profile data.
    /// </summary>
    [Produces("application/json")]
    [HttpPatch("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> EditPartnerProfileById([FromRoute] int id,
        [FromBody] PartnerDto partner, CancellationToken cancellationToken)
    {
        var user = mapper.Map<UpdateUserDto>(partner);
        var command = new UpdateUserProfileCommand(id, user);
        var result = await mediator.Send(command, cancellationToken);
        return Ok(result.RootElement);
    }
}
