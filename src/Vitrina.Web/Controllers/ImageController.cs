using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vitrina.UseCases.YandexBucket.Image.DeleteImage;
using Vitrina.UseCases.YandexBucket.Image.GetImage;
using Vitrina.UseCases.YandexBucket.Image.SaveImage;

namespace Vitrina.Web.Controllers;

/// <summary>
///     Image controller.
/// </summary>
[ApiController]
[Route("api/images")]
[ApiExplorerSettings(GroupName = "images")]
public class ImageController : BaseVitrinaController
{
    private readonly IMediator mediator;

    /// <summary>
    ///     Constructor.
    /// </summary>
    public ImageController(IMediator mediator) => this.mediator = mediator;

    [HttpPost("")]
    [Authorize(Roles = "Student, Curator")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> SaveImage(
        IFormFile file,
        CancellationToken cancellationToken
    )
    {
        var command = new SaveImageCommand(file, "Images/", GetIdAuthorizedUser());
        var result = await mediator.Send(command, cancellationToken);
        return Created($"api/images/{result}", new { Id = result });
    }

    [HttpGet("{image-id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetImage(
        [FromRoute(Name = "image-id")] Guid id,
        CancellationToken cancellationToken
    )
    {
        var command = new GetImageCommand(id);
        return Ok(await mediator.Send(command, cancellationToken));
    }

    [HttpDelete("{image-id:guid}")]
    [Authorize(Roles = "Student, Curator")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteImage(
        [FromRoute(Name = "image-id")] Guid id,
        CancellationToken cancellationToken
    )
    {
        var command = new DeleteImageCommand(id, GetIdAuthorizedUser());
        await mediator.Send(command, cancellationToken);
        return NoContent();
    }
}
