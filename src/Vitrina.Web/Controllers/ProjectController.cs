using MediatR;
using Microsoft.AspNetCore.Mvc;
using Saritasa.Tools.Common.Pagination;
using Vitrina.UseCases.Common;
using Vitrina.UseCases.Project.AddProject;
using Vitrina.UseCases.Project.DeleteProject;
using Vitrina.UseCases.Project.DeleteProjectImages;
using Vitrina.UseCases.Project.GetOrganizations;
using Vitrina.UseCases.Project.GetPeriods;
using Vitrina.UseCases.Project.GetProjectById;
using Vitrina.UseCases.Project.GetProjectsIds;
using Vitrina.UseCases.Project.GetSpheres;
using Vitrina.UseCases.Project.GetTypes;
using Vitrina.UseCases.Project.SearchProjects;
using Vitrina.UseCases.Project.UpdateProject;
using Vitrina.UseCases.Project.UpdateProject.DTO;
using Vitrina.UseCases.Project.UploadImages;
using Vitrina.UseCases.Project.UploadImages.Dto;
using Vitrina.UseCases.Project.YandexBucket.Image.DeleteImage;
using Vitrina.UseCases.Project.YandexBucket.Image.GetImageURL;
using Vitrina.UseCases.Project.YandexBucket.Image.SaveImage;
using Vitrina.UseCases.Project.YandexBucket.Resume.DeleteResume;
using Vitrina.UseCases.Project.YandexBucket.Resume.GetFileURL;
using Vitrina.UseCases.Project.YandexBucket.Resume.ReplacementResume;
using Vitrina.UseCases.Project.YandexBucket.Resume.SaveResume;
using SearchProjectsQuery = Vitrina.UseCases.Project.SearchProjects.SearchProjectsQuery;
using V2 = Vitrina.UseCases.Project.SearchProjects.V2;

namespace Vitrina.Web.Controllers;

/// <summary>
/// Project controller.
/// </summary>
[ApiController]
// For dev [Route("api-dev/project")]
[Route("api/project")]
[ApiExplorerSettings(GroupName = "project")]
public class ProjectController : ControllerBase
{
    private readonly IMediator mediator;
    private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment;

