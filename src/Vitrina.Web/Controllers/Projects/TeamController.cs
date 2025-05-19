using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Vitrina.UseCases.ProjectTeam;
using Vitrina.UseCases.ProjectTeam.AddTeammate;
using Vitrina.UseCases.ProjectTeam.CreateTeam;
using Vitrina.UseCases.ProjectTeam.DeleteTeam;
using Vitrina.UseCases.ProjectTeam.GetTeamById;
using Vitrina.UseCases.ProjectTeam.Teammate;
using Vitrina.UseCases.ProjectTeam.UpdateTeam;

namespace Vitrina.Web.Controllers.Projects;

[ApiController]
[Route("api/teams")]
[ApiExplorerSettings(GroupName = "teams")]
public class TeamController(IMediator mediator) : ControllerBase
{
    /// <summary>
    ///     Adds a team to the project.
    /// </summary>
    [HttpPost("")]
    [Authorize(Roles = "Student, Curator")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AddTeam([FromBody] CreateTeamDto teamDto, CancellationToken cancellationToken)
    {
        var command = new CreateTeamCommand(teamDto, GetIdAuthorizedUser());
        var result = await mediator.Send(command, cancellationToken);
        return Created($"api/teams/{result}", new { id = result });
    }

    /// <summary>
    ///     Creates a new team member.
    /// </summary>
    [HttpPost("{id:guid}/teammates")]
    [Authorize(Roles = "Student, Curator")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AddTeammate([FromRoute] Guid id,
        [FromBody] RequestTeammateDto teammateDto, CancellationToken cancellationToken)
    {
        var command = new AddTeammateCommand(teammateDto, GetIdAuthorizedUser(), id);
        var result = await mediator.Send(command, cancellationToken);
        return Created($"api/teams/{id}teammates/{result}", new { id = result });
    }

    /// <summary>
    ///     Deletes the team.
    /// </summary>
    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Student, Curator")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task Delete([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteTeamCommand(id, GetIdAuthorizedUser());
        await mediator.Send(command, cancellationToken);
    }

    /// <summary>
    ///     Gets the command by id.
    /// </summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var query = new GetTeamByIdQuery(id);
        return Ok(await mediator.Send(query, cancellationToken));
    }

    /// <summary>
    ///     Updates the team by ID.
    /// </summary>
    [HttpPatch("{id:guid}")]
    [Authorize(Roles = "Student, Curator")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromRoute] Guid id,
        [FromBody] JsonPatchDocument<UpdateTeamDto> patchDocument,
        CancellationToken cancellationToken)
    {
        var command = new UpdateTeamCommand(id, patchDocument, GetIdAuthorizedUser());
        return Ok(await mediator.Send(command, cancellationToken));
    }

    private int GetIdAuthorizedUser() => int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
}
