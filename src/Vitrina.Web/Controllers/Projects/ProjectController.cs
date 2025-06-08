using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Saritasa.Tools.Common.Pagination;
using Vitrina.UseCases.Project.CreateProject;
using Vitrina.UseCases.Project.DeleteProject;
using Vitrina.UseCases.Project.Dto;
using Vitrina.UseCases.Project.GetOrganizations;
using Vitrina.UseCases.Project.GetProjectById;
using Vitrina.UseCases.Project.GetProjects;
using Vitrina.UseCases.Project.UpdateProject;

namespace Vitrina.Web.Controllers.Projects;

/// <summary>
///     Project controller.
/// </summary>
[ApiController]
[Route("api/projects")]
[ApiExplorerSettings(GroupName = "projects")]
public class ProjectController(IMediator mediator) : ControllerBase
{
    /// <summary>
    ///     Create project.
    /// </summary>
    /// <returns>Project id.</returns>
    [HttpPost("")]
    [Authorize(Roles = "Student, Curator")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateProject([FromBody] CreateProjectDto projectDto,
        CancellationToken cancellationToken)
    {
        var command = new CreateProjectCommand(projectDto, GetIdAuthorizedUser());
        var result = await mediator.Send(command, cancellationToken);
        return Created($"api/projects/{result}", new { id = result });
    }

    /// <summary>
    ///     Get project by id.
    /// </summary>
    /// <returns>Project.</returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProject([FromRoute] int id, CancellationToken cancellationToken)
        => Ok(await mediator.Send(new GetProjectByIdQuery(id), cancellationToken));

    /// <summary>
    ///     Get organizations.
    /// </summary>
    /// <returns>Organizations.</returns>
    [HttpGet("organizations")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ICollection<string>> GetProjectOrganizations(CancellationToken cancellationToken)
        => await mediator.Send(new GetOrganizationsQuery(), cancellationToken);

    /// <summary>
    ///     Delete project.
    /// </summary>
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Student, Curator")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteProject([FromRoute] int id, CancellationToken cancellationToken)
    {
        var command = new DeleteProjectCommand(id, GetIdAuthorizedUser());
        await mediator.Send(command, cancellationToken);
        return NoContent();
    }

    /// <summary>
    ///     Update project.
    /// </summary>
    [HttpPatch("{id:int}")]
    [Authorize(Roles = "Student, Curator")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateProject([FromRoute] int id,
        [FromBody] JsonPatchDocument<UpdateProjectDto> patchDocument,
        CancellationToken cancellationToken)
    {
        var command = new UpdateProjectCommand(id, patchDocument, GetIdAuthorizedUser());
        return Ok(await mediator.Send(command, cancellationToken));
    }


    /// <summary>
    ///     Search for projects with filtering.
    /// </summary>
    /// <returns>Paged list of projects.</returns>
    [HttpGet("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<PagedListMetadataDto<ResponceProjectDto>> SearchProjects([FromQuery] GetProjectsQuery query,
        CancellationToken cancellationToken) => (await mediator.Send(query, cancellationToken)).ToMetadataObject();

    private int GetIdAuthorizedUser() => int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
}
