using AutoMapper;
using MediatR;
using Saritasa.Tools.Common.Pagination;
using Saritasa.Tools.EntityFrameworkCore.Pagination;
using Vitrina.Infrastructure.Abstractions.Interfaces;
using Vitrina.UseCases.User.DTO;

namespace Vitrina.UseCases.User.GetUsers;

/// <inheritdoc />
public class GetUsersQueryHandler(IAppDbContext dbContext, IMapper mapper)
    : IRequestHandler<GetUsersQuery, PagedList<ResponceShortenedUserDto>>
{
    /// <inheritdoc />
    public async Task<PagedList<ResponceShortenedUserDto>> Handle(GetUsersQuery request,
        CancellationToken cancellationToken)
    {
        var users = dbContext.Users.AsQueryable();
        var filteredProjects = ApplyFilters(users, request);

        var usersPagedList = await EFPagedListFactory.FromSourceAsync(
            filteredProjects,
            request.Page,
            request.PageSize,
            cancellationToken);

        var projectDtos = mapper.Map<List<ResponceShortenedUserDto>>(usersPagedList);
        return PagedListFactory.Create(projectDtos, request.Page, request.PageSize, usersPagedList.TotalCount);
    }

    private IQueryable<Domain.User.User> ApplyFilters(IQueryable<Domain.User.User> query,
        GetUsersQuery filteringParameters)
    {
        if (!string.IsNullOrEmpty(filteringParameters.Email))
        {
            var normalizedEmail = filteringParameters.Email.ToUpper();
            query = query.Where(user => user.NormalizedEmail.Contains(normalizedEmail));
        }

        return query;
    }
}
