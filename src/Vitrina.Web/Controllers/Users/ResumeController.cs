using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vitrina.UseCases.YandexBucket.Resume.DeleteResume;
using Vitrina.UseCases.YandexBucket.Resume.GetResume;
using Vitrina.UseCases.YandexBucket.Resume.ReplacementResume;
using Vitrina.UseCases.YandexBucket.Resume.SaveResume;

namespace Vitrina.Web.Controllers.Users;

/// <summary>
///     Resume controller.
/// </summary>
[ApiController]
[Route("api/resumes")]
[ApiExplorerSettings(GroupName = "resumes")]
public class ResumeController(IMediator mediator) : BaseVitrinaController
{
    [HttpPost]
    [Authorize(Roles = "Student, Curator")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> SaveResume(
        IFormFile file,
        CancellationToken cancellationToken
    )
    {
        var command = new SaveResumeCommand(file, "Resume/", GetIdAuthorizedUser());
        var result = await mediator.Send(command, cancellationToken);
        return Created($"api/resumes/{result}", new { Id = result });
    }

    [HttpPut("{resume-id:guid}")]
    [Authorize(Roles = "Student, Curator")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ReplacementResume(
        [FromRoute(Name = "resume-id")] Guid resumeId,
        IFormFile file,
        CancellationToken cancellationToken
    )
    {
        var command = new ReplacementResumeCommand(file, resumeId, GetIdAuthorizedUser());
        await mediator.Send(command, cancellationToken);
        return NoContent();
    }

    [HttpGet("{resume-id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetResumeUrl(
        [FromRoute(Name = "resume-id")] Guid id,
        CancellationToken cancellationToken
    )
    {
        var command = new GetResumeCommand(id);
        return Ok(await mediator.Send(command, cancellationToken));
    }

    [Authorize(Roles = "Student, Curator")]
    [HttpDelete("{resume-id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteResume(
        [FromRoute(Name = "resume-id")] Guid id,
        CancellationToken cancellationToken
    )
    {
        var command = new DeleteResumeCommand(id, GetIdAuthorizedUser());
        await mediator.Send(command, cancellationToken);
        return NoContent();
    }
}
