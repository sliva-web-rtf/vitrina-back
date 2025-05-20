using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Vitrina.UseCases.ProjectThematics;

namespace Vitrina.Web.Controllers.Projects;

[ApiController]
[Route("api/project-thematics")]
[ApiExplorerSettings(GroupName = "project-thematics")]
public class ProjectThematicsController
{
    [HttpGet("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public Task<ICollection<ResponceThematicsDto>> GetAll(CancellationToken cancellationToken) =>
        throw new NotImplementedException();

    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<ResponceThematicsDto> GetById([FromRoute] Guid id, CancellationToken cancellationToken) =>
        throw new NotImplementedException();

    [HttpPost("")]
    [Authorize(Roles = "Administrator")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public Task<Guid> Create([FromBody] RequestThematicsDto projectSphere, CancellationToken cancellationToken) =>
        throw new NotImplementedException();

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Administrator")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task Delete([FromRoute] Guid id, CancellationToken cancellationToken) =>
        throw new NotImplementedException();

    [HttpPatch("{id:guid}")]
    [Authorize(Roles = "Administrator")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public Task<ResponceThematicsDto> Update([FromRoute] Guid id,
        [FromBody] JsonPatchDocument<RequestThematicsDto> patchDocument,
        CancellationToken cancellationToken) => throw new NotImplementedException();
}
