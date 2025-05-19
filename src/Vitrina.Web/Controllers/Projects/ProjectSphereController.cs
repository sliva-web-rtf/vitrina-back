using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vitrina.UseCases.Project.Dto;

namespace Vitrina.Web.Controllers.Projects;

[ApiController]
[Route("api/project-spheres")]
[ApiExplorerSettings(GroupName = "project-spheres")]
public class ProjectSphereController : ControllerBase
{
    [HttpGet("")]
    public Task<ICollection<ProjectSphereDto>> GetAll(CancellationToken cancellationToken) =>
        throw new NotImplementedException();

    [HttpGet("{id:guid}")]
    public Task<ProjectSphereDto> GetById([FromRoute] Guid id, CancellationToken cancellationToken) =>
        throw new NotImplementedException();

    [HttpPost("")]
    [Authorize(Roles = "Administrator")]
    public Task<int> Create([FromBody] ProjectSphereDto projectSphere, CancellationToken cancellationToken) =>
        throw new NotImplementedException();

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Administrator")]
    public Task<int> Delete([FromRoute] Guid id, CancellationToken cancellationToken) =>
        throw new NotImplementedException();
}
