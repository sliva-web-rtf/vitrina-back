using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Vitrina.UseCases.Common.DTO;
using Vitrina.UseCases.ProjectPage.AddEditorByUserEmail;
using Vitrina.UseCases.ProjectPage.CreateProjectPage;
using Vitrina.UseCases.ProjectPage.DeleteEditorByPageEditorId;
using Vitrina.UseCases.ProjectPage.DeleteProjectPage;
using Vitrina.UseCases.ProjectPage.Dto;
using Vitrina.UseCases.ProjectPage.GetProjectPage;
using Vitrina.UseCases.ProjectPage.GetProjectPageEditors;
using Vitrina.UseCases.ProjectPage.UpdateProjectPage;

namespace Vitrina.Web.Controllers.Projects;

[Authorize]
[ApiController]
[Route("api/project-pages")]
[ApiExplorerSettings(GroupName = "project-pages")]
public class ProjectPageController(IMediator mediator) : BaseVitrinaController
{
    /// <summary>
    ///     Creates a project page.
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "Student, Curator")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateProjectPageDto pageDto,
        CancellationToken cancellationToken)
    {
        var command = new CreateProjectPageCommand(pageDto, GetIdAuthorizedUser());
        var result = await mediator.Send(command, cancellationToken);
        return Created($"api/pages/{result}", new { Id = result });
    }

    /// <summary>
    ///     Deletes the project page.
    /// </summary>
    /// <param name="id">Page identifier.</param>
    [HttpDelete("{page-id:guid}")]
    [Authorize(Roles = "Student, Curator")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute(Name = "page-id")] Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteProjectPageCommand(id, GetIdAuthorizedUser());
        await mediator.Send(command, cancellationToken);
        return NoContent();
    }

    /// <summary>
    ///     Refreshes the project page.
    /// </summary>
    /// <param name="id">Page identifier.</param>
    [HttpPatch("{page-id:guid}")]
    [Authorize(Roles = "Student, Curator")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update([FromRoute(Name = "page-id")] Guid id,
        [FromBody] JsonPatchDocument<UpdateProjectPageDto> patchDocument,
        CancellationToken cancellationToken)
    {
        var command = new UpdateProjectPageCommand(id, patchDocument, GetIdAuthorizedUser());
        await mediator.Send(command, cancellationToken);
        return NoContent();
    }

    /// <summary>
    ///     Gets the project page by id.
    /// </summary>
    /// <param name="id">Page identifier.</param>
    [HttpGet("{page-id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get([FromRoute(Name = "page-id")] Guid id, CancellationToken cancellationToken)
    {
        var query = new GetProjectPageByIdQuery(id, GetIdAuthorizedUser());
        return Ok(await mediator.Send(query, cancellationToken));
    }

    /// <summary>
    ///     Allows you to get a list of users who have rights to edit the page.
    /// </summary>
    /// <param name="id">Page identifier.</param>
    [HttpGet("{page-id:guid}/editors")]
    [Authorize(Roles = "Student, Curator")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetEditors([FromRoute(Name = "page-id")] Guid id,
        CancellationToken cancellationToken)
    {
        var query = new GetProjectPageEditorsQuery(id, GetIdAuthorizedUser());
        return Ok(await mediator.Send(query, cancellationToken));
    }

    /// <summary>
    ///     Adds the user by email address to the list of page editors.
    /// </summary>
    /// <param name="id">Page identifier.</param>
    /// <param name="userEmail">Email user address.</param>
    [HttpPost("{page-id:guid}/editors")]
    [Authorize(Roles = "Student, Curator")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AddEditors([FromRoute(Name = "page-id")] Guid id, [FromBody] EmailDto userEmail,
        CancellationToken cancellationToken)
    {
        var command = new AddEditorByUserEmailCommand(id, userEmail, GetIdAuthorizedUser());
        return Ok(await mediator.Send(command, cancellationToken));
    }

    /// <summary>
    ///     Deleys the editor by ID.
    /// </summary>
    /// <param name="id">Page identifier.</param>
    /// <param name="editorId">Editor identifier.</param>
    [Authorize(Roles = "Student, Curator")]
    [HttpDelete("{page-id:guid}/editors/{editor-id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteEditors([FromRoute(Name = "page-id")] Guid id,
        [FromRoute(Name = "editor-id")] Guid editorId,
        CancellationToken cancellationToken)
    {
        var command = new DeleteEditorByPageEditorIdCommand(id, editorId, GetIdAuthorizedUser());
        await mediator.Send(command, cancellationToken);
        return NoContent();
    }
}
