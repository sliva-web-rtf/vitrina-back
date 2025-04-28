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
/// A controller for working with curators.
/// </summary>
[ApiController]
[Route("api/curators/{id:int}/profile")]
[ApiExplorerSettings(GroupName = "curators")]
public class CuratorsController(IMediator mediator, IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Getting curator profile data by Id.
    /// </summary>
    [Produces("application/json")]
    [HttpGet("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetUserProfileDataById([FromRoute] int id,
        CancellationToken cancellationToken)
    {
        var query = new GetUserProfileByIdQuery(id);
        var result = await mediator.Send(query, cancellationToken);
        return Ok(result.RootElement);
    }

    /// <summary>
    /// Edits curator profile data.
    /// </summary>
    [ValidateUserId]
    [HttpPatch("")]
    [Authorize(Roles = "Curator")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> EditUserProfileById([FromRoute] int id,
        [FromBody] UpdateCuratorDto curator, CancellationToken cancellationToken)
    {
        var user = mapper.Map<UpdateUserDto>(curator);
        var command = new UpdateUserProfileCommand(id, user);
        var result = await mediator.Send(command, cancellationToken);
        return Ok(result.RootElement);
    }
}
