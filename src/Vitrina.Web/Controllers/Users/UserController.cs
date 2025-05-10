using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Vitrina.UseCases.User.DTO;
using Vitrina.UseCases.User.GetUser;
using Vitrina.UseCases.User.UpdateUser;

namespace Vitrina.Web.Controllers.Users;

[Route("api/users/{id:int}")]
[ApiExplorerSettings(GroupName = "users")]
public class UserController(IMediator mediator) : ControllerBase
{
    [HttpGet("")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetStudentProfileDataById([FromRoute] int id, CancellationToken cancellationToken)
    {
        var query = new GetUserByIdQuery(id);
        var result = await mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    ///     Edits student profile data.
    /// </summary>
    [ValidateUserId]
    [Authorize(Roles = "Student, Curator, Partner")]
    [Produces("application/json")]
    [HttpPatch("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> EditStudentProfileById([FromRoute] int id,
        [FromBody] JsonPatchDocument<UpdateUserDto> patchDocument, CancellationToken cancellationToken)
    {
        var command = new UpdateUserByIdCommand(id, patchDocument);
        var result = await mediator.Send(command, cancellationToken);
        return Ok(result);
    }
}
