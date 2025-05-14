using AutoMapper;
using MediatR;
using Saritasa.Tools.Common.Pagination;
using Saritasa.Tools.EntityFrameworkCore.Pagination;
using Vitrina.Infrastructure.Abstractions.Interfaces;
using Vitrina.UseCases.Common.DTO;

namespace Vitrina.UseCases.Project.SearchProjects.V2;

/// <summary>
///     Search projects handler.
/// </summary>
internal class SearchProjectsV2QueryHandler(IMapper mapper, IAppDbContext dbContext)
    : IRequestHandler<SearchProjectsV2Query, PagedList<ProjectDto>>
{
    public async Task<PagedList<ProjectDto>> Handle(SearchProjectsV2Query request,
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
        SearchProjectsV2Query filteringParameters)
    {
        if (!string.IsNullOrEmpty(filteringParameters.Name))
        {
            var nameInLowercase = filteringParameters.Name.ToLower();
            query = query.Where(project => project.Name.ToLower().Contains(nameInLowercase));
        }

        if (!string.IsNullOrEmpty(filteringParameters.Customer))
        {
            query = query.Where(p => p.Client != null && p.Client.Contains(filteringParameters.Customer));
        }

        if (!string.IsNullOrEmpty(filteringParameters.Sphere))
        {
            query = query.Where(p => p.Sphere != null && p.Sphere.Contains(filteringParameters.Sphere));
        }

        if (!string.IsNullOrEmpty(filteringParameters.ProjectType))
        {
            query = query.Where(p => p.Type != null && p.Type.Contains(filteringParameters.ProjectType));
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
