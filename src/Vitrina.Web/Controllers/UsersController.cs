using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Vitrina.Domain.User;
using Vitrina.UseCases.Common;
using Vitrina.UseCases.UserProfile.GetUserById;

namespace Vitrina.Web.Controllers;

/// <summary>
/// A controller for working with users.
/// </summary>
/*[Authorize]*/
[ApiController]
[Route("api/users")]
[ApiExplorerSettings(GroupName = "users")]
public class UsersController(IMediator mediator, IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Getting user profile data by Id.
    /// </summary>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Produces("application/json")]
    [HttpGet("{userId:int}/profile")]
    public async Task<IActionResult> GetUserProfileDataById([FromRoute] int userId, CancellationToken cancellationToken)
    {
        var user = await mediator.Send(new GetUserByIdQuery(userId), cancellationToken);

        return user is null
            ? NotFound("The user with the specified Id was not found")
            : Ok(user.RoleOnPlatform switch
            {
                RoleOnPlatformEnum.Student => mapper.Map<StudentDto>(user),
                RoleOnPlatformEnum.Curator => mapper.Map<CuratorDto>(user),
                RoleOnPlatformEnum.Partner => mapper.Map<PartnerDto>(user),
                _ => throw new InvalidOperationException("There is no handler provided for the role.")
            });
    }

    /// <summary>
    /// Edits user profile data.
    /// </summary>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [Produces("application/json")]
    [HttpPatch("{userId:int}/profile/edit")]
    public async Task<ActionResult> EditUserProfileById([FromRoute] int userId,
        [FromBody] JsonPatchDocument<UpdateUserDto>? updateCommand, CancellationToken cancellationToken)
    {
        if (updateCommand is null)
        {
            return BadRequest("Невалидный JSON");
        }

        var user = await mediator.Send(new GetUserByIdQuery(userId), cancellationToken);
        if (user is null)
        {
            return NotFound("The user with the specified Id was not found");
        }

        var updateResult = await mediator.Send(new UpdateUserCommand(updateCommand, user), cancellationToken);

        return updateResult.IsSuccess
            ? Ok(user)
            : UnprocessableEntity(updateResult.ErrorMessage);
    }
}
