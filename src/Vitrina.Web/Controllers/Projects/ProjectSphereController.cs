using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Vitrina.UseCases.ProjectSphere;
using Vitrina.UseCases.ProjectSphere.CreateSphere;
using Vitrina.UseCases.ProjectSphere.DeleteSphere;
using Vitrina.UseCases.ProjectSphere.GetSphereById;
using Vitrina.UseCases.ProjectSphere.GetSpheres;
using Vitrina.UseCases.ProjectSphere.UpdateSphere;

namespace Vitrina.Web.Controllers.Projects;

[ApiController]
[Route("api/project-spheres")]
[ApiExplorerSettings(GroupName = "project-spheres")]
public class ProjectSphereController(IMediator mediator) : ControllerBase
{
    [HttpGet("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var query = new GetSpheresQuery();
        return Ok(await mediator.Send(query, cancellationToken));
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var query = new GetSphereByIdQuery(id);
        return Ok(await mediator.Send(query, cancellationToken));
    }

    [HttpPost("")]
    [Authorize(Roles = "Administrator")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] RequestSphereDto sphere, CancellationToken cancellationToken)
    {
        var command = new CreateSphereCommand(sphere);
        var result = await mediator.Send(command, cancellationToken);
        return Created($"api/project-spheres/{result}", new { Id = result });
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Administrator")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteSphereCommand(id);
        await mediator.Send(command, cancellationToken);
        return NoContent();
    }

    [HttpPatch("{id:guid}")]
    [Authorize(Roles = "Administrator")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update([FromRoute] Guid id,
        [FromBody] JsonPatchDocument<RequestSphereDto> patchDocument,
        CancellationToken cancellationToken)
    {
        var command = new UpdateSphereCommand(id, patchDocument);
        return Ok(await mediator.Send(command, cancellationToken));
    }
}
