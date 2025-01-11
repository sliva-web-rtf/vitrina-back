using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Saritasa.Tools.Common.Pagination;
using Saritasa.Tools.EntityFrameworkCore.Pagination;
using Vitrina.Domain.Project;
using Vitrina.Infrastructure.Abstractions.Interfaces;

namespace Vitrina.UseCases.Project.SearchProjects.V2;

/// <summary>
/// Search projects handler.
/// </summary>
internal class SearchProjectsV2QueryHandler : IRequestHandler<SearchProjectsV2Query, PagedList<ShortProjectV2Dto>>
{
    private readonly IAppDbContext dbContext;
    private readonly IMapper mapper;

    public SearchProjectsV2QueryHandler(IMapper mapper, IAppDbContext dbContext)
    {
        this.mapper = mapper;
        this.dbContext = dbContext;
    }

    public async Task<PagedList<ShortProjectV2Dto>> Handle(SearchProjectsV2Query request, CancellationToken cancellationToken)
    {
        var query = dbContext.Projects
            .OrderByDescending(p => p.Priority)
            .Include(p => p.Contents)
            .AsQueryable();

        if (!string.IsNullOrEmpty(request.Name))
        {
            query = query.Where(p => p.Name.ToLower().Contains(request.Name.ToLower()));
        }

        if (!string.IsNullOrEmpty(request.Customer))
        {
            query = query.Where(p => p.Client != null && p.Client.Contains(request.Customer));
        }

        if (!string.IsNullOrEmpty(request.Sphere))
        {
            query = query.Where(p => p.Sphere != null && p.Sphere.Contains(request.Sphere));
        }

        if (!string.IsNullOrEmpty(request.ProjectType))
        {
            query = query.Where(p => p.Type != null && p.Type.Contains(request.ProjectType));
        }

        var pagedList = await EFPagedListFactory.FromSourceAsync(query, request.Page, request.PageSize, cancellationToken);
        var result = new List<ShortProjectV2Dto>();
        foreach (var item in pagedList)
        {
            var dto = mapper.Map<ShortProjectV2Dto>(item);
            if (item.PreviewImagePath != null)
            {
                dto.PreviewImagePath = item.PreviewImagePath.Split("/").Last();
            }

            var content = item.Contents.FirstOrDefault();
            if (content != null)
            {
                dto.ImageUrl = content.ImageUrl.Split("/").Last();
            }

            result.Add(dto);
        }
        var newPagedList = PagedListFactory.Create(result, request.Page, request.PageSize, pagedList.TotalCount);
        return newPagedList;
    }
}
