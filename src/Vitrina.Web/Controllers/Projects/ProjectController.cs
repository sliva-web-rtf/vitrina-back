using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Saritasa.Tools.Common.Pagination;
using Vitrina.UseCases.Common.DTO;
using Vitrina.UseCases.Project.CreateProject;
using Vitrina.UseCases.Project.DeleteProject;
using Vitrina.UseCases.Project.DeleteProjectImages;
using Vitrina.UseCases.Project.GetOrganizations;
using Vitrina.UseCases.Project.GetProjectById;
using Vitrina.UseCases.Project.GetProjects;
using Vitrina.UseCases.Project.UploadImages;
using FileDto = Vitrina.UseCases.Project.UploadImages.Dto.FileDto;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Vitrina.Web.Controllers.Projects;

/// <summary>
///     Project controller.
/// </summary>
[ApiController]
[Route("api/projects")]
[ApiExplorerSettings(GroupName = "projects")]
public class ProjectController(IMediator mediator, IHostingEnvironment hostingEnvironment) : ControllerBase
{
    /// <summary>
    ///     Create project.
    /// </summary>
    /// <returns>Project id.</returns>
    [HttpPost("")]
    [Authorize(Roles = "Student, Curator")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateProject(CreateProjectCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return Created($"api/projects/{result}", new { id = result });
    }

    /// <summary>
    ///     Get project by id.
    /// </summary>
    /// <returns>Project.</returns>
    [HttpGet("{id:int}")]
    public async Task<ProjectDto> GetProject(int id, CancellationToken cancellationToken)
        => await mediator.Send(new GetProjectByIdQuery(id), cancellationToken);

    /// <summary>
    ///     Get organizations.
    /// </summary>
    /// <returns>Organizations.</returns>
    [HttpGet("organizations")]
    public async Task<ICollection<string>> GetProjectOrganizations(CancellationToken cancellationToken)
        => await mediator.Send(new GetOrganizationsQuery(), cancellationToken);

    /// <summary>
    ///     Upload images to project.
    /// </summary>
    [Authorize(Roles = "Student, Curator")]
    [HttpPost("{id}/upload-images")]
    public async Task<IActionResult> UploadImagesToProject([FromRoute] int id, IFormFile[] files,
        CancellationToken cancellationToken)
    {
        var command = new UploadImagesCommand { Id = id, IsAvatar = true };
        foreach (var file in files)
        {
            var fileStream = file.OpenReadStream();
            var fileDto = new FileDto(fileStream, file.FileName, file.ContentType);
            command.Files.Add(fileDto);
        }

        await mediator.Send(command, cancellationToken);
        return Ok();
    }

    /// <summary>
    ///     Upload images to project.
    /// </summary>
    [Authorize(Roles = "Student, Curator")]
    [HttpPost("{id}/upload-preview-images")]
    public async Task<IActionResult> UploadPreviewImagesToProject([FromRoute] int id, IFormFile file,
        CancellationToken cancellationToken)
    {
        var command = new UploadImagesCommand { Id = id, IsAvatar = false };
        var fileStream = file.OpenReadStream();
        var fileDto = new FileDto(fileStream, file.FileName, file.ContentType);
        command.Files.Add(fileDto);
        await mediator.Send(command, cancellationToken);
        return Ok();
    }

    /// <summary>
    ///     Get image.
    /// </summary>
    [HttpGet("image/{name}")]
    public IResult GetImage(string name)
    {
        var webRootDirectory = hostingEnvironment.WebRootPath.TrimEnd('/');
        var path = $"/Avatars/{name}";
        var pathToFile = webRootDirectory + path;

        return Results.File(pathToFile);
    }

    /// <summary>
    ///     Get image.
    /// </summary>
    [HttpGet("preview-image/{name}")]
    public IResult GetPreviewImage(string name)
    {
        var webRootDirectory = hostingEnvironment.WebRootPath.TrimEnd('/');
        var path = $"/Preview/{name}";
        var pathToFile = webRootDirectory + path;

        return Results.File(pathToFile);
    }

    /// <summary>
    ///     Delete project.
    /// </summary>
    [Authorize(Roles = "Student, Curator")]
    [HttpDelete("{id}")]
    public async Task DeleteProject(int id, CancellationToken cancellationToken) =>
        await mediator.Send(new DeleteProjectCommand { ProjectId = id }, cancellationToken);

    /// <summary>
    ///     Update project.
    /// </summary>
    [Authorize(Roles = "Student, Curator")]
    [HttpPatch("{id:int}")]
    public async Task<ProjectDto> UpdateProject([FromRoute] int id, [FromBody] JsonPatchDocument<ProjectDto> patchDto,
        CancellationToken cancellationToken) =>
        throw new NotImplementedException();

    /// <summary>
    ///     Delete project images.
    /// </summary>
    [Authorize]
    [HttpDelete("{id}/images")]
    public async Task DeleteProjectImages(int id, CancellationToken cancellationToken) =>
        await mediator.Send(new DeleteProjectImagesCommand { ProjectId = id }, cancellationToken);

    /// <summary>
    ///     Search for projects with filtering.
    /// </summary>
    /// <returns>Paged list of projects.</returns>
    [HttpGet("")]
    public async Task<PagedListMetadataDto<ProjectDto>> SearchProjects([FromQuery] GetProjectsQuery query,
        CancellationToken cancellationToken) => (await mediator.Send(query, cancellationToken)).ToMetadataObject();
}
