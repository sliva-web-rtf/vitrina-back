using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Vitrina.UseCases.ProjectTeam.Teammate;
using Vitrina.UseCases.ProjectTeam.Teammate.DeleteTeammate;
using Vitrina.UseCases.ProjectTeam.Teammate.GetTeammateById;
using Vitrina.UseCases.ProjectTeam.Teammate.UpdateTeammate;

namespace Vitrina.Web.Controllers.Projects;

[ApiController]
[Route("api/teammates")]
[ApiExplorerSettings(GroupName = "teammates")]
public class TeammateController(IMediator mediator) : ControllerBase
{
    /// <summary>
    ///     Updates the data of the team's member.
    /// </summary>
    [HttpPatch("{id:int}")]
    [Authorize(Roles = "Student, Curator")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update([FromRoute] int id,
        [FromBody] JsonPatchDocument<ResponceTeammateDto> patchDocument,
        CancellationToken cancellationToken)
    {
        var idAuthorizedUser = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var command = new UpdateTeammateCommand(patchDocument, id, idAuthorizedUser);
        return Ok(await mediator.Send(command, cancellationToken));
    }

    /// <summary>
    ///     Get a team member by id.
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken cancellationToken)
    {
        var query = new GetTeammateByIdQuery(id);
        return Ok(await mediator.Send(query, cancellationToken));
    }

    /// <summary>
    ///     Delete a user by id.
    /// </summary>
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Student, Curator")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
    {
        var idAuthorizedUser = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var command = new DeleteTeammateCommand(id, idAuthorizedUser);
        await mediator.Send(command, cancellationToken);
        return NoContent();
    }
}
