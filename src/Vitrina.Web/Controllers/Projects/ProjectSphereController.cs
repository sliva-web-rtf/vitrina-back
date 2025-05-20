using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Vitrina.UseCases.ProjectSphere;

namespace Vitrina.Web.Controllers.Projects;

[ApiController]
[Route("api/project-spheres")]
[ApiExplorerSettings(GroupName = "project-spheres")]
public class ProjectSphereController : ControllerBase
{
    [HttpGet("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public Task<ICollection<ResponceSphereDto>> GetAll(CancellationToken cancellationToken) =>
        throw new NotImplementedException();

    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<ResponceSphereDto> GetById([FromRoute] Guid id, CancellationToken cancellationToken) =>
        throw new NotImplementedException();

    [HttpPost("")]
    [Authorize(Roles = "Administrator")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public Task<Guid> Create([FromBody] RequestSphereDto projectSphere, CancellationToken cancellationToken) =>
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
    public Task<ResponceSphereDto> Update([FromRoute] Guid id,
        [FromBody] JsonPatchDocument<RequestSphereDto> patchDocument,
        CancellationToken cancellationToken) => throw new NotImplementedException();
}
