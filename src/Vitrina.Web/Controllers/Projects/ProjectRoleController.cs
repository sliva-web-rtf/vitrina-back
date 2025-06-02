using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Vitrina.UseCases.ProjectTeam.Role;
using Vitrina.UseCases.ProjectTeam.Role.CreateRole;
using Vitrina.UseCases.ProjectTeam.Role.DeleteRole;
using Vitrina.UseCases.ProjectTeam.Role.GetRoleById;
using Vitrina.UseCases.ProjectTeam.Role.GetRoles;
using Vitrina.UseCases.ProjectTeam.Role.UpdateRole;

namespace Vitrina.Web.Controllers.Projects;

[ApiController]
[Route("api/project-roles")]
[ApiExplorerSettings(GroupName = "project-roles")]
public class ProjectRoleController(IMediator mediator) : ControllerBase
{
    /// <summary>
    ///     Gets all the roles.
    /// </summary>
    [HttpGet("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var query = new GetRolesQuery();
        return Ok(await mediator.Send(query, cancellationToken));
    }

    /// <summary>
    ///     Gets a role in ID.
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken cancellationToken)
    {
        var query = new GetRoleByIdQuery(id);
        return Ok(await mediator.Send(query, cancellationToken));
    }

    /// <summary>
    ///     Creates a role
    /// </summary>
    [HttpPost("")]
    [Authorize(Roles = "Administrator")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] RequestRoleDto roleDto, CancellationToken cancellationToken)
    {
        var command = new CreateRoleCommand(roleDto);
        var result = await mediator.Send(command, cancellationToken);
        return Created($"api/project-roles/{result}", new { id = result });
    }

    /// <summary>
    ///     Removes the role.
    /// </summary>
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Administrator")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
    {
        var command = new DeleteRoleCommand(id);
        await mediator.Send(command, cancellationToken);
        return NoContent();
    }

    /// <summary>
    ///     Updates the role.
    /// </summary>
    [HttpPatch("{id:int}")]
    [Authorize(Roles = "Administrator")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update([FromRoute] int id,
        [FromBody] JsonPatchDocument<RequestRoleDto> patchDocument, CancellationToken cancellationToken)
    {
        var command = new UpdateRoleCommand(patchDocument, id);
        return Ok(await mediator.Send(command, cancellationToken));
    }
}
