using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Infrastructure.Abstractions.Interfaces;
using Vitrina.UseCases.Project.Dto;

namespace Vitrina.UseCases.Project.CreateProject;

/// <summary>
///     Add project handler.
/// </summary>
internal class CreateProjectCommandHandler(IMapper mapper, IAppDbContext dbContext)
    : IRequestHandler<CreateProjectCommand, int>
{
    /// <inheritdoc />
    public async Task<int> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        await ValidateModelAsync(request.ProjectDto, cancellationToken);
        var project = mapper.Map<ProjectDto, Domain.Project.Project>(request.ProjectDto);
        await dbContext.Projects.AddAsync(project, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return project.Id;
    }

    private async Task ValidateModelAsync(ProjectDto projectDto, CancellationToken cancellationToken)
    {
        if (await dbContext.Projects.FirstOrDefaultAsync(existingProject => existingProject.PageId == projectDto.PageId,
                cancellationToken) != null)
        {
            throw new DomainException("Project with the insect page already created");
        }

        if (await dbContext.ProjectPages.FindAsync(projectDto.PageId, cancellationToken) == null)
        {
            throw new DomainException($"Page with id = {projectDto.PageId} not found");
        }

        var sphere = projectDto.Sphere;
        if (await dbContext.ProjectSpheres.FirstOrDefaultAsync(existingSphere =>
                existingSphere.Name == sphere.Name && existingSphere.Id == sphere.Id, cancellationToken) == null)
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
