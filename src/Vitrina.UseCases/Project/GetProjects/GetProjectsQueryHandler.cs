using AutoMapper;
using MediatR;
using Saritasa.Tools.Common.Pagination;
using Saritasa.Tools.EntityFrameworkCore.Pagination;
using Vitrina.Infrastructure.Abstractions.Interfaces;
using Vitrina.UseCases.Project.Dto;

namespace Vitrina.UseCases.Project.GetProjects;

/// <summary>
///     Search projects handler.
/// </summary>
internal class GetProjectsQueryHandler(IMapper mapper, IAppDbContext dbContext)
    : IRequestHandler<GetProjectsQuery, PagedList<ProjectDto>>
{
    public async Task<PagedList<ProjectDto>> Handle(GetProjectsQuery request,
        CancellationToken cancellationToken)
    {
        var projects = dbContext.Projects
            .OrderByDescending(project => project.Priority)
            .AsQueryable();
        var filteredProjects = ApplyFilters(projects, request);

        var projectsPagedList = await EFPagedListFactory.FromSourceAsync(
            filteredProjects,
            request.Page,
            request.PageSize,
            cancellationToken);

        var projectDtos = projectsPagedList
            .Select(MapToProjectDto)
            .ToList();
        return PagedListFactory.Create(projectDtos, request.Page, request.PageSize, projectsPagedList.TotalCount);
    }

    private IQueryable<Domain.Project.Project> ApplyFilters(IQueryable<Domain.Project.Project> query,
        GetProjectsQuery filteringParameters)
    {
        if (!string.IsNullOrEmpty(filteringParameters.Name))
        {
            var nameInLowercase = filteringParameters.Name.ToLower();
            query = query.Where(project => project.Name.ToLower().Contains(nameInLowercase));
        }

        if (!string.IsNullOrEmpty(filteringParameters.Client))
        {
            query =
                query.Where(project => project.Client != null && project.Client.Contains(filteringParameters.Client));
        }

        if (!string.IsNullOrEmpty(filteringParameters.Sphere))
        {
            query = query.Where(project =>
                project.Sphere != null && project.Sphere.Name.Contains(filteringParameters.Sphere));
        }

        if (!string.IsNullOrEmpty(filteringParameters.Thematics))
        {
            query = query.Where(p => p.Thematics != null && p.Thematics.Name.Contains(filteringParameters.Thematics));
        }

        return query;
    }

    private ProjectDto MapToProjectDto(Domain.Project.Project project)
    {
        var dto = mapper.Map<ProjectDto>(project);
        if (project.PreviewImagePath != null)
        {
            dto.PreviewImagePath = project.PreviewImagePath.Split("/").Last();
        }

        return dto;
    }
}
