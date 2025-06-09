using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Vitrina.UseCases.ProjectThematics;
using Vitrina.UseCases.ProjectThematics.CreateThematics;
using Vitrina.UseCases.ProjectThematics.DeleteThematics;
using Vitrina.UseCases.ProjectThematics.GetThematics;
using Vitrina.UseCases.ProjectThematics.GetThematicsById;
using Vitrina.UseCases.ProjectThematics.UpdateThematics;

namespace Vitrina.Web.Controllers.Projects;

[ApiController]
[Route("api/project-thematics")]
[ApiExplorerSettings(GroupName = "project-thematics")]
public class ProjectThematicsController(IMediator mediator) : ControllerBase
{
    [HttpGet("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var query = new GetAllThematicsQuery();
        return Ok(await mediator.Send(query, cancellationToken));
    }

    [HttpGet("{thematics-id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute(Name = "thematics-id")] Guid id,
        CancellationToken cancellationToken)
    {
        var query = new GetThematicsByIdQuery(id);
        return Ok(await mediator.Send(query, cancellationToken));
    }

    [HttpPost("")]
    [Authorize(Roles = "Administrator")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] RequestThematicsDto projectSphere,
        CancellationToken cancellationToken)
    {
        var command = new CreateThematicsCommand(projectSphere);
        var result = await mediator.Send(command, cancellationToken);
        return Created($"api/project-thematics/{result}", new { Id = result });
    }

    [HttpDelete("{thematics-id:guid}")]
    [Authorize(Roles = "Administrator")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute(Name = "thematics-id")] Guid id,
        CancellationToken cancellationToken)
    {
        var command = new DeleteThematicsCommand(id);
        await mediator.Send(command, cancellationToken);
        return NoContent();
    }

    [HttpPatch("{thematics-id:guid}")]
    [Authorize(Roles = "Administrator")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update([FromRoute(Name = "thematics-id")] Guid id,
        [FromBody] JsonPatchDocument<RequestThematicsDto> patchDocument,
        CancellationToken cancellationToken)
    {
        var command = new UpdateThematicsCommand(id, patchDocument);
        return Ok(await mediator.Send(command, cancellationToken));
    }
}
