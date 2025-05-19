using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Vitrina.UseCases.Common.DTO;
using Vitrina.UseCases.ProjectPage.CreateProjectPage;
using Vitrina.UseCases.ProjectPages;
using Vitrina.UseCases.ProjectPages.AddEditorByUserEmail;
using Vitrina.UseCases.ProjectPages.DeleteEditorByPageEditorld;
using Vitrina.UseCases.ProjectPages.DeleteProjectPage;
using Vitrina.UseCases.ProjectPages.GetProjectPage;
using Vitrina.UseCases.ProjectPages.GetProjectPageEditor;
using Vitrina.UseCases.ProjectPages.UpdateProjectPage;

namespace Vitrina.Web.Controllers;

[Authorize]
[ApiController]
[Route("api/pages")]
[ApiExplorerSettings(GroupName = "pages")]
public class ProjectPageController(IMediator mediator) : ControllerBase
{
    /// <summary>
    ///     Creates a project page.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<Guid> Create([FromBody] CreateProjectPageCommand command, CancellationToken cancellationToken) =>
        await mediator.Send(command, cancellationToken);

    /// <summary>
    ///     Deletes the project page.
    /// </summary>
    /// <param name="id">Page identifier.</param>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteProjectPageCommand(id);
        await mediator.Send(command, cancellationToken);
        return Ok();
    }

    /// <summary>
    ///     Refreshes the project page.
    /// </summary>
    /// <param name="id">Page identifier.</param>
    [HttpPatch("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] JsonPatchDocument<ProjectPageDto> page,
        CancellationToken cancellationToken)
    {
        var command = new UpdateProjectPageCommand(id, page);
        await mediator.Send(command, cancellationToken);
        return Ok();
    }

    /// <summary>
    ///     Gets the project page by id.
    /// </summary>
    /// <param name="id">Page identifier.</param>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ProjectPageDto> Get([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var query = new GetProjectPageByIdQuery(id);
        return await mediator.Send(query, cancellationToken);
    }

    /// <summary>
    ///     Allows you to get a list of users who have rights to edit the page.
    /// </summary>
    /// <param name="id">Page identifier.</param>
    [HttpGet("{id:guid}/editors")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ICollection<PageEditorDto>> GetEditors([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var query = new GetProjectPageEditorsQuery(id);
        return await mediator.Send(query, cancellationToken);
    }

    /// <summary>
    ///     Adds the user by email address to the list of page editors.
    /// </summary>
    /// <param name="id">Page identifier.</param>
    /// <param name="userEmail">Email user address.</param>
    [HttpPost("{id:guid}/editors")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<PageEditorDto> AddEditors([FromRoute] Guid id, [FromBody] EmailDto userEmail,
        CancellationToken cancellationToken)
    {
        var command = new AddEditorByUserEmailCommand(id, userEmail);
        return await mediator.Send(command, cancellationToken);
    }

    /// <summary>
    ///     Deleys the editor by ID.
    /// </summary>
    /// <param name="id">Page identifier.</param>
    /// <param name="editorId">Editor identifier.</param>
    [HttpDelete("{id:guid}/editors/{editorId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task DeleteEditors([FromRoute] Guid id, [FromRoute] Guid editorId,
        CancellationToken cancellationToken)
    {
        var command = new DeleteEditorByPageEditorIdCommand(id, editorId);
        await mediator.Send(command, cancellationToken);
    }
}
