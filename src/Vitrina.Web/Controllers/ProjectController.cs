using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vitrina.UseCases.Common;
using Vitrina.UseCases.Project.AddProject;
using Vitrina.UseCases.Project.GetProjectById;

namespace Vitrina.Web.Controllers;

/// <summary>
/// Project controller.
/// </summary>
[ApiController]
[Route("project")]
[ApiExplorerSettings(GroupName = "project")]
public class ProjectController : ControllerBase
{
    private readonly IMediator mediator;

    /// <summary>
    /// Constructor.
    /// </summary>
    public ProjectController(IMediator mediator) => this.mediator = mediator;

    /// <summary>
    /// Add project.
    /// </summary>
    /// <returns>Project id.</returns>
    [HttpPost("create")]
    public async Task<int> CreateProject([FromBody] AddProjectCommand command, CancellationToken cancellationToken)
        => await mediator.Send(command, cancellationToken);

    /// <summary>
    /// Get project by id.
    /// </summary>
    /// <returns>Project.</returns>
    [HttpGet("{id}")]
    public async Task<ProjectDto> GetProject(int id, CancellationToken cancellationToken)
        => await mediator.Send(new GetProjectByIdQuery(id), cancellationToken);
}
