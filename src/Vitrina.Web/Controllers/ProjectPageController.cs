using Microsoft.AspNetCore.Mvc;
using Vitrina.UseCases.ProjectPages;

namespace Vitrina.Web.Controllers;

[ApiController]
[Route("api/pages")]
[ApiExplorerSettings(GroupName = "pages")]
public class ProjectPageController : ControllerBase
{
    /// <summary>
    /// Creates a project page.
    /// </summary>
    [HttpPost("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<Guid> Сreate([FromBody] ProjectPageDto page, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Deletes the project page.
    /// </summary>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<Guid> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Refreshes the project page.
    /// </summary>
    [HttpPatch("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task Update([FromRoute] Guid id, [FromBody] ProjectPageDto page, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Gets the project page by id.
    /// </summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ProjectPageDto> Get([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
