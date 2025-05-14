﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Saritasa.Tools.Common.Pagination;
using Vitrina.UseCases.Common.DTO;
using Vitrina.UseCases.Project.AddProject;
using Vitrina.UseCases.Project.DeleteProject;
using Vitrina.UseCases.Project.DeleteProjectImages;
using Vitrina.UseCases.Project.GetOrganizations;
using Vitrina.UseCases.Project.GetProjectById;
using Vitrina.UseCases.Project.GetProjectsIds;
using Vitrina.UseCases.Project.GetSpheres;
using Vitrina.UseCases.Project.GetTypes;
using Vitrina.UseCases.Project.UpdateProject;
using Vitrina.UseCases.Project.UpdateProject.DTO;
using Vitrina.UseCases.Project.UploadImages;
using FileDto = Vitrina.UseCases.Project.UploadImages.Dto.FileDto;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using V2 = Vitrina.UseCases.Project.SearchProjects.V2;

namespace Vitrina.Web.Controllers;

/// <summary>
///     Project controller.
/// </summary>
[ApiController]
// For dev [Route("api-dev/project")]
[Route("api/project")]
[ApiExplorerSettings(GroupName = "project")]
public class ProjectController(IMediator mediator, IHostingEnvironment hostingEnvironment) : ControllerBase
{
    /// <summary>
    ///     Add project.
    /// </summary>
    /// <returns>Project id.</returns>
    [Authorize]
    [HttpPost("create")]
    public async Task<int> CreateProject(CreateProjectCommand command, CancellationToken cancellationToken)
        => await mediator.Send(command, cancellationToken);

    /// <summary>
    ///     Get project by id.
    /// </summary>
    /// <returns>Project.</returns>
    [HttpGet("{id}")]
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
    [Authorize]
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
    [Authorize]
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
    [Authorize]
    [HttpDelete("{id}")]
    public async Task DeleteProject(int id, CancellationToken cancellationToken) =>
        await mediator.Send(new DeleteProjectCommand { ProjectId = id }, cancellationToken);

    /// <summary>
    ///     Update project.
    /// </summary>
    [Authorize]
    [HttpPut("{id}")]
    public async Task UpdateProject([FromRoute] int id, [FromBody] UpdateProjectDto projectDto,
        CancellationToken cancellationToken) =>
        await mediator.Send(new UpdateProjectCommand { ProjectId = id, Project = projectDto }, cancellationToken);

    /// <summary>
    ///     Delete project images.
    /// </summary>
    [Authorize]
    [HttpDelete("{id}/images")]
    public async Task DeleteProjectImages(int id, CancellationToken cancellationToken) =>
        await mediator.Send(new DeleteProjectImagesCommand { ProjectId = id }, cancellationToken);

    /// <summary>
    ///     Get all project ids.
    /// </summary>
    [HttpGet("ids")]
    public async Task<ICollection<int>> GetProjectsIds(CancellationToken cancellationToken) =>
        await mediator.Send(new GetProjectIdsQuery(), cancellationToken);

    /// <summary>
    ///     Search projects with new filters.
    /// </summary>
    /// <param name="v2Query">Query to search.</param>
    /// <returns>Paged list of projects.</returns>
    [HttpPost("v2/search")]
    public async Task<PagedListMetadataDto<ProjectDto>> SearchProjectsV2(
        V2.SearchProjectsV2Query v2Query,
        CancellationToken cancellationToken) => (await mediator.Send(v2Query, cancellationToken)).ToMetadataObject();

    /// <summary>
    ///     Get spheres.
    /// </summary>
    /// <returns>Spheres.</returns>
    [HttpGet("spheres")]
    public async Task<ICollection<string>> GetProjectSpheres(CancellationToken cancellationToken)
        => await mediator.Send(new GetSpheresQuery(), cancellationToken);

    /// <summary>
    ///     Get types.
    /// </summary>
    /// <returns>Types.</returns>
    [HttpGet("types")]
    public async Task<ICollection<string>> GetProjectTypes(CancellationToken cancellationToken)
        => await mediator.Send(new GetTypesQuery(), cancellationToken);
}
