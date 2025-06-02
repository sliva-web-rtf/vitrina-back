using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Domain.Project.Page;
using Vitrina.Infrastructure.Abstractions.Interfaces;
using Vitrina.UseCases.Project.Dto;

namespace Vitrina.UseCases.Project.CreateProject;

/// <summary>
///     Add project handler.
/// </summary>
internal class CreateProjectCommandHandler(IMapper mapper, IAppDbContext dbContext)
    : IRequestHandler<CreateProjectCommand, Guid>
{
    /// <inheritdoc />
    public async Task<Guid> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        await ValidateModelAsync(request.ProjectDto, request.IdAuthorizedUser, cancellationToken);
        var project = new Domain.Project.Project
        {
            Id = Guid.NewGuid(),
            PageId = request.ProjectDto.PageId,
            Name = request.ProjectDto.Name,
            CreatorId = request.IdAuthorizedUser
        };
        mapper.Map(request.ProjectDto, project);
        project.Page.ReadyStatus = PageReadyStatusEnum.UnderReview;
        await dbContext.Projects.AddAsync(project, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return project.Id;
    }

    private async Task ValidateModelAsync(CreateProjectDto projectDto, int idAuthorizedUser,
        CancellationToken cancellationToken)
    {
        if (await dbContext.Projects.FirstOrDefaultAsync(existingProject => existingProject.PageId == projectDto.PageId,
                cancellationToken) != null)
        {
            throw new DomainException("Project with the insect page already created");
        }

        var page = await dbContext.ProjectPages.FindAsync(projectDto.PageId, cancellationToken);
        if (page == null)
        {
            throw new DomainException($"Page with id = {projectDto.PageId} not found");
        }

        page.ThrowExceptionIfNoAccessRights(idAuthorizedUser);

        var sphere = projectDto.Sphere;
        if (await dbContext.ProjectSpheres.FirstOrDefaultAsync(existingSphere => existingSphere.Name == sphere.Name,
                cancellationToken) == null)
        {
            throw new DomainException("Sphere not found");
        }

        var thematics = projectDto.Thematics;
        if (await dbContext.ProjectThematics.FirstOrDefaultAsync(existingThematics =>
                existingThematics.Name == thematics.Name && existingThematics.Id == thematics.Id, cancellationToken) ==
            null)
        {
            throw new DomainException("Thematics not found");
        }
    }
}