    /// <summary>
    /// Constructor.
    /// </summary>
    public ProjectController(IMediator mediator, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
    {
        this.mediator = mediator;
        this.hostingEnvironment = hostingEnvironment;
    }

    /// <summary>
    /// Add project.
    /// </summary>
    /// <returns>Project id.</returns>
    /*[Authorize]*/
    [HttpPost("create")]
    public async Task<int> CreateProject(AddProjectCommand command, CancellationToken cancellationToken) =>
        await mediator.Send(command, cancellationToken);

    /// <summary>
    /// Get project by id.
    /// </summary>
    /// <returns>Project.</returns>
    [HttpGet("{id}")]
    public async Task<ProjectDto> GetProject(int id, CancellationToken cancellationToken) =>
        await mediator.Send(new GetProjectByIdQuery(id), cancellationToken);

    /// <summary>
    /// Search projects by query.
    /// </summary>
    [HttpPost("search")]
    public async Task<PagedListMetadataDto<ShortProjectDto>> SearchProjects(
        [FromBody] SearchProjectsQuery query,
        CancellationToken cancellationToken
    ) =>
        (await mediator.Send(query, cancellationToken)).ToMetadataObject();

    /// <summary>
    /// Project periods.
    /// </summary>
    /// <returns>Periods collection.</returns>
    [HttpGet("periods")]
    public async Task<ICollection<string>> GetProjectPeriods(CancellationToken cancellationToken) =>
        await mediator.Send(new GetPeriodsQuery(), cancellationToken);

    /// <summary>
    /// Get organizations.
    /// </summary>
    /// <returns>Organizations.</returns>
    [HttpGet("organizations")]
    public async Task<ICollection<string>> GetProjectOrganizations(CancellationToken cancellationToken) =>
        await mediator.Send(new GetOrganizationsQuery(), cancellationToken);

    [HttpPost("{id:int}/save-resume")]
    public async Task<IActionResult> SaveResume(
        [FromRoute] int id,
        IFormFile file,
        CancellationToken cancellationToken
    )
    {
        var command = new SaveResumeCommand(file, "Resume/", id);
        await mediator.Send(command, cancellationToken);
        return Ok();
    }

    [HttpPost("{id:int}/replacement-resume")]
    public async Task<IActionResult> ReplacementResume(
        [FromRoute] int id,
        IFormFile file,
        CancellationToken cancellationToken
    )
    {
        var command = new ReplacementResumeCommand(file, "Resume/", id);
        await mediator.Send(command, cancellationToken);
        return Ok();
    }

    [HttpGet("get-resume-url/{userId:int}")]
    public async Task<IActionResult> GetResumeUrl(int userId, CancellationToken cancellationToken)
    {
        var command = new GetResumeURLCommand(userId);
        var result = await mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    [HttpDelete("delete-resume/{userId:int}")]
    public async Task<IActionResult> DeleteResume(int userId, CancellationToken cancellationToken)
    {
        var command = new DeleteResumeCommand(userId);
        await mediator.Send(command, cancellationToken);
        return Ok();
    }

    [HttpPost("{id}/save-image")]
    public async Task<IActionResult> SaveImage(
        [FromRoute] int id,
        IFormFile file,
        CancellationToken cancellationToken
    )
    {
        var command = new SaveImageCommand(file, "Images/", id);
        var result = await mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    [HttpGet("get-image-url/{fileName}")]
    public async Task<IActionResult> GetImageUrl(string fileName, CancellationToken cancellationToken)
    {
        var command = new GetImageURLCommand("Images/" + fileName);
        var result = await mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    [HttpDelete("delete-image/{fileName}")]
    public async Task<IActionResult> DeleteImage(string fileName, CancellationToken cancellationToken)
    {
        var command = new DeleteImageCommand("Images/" + fileName);
        await mediator.Send(command, cancellationToken);
        return Ok();
    }

    /// <summary>
    /// Upload images to project.
    /// </summary>
    /*[Authorize]*/
    [HttpPost("{id}/upload-images")]
    public async Task<IActionResult> UploadImagesToProject(
        [FromRoute] int id,
        IFormFile[] files,
        CancellationToken cancellationToken
    )
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
    /// Upload images to project.
    /// </summary>
    /*[Authorize]*/
    [HttpPost("{id}/upload-preview-images")]
    public async Task<IActionResult> UploadPreviewImagesToProject(
        [FromRoute] int id,
        IFormFile file,
        CancellationToken cancellationToken
    )
    {
        var command = new UploadImagesCommand { Id = id, IsAvatar = false };
        var fileStream = file.OpenReadStream();
        var fileDto = new FileDto(fileStream, file.FileName, file.ContentType);
        command.Files.Add(fileDto);
        await mediator.Send(command, cancellationToken);
        return Ok();
    }

    /// <summary>
    /// Get image.
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
    /// Get image.
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
    /// Delete project.
    /// </summary>
    /*[Authorize]*/
    [HttpDelete("{id}")]
    public async Task DeleteProject(int id, CancellationToken cancellationToken)
    {
        await mediator.Send(new DeleteProjectCommand { ProjectId = id }, cancellationToken);
    }

    /// <summary>
    /// Update project.
    /// </summary>
    /*[Authorize]*/
    [HttpPut("{id}")]
    public async Task UpdateProject(
        [FromRoute] int id,
        [FromBody] UpdateProjectDto projectDto,
        CancellationToken cancellationToken
    )
    {
        await mediator.Send(new UpdateProjectCommand { ProjectId = id, Project = projectDto }, cancellationToken);
    }

    /// <summary>
    /// DeleteProjectImages.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /*[Authorize]*/
    [HttpDelete("{id}/images")]
    public async Task DeleteProjectImages(int id, CancellationToken cancellationToken)
    {
        await mediator.Send(new DeleteProjectImagesCommand { ProjectId = id }, cancellationToken);
    }

    /// <summary>
    /// Get all project ids.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("ids")]
    public async Task<ICollection<int>> GetProjectsIds(CancellationToken cancellationToken)
    {
        return await mediator.Send(new GetProjectIdsQuery(), cancellationToken);
    }

    /// <summary>
    /// Search projects with new filters.
    /// </summary>
    /// <param name="v2Query">Query to search.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Paged list of projects.</returns>
    [HttpPost("v2/search")]
    public async Task<PagedListMetadataDto<V2.ShortProjectV2Dto>> SearchProjectsV2(
        V2.SearchProjectsV2Query v2Query,
        CancellationToken cancellationToken
    ) =>
        (await mediator.Send(v2Query, cancellationToken)).ToMetadataObject();

    /// <summary>
    /// Get spheres.
    /// </summary>
    /// <returns>Spheres.</returns>
    [HttpGet("spheres")]
    public async Task<ICollection<string>> GetProjectSpheres(CancellationToken cancellationToken) =>
        await mediator.Send(new GetSpheresQuery(), cancellationToken);

    /// <summary>
    /// Get types.
    /// </summary>
    /// <returns>Types.</returns>
    [HttpGet("types")]
    public async Task<ICollection<string>> GetProjectTypes(CancellationToken cancellationToken) =>
        await mediator.Send(new GetTypesQuery(), cancellationToken);
}
