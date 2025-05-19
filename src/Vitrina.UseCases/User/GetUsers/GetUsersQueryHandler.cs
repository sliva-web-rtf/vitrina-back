using MediatR;
using Saritasa.Tools.Common.Pagination;
using Vitrina.UseCases.User.DTO;

namespace Vitrina.UseCases.User.GetUsers;

/// <inheritdoc />
public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, PagedList<ResponceShortenedUserDto>>
{
    /// <inheritdoc />
    public Task<PagedList<ResponceShortenedUserDto>>
        Handle(GetUsersQuery request, CancellationToken cancellationToken) => throw new NotImplementedException();
}
