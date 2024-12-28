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
using Vitrina.UseCases.Project.SearchProjects;
using Vitrina.UseCases.Project.UpdateProject;
using Vitrina.UseCases.Project.UpdateProject.DTO;
using Vitrina.UseCases.Project.UploadImages;
using Vitrina.UseCases.Project.UploadImages.Dto;
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
    [HttpPost("create")]
    public async Task<int> CreateProject(AddProjectCommand command, CancellationToken cancellationToken)
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
    public async Task<PagedListMetadataDto<ShortProjectDto>> SearchProjects([FromBody] SearchProjectsQuery query, CancellationToken cancellationToken)
        => (await mediator.Send(query, cancellationToken)).ToMetadataObject();

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

    /// <summary>
    /// Upload images to project.
    /// </summary>
    [HttpPost("{id}/upload-images")]
    public async Task<IActionResult> UploadImagesToProject([FromRoute] int id, IFormFile[] files, CancellationToken cancellationToken)
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
    [HttpPost("{id}/upload-preview-images")]
    public async Task<IActionResult> UploadPreviewImagesToProject([FromRoute] int id, IFormFile file, CancellationToken cancellationToken)
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
    [HttpDelete("{id}")]
    public async Task DeleteProject(int id, CancellationToken cancellationToken)
    {
        await mediator.Send(new DeleteProjectCommand { ProjectId = id }, cancellationToken);
    }

    /// <summary>
    /// Update project.
    /// </summary>
    [HttpPut("{id}")]
    public async Task UpdateProject([FromRoute] int id, [FromBody] UpdateProjectDto projectDto, CancellationToken cancellationToken)
    {
        await mediator.Send(new UpdateProjectCommand { ProjectId = id, Project = projectDto }, cancellationToken);
    }

    /// <summary>
    /// DeleteProjectImages.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
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

    [HttpPost("v2/search")]
    public async Task<PagedListMetadataDto<V2.ShortProjectDto>> SearchProjectsV2(V2.SearchProjectsQuery query, CancellationToken cancellationToken)
        => (await mediator.Send(query, cancellationToken)).ToMetadataObject();
}
