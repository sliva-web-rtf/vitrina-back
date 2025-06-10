using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vitrina.UseCases.Project.YandexBucket.Resume.Dto;
using Vitrina.UseCases.Project.YandexBucket.Resume.GetFileURL;
using Vitrina.UseCases.Project.YandexBucket.Resume.SaveResume;
using Vitrina.UseCases.Project.YandexBucket.Resume.DeleteResume;
using Vitrina.UseCases.Project.YandexBucket.Resume.ReplacementResume;

namespace Vitrina.Web.Controllers;

/// <summary>
/// Resume controller.
/// </summary>
[ApiController]
[Route("api/resumes")]
[ApiExplorerSettings(GroupName = "resumes")]
public class ResumeController : ControllerBase
{
    private readonly IMediator mediator;

    public ResumeController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> SaveResume(
        IFormFile file,
        CancellationToken cancellationToken
    )
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        var id = int.Parse(userIdClaim!.Value);
        var command = new SaveResumeCommand(file, "Resume/", id);
        await mediator.Send(command, cancellationToken);
        return Ok();
    }

    [HttpPut("{resume-id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ReplacementResume(
        [FromRoute(Name = "resume-id")] Guid id,
        IFormFile file,
        CancellationToken cancellationToken
    )
    {
        var command = new ReplacementResumeCommand(file, "Resume/", id);
        await mediator.Send(command, cancellationToken);
        return Ok();
    }

    [HttpGet("{resume-id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetResumeUrl(
        [FromRoute(Name = "resume-id")] Guid id,
        CancellationToken cancellationToken
    )
    {
        var command = new GetResumeURLCommand(id, "Resume/");
        var result = new ResumeDto { Url = await mediator.Send(command, cancellationToken) };
        return Ok(result);
    }

    [HttpDelete("{resume-id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteResume(
        [FromRoute(Name = "resume-id")] Guid id,
        CancellationToken cancellationToken
    )
    {
        var command = new DeleteResumeCommand(id, "Resume/");
        await mediator.Send(command, cancellationToken);
        return Ok();
    }
}
