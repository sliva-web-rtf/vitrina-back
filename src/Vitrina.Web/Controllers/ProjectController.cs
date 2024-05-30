using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Saritasa.RedMan.Web.Controllers;

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
    //public ProjectController(IMediator mediator) => this.mediator = mediator;

    [HttpGet]
    public IActionResult GetProjects()
    {
        return Ok("Hello world.");
    }
}
