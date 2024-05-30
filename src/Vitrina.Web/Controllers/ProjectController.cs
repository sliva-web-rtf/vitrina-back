using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vitrina.UseCases.Common;
using Vitrina.UseCases.Project.AddProject;
using Vitrina.UseCases.Project.GetOrganizations;
using Vitrina.UseCases.Project.GetPeriods;
using Vitrina.UseCases.Project.GetProjectById;
using Vitrina.UseCases.Project.SearchProjects;

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

    /// <summary>
    /// Search projects by query.
    /// </summary>
    [HttpPost("search")]
    public async Task<ICollection<ShortProjectDto>> SearchProjects([FromBody] SearchProjectsQuery query, CancellationToken cancellationToken)
        => await mediator.Send(query, cancellationToken);

    /// <summary>
    /// Project periods.
    /// </summary>
    /// <returns>Periods collection.</returns>
    [HttpGet("periods")]
    public async Task<ICollection<string>> GetProjectPeriods(CancellationToken cancellationToken)
        => await mediator.Send(new GetPeriodsQuery(), cancellationToken);

    /// <summary>
    /// Get organizations.
    /// </summary>
    /// <returns>Organizations.</returns>
    [HttpGet("organizations")]
    public async Task<ICollection<string>> GetProjectOrganizations(CancellationToken cancellationToken)
        => await mediator.Send(new GetOrganizationsQuery(), cancellationToken);
}
