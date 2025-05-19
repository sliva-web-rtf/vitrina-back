using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vitrina.UseCases.Common.DTO;

namespace Vitrina.Web.Controllers.Projects;

[ApiController]
[Route("api/project-roles")]
[ApiExplorerSettings(GroupName = "project-roles")]
public class ProjectRoleController : ControllerBase
{
    [HttpGet("")]
    public Task<ICollection<RoleDto>> GetAll(CancellationToken cancellationToken) =>
        throw new NotImplementedException();

    [HttpGet("{id:int}")]
    public Task<RoleDto> GetById([FromRoute] int id, CancellationToken cancellationToken) =>
        throw new NotImplementedException();

    [HttpPost("")]
    [Authorize(Roles = "Administrator")]
    public Task<int> Create([FromBody] RoleDto roleDto, CancellationToken cancellationToken) =>
        throw new NotImplementedException();

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Administrator")]
    public Task<int> Delete([FromRoute] int id, CancellationToken cancellationToken) =>
        throw new NotImplementedException();
}
