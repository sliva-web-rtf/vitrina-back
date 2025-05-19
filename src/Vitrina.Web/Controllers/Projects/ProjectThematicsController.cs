using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vitrina.UseCases.Project.Dto;

namespace Vitrina.Web.Controllers.Projects;

[ApiController]
[Route("api/project-thematics")]
[ApiExplorerSettings(GroupName = "project-thematics")]
public class ProjectThematicsController
{
    [HttpGet("")]
    public Task<ICollection<ProjectThematicsDto>> GetAll(CancellationToken cancellationToken) =>
        throw new NotImplementedException();

    [HttpGet("{id:guid}")]
    public Task<ProjectThematicsDto> GetById([FromRoute] Guid id, CancellationToken cancellationToken) =>
        throw new NotImplementedException();

    [HttpPost("")]
    [Authorize(Roles = "Administrator")]
    public Task<int> Create([FromBody] ProjectThematicsDto projectSphere, CancellationToken cancellationToken) =>
        throw new NotImplementedException();

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Administrator")]
    public Task<int> Delete([FromRoute] Guid id, CancellationToken cancellationToken) =>
        throw new NotImplementedException();
}
