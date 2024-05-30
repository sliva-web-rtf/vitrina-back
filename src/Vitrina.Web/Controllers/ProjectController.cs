using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vitrina.UseCases.Project.AddProject;

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
    public async Task<int> GetProjects([FromBody] AddProjectCommand command, CancellationToken cancellationToken)
        => await mediator.Send(command, cancellationToken);
}
