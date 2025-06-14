using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Vitrina.UseCases.Project.YandexBucket.Image.Dto;
using Vitrina.UseCases.Project.YandexBucket.Image.SaveImage;
using Vitrina.UseCases.Project.YandexBucket.Image.GetImageURL;
using Vitrina.UseCases.Project.YandexBucket.Image.DeleteImage;

namespace Vitrina.Web.Controllers;

/// <summary>
/// Image controller.
/// </summary>
[ApiController]
[Route("api/images")]
[ApiExplorerSettings(GroupName = "images")]
public class ImageController : ControllerBase
{
    private readonly IMediator mediator;

    /// <summary>
    /// Constructor.
    /// </summary>
    public ImageController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost("{id:int}")]
    [Authorize(Roles = "Student, Curator")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> SaveImage(
        [FromRoute] int id,
        IFormFile file,
        CancellationToken cancellationToken
    )
    {
        var command = new SaveImageCommand(file, "Images/", id);
        var result = new ImageDto { Url = await mediator.Send(command, cancellationToken) };
        return Ok(result);
    }

    [HttpGet("{file-name}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetImageUrl(
        [FromRoute(Name = "file-name")] string fileName,
        CancellationToken cancellationToken
    )
    {
        var command = new GetImageURLCommand(fileName, "Images/");
        var result = new ImageDto { Url = await mediator.Send(command, cancellationToken) };
        return Ok(result);
    }

    [HttpDelete("{file-name}")]
    [Authorize(Roles = "Student, Curator")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteImage(
        [FromRoute(Name = "file-name")] string fileName,
        CancellationToken cancellationToken
    )
    {
        var command = new DeleteImageCommand(fileName, "Images/");
        await mediator.Send(command, cancellationToken);
        return Ok();
    }
}
